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
    public class InscritsController : ControllerBase
    {
        private readonly ImportationDbContext _context;

        public InscritsController(ImportationDbContext context)
        {
            _context = context;
        }

        // GET: api/Inscrits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inscrit>>> GetInscrits()
        {
            return await _context.Inscrits.ToListAsync();
        }

        // GET: api/Inscrits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inscrit>> GetInscrit(int id)
        {
            var inscrit = await _context.Inscrits.FindAsync(id);

            if (inscrit == null)
            {
                return NotFound();
            }

            return inscrit;
        }

        // PUT: api/Inscrits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscrit(int id, Inscrit inscrit)
        {
            if (id != inscrit.IdInscrit)
            {
                return BadRequest();
            }

            _context.Entry(inscrit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscritExists(id))
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

        // POST: api/Inscrits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inscrit>> PostInscrit(Inscrit inscrit)
        {
            _context.Inscrits.Add(inscrit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInscrit", new { id = inscrit.IdInscrit }, inscrit);
        }

        // DELETE: api/Inscrits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscrit(int id)
        {
            var inscrit = await _context.Inscrits.FindAsync(id);
            if (inscrit == null)
            {
                return NotFound();
            }

            _context.Inscrits.Remove(inscrit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InscritExists(int id)
        {
            return _context.Inscrits.Any(e => e.IdInscrit == id);
        }
    }
}
