using CoreApp.Common;
using CoreApp.Dto;
using CoreApp.Entities;

namespace CoreApp.Services;

public interface ILecturerService
{
    Task<PagedResult<LecturerSummaryDto>> FindAllLecturersPaged(int page, int size);
    Task<LecturerDetailDto?> GetLecturerByIdAsync(Guid id);
    Task<LecturerDetailDto> CreateLecturerAsync(LecturerCreateDto dto);
    Task<LecturerDetailDto> UpdateLecturerAsync(Guid id, LecturerUpdateDto dto);
    Task<IEnumerable<Course>> GetCoursesByLecturerIdAsync(Guid lecturerId);
}