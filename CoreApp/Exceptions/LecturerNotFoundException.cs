namespace CoreApp.Exceptions;

public class LecturerNotFoundException : Exception
{
    public LecturerNotFoundException(Guid id) 
        : base($"Lecturer with id={id} not found!") { }
}