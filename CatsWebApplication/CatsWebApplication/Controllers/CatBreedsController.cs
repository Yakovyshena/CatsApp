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
    public class CatBreedsController : ControllerBase
    {
        private readonly CatsAPIContext _context;

        public CatBreedsController(CatsAPIContext context)
        {
            _context = context;
        }

        // GET: api/CatBreeds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatBreed>>> GetCatBreeds()
        {
          if (_context.CatBreeds == null)
          {
              return NotFound();
          }
            return await _context.CatBreeds.ToListAsync();
        }

        // GET: api/CatBreeds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CatBreed>> GetCatBreed(int id)
        {
          if (_context.CatBreeds == null)
          {
              return NotFound();
          }
            var catBreed = await _context.CatBreeds.FindAsync(id);

            if (catBreed == null)
            {
                return NotFound();
            }

            return catBreed;
        }

        // PUT: api/CatBreeds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatBreed(int id, CatBreed catBreed)
        {
            if (id != catBreed.Id)
            {
                return BadRequest();
            }

            _context.Entry(catBreed).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatBreedExists(id))
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

        // POST: api/CatBreeds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CatBreed>> PostCatBreed(CatBreed catBreed)
        {
          if (_context.CatBreeds == null)
          {
              return Problem("Entity set 'CatsAPIContext.CatBreeds'  is null.");
          }
            _context.CatBreeds.Add(catBreed);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCatBreed", new { id = catBreed.Id }, catBreed);
        }

        // DELETE: api/CatBreeds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatBreed(int id)
        {
            if (_context.CatBreeds == null)
            {
                return NotFound();
            }
            var catBreed = await _context.CatBreeds.FindAsync(id);
            if (catBreed == null)
            {
                return NotFound();
            }

            _context.CatBreeds.Remove(catBreed);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatBreedExists(int id)
        {
            return (_context.CatBreeds?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public int GetCatCountPerCatBreed(int? catBreedId)
        {
            var catBreed = _context.CatBreeds.FindAsync(catBreedId);
            int cc = 0;
            foreach(var c in _context.Cats)
            {
                if (c.CatBreedId == catBreedId)
                    cc++;
            }
            return cc;
        }
    }
}
