using CoreApp.Entities;
using CoreApp.Enums;
using CoreApp.Repositories;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories;

public class EfStudentRepository(UniversityDbContext context)
    : EfGenericRepository<Student>(context.Students), IStudentRepository
{
    public async Task<IEnumerable<Student>> FindByYearOfStudyAsync(int year)
    {
        return await context.Students
            .Where(s => s.YearOfStudy == year)
            .ToListAsync();
    }

    public async Task<IEnumerable<Student>> FindByDegreeProgramAsync(Guid degreeProgramId)
    {
        return await context.Students
            .Where(s => s.DegreeProgram != null && s.DegreeProgram.Id == degreeProgramId)
            .ToListAsync();
    }

    public async Task ChangeStudentStatusAsync(Guid studentId, StudentStatus status)
    {
        var student = await context.Students.FindAsync(studentId)
                      ?? throw new KeyNotFoundException($"Student {studentId} nie istnieje.");
        student.Status = status;
    }
}