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
using DaltroStore.Identity.API.Services;

namespace DaltroStore.Identity.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly JwtGeneratorService jwtGeneratorService;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              JwtGeneratorService jwtGeneratorService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.jwtGeneratorService = jwtGeneratorService;
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
                return CreatedAtAction(nameof(Register), await jwtGeneratorService.GenerateJwt(identityUser));

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
                    return Ok(await jwtGeneratorService.GenerateJwt(identityUser));
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
    }
}