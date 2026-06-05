using CoreApp.Entities;
using CoreApp.Repositories;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories;

public class EfLecturerRepository(UniversityDbContext context)
    : EfGenericRepository<Lecturer>(context.Lecturers), ILecturerRepository
{
    public async Task<IEnumerable<Lecturer>> FindByCourse(Guid courseId)
    {
        return await context.Lecturers
            .Where(l => l.TaughtCourses.Any(c => c.Id == courseId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Lecturer>> FindByTitle(string title)
    {
        return await context.Lecturers
            .Where(l => l.Title == title)
            .ToListAsync();
    }

    public async Task<IEnumerable<Lecturer>> FindByFacultyAsync(string faculty)
    {
        return await context.Lecturers
            .Where(l => l.Faculty == faculty)
            .ToListAsync();
    }
}