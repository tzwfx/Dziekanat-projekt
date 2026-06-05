using CoreApp.Entities;
using CoreApp.Repositories;

namespace Infrastructure.Memory;

public class MemoryLecturerRepository : MemoryGenericRepository<Lecturer>, ILecturerRepository
{
    public MemoryLecturerRepository() : base()
    {
        var id1 = Guid.NewGuid();
        _data.Add(id1, new Lecturer
        {
            Id       = id1,
            FirstName = "Jan",
            LastName  = "Wiśniewski",
            Email     = "jan.wisniewski@uczelnia.pl",
            NationalId = "11111111111",
            Title    = "dr",
            Faculty  = "Wydział Informatyki",
            TaughtCourses = new()
        });
    }

    public Task<IEnumerable<Lecturer>> FindByCourse(Guid courseId)
    {
        var result = _data.Values
            .Where(l => l.TaughtCourses.Any(c => c.Id == courseId));
        return Task.FromResult(result);
    }

    public Task<IEnumerable<Lecturer>> FindByTitle(string title)
    {
        var result = _data.Values.Where(l => l.Title == title);
        return Task.FromResult(result);
    }

    public Task<IEnumerable<Lecturer>> FindByFacultyAsync(string faculty)
    {
        var result = _data.Values.Where(l => l.Faculty == faculty);
        return Task.FromResult(result);
    }
}