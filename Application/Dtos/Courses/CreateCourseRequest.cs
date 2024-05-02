namespace Application.Dtos.Courses;

public class CreateCourseRequest
{
    public IFormFile Image { get; init; }
    
    public string Name { get; init; }

    public string Description { get; init; }

    public Guid? IdUser { get; init; }
}