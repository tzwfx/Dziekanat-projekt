namespace CoreApp.Entities;

public class AcademicYear : BaseEntity
{
    public int YearFrom { get; set; }
    public int YearTo { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
}