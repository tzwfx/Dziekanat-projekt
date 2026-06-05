using CoreApp.Entities;
using CoreApp.Repositories;

namespace CoreApp.UnitOfWork;

public interface IUniversityUnitOfWork : IAsyncDisposable
{
    IStudentRepository Students { get; }
    ILecturerRepository Lecturers { get; }
    IGradeRepository Grades { get; }
    IGenericRepositoryAsync<Course> Courses { get; }
    IGenericRepositoryAsync<AcademicYear> AcademicYears { get; }
    IGenericRepositoryAsync<DegreeProgram> DegreePrograms { get; }
    

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
