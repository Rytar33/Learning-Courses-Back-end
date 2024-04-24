namespace Application.Dtos.Ratings;

public class SendRatingRequest
{
    public Guid IdUser { get; init; }
    
    public Guid IdCourse { get; init; }
    
    public int QuantityScore { get; init; }
    
    public string? Comment { get; init; }
}