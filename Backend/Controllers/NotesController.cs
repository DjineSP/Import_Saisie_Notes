using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using Backend.Models.ImportationModels;

[Route("api/[controller]")]
[ApiController]
[IgnoreAntiforgeryToken]
public class NotesController : ControllerBase
{
    private readonly ImportationDbContext _context;

    public NotesController(ImportationDbContext context)
    {
        _context = context;
    }

    [HttpPost("insertnotes")]
    public async Task<IActionResult> PostNote([FromBody] InsertNotesRequest noteRequest)
    {
        if (noteRequest == null || string.IsNullOrEmpty(noteRequest.CodeUE) ||
            string.IsNullOrEmpty(noteRequest.Evaluation) || noteRequest.ListeEtudiants == null ||
            !noteRequest.ListeEtudiants.Any())
        {
            return BadRequest("Données invalides.");
        }

        // Vérifier si le CodeEvaluation est valide
        var codesValides = new List<string> { "CC", "TP", "SN" };
        if (!codesValides.Contains(noteRequest.Evaluation))
            return BadRequest("Type d'évaluation invalide.");

        // Récupérer l'IdUE à partir du CodeUE
        var ue = await _context.UEs
            .Where(ue => ue.CodeUE == noteRequest.CodeUE)
            .Select(ue => ue.IdUE)
            .FirstOrDefaultAsync();

        if (ue == 0) return BadRequest("Unité d'enseignement non trouvée.");

        // Vérifier si l'évaluation existe déjà
        var evaluation = await _context.Evaluations
            .Where(e => e.IdUE == ue && e.CodeEvaluation == noteRequest.Evaluation)
            .FirstOrDefaultAsync();

        if (evaluation == null)
        {
            evaluation = new Evaluation
            {
                IdUE = ue,
                CodeEvaluation = noteRequest.Evaluation,
                Credit = noteRequest.Echelle
            };
            _context.Evaluations.Add(evaluation);
            await _context.SaveChangesAsync();
        }

        // Vérifier l'existence des étudiants/anonymats
        if (noteRequest.Evaluation == "CC" || noteRequest.Evaluation == "TP")
        {
            var matriculesDemandes = noteRequest.ListeEtudiants.Select(e => e.Matricule).ToList();
            var matriculesExistants = await _context.Etudiants
                .Where(et => matriculesDemandes.Contains(et.Matricule))
                .Select(et => et.Matricule)
                .ToListAsync();

            var matriculesInvalides = matriculesDemandes.Except(matriculesExistants).ToList();
            if (matriculesInvalides.Any())
            {
                return BadRequest($"Matricules non trouvés : {string.Join(", ", matriculesInvalides)}");
            }
        }
        else if (noteRequest.Evaluation == "SN")
        {
            var anonymatsDemandes = noteRequest.ListeEtudiants.Select(e => e.NumAnonymat).ToList();
            var anonymatsExistants = await _context.AnonymatsNoms
                .Where(an => anonymatsDemandes.Contains(an.NumAnonymat))
                .Select(an => an.NumAnonymat)
                .ToListAsync();

            var anonymatsInvalides = anonymatsDemandes.Except(anonymatsExistants).ToList();
            if (anonymatsInvalides.Any())
            {
                return BadRequest($"Numéros d'anonymat non trouvés : {string.Join(", ", anonymatsInvalides)}");
            }
        }

        // Insérer ou mettre à jour les notes
        if (noteRequest.Evaluation == "CC")
        {
            foreach (var etudiant in noteRequest.ListeEtudiants)
            {
                var existingNote = await _context.NotesCC
                    .FirstOrDefaultAsync(n => n.IdEvaluation == evaluation.IdEvaluation && n.Matricule == etudiant.Matricule);

                if (existingNote != null)
                {
                    existingNote.Note = etudiant.Note;
                    existingNote.Observation = etudiant.Observation ?? string.Empty;
                }
                else
                {
                    _context.NotesCC.Add(new NoteCC
                    {
                        IdEvaluation = evaluation.IdEvaluation,
                        Matricule = etudiant.Matricule ?? string.Empty,
                        Note = etudiant.Note,
                        Observation = etudiant.Observation ?? string.Empty
                    });
                }
            }
        }
        else if (noteRequest.Evaluation == "TP")
        {
            foreach (var etudiant in noteRequest.ListeEtudiants)
            {
                var existingNote = await _context.NotesTP
                    .FirstOrDefaultAsync(n => n.IdEvaluation == evaluation.IdEvaluation && n.Matricule == etudiant.Matricule);

                if (existingNote != null)
                {
                    existingNote.Note = etudiant.Note;
                    existingNote.Observation = etudiant.Observation ?? string.Empty;
                }
                else
                {
                    _context.NotesTP.Add(new NoteTP
                    {
                        IdEvaluation = evaluation.IdEvaluation,
                        Matricule = etudiant.Matricule ?? string.Empty,
                        Note = etudiant.Note,
                        Observation = etudiant.Observation ?? string.Empty
                    });
                }
            }
        }
        else if (noteRequest.Evaluation == "SN")
        {
            foreach (var etudiant in noteRequest.ListeEtudiants)
            {
                var existingNote = await _context.AnonymatsNotes
                    .FirstOrDefaultAsync(n => n.IdEvaluation == evaluation.IdEvaluation && n.NumAnonymat == etudiant.NumAnonymat);

                if (existingNote != null)
                {
                    existingNote.Note = etudiant.Note;
                    existingNote.Observation = etudiant.Observation ?? string.Empty;
                }
                else
                {
                    _context.AnonymatsNotes.Add(new AnonymatNote
                    {
                        IdEvaluation = evaluation.IdEvaluation,
                        NumAnonymat = etudiant.NumAnonymat ?? string.Empty,
                        Note = etudiant.Note,
                        Observation = etudiant.Observation ?? string.Empty
                    });
                }
            }
        }

        await _context.SaveChangesAsync();

        return Ok(new { message = "Notes insérées/mises à jour avec succès." });
    }


    [HttpGet("getnotes")]
    public async Task<IActionResult> GetNotes([FromQuery] Parameters request)
    {
        if (string.IsNullOrEmpty(request.codeUE) || string.IsNullOrEmpty(request.evaluation) || string.IsNullOrEmpty(request.annee) || string.IsNullOrEmpty(request.semestre) || string.IsNullOrEmpty(request.codeClasse))
        {
            return BadRequest("Tous les paramètres doivent être fournis.");
        }

        // Récupérer l'IdUE à partir du CodeUE
        var ue = await _context.UEs
            .Where(ue => ue.CodeUE == request.codeUE)
            .Select(ue => ue.IdUE)
            .FirstOrDefaultAsync();

        if (ue == 0) return BadRequest("Unité d'enseignement non trouvée.");

        // Vérifier si l'évaluation existe
        var evaluationEntity = await _context.Evaluations
            .Where(e => e.IdUE == ue && e.CodeEvaluation == request.evaluation)
            .FirstOrDefaultAsync();

        if (evaluationEntity == null)
        {
            return BadRequest("Évaluation non trouvée.");
        }

        // Récupérer les notes en fonction du type d'évaluation
        List<EtudiantNote> notes = new List<EtudiantNote>();

        if (request.evaluation == "CC")
        {
            notes = await _context.NotesCC
                .Where(n => n.IdEvaluation == evaluationEntity.IdEvaluation)
                .Select(n => new EtudiantNote
                {
                    Matricule = n.Matricule,
                    NomPrenom = _context.Etudiants
                        .Where(e => e.Matricule == n.Matricule)
                        .Select(e => e.Nom + " " + e.Prenoms)
                        .FirstOrDefault(), // Concaténer nom et prenoms
                    Note = n.Note,
                    Observation = n.Observation
                })
                .ToListAsync();
        }
        else if (request.evaluation == "TP")
        {
            notes = await _context.NotesTP
                .Where(n => n.IdEvaluation == evaluationEntity.IdEvaluation)
                .Select(n => new EtudiantNote
                {
                    Matricule = n.Matricule,
                    NomPrenom = _context.Etudiants
                        .Where(e => e.Matricule == n.Matricule)
                        .Select(e => e.Nom + " " + e.Prenoms)
                        .FirstOrDefault(), // Concaténer nom et prenoms
                    Note = n.Note,
                    Observation = n.Observation
                })
                .ToListAsync();
        } 
        else {
            notes = await _context.AnonymatsNotes
                .Where(n => n.IdEvaluation == evaluationEntity.IdEvaluation)
                .Select(n => new EtudiantNote
                {
                    NumAnonymat = n.NumAnonymat,
                    Note = n.Note,
                    Observation = n.Observation
                })
                .ToListAsync();

        }

        return Ok(new { Notes = notes });
    }
}

    public class EtudiantNote
    {
        public string? Matricule { get; set; }
        public string? NumAnonymat { get; set; }
        public string? NomPrenom { get; set; } // This will hold the concatenated name
        public decimal Note { get; set; }
        public string? Observation { get; set; }
    }
    public class Parameters
{
    public string? codeUE { get; set; }

    public string? evaluation { get; set; }
    public string? annee { get; set; }
    public string? semestre { get; set; }

    public string? codeClasse { get; set; }
}

// Modèle pour la requête d'insertion
public class InsertNotesRequest
{
    public string? Annee { get; set; }
    public string? Semestre { get; set; }
    public string? CodeClasse { get; set; }
    public string? CodeUE { get; set; }
    public string? Evaluation { get; set; }
    public int Echelle { get; set; }
    public List<EtudiantNoteRequest> ListeEtudiants { get; set; } = new();
}

// Modèle pour un étudiant dans la liste
public class EtudiantNoteRequest
{
    public string? Matricule { get; set; } // Seulement pour CC et TP
    public string? NumAnonymat { get; set; } // Seulement pour SN
    public decimal Note { get; set; }
    public string? Observation { get; set; }
}
