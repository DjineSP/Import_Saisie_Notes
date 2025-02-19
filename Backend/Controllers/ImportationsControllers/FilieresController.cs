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
    public class FilieresController : ControllerBase
    {
        private readonly ImportationDbContext _context;

        public FilieresController(ImportationDbContext context)
        {
            _context = context;
        }

        // GET: api/Filieres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filiere>>> GetFilieres()
        {
            return await _context.Filieres.ToListAsync();
        }

        // GET: api/Filieres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Filiere>> GetFiliere(int id)
        {
            var filiere = await _context.Filieres.FindAsync(id);

            if (filiere == null)
            {
                return NotFound();
            }

            return filiere;
        }

        // PUT: api/Filieres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFiliere(int id, Filiere filiere)
        {
            if (id != filiere.IdFiliere)
            {
                return BadRequest();
            }

            _context.Entry(filiere).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FiliereExists(id))
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

        // POST: api/Filieres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Filiere>> PostFiliere(Filiere filiere)
        {
            _context.Filieres.Add(filiere);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFiliere", new { id = filiere.IdFiliere }, filiere);
        }

        // DELETE: api/Filieres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFiliere(int id)
        {
            var filiere = await _context.Filieres.FindAsync(id);
            if (filiere == null)
            {
                return NotFound();
            }

            _context.Filieres.Remove(filiere);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FiliereExists(int id)
        {
            return _context.Filieres.Any(e => e.IdFiliere == id);
        }
    }
}
