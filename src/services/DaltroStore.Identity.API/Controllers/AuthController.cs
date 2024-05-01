using DaltroStore.Identity.API.Extensions;
using DaltroStore.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Text;

namespace DaltroStore.Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
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
        public async Task<ActionResult> Register(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var identityUser = new IdentityUser
            {
                UserName = userRegisterDto.Name,
                Email = userRegisterDto.Email,
                EmailConfirmed = true
            };

            var registerResult = await userManager.CreateAsync(identityUser, userRegisterDto.Password);

            if (registerResult.Succeeded)
            {
                await signInManager.SignInAsync(identityUser, isPersistent: false);
                return CreatedAtAction(nameof(Register), await GenerateJwt(identityUser));
            }

            return BadRequest(registerResult.Errors.Select(identityError => identityError.Description));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await signInManager.PasswordSignInAsync(userLoginDto.Email, userLoginDto.Password,
                isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                IdentityUser? identityUser = await userManager.FindByEmailAsync(userLoginDto.Email);
                if (identityUser != null)
                    return Ok(await GenerateJwt(identityUser));
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }

        private async Task<UserLoginResponseDto> GenerateJwt(IdentityUser user)
        {
            var claims = await userManager.GetClaimsAsync(user);
            IEnumerable<string> roles = await userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToTimestamp(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToTimestamp(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var role in roles)
                claims.Add(new Claim("role", role));

            var identityClaims = new ClaimsIdentity(claims);

            var tokenHandler = new JsonWebTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = appSettings.Issuer,
                Audience = appSettings.Audience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var response = new UserLoginResponseDto()
            {
                AcessToken = token,
                ExpiresIn = TimeSpan.FromHours(appSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserTokenDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(claim => new UserClaimDto { Type = claim.Type, Value = claim.Value })
                }
            };

            return response;
        }

        private static long ToTimestamp(DateTime date)
        {
            TimeSpan deltaTime = date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)Math.Round(deltaTime.TotalSeconds);
        }
    }
}