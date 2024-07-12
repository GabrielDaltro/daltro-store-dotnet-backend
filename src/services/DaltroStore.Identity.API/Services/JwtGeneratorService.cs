using DaltroStore.Identity.API.Extensions;
using DaltroStore.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace DaltroStore.Identity.API.Services
{
    public class JwtGeneratorService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly AppSettings appSettings;

        public JwtGeneratorService(UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
        }

        public async Task<UserLoginResponseDto> GenerateJwt(IdentityUser user)
        {
            IEnumerable<Claim> claims = await GetClaimsIdentity(user);

            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException($"Email of user id {user.Id} is NULL or EMPTY");

            if (string.IsNullOrEmpty(user.UserName))
                throw new ArgumentException($"UserName of user id {user.Id} is NULL or EMPTY");

            var response = new UserLoginResponseDto(
            id: user.Id,
            email: user.Email, 
            name: user.UserName,
            claims: claims.Select(claim => new UserClaimDto(claim.Value, claim.Type)),
            tokenInfo: new UserTokenInfoDto(GenerateToken(claims), TimeSpan.FromHours(appSettings.ExpirationHours).TotalSeconds)
            );
            return response;
        }

        private async Task<IEnumerable<Claim>> GetClaimsIdentity(IdentityUser user)
        {
            IEnumerable<string> roles = await userManager.GetRolesAsync(user);
            IList<Claim> claims = await userManager.GetClaimsAsync(user);
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToTimestamp(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToTimestamp(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var role in roles)
                claims.Add(new Claim("role", role));

            return claims;
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenHandler = new JsonWebTokenHandler();
            string token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = appSettings.Issuer,
                Audience = appSettings.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return token;
        }

        private static long ToTimestamp(DateTime date)
        {
            TimeSpan deltaTime = date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)Math.Round(deltaTime.TotalSeconds);
        }
    }
}
