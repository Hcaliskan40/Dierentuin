using System.ComponentModel.DataAnnotations;

namespace Dierentuin.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naam is verplicht.")]
        [StringLength(100, ErrorMessage = "De naam mag maximaal 100 tekens lang zijn.")]
        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        // Bijhouden van toegewezen dieren aan de categorie
        public List<Animal> Animals { get; set; } = new List<Animal>();

        // Hulpmethoden
        public void AddAnimal(Animal animal)
        {
            if (animal != null && !Animals.Contains(animal))
            {
                Animals.Add(animal);
            }
        }

        public void RemoveAnimal(Animal animal)
        {
            if (animal != null && Animals.Contains(animal))
            {
                Animals.Remove(animal);
            }
        }

        public bool HasAnimals()
        {
            return Animals.Any();
        }
    }
}
