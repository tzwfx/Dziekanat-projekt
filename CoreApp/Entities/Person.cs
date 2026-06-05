namespace CoreApp.Entities;

public abstract class Person : BaseEntity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string NationalId { get; set; }

    public string Email { get; set; }
}