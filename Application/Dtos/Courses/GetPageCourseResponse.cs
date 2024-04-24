using Application.Dtos.Pages;

namespace Application.Dtos.Courses;

public class GetPageCourseResponse
{
    public IEnumerable<CourseListItem> Items { get; init; }
    
    public GetPageResponse Page { get; init; }
}