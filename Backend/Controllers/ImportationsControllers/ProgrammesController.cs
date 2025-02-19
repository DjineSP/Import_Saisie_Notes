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
    public class ProgrammesController : ControllerBase
    {
        private readonly ImportationDbContext _context;

        public ProgrammesController(ImportationDbContext context)
        {
            _context = context;
        }

        // GET: api/Programmes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Programme>>> GetProgrammes()
        {
            return await _context.Programmes.ToListAsync();
        }

        // GET: api/Programmes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Programme>> GetProgramme(int id)
        {
            var programme = await _context.Programmes.FindAsync(id);

            if (programme == null)
            {
                return NotFound();
            }

            return programme;
        }

        // PUT: api/Programmes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgramme(int id, Programme programme)
        {
            if (id != programme.IdProgramme)
            {
                return BadRequest();
            }

            _context.Entry(programme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgrammeExists(id))
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

        // POST: api/Programmes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Programme>> PostProgramme(Programme programme)
        {
            _context.Programmes.Add(programme);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProgramme", new { id = programme.IdProgramme }, programme);
        }

        // DELETE: api/Programmes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramme(int id)
        {
            var programme = await _context.Programmes.FindAsync(id);
            if (programme == null)
            {
                return NotFound();
            }

            _context.Programmes.Remove(programme);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgrammeExists(int id)
        {
            return _context.Programmes.Any(e => e.IdProgramme == id);
        }
    }
}
