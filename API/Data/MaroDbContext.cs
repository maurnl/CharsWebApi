using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MaroDbContext : DbContext
    {
        public MaroDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Character> Characters { get; set; }
    }
}
