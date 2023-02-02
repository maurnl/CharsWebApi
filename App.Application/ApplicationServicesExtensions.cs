using App.Application.Services;
using App.Application.Services.Abstractions;
using App.Application.Services.Jwt;
using ContainerNinja.Core.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddServices(env);

            services.AddHttpContextAccessor();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<IRelationshipsService, RelationshipsService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            services.ConfigureOptions<ConfigureJwtBearerOptions>()
                    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer();

            return services;
        }
    }
}
