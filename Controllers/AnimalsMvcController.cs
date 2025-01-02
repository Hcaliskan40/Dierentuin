using Dierentuin.Data;
using Dierentuin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dierentuin.Controllers
{
    public class AnimalsMvcController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimalsMvcController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var animals = await _context.Animals.ToListAsync();
            return View(animals);
        }
    }
}