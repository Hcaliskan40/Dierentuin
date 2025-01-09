using Dierentuin.Data;
using Dierentuin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AnimalsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Animals
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
    {
        return await _context.Animals.ToListAsync();
    }

    // GET: api/Animals/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Animal>> GetAnimal(int id)
    {
        var animal = await _context.Animals.FindAsync(id);
        if (animal == null)
        {
            return NotFound();
        }
        return animal;
    }

    // GET: api/Animals/filter?name=Lion&species=Panthera&size=Medium&diet=Carnivore
    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<Animal>>> GetFilteredAnimals(string? name, string? species, SizeEnum? size, DietaryClassEnum? dietaryClass)
    {
        var query = _context.Animals.AsQueryable();

        if (!string.IsNullOrEmpty(name))
            query = query.Where(a => a.Name.Contains(name));

        if (!string.IsNullOrEmpty(species))
            query = query.Where(a => a.Species.Contains(species));

        if (size.HasValue)
            query = query.Where(a => a.Size == size.Value);

        if (dietaryClass.HasValue)
            query = query.Where(a => a.DietaryClass == dietaryClass.Value);

        return await query.ToListAsync();
    }

    // POST: api/Animals
    [HttpPost]
    public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
    {
        _context.Animals.Add(animal);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetAnimal", new { id = animal.Id }, animal);
    }

    // PUT: api/Animals/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAnimal(int id, Animal animal)
    {
        if (id != animal.Id)
        {
            return BadRequest();
        }
        _context.Entry(animal).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Animals/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
        var animal = await _context.Animals.FindAsync(id);
        if (animal == null)
        {
            return NotFound();
        }
        _context.Animals.Remove(animal);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // POST: api/Animals/{id}/sunrise
    [HttpPost("{id}/sunrise")]
    public IActionResult TriggerSunriseAction(int id)
    {
        var animal = _context.Animals.Find(id);
        if (animal == null)
        {
            return NotFound();
        }
        animal.SunriseActionEffect();
        return Ok($"Sunrise action for {animal.Name} triggered.");
    }

    // POST: api/Animals/{id}/sunset
    [HttpPost("{id}/sunset")]
    public IActionResult TriggerSunsetAction(int id)
    {
        var animal = _context.Animals.Find(id);
        if (animal == null)
        {
            return NotFound();
        }
        animal.SunsetActionEffect();
        return Ok($"Sunset action for {animal.Name} triggered.");
    }

    // POST: api/Animals/{id}/feeding
    [HttpPost("{id}/feeding")]
    public IActionResult TriggerFeedingTimeAction(int id)
    {
        var animal = _context.Animals.Find(id);
        if (animal == null)
        {
            return NotFound();
        }
        animal.FeedingTimeEffect();
        return Ok($"Feeding time for {animal.Name} triggered.");
    }
}


