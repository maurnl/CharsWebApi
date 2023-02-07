using App.Core.Model;
using App.Core.Model.Relationships;
using App.Core.Model.Relationships.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;

namespace App.DataAccess.Util
{
    public static class DbInitializer
    {
        public static void PopulateDb(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var context = scope.ServiceProvider.GetRequiredService<MaroDbContext>();
            SeedUsers(userManager);
            SeedCharacters(userManager, context);
        }

        private static void SeedUsers(UserManager<AppUser> userManager)
        {
            if(userManager.Users.Any())
            {
                return;
            }

            var user = new AppUser
            {
                UserName = "Yura",
                Email = "yuramail@mail.com",
                FullName = "Mauro Luciano"
            };
            userManager.CreateAsync(user, "Pa55w0rd!");
        }

        private static async void SeedCharacters(UserManager<AppUser> userManager, MaroDbContext context)
        {
            var owner = await userManager.FindByEmailAsync("yuramail@mail.com");
            if (!context.Characters.Any())
            {
                var charUno = new Character
                {
                    Owner = owner,
                    OwnerId = owner.Id,
                    Name = "Testonio",
                    Gender = Gender.Male,
                    Strength = 25,
                    Knowledge = 25,
                    Dexterity = 25
                };
                var charDos = new Character
                {
                    Owner = owner,
                    OwnerId = owner.Id,
                    Name = "Tester",
                    Gender = Gender.Female
                };

                context.Characters.Add(charUno);
                context.Characters.Add(charDos);

                context.Characters.Add(
                    new Character
                    {
                        Owner = owner,
                        OwnerId = owner.Id,
                        Name = "Testo",
                        Gender = Gender.Unknown,
                        Strength = 14,
                        Knowledge = 16,
                        Dexterity = 14
                    });

                context.SaveChanges();

                context.Relationships.Add(new Relationship
                {
                    Character = charUno,
                    CharacterId = charUno.Id,
                    RelatedCharacter = charDos,
                    RelatedCharacterId = charDos.Id,
                    RelationshipTypeName = "parental",
                });

                context.SaveChanges();

            }
        }
    }
}
