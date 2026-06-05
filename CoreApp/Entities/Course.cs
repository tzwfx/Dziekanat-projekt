using CoreApp.Enums;

namespace CoreApp.Entities;

public class Course : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public int EctsCredits { get; set; }
    public CompletionType CompletionType { get; set; }
    public Semester Semester { get; set; }
    public AcademicYear? AcademicYear { get; set; }
    public DegreeProgram? DegreeProgram { get; set; }
    public List<Student> Enrollments { get; set; } = new();
}