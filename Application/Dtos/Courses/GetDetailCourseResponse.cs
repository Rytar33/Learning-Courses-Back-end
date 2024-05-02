using Application.Dtos.Ratings;

namespace Application.Dtos.Courses;

public class GetDetailCourseResponse
{
    public Guid IdCourse { get; init; }
    
    public string? PathImage { get; init; }
    
    public string Name { get; init; }
    
    public string Description { get; init; }
    
    public float AverageRating { get; init; }
    
    public bool IsActive { get; init; }
    
    public GetPageRatingResponse PageRating { get; init; }
}