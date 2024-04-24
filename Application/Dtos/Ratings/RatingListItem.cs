namespace Application.Dtos.Ratings;

public class RatingListItem
{
    public Guid IdUser { get; init; }
    
    public string PathImage { get; init; }
    
    public string UserName { get; init; }
    
    public int QuantityScore { get; init; }
    
    public string? Comment { get; init; }
}