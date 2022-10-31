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
    public class HumanController : ControllerBase
    {
        private readonly CatsAPIContext _context;

        public HumanController(CatsAPIContext context)
        {
            _context = context;
        }

        // GET: api/Humen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Human>>> GetHumans()
        {
          if (_context.Humans == null)
          {
              return NotFound();
          }
            return await _context.Humans.ToListAsync();
        }

        // GET: api/Humen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Human>> GetHuman(int id)
        {
          if (_context.Humans == null)
          {
              return NotFound();
          }
            var human = await _context.Humans.FindAsync(id);

            if (human == null)
            {
                return NotFound();
            }

            return human;
        }

        // PUT: api/Humen/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHuman(int id, Human human)
        {
            if (id != human.Id)
            {
                return BadRequest();
            }

            _context.Entry(human).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HumanExists(id))
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

        // POST: api/Humen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Human>> PostHuman(Human human)
        {
          if (_context.Humans == null)
          {
              return Problem("Entity set 'CatsAPIContext.Humans'  is null.");
          }
            _context.Humans.Add(human);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHuman", new { id = human.Id }, human);
        }

        // DELETE: api/Humen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHuman(int id)
        {
            if (_context.Humans == null)
            {
                return NotFound();
            }
            var human = await _context.Humans.FindAsync(id);
            if (human == null)
            {
                return NotFound();
            }

            _context.Humans.Remove(human);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HumanExists(int id)
        {
            return (_context.Humans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
