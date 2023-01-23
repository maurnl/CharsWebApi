using App.Application.Services;
using App.Application.Services.Impl;
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

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddScoped<ICharacterService, CharacterService>();
            return services;
        }
    }
}
