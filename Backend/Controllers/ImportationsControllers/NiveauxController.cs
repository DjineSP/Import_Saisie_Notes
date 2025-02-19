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
    public class NiveauxController : ControllerBase
    {
        private readonly ImportationDbContext _context;

        public NiveauxController(ImportationDbContext context)
        {
            _context = context;
        }

        // GET: api/Niveaux
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Niveau>>> GetNiveaux()
        {
            return await _context.Niveaux.ToListAsync();
        }

        // GET: api/Niveaux/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Niveau>> GetNiveau(int id)
        {
            var niveau = await _context.Niveaux.FindAsync(id);

            if (niveau == null)
            {
                return NotFound();
            }

            return niveau;
        }

        // PUT: api/Niveaux/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNiveau(int id, Niveau niveau)
        {
            if (id != niveau.IdNiveau)
            {
                return BadRequest();
            }

            _context.Entry(niveau).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NiveauExists(id))
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

        // POST: api/Niveaux
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Niveau>> PostNiveau(Niveau niveau)
        {
            _context.Niveaux.Add(niveau);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNiveau", new { id = niveau.IdNiveau }, niveau);
        }

        // DELETE: api/Niveaux/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNiveau(int id)
        {
            var niveau = await _context.Niveaux.FindAsync(id);
            if (niveau == null)
            {
                return NotFound();
            }

            _context.Niveaux.Remove(niveau);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NiveauExists(int id)
        {
            return _context.Niveaux.Any(e => e.IdNiveau == id);
        }
    }
}
