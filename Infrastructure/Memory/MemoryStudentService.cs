using CoreApp.Common;
using CoreApp.Dto;
using CoreApp.Entities;
using CoreApp.Enums;
using CoreApp.Exceptions;
using CoreApp.Services;
using CoreApp.UnitOfWork;

namespace Infrastructure.Memory;

public class MemoryStudentService(IUniversityUnitOfWork unitOfWork) : IStudentService
{
    public async Task<PagedResult<StudentSummaryDto>> FindAllStudentsPaged(int page, int size)
    {
        var paged = await unitOfWork.Students.FindPagedAsync(page, size);
        var items = paged.Items
            .Select(s => (StudentSummaryDto)s)
            .ToList();
        return new PagedResult<StudentSummaryDto>(items, paged.TotalCount, paged.Page, paged.PageSize);
    }

    public async Task<StudentDetailDto?> GetStudentByIdAsync(Guid id)
    {
        var student = await unitOfWork.Students.FindByIdAsync(id);
        return student is null ? null : (StudentDetailDto)student;
    }

    public async Task<StudentDetailDto> GetById(Guid id)
    {
        var student = await unitOfWork.Students.FindByIdAsync(id)
            ?? throw new StudentNotFoundException(id);
        return (StudentDetailDto)student;
    }

    public async Task<StudentDetailDto> CreateStudentAsync(StudentCreateDto dto)
    {
        var student = (Student)dto;
        await unitOfWork.Students.AddAsync(student);
        await unitOfWork.SaveChangesAsync();
        return (StudentDetailDto)student;
    }

    public async Task<StudentDetailDto> UpdateStudentAsync(Guid id, StudentUpdateDto dto)
    {
        var student = await unitOfWork.Students.FindByIdAsync(id)
            ?? throw new StudentNotFoundException(id);

        student.YearOfStudy = dto.YearOfStudy;
        student.Status      = dto.Status;
        await unitOfWork.SaveChangesAsync();
        return (StudentDetailDto)student;
    }

    public async Task ChangeStudentStatusAsync(Guid id, StudentStatus status)
    {
        await unitOfWork.Students.ChangeStudentStatusAsync(id, status);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<Grade> AddGrade(Guid studentId, GradeDto dto)
    {
        var student = await unitOfWork.Students.FindByIdAsync(studentId)
            ?? throw new StudentNotFoundException(studentId);

        var lecturer = await unitOfWork.Lecturers.FindByIdAsync(dto.LecturerId)
            ?? throw new LecturerNotFoundException(dto.LecturerId);

        var course = await unitOfWork.Courses.FindByIdAsync(dto.CourseId)
            ?? throw new CourseNotFoundException(dto.CourseId);

        var academicYear = await unitOfWork.AcademicYears.FindByIdAsync(dto.AcademicYearId)
            ?? throw new Exception($"AcademicYear with id={dto.AcademicYearId} not found!");

        var grade = new Grade
        {
            Id           = Guid.NewGuid(),
            Student      = student,
            Course       = course,
            Instructor   = lecturer,
            AcademicYear = academicYear,
            Date         = dto.Date,
            GradeType    = dto.GradeType,
            GradeValue   = dto.GradeValue
        };

        student.Grades.Add(grade);
        await unitOfWork.Grades.AddAsync(grade);
        await unitOfWork.SaveChangesAsync();
        return grade;
    }

    public async Task<IEnumerable<GradeDto>> GetGradesByStudentId(Guid studentId)
    {
        var student = await unitOfWork.Students.FindByIdAsync(studentId)
            ?? throw new StudentNotFoundException(studentId);

        return student.Grades.Select(g => new GradeDto
        {
            CourseId       = g.Course.Id,
            LecturerId     = g.Instructor?.Id ?? Guid.Empty,
            AcademicYearId = g.AcademicYear?.Id ?? Guid.Empty,
            Date           = g.Date,
            GradeType      = g.GradeType,
            GradeValue     = g.GradeValue
        });
    }
    public async Task<StudentDetailDto> AssignDegreeProgramAsync(Guid studentId, Guid degreeProgramId, Guid academicYearId)
    {
        var student = await unitOfWork.Students.FindByIdAsync(studentId)
                      ?? throw new StudentNotFoundException(studentId);

        var degreeProgram = await unitOfWork.DegreePrograms.FindByIdAsync(degreeProgramId)
                            ?? throw new Exception($"DegreeProgram with id={degreeProgramId} not found!");

        var academicYear = await unitOfWork.AcademicYears.FindByIdAsync(academicYearId)
                           ?? throw new Exception($"AcademicYear with id={academicYearId} not found!");

        student.DegreeProgram  = degreeProgram;
        student.EnrollmentYear = academicYear;
        student.ProgramName    = degreeProgram.Name;

        await unitOfWork.SaveChangesAsync();
        return (StudentDetailDto)student;
    }

    public async Task<GradeDto> UpdateGrade(Guid studentId, Guid gradeId, GradeUpdateDto dto)
    {
        var student = await unitOfWork.Students.FindByIdAsync(studentId)
            ?? throw new StudentNotFoundException(studentId);

        var grade = student.Grades.FirstOrDefault(g => g.Id == gradeId)
            ?? throw new Exception($"Grade with id={gradeId} not found!");

        grade.Date       = dto.Date;
        grade.GradeType  = dto.GradeType;
        grade.GradeValue = dto.GradeValue;

        await unitOfWork.SaveChangesAsync();

        return new GradeDto
        {
            CourseId       = grade.Course.Id,
            LecturerId     = grade.Instructor?.Id ?? Guid.Empty,
            AcademicYearId = grade.AcademicYear?.Id ?? Guid.Empty,
            Date           = grade.Date,
            GradeType      = grade.GradeType,
            GradeValue     = grade.GradeValue
        };
    }
}