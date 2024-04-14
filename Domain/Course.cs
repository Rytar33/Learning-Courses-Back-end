namespace Domain;

public class Course : BaseEntity
{
    public Course(
        string name,
        string description,
        Guid? idAuthor) 
    {
        Name = name;
        Description = description;
        IdAuthor = idAuthor;
    }

    public string Name { get; init; }

    public string Description { get; init; }

    public Guid? IdAuthor { get; init; }

    protected User? User { get; private init; }
}
