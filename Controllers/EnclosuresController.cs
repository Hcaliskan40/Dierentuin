using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dierentuin.Data;
using Dierentuin.Models;

namespace Dierentuin.Controllers
{
    public class EnclosuresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnclosuresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Index - Lijst van alle verblijven
        public async Task<IActionResult> Index()
        {
            var enclosures = await _context.Enclosures.Include(e => e.Animals).ToListAsync();
            return View(enclosures);
        }

        // Details - Details van een verblijf
        public async Task<IActionResult> Details(int id)
        {
            var enclosure = await _context.Enclosures
                .Include(e => e.Animals)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (enclosure == null)
            {
                return NotFound();
            }
            return View(enclosure);
        }

        // Create - GET
        [HttpGet]
        public IActionResult Create()
        {
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
            if (enclosure != null)
            {
                _context.Enclosures.Remove(enclosure);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
