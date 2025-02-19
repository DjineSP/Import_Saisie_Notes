namespace Backend.Models.AuxiliairiesModels;

public class SemesterUes
{
    public string? Semester { get; set; }
    public List<string> UEs { get; set; } = new();
}
