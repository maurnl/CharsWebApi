using App.Core.Model;
using App.Core.Model.Relationships;
using App.Core.Model.Relationships.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;

namespace App.DataAccess.Util
{
    public static class DbInitializer
    {
        public static void PopulateDb(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            SeedData(scope.ServiceProvider.GetRequiredService<MaroDbContext>());
        }

        private static void SeedData(MaroDbContext context)
        {
            if (!context.Characters.Any())
            {
                var charUno = new Character
                {
                    Name = "Testonio",
                    Gender = Gender.Male
                };
                var charDos = new Character
                {
                    Name = "Tester",
                    Gender = Gender.Female
                };

                context.Characters.Add(charUno);
                context.Characters.Add(charDos);

                context.Characters.Add(
                    new Character
                    {
                        Name = "Testo",
                        Gender = Gender.Unknown
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
