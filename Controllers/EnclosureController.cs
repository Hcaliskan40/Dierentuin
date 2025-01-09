using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;  // Voor dropdowns
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
            // Enum waarden vullen voor dropdowns
            ViewBag.Climates = new SelectList(Enum.GetValues(typeof(ClimateEnum)), ClimateEnum.Temperate);
            ViewBag.HabitatTypes = new SelectList(Enum.GetValues(typeof(HabitatTypeEnum)), HabitatTypeEnum.Forest);
            ViewBag.SecurityLevels = new SelectList(Enum.GetValues(typeof(SecurityLevelEnum)), SecurityLevelEnum.Medium);

            return View();
        }

        // Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Enclosure enclosure)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Enclosures.Add(enclosure);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index)); // Redirect naar Index
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Create POST: Error saving enclosure. Exception: {ex.Message}");
                }
            }

            // Dropdowns opnieuw vullen bij validatiefouten
            ViewBag.Climates = new SelectList(Enum.GetValues(typeof(ClimateEnum)), enclosure.Climate);
            ViewBag.HabitatTypes = new SelectList(Enum.GetValues(typeof(HabitatTypeEnum)), enclosure.HabitatType);
            ViewBag.SecurityLevels = new SelectList(Enum.GetValues(typeof(SecurityLevelEnum)), enclosure.SecurityLevel);

            return View(enclosure); // Return dezelfde view met validatiefouten
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

            // Dropdowns vullen
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
                try
                {
                    _context.Update(enclosure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnclosureExists(enclosure.Id))
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

            // Dropdowns opnieuw vullen bij validatiefouten
            ViewBag.Climates = new SelectList(Enum.GetValues(typeof(ClimateEnum)), enclosure.Climate);
            ViewBag.HabitatTypes = new SelectList(Enum.GetValues(typeof(HabitatTypeEnum)), enclosure.HabitatType);
            ViewBag.SecurityLevels = new SelectList(Enum.GetValues(typeof(SecurityLevelEnum)), enclosure.SecurityLevel);

            return View(enclosure);
        }

        // Delete - GET
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var enclosure = await _context.Enclosures
                    .Include(e => e.Animals) // Include gerelateerde data
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (enclosure == null)
                {
                    _logger.LogWarning($"Delete GET: Enclosure with ID {id} not found.");
                    return NotFound();
                }

                return View(enclosure);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete GET: Error fetching enclosure with ID {id}. Exception: {ex.Message}");
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
                var enclosure = await _context.Enclosures.FindAsync(id);

                if (enclosure == null)
                {
                    _logger.LogWarning($"Delete POST: Enclosure with ID {id} not found for deletion.");
                    return NotFound();
                }

                _context.Enclosures.Remove(enclosure);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Delete POST: Enclosure with ID {id} deleted successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete POST: Error deleting enclosure with ID {id}. Exception: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        private bool EnclosureExists(int id)
        {
            return _context.Enclosures.Any(e => e.Id == id);
        }
    }
}
