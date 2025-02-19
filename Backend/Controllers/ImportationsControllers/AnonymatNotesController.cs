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
    public class AnonymatNotesController : ControllerBase
    {
        private readonly ImportationDbContext _context;

        public AnonymatNotesController(ImportationDbContext context)
        {
            _context = context;
        }

        // GET: api/AnonymatNotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnonymatNote>>> GetAnonymatsNotes()
        {
            return await _context.AnonymatsNotes.ToListAsync();
        }

        // GET: api/AnonymatNotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnonymatNote>> GetAnonymatNote(int id)
        {
            var anonymatNote = await _context.AnonymatsNotes.FindAsync(id);

            if (anonymatNote == null)
            {
                return NotFound();
            }

            return anonymatNote;
        }

        // PUT: api/AnonymatNotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnonymatNote(int id, AnonymatNote anonymatNote)
        {
            if (id != anonymatNote.IdAnonymatNote)
            {
                return BadRequest();
            }

            _context.Entry(anonymatNote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnonymatNoteExists(id))
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

        // POST: api/AnonymatNotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnonymatNote>> PostAnonymatNote(AnonymatNote anonymatNote)
        {
            _context.AnonymatsNotes.Add(anonymatNote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnonymatNote", new { id = anonymatNote.IdAnonymatNote }, anonymatNote);
        }

        // DELETE: api/AnonymatNotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnonymatNote(int id)
        {
            var anonymatNote = await _context.AnonymatsNotes.FindAsync(id);
            if (anonymatNote == null)
            {
                return NotFound();
            }

            _context.AnonymatsNotes.Remove(anonymatNote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnonymatNoteExists(int id)
        {
            return _context.AnonymatsNotes.Any(e => e.IdAnonymatNote == id);
        }
    }
}
