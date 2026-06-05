using CoreApp.Common;
using CoreApp.Dto;
using CoreApp.Entities;
using CoreApp.Enums;

namespace CoreApp.Services;

public interface IStudentService
{
    Task<PagedResult<StudentSummaryDto>> FindAllStudentsPaged(int page, int size);
    Task<StudentDetailDto?> GetStudentByIdAsync(Guid id);
    Task<StudentDetailDto> CreateStudentAsync(StudentCreateDto dto);
    Task<StudentDetailDto> UpdateStudentAsync(Guid id, StudentUpdateDto dto);
    Task ChangeStudentStatusAsync(Guid id, StudentStatus status);
    Task<Grade> AddGrade(Guid studentId, GradeDto gradeDto);
    Task<StudentDetailDto> GetById(Guid id);
    Task<IEnumerable<GradeDto>> GetGradesByStudentId(Guid studentId);
    Task<GradeDto> UpdateGrade(Guid studentId, Guid gradeId, GradeUpdateDto dto);
    Task<StudentDetailDto> AssignDegreeProgramAsync(Guid studentId, Guid degreeProgramId, Guid academicYearId);
}
