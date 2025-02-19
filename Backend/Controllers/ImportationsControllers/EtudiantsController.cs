using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models.ImportationModels;

namespace Backend.Controllers.UserControllers;

[Route("api/[controller]")]
[ApiController]
public class EtudiantsController : ControllerBase
{
    private readonly ImportationDbContext _context;

    public EtudiantsController(ImportationDbContext context)
    {
        _context = context;
    }

    // GET: api/Etudiants
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Etudiant>>> GetEtudiants()
    {
        return await _context.Etudiants.ToListAsync();
    }

    // GET: api/Etudiants/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Etudiant>> GetEtudiant(string id)
    {
        var etudiant = await _context.Etudiants.FindAsync(id);

        if (etudiant == null)
        {
            return NotFound();
        }

        return etudiant;
    }

    // PUT: api/Etudiants/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEtudiant(string id, Etudiant etudiant)
    {
        if (id != etudiant.Matricule)
        {
            return BadRequest();
        }

        _context.Entry(etudiant).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EtudiantExists(id))
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

    // POST: api/Etudiants
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Etudiant>> PostEtudiant(Etudiant etudiant)
    {
        _context.Etudiants.Add(etudiant);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (EtudiantExists(etudiant.Matricule))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetEtudiant", new { id = etudiant.Matricule }, etudiant);
    }

    // DELETE: api/Etudiants/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEtudiant(string id)
    {
        var etudiant = await _context.Etudiants.FindAsync(id);
        if (etudiant == null)
        {
            return NotFound();
        }

        _context.Etudiants.Remove(etudiant);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EtudiantExists(string id)
    {
        return _context.Etudiants.Any(e => e.Matricule == id);
    }
}
