using CoreApp.Dto;

namespace CoreApp.Entities;

public class Lecturer : Person
{
    public string Title { get; set; } = string.Empty;
    public string Faculty { get; set; } = string.Empty;
    public List<Course> TaughtCourses { get; set; } = new();

    // Lecturer -> LecturerSummaryDto
    public static explicit operator LecturerSummaryDto(Lecturer l) => new()
    {
        Title       = l.Title,
        DisplayName = $"{l.Title} {l.FirstName} {l.LastName}"
    };

    // Lecturer -> LecturerDetailDto
    public static explicit operator LecturerDetailDto(Lecturer l) => new()
    {
        Id        = l.Id,
        FirstName = l.FirstName,
        LastName  = l.LastName,
        Email     = l.Email,
        Title     = l.Title,
        Faculty   = l.Faculty
    };

    // LecturerCreateDto -> Lecturer
    public static explicit operator Lecturer(LecturerCreateDto dto) => new()
    {
        Id         = Guid.NewGuid(),
        FirstName  = dto.FirstName,
        LastName   = dto.LastName,
        Email      = dto.Email,
        NationalId = dto.NationalId,
        Title      = dto.Title,
        Faculty    = dto.Faculty
    };
}