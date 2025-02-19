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
    public class NoteCCsController : ControllerBase
    {
        private readonly ImportationDbContext _context;

        public NoteCCsController(ImportationDbContext context)
        {
            _context = context;
        }

        // GET: api/NoteCCs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteCC>>> GetNotesCC()
        {
            return await _context.NotesCC.ToListAsync();
        }

        // GET: api/NoteCCs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteCC>> GetNoteCC(int id)
        {
            var noteCC = await _context.NotesCC.FindAsync(id);

            if (noteCC == null)
            {
                return NotFound();
            }

            return noteCC;
        }

        // PUT: api/NoteCCs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoteCC(int id, NoteCC noteCC)
        {
            if (id != noteCC.IdNoteCC)
            {
                return BadRequest();
            }

            _context.Entry(noteCC).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteCCExists(id))
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

        // POST: api/NoteCCs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NoteCC>> PostNoteCC(NoteCC noteCC)
        {
            _context.NotesCC.Add(noteCC);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNoteCC", new { id = noteCC.IdNoteCC }, noteCC);
        }

        // DELETE: api/NoteCCs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteCC(int id)
        {
            var noteCC = await _context.NotesCC.FindAsync(id);
            if (noteCC == null)
            {
                return NotFound();
            }

            _context.NotesCC.Remove(noteCC);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteCCExists(int id)
        {
            return _context.NotesCC.Any(e => e.IdNoteCC == id);
        }
    }
}
