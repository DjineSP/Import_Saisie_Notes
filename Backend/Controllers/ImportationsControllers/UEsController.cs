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
    public class UEsController : ControllerBase
    {
        private readonly ImportationDbContext _context;

        public UEsController(ImportationDbContext context)
        {
            _context = context;
        }

        // GET: api/UEs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UE>>> GetUEs()
        {
            return await _context.UEs.ToListAsync();
        }

        // GET: api/UEs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UE>> GetUE(int id)
        {
            var uE = await _context.UEs.FindAsync(id);

            if (uE == null)
            {
                return NotFound();
            }

            return uE;
        }

        // PUT: api/UEs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUE(int id, UE uE)
        {
            if (id != uE.IdUE)
            {
                return BadRequest();
            }

            _context.Entry(uE).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UEExists(id))
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

        // POST: api/UEs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UE>> PostUE(UE uE)
        {
            _context.UEs.Add(uE);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUE", new { id = uE.IdUE }, uE);
        }

        // DELETE: api/UEs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUE(int id)
        {
            var uE = await _context.UEs.FindAsync(id);
            if (uE == null)
            {
                return NotFound();
            }

            _context.UEs.Remove(uE);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UEExists(int id)
        {
            return _context.UEs.Any(e => e.IdUE == id);
        }
    }
}
