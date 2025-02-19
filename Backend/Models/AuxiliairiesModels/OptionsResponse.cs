namespace Backend.Models.AuxiliairiesModels;

public class OptionsResponse
{
    public List<string> Annee { get; set; } = new(); // List of academic years
    public List<string> Semestres { get; set; } = new(); // List of semesters (S1, S2)
    public List<string> Classes { get; set; } = new(); // List of classes
    public List<string> Evaluations { get; set; } = new();// List of evaluation types (CC, TP, SN)
}
