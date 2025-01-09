﻿namespace Dierentuin.Models
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
    }

    public enum SizeEnum
    {
        Microscopic,
        VerySmall,
        Small,
        Medium,
        Large,
        VeryLarge
    }

    public enum DietaryClassEnum
    {
        Carnivore,
        Herbivore,
        Omnivore,
        Insectivore,
        Piscivore
    }

    public enum ActivityPatternEnum
    {
        Diurnal,
        Nocturnal,
        Cathemeral
    }
}