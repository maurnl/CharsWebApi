using App.Application.Dtos;
using App.Application.Services.Abstractions;
using App.Core.Model;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace App.Application.Services.Jwt
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenReadDto GenerateToken(CustomUser user)
        {
            List<Claim> claims = new List<Claim>() {
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim (JwtRegisteredClaimNames.Email, user.Email),
                new Claim (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim (JwtRegisteredClaimNames.Name, user.FullName)
            };

            JwtSecurityToken token = new TokenBuilder()
            .AddAudience(_configuration["Jwt:Audience"]!)
            .AddIssuer(_configuration["Jwt:Issuer"]!)
            .AddExpiry(int.Parse(_configuration["Jwt:ExpiresIn"]!))
            .AddKey(_configuration["Jwt:Key"]!)
            .AddClaims(claims)
            .Build();

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenReadDto
            {
                Token = accessToken,
                ExpresInMinutes = int.Parse(_configuration["Jwt:ExpiresIn"]!)
            };
        }
    }
}
