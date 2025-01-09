namespace Dierentuin.Models
{
    public class Enclosure
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Dieren in het verblijf
        public List<Animal> Animals { get; set; } = new List<Animal>();

        // Klimaat van het verblijf (enum)
        public ClimateEnum Climate { get; set; }

        // HabitatType van het verblijf (enum)
        public HabitatTypeEnum HabitatType { get; set; }

        // Beveiligingsniveau van het verblijf (enum)
        public SecurityLevelEnum SecurityLevel { get; set; }

        // Grootte van het verblijf in vierkante meters
        public double Size { get; set; }
    }

    public enum ClimateEnum
    {
        Tropical,
        Temperate,
        Arctic
    }

    [Flags]
    public enum HabitatTypeEnum
    {
        Forest = 1,
        Aquatic = 2,
        Desert = 4,
        Grassland = 8
    }

    public enum SecurityLevelEnum
    {
        Low,
        Medium,
        High
    }
}