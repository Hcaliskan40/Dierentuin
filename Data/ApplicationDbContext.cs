using Microsoft.EntityFrameworkCore;
using Dierentuin.Models;

namespace Dierentuin.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Enclosure> Enclosures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optionele seeding data
            modelBuilder.Entity<Enclosure>().HasData(
                new Enclosure
                {
                    Id = 1,
                    Name = "Savannah",
                    Size = 1000,
                    Climate = "Tropical",
                    HabitatType = HabitatType.Grassland,
                    SecurityLevel = SecurityLevel.Medium
                },
                new Enclosure
                {
                    Id = 2,
                    Name = "Aquatic Zone",
                    Size = 800,
                    Climate = "Temperate",
                    HabitatType = HabitatType.Aquatic,
                    SecurityLevel = SecurityLevel.High
                }
            );
        }
    }
}