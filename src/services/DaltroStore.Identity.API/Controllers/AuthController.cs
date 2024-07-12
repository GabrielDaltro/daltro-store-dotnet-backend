using DaltroStore.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            else
            {
                AddProcessingError("register", registerResult.Errors.Select(error => error.Description).ToArray());
                return CustomBadRequest();
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserLoginResponseDto>> Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid) return CustomBadRequest(ModelState);

            IdentityUser? identityUser = await userManager.FindByEmailAsync(userLoginDto.Email);
            if (identityUser == null)
                return CustomUnauthorized(title: "Email ou senha incorretos");

            var result = await signInManager.PasswordSignInAsync(identityUser, userLoginDto.Password,
                isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return Ok(await jwtGeneratorService.GenerateJwt(identityUser));
            }
            else if (result.IsLockedOut)
            {
                return CustomTooManyRequests(title: "Usuário temporariamente bloqueado por exceder número de tentativas");
            }
            else
            {
                return CustomUnauthorized(title: "Email ou senha incorretos");
            }
        }
    }
}