namespace Dierentuin.Models
{
    public class Enclosure
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Animal> Animals { get; set; } = new List<Animal>();
        public ClimateEnum Climate { get; set; }
        public HabitatTypeEnum HabitatType { get; set; }
        public SecurityLevelEnum SecurityLevel { get; set; }
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