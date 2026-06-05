using CoreApp.Entities;
using CoreApp.Enums;
using CoreApp.Repositories;

namespace Infrastructure.Memory;

public class MemoryDegreeProgramRepository : MemoryGenericRepository<DegreeProgram>, IGenericRepositoryAsync<DegreeProgram>
{
    public MemoryDegreeProgramRepository() : base()
    {
        var id1 = Guid.NewGuid();
        _data.Add(id1, new DegreeProgram
        {
            Id             = id1,
            Code           = "INF",
            Name           = "Informatyka",
            Faculty        = "Wydział Informatyki",
            DegreeType     = DegreeType.Bachelor,
            DurationYears  = 3,
            MinEctsForDiploma = 180
        });

        var id2 = Guid.NewGuid();
        _data.Add(id2, new DegreeProgram
        {
            Id             = id2,
            Code           = "MAT",
            Name           = "Matematyka",
            Faculty        = "Wydział Matematyki",
            DegreeType     = DegreeType.Bachelor,
            DurationYears  = 3,
            MinEctsForDiploma = 180
        });
    }
}