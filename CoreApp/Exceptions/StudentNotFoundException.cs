namespace CoreApp.Exceptions;

public class StudentNotFoundException : Exception
{
    public StudentNotFoundException(Guid id) 
        : base($"Student with id={id} not found!") { }
}