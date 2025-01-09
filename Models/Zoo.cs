namespace Dierentuin.Models
{
    public class Zoo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Relationships with other models (e.g. animals, enclosures)
        public List<Animal> Animals { get; set; } = new List<Animal>();
        public List<Enclosure> Enclosures { get; set; } = new List<Enclosure>();

        // Actions (Sunrise, Sunset, FeedingTime, CheckConstraints, AutoAssign)

        public void Sunrise()
        {
            foreach (var animal in Animals)
            {
                animal.SunriseActionEffect();
            }
        }

        public void Sunset()
        {
            foreach (var animal in Animals)
            {
                animal.SunsetActionEffect();
            }
        }

        public void FeedingTime()
        {
            foreach (var animal in Animals)
            {
                animal.FeedingTimeEffect();
            }
        }

        public void CheckConstraints()
        {
            foreach (var animal in Animals)
            {
                if (!animal.CheckConstraints())
                {
                    Console.WriteLine($"{animal.Name} does not meet the constraints!");
                }
                else
                {
                    Console.WriteLine($"{animal.Name} meets the constraints.");
                }
            }
        }

        public void AutoAssign()
        {
            // AutoAssign logic
            Console.WriteLine("Auto-assigning animals to enclosures...");

            // Iterate over the list of animals and assign them to enclosures.
            foreach (var animal in Animals)
            {
                // Example: Check the animal's space and security requirements, and assign them to an enclosure.
                var availableEnclosure = Enclosures.FirstOrDefault(e => e.SpaceRequirement >= animal.SpaceRequirement && e.SecurityLevel >= animal.SecurityRequirement);
                if (availableEnclosure != null)
                {
                    Console.WriteLine($"{animal.Name} assigned to enclosure {availableEnclosure.Name}");
                    // Assign animal to enclosure (example of assignment)
                    availableEnclosure.Animals.Add(animal);
                }
                else
                {
                    Console.WriteLine($"No suitable enclosure found for {animal.Name}. Creating a new one.");
                    // Optionally, create a new enclosure if none is suitable (can ask the user to confirm)
                    var newEnclosure = new Enclosure
                    {
                        Name = $"{animal.Name}'s Enclosure",
                        SpaceRequirement = animal.SpaceRequirement,
                        SecurityLevel = animal.SecurityRequirement
                    };
                    Enclosures.Add(newEnclosure);
                    newEnclosure.Animals.Add(animal);
                }
            }
        }
    }

    // Enclosure model (simplified)
    public class Enclosure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double SpaceRequirement { get; set; }
        public SecurityLevelEnum SecurityLevel { get; set; }
        public List<Animal> Animals { get; set; } = new List<Animal>();
    }
}
