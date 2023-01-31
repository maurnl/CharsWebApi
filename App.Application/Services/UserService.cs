using App.Application.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;
        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public string GetCurrentUserGuid()
        {
            var context = _httpContext.HttpContext;
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
