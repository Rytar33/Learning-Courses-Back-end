namespace Application.Dtos.Courses;

public class ChangeStatusCourseRequest
{
    public Guid IdCourse { get; init; }
    
    public bool IsActive { get; init; }
}