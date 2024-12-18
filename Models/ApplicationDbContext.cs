using Microsoft.EntityFrameworkCore;

namespace Dierentuin.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Define DbSets for your entities, for example:
        public DbSet<Animal> Animals { get; set; }
    }
}
