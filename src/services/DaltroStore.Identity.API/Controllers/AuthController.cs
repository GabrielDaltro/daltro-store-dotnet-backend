using DaltroStore.Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DaltroStore.Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
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
                return CreatedAtAction(nameof(Register), userRegisterDto);
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await signInManager.PasswordSignInAsync(userLoginDto.Email, userLoginDto.Password, 
                isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
                return Ok();
            return BadRequest();
        }
    }
}