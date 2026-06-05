using CoreApp.Authorization;
using CoreApp.Dto;
using CoreApp.Enums;
using CoreApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/students")]
public class StudentsController(IStudentService service) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = nameof(AppPolicies.AdminOnly))]
    public async Task<IActionResult> GetAllStudents(
        [FromQuery] int page = 1,
        [FromQuery] int size = 10)
        => Ok(await service.FindAllStudentsPaged(page, size));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetStudent(Guid id)
    {
        var result = await service.GetStudentByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent([FromBody] StudentCreateDto dto)
    {
        var created = await service.CreateStudentAsync(dto);
        return CreatedAtAction(nameof(GetStudent), new { id = created.StudentId }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] StudentUpdateDto dto)
        => Ok(await service.UpdateStudentAsync(id, dto));

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromQuery] StudentStatus status)
    {
        await service.ChangeStudentStatusAsync(id, status);
        return NoContent();
    }

    [HttpPost("{studentId:guid}/grades")]
    [ProducesResponseType(typeof(GradeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddGrade(
        [FromRoute] Guid studentId,
        [FromBody] GradeDto dto)
    {
        var grade = await service.AddGrade(studentId, dto);
        return CreatedAtAction(nameof(GetGrades), new { studentId }, grade);
    }

    [HttpPatch("{studentId:guid}/assign")]
    public async Task<IActionResult> AssignDegreeProgram(
        Guid studentId,
        [FromQuery] Guid degreeProgramId,
        [FromQuery] Guid academicYearId)
    {
        var result = await service.AssignDegreeProgramAsync(studentId, degreeProgramId, academicYearId);
        return Ok(result);
    }

    [HttpGet("{studentId:guid}/grades")]
    [ProducesResponseType(typeof(IEnumerable<GradeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGrades([FromRoute] Guid studentId)
    {
        var grades = await service.GetGradesByStudentId(studentId);
        return Ok(grades);
    }
}