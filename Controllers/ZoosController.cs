using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dierentuin.Data;
using Dierentuin.Models;

namespace Dierentuin.Controllers
{
    public class ZoosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZoosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zoos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Zoos.ToListAsync());
        }

        // GET: Zoos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoo = await _context.Zoos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zoo == null)
            {
                return NotFound();
            }

            return View(zoo);
        }

        // GET: Zoos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zoos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Zoo zoo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zoo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zoo);
        }

        // GET: Zoos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoo = await _context.Zoos.FindAsync(id);
            if (zoo == null)
            {
                return NotFound();
            }
            return View(zoo);
        }

        // POST: Zoos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Zoo zoo)
        {
            if (id != zoo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zoo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZooExists(zoo.Id))
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
            return View(zoo);
        }


        public IActionResult Sunrise()
        {
            var animals = _context.Animals.ToList();

            foreach (var animal in animals)
            {
                bool isAwake = animal.ActivityPattern == ActivityPatternEnum.Diurnal
                               || animal.ActivityPattern == ActivityPatternEnum.Cathemeral;

                ViewBag.SunriseMessages ??= new List<string>();
                ViewBag.SunriseMessages.Add(isAwake
                    ? $"{animal.Name} is now awake!"
                    : $"{animal.Name} remains asleep.");
            }

            return View(animals);
        }

        public IActionResult Sunset()
        {
            var animals = _context.Animals.ToList();

            foreach (var animal in animals)
            {
                bool isAsleep = animal.ActivityPattern == ActivityPatternEnum.Nocturnal
                                || animal.ActivityPattern == ActivityPatternEnum.Cathemeral;

                ViewBag.SunsetMessages ??= new List<string>();
                ViewBag.SunsetMessages.Add(isAsleep
                    ? $"{animal.Name} is now asleep!"
                    : $"{animal.Name} remains awake.");
            }

            return View(animals);
        }

        public IActionResult FeedingTime()
        {
            var animals = _context.Animals.ToList();

            foreach (var animal in animals)
            {
                string foodType = animal.DietaryClass switch
                {
                    DietaryClassEnum.Carnivore => "meat",
                    DietaryClassEnum.Herbivore => "plants",
                    DietaryClassEnum.Omnivore => "plants and meat",
                    DietaryClassEnum.Insectivore => "insects",
                    DietaryClassEnum.Piscivore => "fish",
                    _ => "unknown food"
                };

                ViewBag.FeedingMessages ??= new List<string>();
                ViewBag.FeedingMessages.Add($"{animal.Name} eats {foodType}.");
            }

            return View(animals);
        }

        public IActionResult CheckConstraints()
        {
            var animals = _context.Animals.ToList();

            foreach (var animal in animals)
            {
                bool satisfiesConstraints = true;

                if (animal.Size == SizeEnum.Microscopic) // voorbeeldconstraint
                {
                    satisfiesConstraints = false;
                }

                ViewBag.ConstraintMessages ??= new List<string>();
                ViewBag.ConstraintMessages.Add(satisfiesConstraints
                    ? $"{animal.Name} voldoet aan de constraints."
                    : $"{animal.Name} voldoet niet aan de constraints!");
            }

            return View(animals);
        }

        public IActionResult AutoAssign(int id, bool resetEnclosures = false)
        {
            var zoo = _context.Zoos
                .Include(z => z.Animals)
                .Include(z => z.Enclosures)
                .FirstOrDefault(z => z.Id == id);

            if (zoo == null)
            {
                return NotFound();
            }

            if (resetEnclosures)
            {
                zoo.Enclosures.Clear();
            }

            foreach (var animal in zoo.Animals)
            {
                var suitableEnclosure = zoo.Enclosures.FirstOrDefault(e =>
                    e.SpaceAvailable >= animal.SpaceRequirement &&
                    e.SecurityLevel >= animal.SecurityRequirement);

                if (suitableEnclosure == null)
                {
                    suitableEnclosure = new Enclosure
                    {
                        Name = $"Enclosure {zoo.Enclosures.Count + 1}",
                        SpaceAvailable = 100, // Example default
                        SecurityLevel = SecurityLevelEnum.Medium // Example default
                    };
                    zoo.Enclosures.Add(suitableEnclosure);
                }

                suitableEnclosure.Animals.Add(animal);
            }

            _context.SaveChanges();
            return RedirectToAction("Details", new { id = zoo.Id });
        }



        // GET: Zoos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoo = await _context.Zoos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zoo == null)
            {
                return NotFound();
            }

            return View(zoo);
        }

        // POST: Zoos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zoo = await _context.Zoos.FindAsync(id);
            if (zoo != null)
            {
                _context.Zoos.Remove(zoo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZooExists(int id)
        {
            return _context.Zoos.Any(e => e.Id == id);
        }
    }
}
