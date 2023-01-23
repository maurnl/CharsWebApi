using App.Application;
using App.DataAccess.Util;
using App.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace App.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddApplication(builder.Environment);

            builder.Services.AddDataAccess();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            DbInitializer.PopulateDb(app);

            app.Run();
        }
    }
}