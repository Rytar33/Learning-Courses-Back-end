using Application.Dtos.Ratings;
using Microsoft.AspNetCore.Mvc;

namespace Application.Dtos.Courses;

public class GetDetailCourseRequest
{
    public GetDetailCourseRequest()
    {
        PageRating = new GetPageRatingRequest { IdCourse = IdCourse };
    }
    [FromRoute]
    public Guid IdCourse { get; init; }
    
    public GetPageRatingRequest PageRating { get; init; }
}