using CoreApp.Entities;
using CoreApp.Repositories;

namespace Infrastructure.Memory;

public class MemoryGradeRepository : MemoryGenericRepository<Grade>, IGradeRepository
{
    public Task<IEnumerable<Grade>> FindByStudentAsync(Guid studentId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Grade>> FindByCourseAsync(Guid courseId)
    {
        throw new NotImplementedException();
    }
}