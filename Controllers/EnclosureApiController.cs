using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dierentuin.Data;
using Dierentuin.Models;

namespace Dierentuin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnclosureApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EnclosureApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Enclosure
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enclosure>>> GetEnclosures()
        {
            return await _context.Enclosures.ToListAsync();
        }

        // GET: api/Enclosure/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enclosure>> GetEnclosure(int id)
        {
            var enclosure = await _context.Enclosures.FindAsync(id);

            if (enclosure == null) return NotFound();

            return enclosure;
        }

        // POST: api/Enclosure
        [HttpPost]
        public async Task<ActionResult<Enclosure>> PostEnclosure(Enclosure enclosure)
        {
            _context.Enclosures.Add(enclosure);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnclosure", new { id = enclosure.Id }, enclosure);
        }

        // PUT: api/Enclosure/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnclosure(int id, Enclosure enclosure)
        {
            if (id != enclosure.Id) return BadRequest();

            _context.Entry(enclosure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Enclosures.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/Enclosure/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnclosure(int id)
        {
            var enclosure = await _context.Enclosures.FindAsync(id);
            if (enclosure == null) return NotFound();

            _context.Enclosures.Remove(enclosure);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
