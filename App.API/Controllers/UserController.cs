using App.Core.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using App.Application.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace App.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<CustomUser> _userManager;

        public UserController(UserManager<CustomUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<Results<Ok, ProblemHttpResult>> Register(UserCreateDto newUser)
        {
            if(newUser is null)
            {
                return TypedResults.Problem();
            }

            var user = new CustomUser
            {
                UserName = newUser.Username,
                Email = newUser.Email
            };

            var result = await _userManager.CreateAsync(user, newUser.Password);

            if(!result.Succeeded)
            {
                return TypedResults.Problem(result.Errors.FirstOrDefault()?.Description);
            }

            return TypedResults.Ok();
        }
    }
}
