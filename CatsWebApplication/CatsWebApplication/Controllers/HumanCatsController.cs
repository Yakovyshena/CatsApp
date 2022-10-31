using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatsWebApplication.Models;

namespace CatsWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumanCatsController : ControllerBase
    {
        private readonly CatsAPIContext _context;

        public HumanCatsController(CatsAPIContext context)
        {
            _context = context;
        }

        // GET: api/HumanCats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HumanCat>>> GetHumanCats()
        {
          if (_context.HumanCats == null)
          {
              return NotFound();
          }
            return await _context.HumanCats.ToListAsync();
        }

        // GET: api/HumanCats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HumanCat>> GetHumanCat(int id)
        {
          if (_context.HumanCats == null)
          {
              return NotFound();
          }
            var humanCat = await _context.HumanCats.FindAsync(id);

            if (humanCat == null)
            {
                return NotFound();
            }

            return humanCat;
        }

        // PUT: api/HumanCats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHumanCat(int id, HumanCat humanCat)
        {
            if (id != humanCat.Id)
            {
                return BadRequest();
            }

            _context.Entry(humanCat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HumanCatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HumanCats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HumanCat>> PostHumanCat(HumanCat humanCat)
        {
          if (_context.HumanCats == null)
          {
              return Problem("Entity set 'CatsAPIContext.HumanCats'  is null.");
          }
            _context.HumanCats.Add(humanCat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHumanCat", new { id = humanCat.Id }, humanCat);
        }

        // DELETE: api/HumanCats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHumanCat(int id)
        {
            if (_context.HumanCats == null)
            {
                return NotFound();
            }
            var humanCat = await _context.HumanCats.FindAsync(id);
            if (humanCat == null)
            {
                return NotFound();
            }

            _context.HumanCats.Remove(humanCat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HumanCatExists(int id)
        {
            return (_context.HumanCats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
