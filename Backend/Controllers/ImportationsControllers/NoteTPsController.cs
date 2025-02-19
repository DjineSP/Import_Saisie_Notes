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
    public class NoteTPsController : ControllerBase
    {
        private readonly ImportationDbContext _context;

        public NoteTPsController(ImportationDbContext context)
        {
            _context = context;
        }

        // GET: api/NoteTPs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteTP>>> GetNotesTP()
        {
            return await _context.NotesTP.ToListAsync();
        }

        // GET: api/NoteTPs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteTP>> GetNoteTP(int id)
        {
            var noteTP = await _context.NotesTP.FindAsync(id);

            if (noteTP == null)
            {
                return NotFound();
            }

            return noteTP;
        }

        // PUT: api/NoteTPs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoteTP(int id, NoteTP noteTP)
        {
            if (id != noteTP.IdNoteTP)
            {
                return BadRequest();
            }

            _context.Entry(noteTP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteTPExists(id))
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

        // POST: api/NoteTPs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NoteTP>> PostNoteTP(NoteTP noteTP)
        {
            _context.NotesTP.Add(noteTP);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNoteTP", new { id = noteTP.IdNoteTP }, noteTP);
        }

        // DELETE: api/NoteTPs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteTP(int id)
        {
            var noteTP = await _context.NotesTP.FindAsync(id);
            if (noteTP == null)
            {
                return NotFound();
            }

            _context.NotesTP.Remove(noteTP);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteTPExists(int id)
        {
            return _context.NotesTP.Any(e => e.IdNoteTP == id);
        }
    }
}
