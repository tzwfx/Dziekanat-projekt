using CoreApp.Entities;
using CoreApp.Repositories;

namespace Infrastructure.Memory;

public class MemoryAcademicYearRepository : MemoryGenericRepository<AcademicYear>, IGenericRepositoryAsync<AcademicYear>
{
    public MemoryAcademicYearRepository() : base()
    {
        var year1Id = Guid.NewGuid();
        _data.Add(year1Id, new AcademicYear
        {
            Id       = year1Id,
            YearFrom = 2024,
            YearTo   = 2025,
            IsActive = false,
            Name     = "2024/2025",
            Year     = "2024/2025"
        });

        var year2Id = Guid.NewGuid();
        _data.Add(year2Id, new AcademicYear
        {
            Id       = year2Id,
            YearFrom = 2025,
            YearTo   = 2026,
            IsActive = true,
            Name     = "2025/2026",
            Year     = "2025/2026"
        });
    }
}