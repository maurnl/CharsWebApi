using App.Core.Model;
using App.Core.Model.Relationships;
using App.DataAccess.Abstractions;
using App.DataAccess.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.DataAccess
{
    public static class DataAcessServicesExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            AddDatabase(services);

            AddRepos(services);

            return services;
        }

        public static IServiceCollection AddRepos(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Character>, Repository<Character>>();
            services.AddScoped<IRepository<Relationship>, Repository<Relationship>>();

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<MaroDbContext>(opt =>
            {
                opt.UseInMemoryDatabase("marodb");
            });

            return services;
        }
    }
}
