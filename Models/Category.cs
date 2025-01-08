namespace Dierentuin.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
 
        public bool IsActive { get; set; } 

        public List<Animal> Animals { get; set; } = new List<Animal>();
    }
}