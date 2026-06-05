using CoreApp.Dto;
using CoreApp.Enums;

namespace CoreApp.Entities;

public class Student : Person
{
    public Guid StudentId { get; set; }
    public int YearOfStudy { get; set; }
    public DegreeProgram? DegreeProgram { get; set; }
    public AcademicYear? EnrollmentYear { get; set; }
    public StudentStatus Status { get; set; }
    public string ProgramName { get; set; } = string.Empty;
    public List<Grade> Grades { get; set; } = new();

    // Student -> StudentSummaryDto
    public static explicit operator StudentSummaryDto(Student s) => new()
    {
        FirstName   = s.FirstName,
        LastName    = s.LastName,
        Email       = s.Email,
        StudentId   = s.StudentId.ToString(),
        ProgramName = s.DegreeProgram?.Name ?? s.ProgramName,
        YearOfStudy = s.YearOfStudy,
        Status      = s.Status
    };

    // Student -> StudentDetailDto
    public static explicit operator StudentDetailDto(Student s) => new()
    {
        FirstName            = s.FirstName,
        LastName             = s.LastName,
        Email                = s.Email,
        StudentId            = s.StudentId.ToString(),
        ProgramCode          = s.DegreeProgram?.Code ?? string.Empty,
        ProgramName          = s.DegreeProgram?.Name ?? s.ProgramName,
        EnrollmentYear       = s.EnrollmentYear?.Year.ToString() ?? string.Empty,
        YearOfStudy          = s.YearOfStudy,
        Status               = s.Status,
        GradePointAverage    = 0,
        TotalEctsEarned      = 0,
        IsEligibleForDiploma = false
    };

    // StudentCreateDto -> Student
    public static explicit operator Student(StudentCreateDto dto) => new()
    {
        Id          = Guid.NewGuid(),
        StudentId   = Guid.TryParse(dto.StudentId, out var id) ? id : Guid.NewGuid(),
        FirstName   = dto.FirstName,
        LastName    = dto.LastName,
        Email       = dto.Email,
        NationalId  = dto.NationalId,
        YearOfStudy = dto.YearOfStudy,
        Status      = StudentStatus.Active,
        ProgramName = string.Empty
    };
}