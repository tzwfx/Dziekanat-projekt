using CoreApp.Dto;
using CoreApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/lecturers")]
public class LecturersController(ILecturerService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllLecturers(
        [FromQuery] int page = 1,
        [FromQuery] int size = 10)
        => Ok(await service.FindAllLecturersPaged(page, size));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetLecturer(Guid id)
    {
        var result = await service.GetLecturerByIdAsync(id);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLecturer([FromBody] LecturerCreateDto dto)
    {
        var created = await service.CreateLecturerAsync(dto);
        return CreatedAtAction(nameof(GetLecturer), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateLecturer(Guid id, [FromBody] LecturerUpdateDto dto)
        => Ok(await service.UpdateLecturerAsync(id, dto));

    [HttpGet("{id:guid}/courses")]
    public async Task<IActionResult> GetCourses(Guid id)
    {
        var courses = await service.GetCoursesByLecturerIdAsync(id);
        return Ok(courses);
    }
}