using CoreApp.Module;
using Infrastructure;
using Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddStudentsModule(builder.Configuration);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddOpenApi();

// Moduł EF
builder.Services.AddUniversityEfModule(builder.Configuration);

// JWT
builder.Services.AddSingleton<JwtSettings>();
builder.Services.AddJwt(new JwtSettings(builder.Configuration));

// Obsługa wyjątków
builder.Services.AddExceptionHandler<WebApi.ProblemDetailsExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    using var scope = app.Services.CreateScope();
    var seeders = scope.ServiceProvider
        .GetServices<IDataSeeder>()
        .OrderBy(s => s.Order);
    foreach (var seeder in seeders)
        await seeder.SeedAsync();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();
app.MapControllers();

app.MapGet("/api/debug", async (CoreApp.UnitOfWork.IUniversityUnitOfWork uow) =>
{
    var courses = await uow.Courses.FindAllAsync();
    var lecturers = await uow.Lecturers.FindAllAsync();
    var years = await uow.AcademicYears.FindAllAsync();
    var degreePrograms = await uow.DegreePrograms.FindAllAsync();
    return Results.Ok(new { courses, lecturers, years, degreePrograms });
});

app.Run();

public partial class Program { }