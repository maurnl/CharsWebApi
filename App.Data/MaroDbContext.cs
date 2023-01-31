using App.Core.Model;
using App.Core.Model.Relationships;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DataAccess
{
    public class MaroDbContext : IdentityDbContext<CustomUser>
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
                        .HasForeignKey(r => r.CharacterId)
                        .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Relationship>()
                        .HasOne(r => r.RelatedCharacter)
                        .WithMany(r => r.RelatedTo)
                        .HasForeignKey(r => r.RelatedCharacterId)
                        .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<CustomUser>()
                        .HasMany(u => u.Characters)
                        .WithOne(c => c.Owner)
                        .HasForeignKey(c => c.OwnerId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
