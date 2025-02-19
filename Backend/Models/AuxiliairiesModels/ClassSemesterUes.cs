using System.Security.Cryptography.Xml;

namespace Backend.Models.AuxiliairiesModels;

public class ClassSemesterUes
{
    public string? Class { get; set; }
    public List<SemesterUes> Semesters { get; set; } = new();
}