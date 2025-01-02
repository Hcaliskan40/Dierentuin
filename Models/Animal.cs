namespace Dierentuin.Models
{
    public class Animal
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }

        // Enums voor HabitatType en SecurityLevel
        public HabitatType HabitatType { get; set; }
        public SecurityLevel SecurityLevel { get; set; }
    }

    // Enum voor HabitatType
    public enum HabitatType
    {
        Forest,
        Desert,
        Savannah,
        Ocean
    }

    // Enum voor SecurityLevel
    public enum SecurityLevel
    {
        Low,
        Medium,
        High
    }
}