using Application.Dtos.Pages;

namespace Application.Dtos.Ratings;

public class GetPageRatingRequest
{
    public Guid? IdCourse { get; init; }

    public Guid? IdUser { get; init; }
    
    public GetPageRequest Page { get; init; } = new GetPageRequest();
}