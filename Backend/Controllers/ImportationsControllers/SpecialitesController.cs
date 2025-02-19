using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models.ImportationModels;

namespace Backend.Controllers.ImportationsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialitesController : ControllerBase
    {
        private readonly ImportationDbContext _context;

        public SpecialitesController(ImportationDbContext context)
        {
            _context = context;
        }

        // GET: api/Specialites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialite>>> GetSpecialites()
        {
            return await _context.Specialites.ToListAsync();
        }

        // GET: api/Specialites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Specialite>> GetSpecialite(int id)
        {
            var specialite = await _context.Specialites.FindAsync(id);

            if (specialite == null)
            {
                return NotFound();
            }

            return specialite;
        }

        // PUT: api/Specialites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialite(int id, Specialite specialite)
        {
            if (id != specialite.IdSpecialite)
            {
                return BadRequest();
            }

            _context.Entry(specialite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialiteExists(id))
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

        // POST: api/Specialites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Specialite>> PostSpecialite(Specialite specialite)
        {
            _context.Specialites.Add(specialite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecialite", new { id = specialite.IdSpecialite }, specialite);
        }

        // DELETE: api/Specialites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialite(int id)
        {
            var specialite = await _context.Specialites.FindAsync(id);
            if (specialite == null)
            {
                return NotFound();
            }

            _context.Specialites.Remove(specialite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecialiteExists(int id)
        {
            return _context.Specialites.Any(e => e.IdSpecialite == id);
        }
    }
}
