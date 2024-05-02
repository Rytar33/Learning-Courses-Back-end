namespace Application.Dtos.Courses;

public class CourseListItem
{
    public Guid IdCourse { get; init; }
    
    public string Name { get; init; }
    
    public string Description { get; init; }
    
    public string? PathImage { get; init; }
    
    public Guid? IdUserAuthor { get; init; }
    
    public string? UserName { get; init; }
    
    public float AverageScore { get; init; }
    
    public bool IsActive { get; init; }
}