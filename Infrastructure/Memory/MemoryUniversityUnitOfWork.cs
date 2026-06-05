using CoreApp.Entities;
using CoreApp.Repositories;
using CoreApp.UnitOfWork;

namespace Infrastructure.Memory;

public class MemoryUniversityUnitOfWork(
    IStudentRepository students,
    ILecturerRepository lecturers,
    IGradeRepository grades,
    IGenericRepositoryAsync<Course> courses,
    IGenericRepositoryAsync<AcademicYear> academicYears,
    IGenericRepositoryAsync<DegreeProgram> degreePrograms
) : IUniversityUnitOfWork
{
    public IGenericRepositoryAsync<DegreeProgram> DegreePrograms => degreePrograms;
    public IStudentRepository Students               => students;
    public ILecturerRepository Lecturers             => lecturers;
    public IGradeRepository Grades                   => grades;
    public IGenericRepositoryAsync<Course> Courses           => courses;
    public IGenericRepositoryAsync<AcademicYear> AcademicYears => academicYears;

    public Task<int> SaveChangesAsync()    => Task.FromResult(0);
    public Task BeginTransactionAsync()    => Task.CompletedTask;
    public Task CommitTransactionAsync()   => Task.CompletedTask;
    public Task RollbackTransactionAsync() => Task.CompletedTask;
    public ValueTask DisposeAsync()        => ValueTask.CompletedTask;
}