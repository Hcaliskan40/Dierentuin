using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dierentuin.Data;
using Dierentuin.Models;

namespace Dierentuin.Controllers
{
    public class EnclosureMvcController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EnclosureMvcController> _logger;

        public EnclosureMvcController(ApplicationDbContext context, ILogger<EnclosureMvcController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Index - Lijst van alle verblijven
        public async Task<IActionResult> Index()
        {
            var enclosures = await _context.Enclosures.Include(e => e.Animals).ToListAsync();
            return View(enclosures);
        }

        // Create - GET
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Climates = new SelectList(Enum.GetValues(typeof(ClimateEnum)));
            ViewBag.HabitatTypes = new SelectList(Enum.GetValues(typeof(HabitatTypeEnum)));
            ViewBag.SecurityLevels = new SelectList(Enum.GetValues(typeof(SecurityLevelEnum)));
            return View();
        }

        // Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Enclosure enclosure)
        {
            if (ModelState.IsValid)
            {
                _context.Enclosures.Add(enclosure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Climates = new SelectList(Enum.GetValues(typeof(ClimateEnum)));
            ViewBag.HabitatTypes = new SelectList(Enum.GetValues(typeof(HabitatTypeEnum)));
            ViewBag.SecurityLevels = new SelectList(Enum.GetValues(typeof(SecurityLevelEnum)));
            return View(enclosure);
        }

        // Edit - GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var enclosure = await _context.Enclosures.FindAsync(id);
            if (enclosure == null)
            {
                return NotFound();
            }

            ViewBag.Climates = new SelectList(Enum.GetValues(typeof(ClimateEnum)), enclosure.Climate);
            ViewBag.HabitatTypes = new SelectList(Enum.GetValues(typeof(HabitatTypeEnum)), enclosure.HabitatType);
            ViewBag.SecurityLevels = new SelectList(Enum.GetValues(typeof(SecurityLevelEnum)), enclosure.SecurityLevel);
            return View(enclosure);
        }

        // Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Enclosure enclosure)
        {
            if (id != enclosure.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Update(enclosure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Climates = new SelectList(Enum.GetValues(typeof(ClimateEnum)), enclosure.Climate);
            ViewBag.HabitatTypes = new SelectList(Enum.GetValues(typeof(HabitatTypeEnum)), enclosure.HabitatType);
            ViewBag.SecurityLevels = new SelectList(Enum.GetValues(typeof(SecurityLevelEnum)), enclosure.SecurityLevel);
            return View(enclosure);
        }

        // Delete - GET
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var enclosure = await _context.Enclosures.FindAsync(id);
            if (enclosure == null)
            {
                return NotFound();
            }

            return View(enclosure);
        }

        // Delete - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enclosure = await _context.Enclosures.FindAsync(id);
            if (enclosure == null)
            {
                return NotFound();
            }

            _context.Enclosures.Remove(enclosure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Sunrise
        public async Task<IActionResult> Sunrise(int id)
        {
            var enclosure = await _context.Enclosures
                .Include(e => e.Animals)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enclosure == null)
            {
                return NotFound();
            }

            var sunriseStatus = enclosure.Animals.Select(animal => new
            {
                Name = animal.Name,
                Status = animal.ActivityPattern switch
                {
                    ActivityPatternEnum.Diurnal => "Waking Up",
                    ActivityPatternEnum.Nocturnal => "Going to Sleep",
                    _ => "Always Active"
                }
            });

            ViewBag.SunriseStatus = sunriseStatus;

            return View("ActionResult", enclosure);
        }

        // Sunset
        public async Task<IActionResult> Sunset(int id)
        {
            var enclosure = await _context.Enclosures
                .Include(e => e.Animals)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enclosure == null)
            {
                return NotFound();
            }

            var sunsetStatus = enclosure.Animals.Select(animal => new
            {
                Name = animal.Name,
                Status = animal.ActivityPattern switch
                {
                    ActivityPatternEnum.Nocturnal => "Waking Up",
                    ActivityPatternEnum.Diurnal => "Going to Sleep",
                    _ => "Always Active"
                }
            });

            ViewBag.SunsetStatus = sunsetStatus;

            return View("ActionResult", enclosure);
        }

        // Feeding Time
        public async Task<IActionResult> FeedingTime(int id)
        {
            var enclosure = await _context.Enclosures
                .Include(e => e.Animals)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enclosure == null)
            {
                return NotFound();
            }

            var feedingSchedule = enclosure.Animals.Select(animal => new
            {
                Name = animal.Name,
                Food = animal.DietaryClass switch
                {
                    DietaryClassEnum.Carnivore => "Eats other animals",
                    DietaryClassEnum.Herbivore => "Eats plants",
                    DietaryClassEnum.Omnivore => "Eats both plants and animals",
                    DietaryClassEnum.Insectivore => "Eats insects",
                    DietaryClassEnum.Piscivore => "Eats fish",
                    _ => "Unknown"
                }
            });

            ViewBag.FeedingSchedule = feedingSchedule;

            return View("ActionResult", enclosure);
        }
    }
}
