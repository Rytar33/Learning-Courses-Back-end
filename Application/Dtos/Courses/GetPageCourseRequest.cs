using Application.Dtos.Pages;

namespace Application.Dtos.Courses;

public class GetPageCourseRequest
{
    public string? Search { get; init; }
    
    public Guid? IdUserAuthor { get; init; }
    
    public GetPageRequest Page { get; init; } = new GetPageRequest();
}