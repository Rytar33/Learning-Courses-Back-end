namespace Application.Dtos.Pages;

public class GetPageRequest
{
    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}