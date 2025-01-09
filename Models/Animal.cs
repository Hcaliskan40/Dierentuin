namespace Dierentuin.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public SizeEnum Size { get; set; }
        public DietaryClassEnum DietaryClass { get; set; }
        public ActivityPatternEnum ActivityPattern { get; set; }

        public List<Animal> Prey { get; set; } = new List<Animal>();
        public double SpaceRequirement { get; set; }
        public SecurityLevelEnum SecurityRequirement { get; set; }

        // Toevoegen van Predator relatie
        public int? PredatorId { get; set; }  // Foreign key naar de Predator
        public Animal? Predator { get; set; } // Verwijzing naar de predator (jager)
        public ActionEnum SunriseAction { get; set; }
        public ActionEnum SunsetAction { get; set; }
        public ActionEnum FeedingTime { get; set; }

        // Relatie met Zoo
        public int? ZooId { get; set; }       // Foreign key naar de Zoo
        public Zoo? Zoo { get; set; }

        public bool CheckConstraints()
        {
            if (SpaceRequirement > 10 && SecurityRequirement != SecurityLevelEnum.High)
            {
                return false;
            }
            return true;
        }

        // SunriseAction Methode
        public void SunriseActionEffect()
        {
            switch (SunriseAction)
            {
                case ActionEnum.WakeUp:
                    Console.WriteLine($"{Name} is waking up.");
                    break;
                case ActionEnum.Sleep:
                    Console.WriteLine($"{Name} remains asleep.");
                    break;
                case ActionEnum.AlwaysActive:
                    Console.WriteLine($"{Name} is always active.");
                    break;
            }
        }

        // SunsetAction Methode
        public void SunsetActionEffect()
        {
            switch (SunsetAction)
            {
                case ActionEnum.WakeUp:
                    Console.WriteLine($"{Name} is waking up.");
                    break;
                case ActionEnum.Sleep:
                    Console.WriteLine($"{Name} is going to sleep.");
                    break;
                case ActionEnum.AlwaysActive:
                    Console.WriteLine($"{Name} remains active.");
                    break;
            }
        }

        // FeedingTime Methode
        public void FeedingTimeEffect()
        {
            switch (FeedingTime)
            {
                case ActionEnum.WakeUp:
                    Console.WriteLine($"{Name} is eating its food.");
                    break;
                case ActionEnum.Sleep:
                    Console.WriteLine($"{Name} is not eating because it is asleep.");
                    break;
                case ActionEnum.AlwaysActive:
                    Console.WriteLine($"{Name} is always eating as it remains active.");
                    break;
            }
        }
    }

    // Enum voor Grootte van het dier
    public enum SizeEnum
    {
        Microscopic,
        VerySmall,
        Small,
        Medium,
        Large,
        VeryLarge
    }

    // Enum voor de Voedingsklasse van het dier
    public enum DietaryClassEnum
    {
        Carnivore,
        Herbivore,
        Omnivore,
        Insectivore,
        Piscivore
    }

    // Enum voor Activiteitspatroon van het dier
    public enum ActivityPatternEnum
    {
        Diurnal,
        Nocturnal,
        Cathemeral
    }

    // Enum voor Beveiligingsniveau van het dier
    public enum SecurityLevelEnum
    {
        Low,
        Medium,
        High
    }

    // Enum voor Acties zoals Sunrise, Sunset en FeedingTime
    public enum ActionEnum
    {
        WakeUp,
        Sleep,
        AlwaysActive
    }
}

