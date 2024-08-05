using System.Security.Claims;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(SignInManager<User> signInManager) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {

            var result = await signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await signInManager.UserManager.FindByNameAsync(loginDto.Username);

                if (await signInManager.UserManager.IsInRoleAsync(user!, "admin"))
                {
					return Ok(new { message = "Login successful" });
				}
                else
                {
                    await signInManager.SignOutAsync();
                    return Unauthorized(new { message = "Only users with the Admin role can log in" });
                }

            }
            else
            {
				return Unauthorized(new { message = "Invalid login attempt" });
			}
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();

			return Ok(new { message = "Logout successful" });
		}

        [HttpGet("auth-status")]
        public ActionResult GetAuthState()
        {
            return Ok(new { IsAuthenticated = User.Identity?.IsAuthenticated ?? false });
        }

        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
			if (User.Identity?.IsAuthenticated == false) return NoContent();

			var user = await signInManager.UserManager.FindByNameAsync(User.Identity!.Name!);

            return Ok(new
            {
				user!.Name,
                Username = user.UserName,
                Roles = signInManager.UserManager.GetRolesAsync(user).Result
			});
        }

    }
}