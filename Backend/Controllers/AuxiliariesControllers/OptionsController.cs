using System.Security.Cryptography.Xml;
using Backend.Data;
using Backend.Models.AuxiliairiesModels;
using Backend.Models.ImportationModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers.AuxiliariesControllers;

[Route("api/[controller]")]
[ApiController]
public class OptionsController : ControllerBase
{
    private readonly ImportationDbContext _context;

    public OptionsController(ImportationDbContext context)
    {
        _context = context;
    }

    [HttpGet("combinations")]
    public async Task<ActionResult<OptionsResponse>> GetCombinations()
    {
        var last5Years = await _context.Inscrits
            .OrderByDescending(i => i.Annee)
            .Select(i => i.Annee)
            .Distinct()
            .Take(5)
            .ToListAsync();

        // Charger toutes les classes disponibles
        var allClasses = await _context.Classes
            .Select(c => c.CodeClasse)
            .ToListAsync();

        var semesters = new List<string> { "S1", "S2" };
        var evaluations = new List<string> { "CC", "TP", "SN" };

        var response = new OptionsResponse
        {
            Annee = last5Years,
            Semestres = semesters,
            Classes = allClasses,
            Evaluations = evaluations,
        };

        return Ok(response);
    }

    // Nouvel endpoint qui prend le semestre et le code de la classe comme paramètres
    [HttpGet("ues")]
    public async Task<ActionResult<List<string>>> GetUEs([FromQuery] string semestre, [FromQuery] string codeClasse)
    {
        if (string.IsNullOrEmpty(codeClasse) || string.IsNullOrEmpty(semestre))
        {
            return BadRequest("Le code de la classe et le semestre sont requis.");
        }

        // Requête pour obtenir les UEs pour une classe et un semestre donnés
        var ues = await _context.Programmes
            .Join(_context.UEs, p => p.IdUE, ue => ue.IdUE, (p, ue) => new
            {
                p.Classe.CodeClasse,
                ue.CodeUE,
                ue.Semestre
            })
            .Where(x => x.CodeClasse == codeClasse && x.Semestre == semestre)
            .Select(x => x.CodeUE)
            .ToListAsync();

        if (ues.Count == 0)
        {
            return NotFound("Aucune UE trouvée pour la classe et le semestre spécifiés.");
        }

        return Ok(ues);
    }

    [HttpGet("credit")]
    public async Task<ActionResult<int>> GetCredit([FromQuery] string ue, [FromQuery] string codeEval)
    {
        if (string.IsNullOrEmpty(ue) || string.IsNullOrEmpty(codeEval))
        {
            return BadRequest("L'UE et l'évaluation sont requis.");
        }

        // Rechercher l'ID de l'UE à partir du codeUE
        var ueId = await _context.UEs
            .Where(u => u.CodeUE == ue) // Vérifie si le codeUE correspond au nom de l'UE
            .Select(u => u.IdUE)
            .FirstOrDefaultAsync();

        if (ueId == 0)
        {
            return NotFound("Cette UE n'existe pas !");
        }

        // Rechercher le crédit de l'évaluation en fonction de l'ID de l'UE et du code de l'évaluation
        var credit = await _context.Evaluations
            .Where(e => e.IdUE == ueId && e.CodeEvaluation == codeEval)
            .Select(e => e.Credit)
            .FirstOrDefaultAsync();

        if (credit == 0)
        {
            return NotFound("Cette évaluation n'existe pas !");
        }

        return Ok(credit);
    }



}