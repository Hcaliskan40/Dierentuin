using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;  // Vergeet deze niet
using Microsoft.EntityFrameworkCore;
using Dierentuin.Data;
using Dierentuin.Models;

namespace Dierentuin.Controllers
{
    public class AnimalsMvcController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<AnimalsMvcController> _logger;

        public AnimalsMvcController(ApplicationDbContext context, ILogger<AnimalsMvcController> logger)
        {
            _context = context;
            _logger = logger;
        }



        // Index - Lijst van alle dieren
        public async Task<IActionResult> Index()
        {
            var animals = await _context.Animals.Include(a => a.Category).ToListAsync();
            return View(animals);
        }

        // Create - GET
        [HttpGet]
        public IActionResult Create()
        {
            // Populating ViewBag for dropdowns, including enums
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");

            // Populating enum values for dropdowns (Size, DietaryClass, ActivityPattern)
            ViewBag.Sizes = new SelectList(Enum.GetValues(typeof(SizeEnum)), SizeEnum.Small);
            ViewBag.DietaryClasses = new SelectList(Enum.GetValues(typeof(DietaryClassEnum)), DietaryClassEnum.Herbivore);
            ViewBag.ActivityPatterns = new SelectList(Enum.GetValues(typeof(ActivityPatternEnum)), ActivityPatternEnum.Nocturnal);

            return View();
        }

        // Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Animal animal)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Animals.Add(animal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index)); // Redirect to Index
                }
                catch (Exception ex)
                {
                    // Log the error if needed
                    Console.WriteLine("Error saving to database: " + ex.Message);
                }
            }

            // Repopulate the dropdowns in case of validation errors
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", animal.CategoryId);
            ViewBag.Sizes = new SelectList(Enum.GetValues(typeof(SizeEnum)), animal.Size);
            ViewBag.DietaryClasses = new SelectList(Enum.GetValues(typeof(DietaryClassEnum)), animal.DietaryClass);
            ViewBag.ActivityPatterns = new SelectList(Enum.GetValues(typeof(ActivityPatternEnum)), animal.ActivityPattern);

            return View(animal); // Return the same view with validation errors
        }

        // Edit - GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            // Populate ViewBag with Enum values for dropdowns
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", animal.CategoryId);

            // Pass enum values for the dropdowns
            ViewBag.Sizes = new SelectList(Enum.GetValues(typeof(SizeEnum)).Cast<SizeEnum>(), animal.Size);
            ViewBag.DietaryClasses = new SelectList(Enum.GetValues(typeof(DietaryClassEnum)).Cast<DietaryClassEnum>(), animal.DietaryClass);
            ViewBag.ActivityPatterns = new SelectList(Enum.GetValues(typeof(ActivityPatternEnum)).Cast<ActivityPatternEnum>(), animal.ActivityPattern);

            return View(animal);
        }


        // Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Animal animal)
        {
            if (id != animal.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Repopulate ViewBag in case of validation errors
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", animal.CategoryId);
            ViewBag.Sizes = new SelectList(Enum.GetValues(typeof(SizeEnum)).Cast<SizeEnum>(), animal.Size);
            ViewBag.DietaryClasses = new SelectList(Enum.GetValues(typeof(DietaryClassEnum)).Cast<DietaryClassEnum>(), animal.DietaryClass);
            ViewBag.ActivityPatterns = new SelectList(Enum.GetValues(typeof(ActivityPatternEnum)).Cast<ActivityPatternEnum>(), animal.ActivityPattern);

            return View(animal);
        }

        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(e => e.Id == id);
        }


        // Action for Sunrise
        public IActionResult Sunrise(int id)
        {
            var animal = _context.Animals.Find(id);
            if (animal == null)
            {
                return NotFound();
            }

            // Check if the animal wakes up based on its activity pattern
            bool isAwake = animal.ActivityPattern == ActivityPatternEnum.Diurnal
                           || animal.ActivityPattern == ActivityPatternEnum.Cathemeral;

            ViewBag.IsAwakeMessage = isAwake
                ? $"{animal.Name} is now awake!"
                : $"{animal.Name} remains asleep.";

            return View(animal);
        }


        // Action for Sunset
        public IActionResult Sunset(int id)
        {
            var animal = _context.Animals.Find(id);
            if (animal == null)
            {
                return NotFound();
            }

            // Check if the animal goes to sleep based on its activity pattern
            bool isAsleep = animal.ActivityPattern == ActivityPatternEnum.Nocturnal
                            || animal.ActivityPattern == ActivityPatternEnum.Cathemeral;

            ViewBag.IsAsleepMessage = isAsleep
                ? $"{animal.Name} is now asleep!"
                : $"{animal.Name} remains awake.";

            return View(animal);
        }


        public IActionResult FeedingTime(int id)
        {
            var animal = _context.Animals.Find(id);
            if (animal == null)
            {
                return NotFound();
            }

            // Define feeding behavior
            string foodType = animal.DietaryClass switch
            {
                DietaryClassEnum.Carnivore => "meat",
                DietaryClassEnum.Herbivore => "plants",
                DietaryClassEnum.Omnivore => "plants and meat",
                DietaryClassEnum.Insectivore => "insects",
                DietaryClassEnum.Piscivore => "fish",
                _ => "unknown food"
            };

            ViewBag.FeedingInfo = $"{animal.Name} eats {foodType}.";
            return View(animal);
        }

        public IActionResult CheckConstraints(int id)
        {
            var animal = _context.Animals.Find(id);
            if (animal == null)
            {
                return NotFound();
            }

            // Logic to check constraints
            // Example: Check if the animal fits within the constraints (you can modify as per your rules)
            bool satisfiesConstraints = true;

            if (animal.Size == SizeEnum.Microscopic) // example constraint
            {
                satisfiesConstraints = false;
            }

            ViewBag.ConstraintsMessage = satisfiesConstraints ? "Constraints met!" : "Constraints niet voldaan!";
            return View(animal);
        }


        public IActionResult Search(string searchTerm, ActivityPatternEnum? activityPattern, DietaryClassEnum? dietaryClass)
        {
            // Start met een queryable lijst van alle dieren
            var animalsQuery = _context.Animals.Include(a => a.Category).AsQueryable();

            // Zoektermfilter
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                animalsQuery = animalsQuery.Where(a => a.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // Activiteitspatroonfilter
            if (activityPattern.HasValue)
            {
                animalsQuery = animalsQuery.Where(a => a.ActivityPattern == activityPattern.Value);
            }

            // Dieetklassefilter
            if (dietaryClass.HasValue)
            {
                animalsQuery = animalsQuery.Where(a => a.DietaryClass == dietaryClass.Value);
            }

            // Resultaten ophalen
            var filteredAnimals = animalsQuery.ToList();

            // Zoekcriteria opslaan in ViewData om de staat van het zoekformulier te behouden
            ViewData["SearchTerm"] = searchTerm;
            ViewData["SelectedActivityPattern"] = activityPattern;
            ViewData["SelectedDietaryClass"] = dietaryClass;

            // Retourneer de Index-view met de gefilterde lijst
            return View("Index", filteredAnimals);
        }




        // Delete - GET
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var animal = await _context.Animals
                    .Include(a => a.Category) // Include related data if necessary
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (animal == null)
                {
                    _logger.LogWarning($"Delete GET: Animal with ID {id} not found.");
                    return NotFound();
                }

                return View(animal);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete GET: Error fetching animal with ID {id}. Exception: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        // Delete - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var animal = await _context.Animals.FindAsync(id);

                if (animal == null)
                {
                    _logger.LogWarning($"Delete POST: Animal with ID {id} not found for deletion.");
                    return NotFound();
                }

                _context.Animals.Remove(animal);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Delete POST: Animal with ID {id} deleted successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete POST: Error deleting animal with ID {id}. Exception: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
    }
