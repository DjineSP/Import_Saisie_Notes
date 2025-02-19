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
    public class ClassesController : ControllerBase
    {
        private readonly ImportationDbContext _context;

        public ClassesController(ImportationDbContext context)
        {
            _context = context;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classe>>> GetClasses()
        {
            return await _context.Classes.ToListAsync();
        }

        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Classe>> GetClasse(int id)
        {
            var classe = await _context.Classes.FindAsync(id);

            if (classe == null)
            {
                return NotFound();
            }

            return classe;
        }

        // PUT: api/Classes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClasse(int id, Classe classe)
        {
            if (id != classe.IdClasse)
            {
                return BadRequest();
            }

            _context.Entry(classe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClasseExists(id))
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

        // POST: api/Classes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Classe>> PostClasse(Classe classe)
        {
            _context.Classes.Add(classe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClasse", new { id = classe.IdClasse }, classe);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClasse(int id)
        {
            var classe = await _context.Classes.FindAsync(id);
            if (classe == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(classe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClasseExists(int id)
        {
            return _context.Classes.Any(e => e.IdClasse == id);
        }
    }
}
