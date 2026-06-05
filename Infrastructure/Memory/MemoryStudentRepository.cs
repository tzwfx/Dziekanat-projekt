using CoreApp.Entities;
using CoreApp.Enums;
using CoreApp.Repositories;

namespace Infrastructure.Memory;

public class MemoryStudentRepository : MemoryGenericRepository<Student>, IStudentRepository
{
    public MemoryStudentRepository() : base()
    {
        var student1Id = Guid.NewGuid();
        _data.Add(student1Id, new Student
        {
            Id             = student1Id,
            StudentId      = student1Id,
            FirstName      = "Adam",
            LastName       = "Nowak",
            Email          = "adam.nowak@uczelnia.pl",
            NationalId     = "12345678901",
            YearOfStudy    = 2,
            Status         = StudentStatus.Active,
            ProgramName    = "Informatyka",
            DegreeProgram  = null,
            EnrollmentYear = null
        });

        var student2Id = Guid.NewGuid();
        _data.Add(student2Id, new Student
        {
            Id             = student2Id,
            StudentId      = student2Id,
            FirstName      = "Anna",
            LastName       = "Kowalska",
            Email          = "anna.kowalska@uczelnia.pl",
            NationalId     = "98765432100",
            YearOfStudy    = 1,
            Status         = StudentStatus.Active,
            ProgramName    = "Matematyka",
            DegreeProgram  = null,
            EnrollmentYear = null
        });
    }

    public Task<IEnumerable<Student>> FindByYearOfStudyAsync(int year)
    {
        var result = _data.Values.Where(s => s.YearOfStudy == year);
        return Task.FromResult(result);
    }

    public Task<IEnumerable<Student>> FindByDegreeProgramAsync(Guid degreeProgramId)
    {
        var result = _data.Values
            .Where(s => s.DegreeProgram != null && s.DegreeProgram.Id == degreeProgramId);
        return Task.FromResult(result);
    }

    public Task ChangeStudentStatusAsync(Guid studentId, StudentStatus status)
    {
        var student = _data.Values.FirstOrDefault(s => s.StudentId == studentId)
            ?? throw new KeyNotFoundException($"Student o id {studentId} nie istnieje.");

        student.Status = status;
        return Task.CompletedTask;
    }
}