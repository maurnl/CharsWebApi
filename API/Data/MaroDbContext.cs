using API.Model;
using API.Model.Relationships;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MaroDbContext : DbContext
    {
        public MaroDbContext(DbContextOptions<MaroDbContext> options) : base(options)
        {

        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Relationship> Relationships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Relationship>()
                        .HasKey(t => new
                        {
                            t.CharacterId,
                            t.RelatedCharacterId
                        });

            modelBuilder.Entity<Character>()
                        .HasMany(e => e.RelatedChars)
                        .WithMany(e => e.RelatedToChars)
                        .UsingEntity<Relationship>(
                            je => je.HasOne(e => e.Character).WithMany(e => e.Relationships), // <-- here you would specify the corresponding collection nav property when exists
                            je => je.HasOne(e => e.RelatedCharacter).WithMany(e => e.RelatedTo) // <-- here you would specify the corresponding collection nav property when exists
                        );
        }
    }
}
