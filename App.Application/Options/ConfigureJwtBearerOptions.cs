using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ContainerNinja.Core.Options
{
    public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly IConfiguration _configuration;

        public ConfigureJwtBearerOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            if (name == JwtBearerDefaults.AuthenticationScheme)
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"]
                };

                var lista = new List<string> { _configuration["Jwt:Key"], _configuration["Jwt:Issuer"], _configuration["Jwt:Audience"] };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = (context) =>
                    {
                        Console.WriteLine(context.Exception);
                        return Task.CompletedTask;
                    },

                    OnMessageReceived = (context) =>
                    {
                        return Task.CompletedTask;
                    },

                    OnTokenValidated = (context) =>
                    {
                        return Task.CompletedTask;
                    }
                };
            }
        }

        public void Configure(JwtBearerOptions options)
        {
            Configure(JwtBearerDefaults.AuthenticationScheme, options);
        }
    }
}