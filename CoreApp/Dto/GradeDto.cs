using CoreApp.Enums;

namespace CoreApp.Dto;

public record GradeDto
{
    public Guid CourseId { get; init; }
    public Guid LecturerId { get; init; }
    public Guid AcademicYearId { get; init; }
    public DateTime Date { get; init; }
    public GradeType GradeType { get; init; }
    public GradeValue GradeValue { get; init; }
}

public record GradeUpdateDto
{
    public DateTime Date { get; init; }
    public GradeType GradeType { get; init; }
    public GradeValue GradeValue { get; init; }
}