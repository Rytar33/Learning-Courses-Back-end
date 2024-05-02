namespace Application.Dtos.Ratings;

public class RatingListItem
{
    public Guid IdRating { get; init; }
    
    public Guid IdCourse { get; init; }
    
    public string CourseName { get; init; }
    
    public Guid IdUser { get; init; }
    
    public string? PathImage { get; init; }
    
    public string UserName { get; init; }
    
    public int QuantityScore { get; init; }
    
    public string? Comment { get; init; }
}