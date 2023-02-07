using App.Core.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using App.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using App.Application.Services.Abstractions;
using System.Security.Claims;

namespace App.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public UserController(
            UserManager<AppUser> userManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<Results<Ok, ProblemHttpResult>> Register(UserCreateDto newUser)
        {
            if(newUser is null)
            {
                return TypedResults.Problem();
            }

            var user = new AppUser
            {
                UserName = newUser.Username,
                Email = newUser.Email,
                FullName = newUser.FullName
            };

            var result = await _userManager.CreateAsync(user, newUser.Password);

            if(!result.Succeeded)
            {
                return TypedResults.Problem(result.Errors.FirstOrDefault()?.Description, statusCode: 400);
            }

            return TypedResults.Ok();
        }

        [HttpPost("token")]
        [ProducesResponseType(typeof(TokenReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TokenReadDto), StatusCodes.Status400BadRequest)]
        public async Task<Results<Ok<TokenReadDto>, BadRequest>> GetToken(UserLoginDto user)
        {
            var loggedUser = await _userManager.FindByNameAsync(user.Username);
            if (loggedUser is null || !await _userManager.CheckPasswordAsync(loggedUser, user.Password))
            {
                return TypedResults.BadRequest();
            }

            var token = _tokenService.GenerateToken(loggedUser);

            return TypedResults.Ok(token);
        }
    }
}
