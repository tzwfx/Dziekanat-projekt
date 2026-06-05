using CoreApp.Entities;
using CoreApp.Enums;

namespace CoreApp.Repositories;

public interface IStudentRepository : IGenericRepositoryAsync<Student>
{
    Task<IEnumerable<Student>> FindByYearOfStudyAsync(int year);
    Task<IEnumerable<Student>> FindByDegreeProgramAsync(Guid degreeProgramId);
    Task ChangeStudentStatusAsync(Guid studentId, StudentStatus status);
}