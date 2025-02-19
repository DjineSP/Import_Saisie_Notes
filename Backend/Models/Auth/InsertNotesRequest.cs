namespace Backend.Models.Auth;

public class InsertNotesRequest
{
    public string? Matricule { get; set; }
    public string? CodeClasse { get; set; }
    public string? CodeUE { get; set; }
    public string? Semestre { get; set; }
    public string? Annee { get; set; }
    public string? CodeEvaluation { get; set; }
    public int Credit { get; set; }
    public string? TypeNote { get; set; } // "CC" ou "TP"
    public decimal Note { get; set; }
    public string? Observation { get; set; }
}
