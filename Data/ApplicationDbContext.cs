using Dierentuin.Models;
using Microsoft.EntityFrameworkCore;

namespace Dierentuin.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Zoo> Zoos { get; set; } = default!;

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
                .WithOne(p => p.Predator)
                .HasForeignKey(a => a.PredatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relatie tussen Zoo en Animals
            modelBuilder.Entity<Zoo>()
                .HasMany(z => z.Animals)
                .WithOne(a => a.Zoo)
                .HasForeignKey(a => a.ZooId)
                .OnDelete(DeleteBehavior.Cascade); // Als een Zoo verwijderd wordt, worden de dieren ook verwijderd

            // Relatie tussen Zoo en Categories
            modelBuilder.Entity<Zoo>()
                .HasMany(z => z.Categories)
                .WithOne(c => c.Zoo) // We don't need a navigation property in Category for Zoo
                .HasForeignKey("ZooId")  // Explicitly define the foreign key for the Zoo-Categories relationship
                .OnDelete(DeleteBehavior.Cascade); // Choose your delete behavior
            //Zoo en Enclosure
            modelBuilder.Entity<Enclosure>()
           .HasOne(e => e.Zoo)
           .WithMany(z => z.Enclosures)
           .HasForeignKey(e => e.ZooId);

            // Seeding Zoo data (Only one zoo with Id = 1)
            modelBuilder.Entity<Zoo>().HasData(
                new Zoo { Id = 1, Name = "Safari Park" },
                new Zoo { Id = 2, Name = "Wildlife Reserve" }
            );

            // Categorieën seeding
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Mammals", ZooId = 1, IsActive = true },
                new Category { Id = 2, Name = "Reptiles", ZooId = 1, IsActive = true }
            );

            // Dieren seeding
            modelBuilder.Entity<Animal>().HasData(
                new Animal
                {
                    Id = 1,
                    Name = "Lion",
                    Species = "Panthera leo",
                    CategoryId = 1,
                    ZooId = 1, // Verwijzing naar een dierentuin
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
                    ZooId = 1, // Verwijzing naar een dierentuin
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
                    ZooId = 1, // Verwijzing naar een dierentuin
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
                    ZooId = 1, // Verwijzing naar een dierentuin
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


