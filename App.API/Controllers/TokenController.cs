using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using App.Application.Dtos;
using App.Application.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using App.Core.Model;

namespace App.API
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<CustomUser> _userManager;

        public TokenController(ITokenService tokenService, UserManager<CustomUser> userManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        
        [HttpPost]
        public async Task<Results<Ok<TokenReadDto>, BadRequest>> GetToken(UserLoginDto user)
        {
            var loggedUser = await _userManager.FindByNameAsync(user.Username);
            if (loggedUser is null || await _userManager.CheckPasswordAsync(loggedUser, user.Password))
            {
                return TypedResults.BadRequest();
            }

            var token = _tokenService.GenerateToken(loggedUser);

            return TypedResults.Ok(token);
        }
    }
}