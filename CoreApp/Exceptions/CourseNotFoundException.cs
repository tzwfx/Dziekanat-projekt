namespace CoreApp.Exceptions;

public class CourseNotFoundException : Exception
{
    public CourseNotFoundException(Guid id) 
        : base($"Course with id={id} not found!") { }
}