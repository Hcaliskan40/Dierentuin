using Microsoft.EntityFrameworkCore;
using Dierentuin.Models;

namespace Dierentuin.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets voor de entiteiten
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding data for categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Mammals" },
                new Category { Id = 2, Name = "Reptiles" }
            );

            // Seeding data for animals
            modelBuilder.Entity<Animal>().HasData(
                new Animal
                {
                    Id = 1,
                    Name = "Lion",
                    Species = "Panthera leo",
                    CategoryId = 1,
                    Size = SizeEnum.Large,
                    DietaryClass = DietaryClassEnum.Carnivore,
                    ActivityPattern = ActivityPatternEnum.Diurnal
                },
                new Animal
                {
                    Id = 2,
                    Name = "Python",
                    Species = "Python regius",
                    CategoryId = 2,
                    Size = SizeEnum.Medium,
                    DietaryClass = DietaryClassEnum.Carnivore,
                    ActivityPattern = ActivityPatternEnum.Nocturnal
                }
            );
        }
    }
}
