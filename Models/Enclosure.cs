using System.Collections.Generic;

namespace Dierentuin.Models
{
    public class Enclosure
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Size { get; set; }
        public string Climate { get; set; } = string.Empty;
        public HabitatType HabitatType { get; set; }
        public SecurityLevel SecurityLevel { get; set; }

        // Relatie met Animals
        public List<Animal> Animals { get; set; } = new List<Animal>();
    }

    [System.Flags]
    public enum HabitatType
    {
        Forest = 1,
        Aquatic = 2,
        Desert = 4,
        Grassland = 8
    }

    public enum SecurityLevel
    {
        Low,
        Medium,
        High
    }
}