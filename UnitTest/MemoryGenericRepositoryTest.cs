using CoreApp.Entities;
using CoreApp.Repositories;
using Infrastructure.Memory;

namespace UnitTest;
using Xunit;

public class MemoryGenericRepositoryTest
{
    private IGenericRepositoryAsync<Student>  _repo = new MemoryGenericRepository<Student>();

    [Fact]
    public async Task AddStudentToRepositoryTestAsync()
    {
        // Arrange
        var expected = new Student()
        {
            FirstName = "Adam"
            // pozostałe właściwości
        };
        // Act
        await _repo.AddAsync(expected);
        // Assert
        var actual = await _repo.FindByIdAsync(expected.Id);
        Assert.Equal(expected, actual);
        Assert.Equal(expected.Id, actual?.Id);
    }
    
    [Fact]
    public async Task FindAllShouldReturnAllStudents()
    {
        var s1 = new Student { Id = Guid.NewGuid(), FirstName = "Adam" };
        var s2 = new Student { Id = Guid.NewGuid(), FirstName = "John" };

        await _repo.AddAsync(s1);
        await _repo.AddAsync(s2);

        var result = await _repo.FindAllAsync();

        Assert.Equal(2, result.Count());
    }
    
    [Fact]
    public async Task RemoveStudentShouldDeleteStudent()
    {
        var student = new Student
        {
            Id = Guid.NewGuid(),
            FirstName = "Adam"
        };

        await _repo.AddAsync(student);

        await _repo.RemoveByIdAsync(student.Id);

        var result = await _repo.FindByIdAsync(student.Id);

        Assert.Null(result);
    }
    
    [Fact]
    public async Task UpdateStudentShouldChangeData()
    {
        var student = new Student
        {
            Id = Guid.NewGuid(),
            FirstName = "Adam"
        };

        await _repo.AddAsync(student);

        student.FirstName = "Updated";

        await _repo.UpdateAsync(student);

        var result = await _repo.FindByIdAsync(student.Id);

        Assert.Equal("Updated", result!.FirstName);
    }
    
    [Fact]
    public async Task FindPagedShouldReturnCorrectPage()
    {
        for (int i = 0; i < 10; i++)
        {
            await _repo.AddAsync(new Student
            {
                Id = Guid.NewGuid(),
                FirstName = $"Student{i}"
            });
        }
    
        var page = await _repo.FindPagedAsync(1, 5);
    
        Assert.Equal(5, page.Items.Count);
        Assert.Equal(10, page.TotalCount);
    }
}