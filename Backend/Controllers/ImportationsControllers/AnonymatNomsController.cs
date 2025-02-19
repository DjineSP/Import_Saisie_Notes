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
    public class AnonymatNomsController : ControllerBase
    {
        private readonly ImportationDbContext _context;

        public AnonymatNomsController(ImportationDbContext context)
        {
            _context = context;
        }

        // GET: api/AnonymatNoms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnonymatNom>>> GetAnonymatsNoms()
        {
            return await _context.AnonymatsNoms.ToListAsync();
        }

        // GET: api/AnonymatNoms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnonymatNom>> GetAnonymatNom(string id)
        {
            var anonymatNom = await _context.AnonymatsNoms.FindAsync(id);

            if (anonymatNom == null)
            {
                return NotFound();
            }

            return anonymatNom;
        }

        // PUT: api/AnonymatNoms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnonymatNom(string id, AnonymatNom anonymatNom)
        {
            if (id != anonymatNom.NumAnonymat)
            {
                return BadRequest();
            }

            _context.Entry(anonymatNom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnonymatNomExists(id))
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

        // POST: api/AnonymatNoms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnonymatNom>> PostAnonymatNom(AnonymatNom anonymatNom)
        {
            _context.AnonymatsNoms.Add(anonymatNom);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AnonymatNomExists(anonymatNom.NumAnonymat))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAnonymatNom", new { id = anonymatNom.NumAnonymat }, anonymatNom);
        }

        // DELETE: api/AnonymatNoms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnonymatNom(string id)
        {
            var anonymatNom = await _context.AnonymatsNoms.FindAsync(id);
            if (anonymatNom == null)
            {
                return NotFound();
            }

            _context.AnonymatsNoms.Remove(anonymatNom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnonymatNomExists(string id)
        {
            return _context.AnonymatsNoms.Any(e => e.NumAnonymat == id);
        }
    }
}
