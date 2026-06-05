using CoreApp.Entities;
using CoreApp.Repositories;
using CoreApp.UnitOfWork;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.Repositories;

namespace Infrastructure.EntityFramework.UnitOfWork;

public class EfUniversityUnitOfWork(
    UniversityDbContext context,
    IStudentRepository students,
    ILecturerRepository lecturers,
    IGradeRepository grades,
    IGenericRepositoryAsync<Course> courses,
    IGenericRepositoryAsync<AcademicYear> academicYears,
    IGenericRepositoryAsync<DegreeProgram> degreePrograms
) : IUniversityUnitOfWork
{
    public IStudentRepository Students               => students;
    public ILecturerRepository Lecturers             => lecturers;
    public IGradeRepository Grades                   => grades;
    public IGenericRepositoryAsync<Course> Courses           => courses;
    public IGenericRepositoryAsync<AcademicYear> AcademicYears => academicYears;
    public IGenericRepositoryAsync<DegreeProgram> DegreePrograms => degreePrograms;

    public Task<int> SaveChangesAsync()    => context.SaveChangesAsync();
    public Task BeginTransactionAsync()    => context.Database.BeginTransactionAsync();
    public Task CommitTransactionAsync()   => context.Database.CommitTransactionAsync();
    public Task RollbackTransactionAsync() => context.Database.RollbackTransactionAsync();
    public ValueTask DisposeAsync()        => context.DisposeAsync();
}