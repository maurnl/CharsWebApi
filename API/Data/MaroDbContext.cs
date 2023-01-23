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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Relationship>()
                        .HasOne(r => r.Character)
                        .WithMany(r => r.Relationships)
                        .HasForeignKey(r => r.CharacterId);

            modelBuilder.Entity<Relationship>()
                        .HasOne(r => r.RelatedCharacter)
                        .WithMany(r => r.RelatedTo)
                        .HasForeignKey(r => r.RelatedCharacterId);
        }
    }
}
