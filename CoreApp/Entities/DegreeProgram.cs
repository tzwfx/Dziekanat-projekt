using CoreApp.Enums;

namespace CoreApp.Entities;

public class DegreeProgram: BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Faculty { get; set; }
    public DegreeType DegreeType { get; set; }
    public int DurationYears { get; set; }
    public int MinEctsForDiploma { get; set; }
    public List<Course> Courses { get; set; } = new();
}