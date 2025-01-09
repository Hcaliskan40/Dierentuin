using Dierentuin.Models;
using Microsoft.EntityFrameworkCore;

namespace Dierentuin.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relatie tussen dieren en categorieën
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Animals)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            // Relatie voor prooi (self-referencing)
            modelBuilder.Entity<Animal>()
                .HasMany(a => a.Prey)
                .WithOne(p => p.Predator) // 'Predator' is de eigenschap in de prooi die naar de jager wijst
                .HasForeignKey(a => a.PredatorId) // 'PredatorId' is de foreign key in het dier
                .OnDelete(DeleteBehavior.Restrict);

            // Categorieën seeding
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Mammals", IsActive = true },
                new Category { Id = 2, Name = "Reptiles", IsActive = true }
            );

            // Dieren seeding
            modelBuilder.Entity<Animal>().HasData(
                new Animal
                {
                    Id = 1,
                    Name = "Lion",
                    Species = "Panthera leo",
                    CategoryId = 1,
                    Size = SizeEnum.Large,
                    DietaryClass = DietaryClassEnum.Carnivore,
                    ActivityPattern = ActivityPatternEnum.Diurnal,
                    SpaceRequirement = 12.5,
                    SecurityRequirement = SecurityLevelEnum.High,
                    SunriseAction = ActionEnum.WakeUp,
                    SunsetAction = ActionEnum.Sleep,
                    FeedingTime = ActionEnum.WakeUp
                },
                new Animal
                {
                    Id = 2,
                    Name = "Python",
                    Species = "Python regius",
                    CategoryId = 2,
                    Size = SizeEnum.Medium,
                    DietaryClass = DietaryClassEnum.Carnivore,
                    ActivityPattern = ActivityPatternEnum.Nocturnal,
                    SpaceRequirement = 8.0,
                    SecurityRequirement = SecurityLevelEnum.Medium,
                    SunriseAction = ActionEnum.Sleep,
                    SunsetAction = ActionEnum.WakeUp,
                    FeedingTime = ActionEnum.Sleep
                },
                new Animal
                {
                    Id = 3,
                    Name = "Deer",
                    Species = "Cervidae",
                    CategoryId = 1,
                    Size = SizeEnum.Medium,
                    DietaryClass = DietaryClassEnum.Herbivore,
                    ActivityPattern = ActivityPatternEnum.Diurnal,
                    SpaceRequirement = 10.0,
                    SecurityRequirement = SecurityLevelEnum.Low,
                    SunriseAction = ActionEnum.WakeUp,
                    SunsetAction = ActionEnum.Sleep,
                    FeedingTime = ActionEnum.WakeUp
                },
                new Animal
                {
                    Id = 4,
                    Name = "Tiger",
                    Species = "Panthera tigris",
                    CategoryId = 1,
                    Size = SizeEnum.Large,
                    DietaryClass = DietaryClassEnum.Carnivore,
                    ActivityPattern = ActivityPatternEnum.Nocturnal,
                    SpaceRequirement = 15.0,
                    SecurityRequirement = SecurityLevelEnum.High,
                    SunriseAction = ActionEnum.Sleep,
                    SunsetAction = ActionEnum.WakeUp,
                    FeedingTime = ActionEnum.Sleep,
                    PredatorId = 1 // Set the foreign key for the predator (Lion)
                }
            );
        }

    }
}

