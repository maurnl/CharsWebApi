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
        private readonly UserManager<CustomUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _context;

        public UserController(UserManager<CustomUser> userManager, ITokenService tokenService, IHttpContextAccessor context)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
        }
        
        [HttpPost("register")]
        public async Task<Results<Ok, ProblemHttpResult>> Register(UserCreateDto newUser)
        {
            if(newUser is null)
            {
                return TypedResults.Problem();
            }

            var user = new CustomUser
            {
                UserName = newUser.Username,
                Email = newUser.Email,
                FullName = newUser.FullName
            };

            var result = await _userManager.CreateAsync(user, newUser.Password);

            if(!result.Succeeded)
            {
                return TypedResults.Problem(result.Errors.FirstOrDefault()?.Description);
            }

            return TypedResults.Ok();
        }

        [HttpPost("token")]
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

        [Authorize]
        [HttpGet("who")]
        public ActionResult<string> CurrentUser()
        {
            var context = _context.HttpContext;
            if (context?.User != null && context?.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                var identifier = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (identifier != null)
                {
                    return identifier.Value;
                }
            }

            return string.Empty;
        }
    }
}
