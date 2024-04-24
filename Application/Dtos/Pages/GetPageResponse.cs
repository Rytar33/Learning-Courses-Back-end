namespace Application.Dtos.Pages;

public class GetPageResponse
{
    public int Page { get; init; }
    
    public int PageSize { get; init; }
    
    public int Count { get; init; }
}