using CoreApp.Entities;
using CoreApp.Repositories;

namespace Infrastructure.Memory;

public class MemoryCourseRepository : MemoryGenericRepository<Course>, IGenericRepositoryAsync<Course>
{
    public MemoryCourseRepository() : base()
    {
        var course1Id = Guid.NewGuid();
        _data.Add(course1Id, new Course
        {
            Id             = course1Id,
            Code           = "INF001",
            Name           = "Programowanie obiektowe",
            EctsCredits    = 6,
            CompletionType = CoreApp.Enums.CompletionType.Exam,
            Semester       = CoreApp.Enums.Semester.Winter,
            AcademicYear   = null,
            DegreeProgram  = null
        });

        var course2Id = Guid.NewGuid();
        _data.Add(course2Id, new Course
        {
            Id             = course2Id,
            Code           = "INF002",
            Name           = "Bazy danych",
            EctsCredits    = 4,
            CompletionType = CoreApp.Enums.CompletionType.Exam,
            Semester       = CoreApp.Enums.Semester.Summer,
            AcademicYear   = null,
            DegreeProgram  = null
        });
    }
}
