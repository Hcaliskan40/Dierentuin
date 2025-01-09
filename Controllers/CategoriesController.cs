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
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index(string searchCategory, bool? isActive)
        {
            var categories = from c in _context.Categories
                             select c;

            if (!string.IsNullOrEmpty(searchCategory))
            {
                categories = categories.Where(c => c.Name.Contains(searchCategory));
            }

            if (isActive.HasValue)
            {
                categories = categories.Where(c => c.IsActive == isActive.Value);
            }

            ViewData["SearchCategory"] = searchCategory;
            ViewData["IsActive"] = isActive;

            return View(await categories.Include(c => c.Animals).ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Animals) // Assuming you have a navigation property for Animals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsActive")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Animals) // Fetch associated animals
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            ViewData["Animals"] = new MultiSelectList(_context.Animals, "Id", "Name", category.Animals.Select(a => a.Id));

            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsActive")] Category category, int[] selectedAnimals)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingCategory = await _context.Categories
                        .Include(c => c.Animals)
                        .FirstOrDefaultAsync(c => c.Id == id);

                    if (existingCategory != null)
                    {
                        existingCategory.Name = category.Name;
                        existingCategory.IsActive = category.IsActive;

                        existingCategory.Animals.Clear();
                        var animals = await _context.Animals.Where(a => selectedAnimals.Contains(a.Id)).ToListAsync();
                        foreach (var animal in animals)
                        {
                            existingCategory.Animals.Add(animal);
                        }

                        _context.Update(existingCategory);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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

            ViewData["Animals"] = new MultiSelectList(_context.Animals, "Id", "Name", selectedAnimals);
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Animals)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.Include(c => c.Animals).FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                category.Animals.Clear(); // Clear associated animals
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
