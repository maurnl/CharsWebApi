using API.Model;

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
                context.Characters.AddRange(new List<Character>
                {
                    new Character
                    {
                        Id = Guid.NewGuid(),
                        Name = "Testo"
                    },
                    new Character
                    {
                        Id = Guid.NewGuid(),
                        Name = "Testonio"
                    },
                    new Character
                    {
                        Id = Guid.NewGuid(),
                        Name = "Tester"
                    }
                });
                context.SaveChanges();
            }
        }
    }
}
