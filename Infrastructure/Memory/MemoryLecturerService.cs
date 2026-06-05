using CoreApp.Common;
using CoreApp.Dto;
using CoreApp.Entities;
using CoreApp.Exceptions;
using CoreApp.Services;
using CoreApp.UnitOfWork;

namespace Infrastructure.Memory;

public class MemoryLecturerService(IUniversityUnitOfWork unitOfWork) : ILecturerService
{
    public async Task<PagedResult<LecturerSummaryDto>> FindAllLecturersPaged(int page, int size)
    {
        var paged = await unitOfWork.Lecturers.FindPagedAsync(page, size);
        var items = paged.Items
            .Select(l => (LecturerSummaryDto)l)
            .ToList();
        return new PagedResult<LecturerSummaryDto>(items, paged.TotalCount, paged.Page, paged.PageSize);
    }

    public async Task<LecturerDetailDto?> GetLecturerByIdAsync(Guid id)
    {
        var lecturer = await unitOfWork.Lecturers.FindByIdAsync(id);
        return lecturer is null ? null : (LecturerDetailDto)lecturer;
    }

    public async Task<LecturerDetailDto> CreateLecturerAsync(LecturerCreateDto dto)
    {
        var lecturer = (Lecturer)dto;
        await unitOfWork.Lecturers.AddAsync(lecturer);
        await unitOfWork.SaveChangesAsync();
        return (LecturerDetailDto)lecturer;
    }

    public async Task<LecturerDetailDto> UpdateLecturerAsync(Guid id, LecturerUpdateDto dto)
    {
        var lecturer = await unitOfWork.Lecturers.FindByIdAsync(id)
                       ?? throw new LecturerNotFoundException(id);

        lecturer.FirstName = dto.FirstName;
        lecturer.LastName  = dto.LastName;
        lecturer.Email     = dto.Email;
        lecturer.Title     = dto.Title;
        lecturer.Faculty   = dto.Faculty;

        await unitOfWork.SaveChangesAsync();
        return (LecturerDetailDto)lecturer;
    }

    public async Task<IEnumerable<Course>> GetCoursesByLecturerIdAsync(Guid lecturerId)
    {
        var lecturer = await unitOfWork.Lecturers.FindByIdAsync(lecturerId)
                       ?? throw new LecturerNotFoundException(lecturerId);

        return lecturer.TaughtCourses;
    }
}