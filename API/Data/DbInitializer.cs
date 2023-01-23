using API.Model;
using API.Model.Relationships;
using API.Model.Relationships.Types;
using System.Net.Mime;

namespace API.Data
{
    public static class DbInitializer
    {
        public static void PopulateDb(IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MaroDbContext>();
                SeedData(context);
            }
        }

        private static void SeedData(MaroDbContext context)
        {
            if(!context.Characters.Any())
            {
                var charUno = new Character
                {
                    Name = "Testonio"
                };
                var charDos = new Character
                {
                    Name = "Tester"
                };

                context.Characters.Add(charUno);
                context.Characters.Add(charDos);

                context.Characters.Add(
                    new Character
                    {
                        Name = "Testo",
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
