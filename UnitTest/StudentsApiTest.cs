using System.Net;
using System.Net.Http.Json;
using CoreApp.Dto;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.EntityFramework.Context;
using CoreApp.Entities;
using CoreApp.Enums;
using Xunit;

namespace UnitTest;

public class StudentsApiTest : IClassFixture<UniversityAppTestFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly UniversityAppTestFactory<Program> _app;

    public StudentsApiTest(UniversityAppTestFactory<Program> app)
    {
        _app    = app;
        _client = app.CreateClient();

        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UniversityDbContext>();

        if (!context.Students.Any())
        {
            context.Students.AddRange(
                new Student
                {
                    Id          = Guid.NewGuid(),
                    StudentId   = Guid.NewGuid(),
                    FirstName   = "Adam",
                    LastName    = "Nowak",
                    Email       = "adam@test.pl",
                    NationalId  = "12345678901",
                    YearOfStudy = 1,
                    Status      = StudentStatus.Active,
                    ProgramName = "Informatyka",
                    Grades      = new List<Grade>()
                },
                new Student
                {
                    Id          = Guid.NewGuid(),
                    StudentId   = Guid.NewGuid(),
                    FirstName   = "Anna",
                    LastName    = "Kowalska",
                    Email       = "anna@test.pl",
                    NationalId  = "98765432100",
                    YearOfStudy = 2,
                    Status      = StudentStatus.Active,
                    ProgramName = "Matematyka",
                    Grades      = new List<Grade>()
                }
            );
            context.SaveChanges();
        }
    }

    [Fact]
    public async Task GetAllStudents_ShouldReturnOkStatus()
    {
        var result = await _client.GetAsync("/api/students");
        Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
    }

    [Fact]
    public async Task GetAllStudents_ShouldReturnJsonContentType()
    {
        var result = await _client.GetAsync("/api/students");
        Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
    }

    [Fact]
    public async Task CreateStudent_ShouldReturnCreated()
    {
        var dto = new StudentCreateDto
        {
            FirstName        = "Piotr",
            LastName         = "Wiśniewski",
            Email            = "piotr@test.pl",
            NationalId       = "11111111111",
            StudentId        = "",
            YearOfStudy      = 1,
            ProgramCode      = "INF",
            EnrollmentYearFrom = 2024
        };

        var result = await _client.PostAsJsonAsync("/api/students", dto);
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    }
}