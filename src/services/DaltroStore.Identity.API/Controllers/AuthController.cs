using DaltroStore.Identity.API.Extensions;
using DaltroStore.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Text;
using System.Data;

namespace DaltroStore.Identity.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly AppSettings appSettings;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
        }

        [HttpPost("create-account")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserLoginResponseDto>> Register(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid) return CustomBadRequest(ModelState);

            var identityUser = new IdentityUser
            {
                UserName = userRegisterDto.Name,
                Email = userRegisterDto.Email,
                EmailConfirmed = true
            };

            var registerResult = await userManager.CreateAsync(identityUser, userRegisterDto.Password);

            if (registerResult.Succeeded)
                return CreatedAtAction(nameof(Register), await GenerateJwt(identityUser));

            foreach (var registrationError in registerResult.Errors)
                AddProcessingError(registrationError.Description);
            return CustomBadRequest();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails) ,StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserLoginResponseDto>> Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid) return CustomBadRequest(ModelState);

            var result = await signInManager.PasswordSignInAsync(userLoginDto.Email, userLoginDto.Password,
                isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                IdentityUser? identityUser = await userManager.FindByEmailAsync(userLoginDto.Email);
                if (identityUser != null)
                    return Ok(await GenerateJwt(identityUser));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else if (result.IsLockedOut) 
            {
                AddProcessingError("Usuário temporariamente bloqueado por exceder número de tentativas");
                return CustomBadRequest();
            }
            else
            {
                AddProcessingError("Email ou senha incorretos");
                return CustomBadRequest();
            }
        }

        private async Task<UserLoginResponseDto> GenerateJwt(IdentityUser user)
        {
            IList<Claim> claims = await GetClaimsIdentity(user);

            var response = new UserLoginResponseDto()
            {
                AccessToken = GenerateToken(claims),
                ExpiresIn = TimeSpan.FromHours(appSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserTokenDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.UserName,
                    Claims = claims.Select(claim => new UserClaimDto { Type = claim.Type, Value = claim.Value })
                }
            };

            return response;
        }

        private async Task<IList<Claim>> GetClaimsIdentity(IdentityUser user)
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

        private string GenerateToken(IList<Claim> claims)
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