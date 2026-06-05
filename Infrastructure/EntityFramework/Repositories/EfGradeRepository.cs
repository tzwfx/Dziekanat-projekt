using CoreApp.Entities;
using CoreApp.Repositories;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories;

public class EfGradeRepository(UniversityDbContext context)
    : EfGenericRepository<Grade>(context.Grades), IGradeRepository
{
    public async Task<IEnumerable<Grade>> FindByStudentAsync(Guid studentId)
    {
        return await context.Grades
            .Where(g => g.Student.Id == studentId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Grade>> FindByCourseAsync(Guid courseId)
    {
        return await context.Grades
            .Where(g => g.Course.Id == courseId)
            .ToListAsync();
    }
}