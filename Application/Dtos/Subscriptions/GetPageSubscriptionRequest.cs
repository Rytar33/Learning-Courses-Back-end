using Application.Dtos.Pages;

namespace Application.Dtos.Subscriptions;

public class GetPageSubscriptionRequest
{
    public Guid IdCourse { get; init; }
    
    public Guid IdUser { get; init; }

    public GetPageRequest Page { get; init; } = new GetPageRequest();
}