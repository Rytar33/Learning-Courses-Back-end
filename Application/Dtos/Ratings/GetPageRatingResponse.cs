using Application.Dtos.Pages;

namespace Application.Dtos.Ratings;

public class GetPageRatingResponse
{
    public IEnumerable<RatingListItem> Items { get; init; }
    
    public GetPageResponse Page { get; init; }
}