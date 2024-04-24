using Application.Dtos.Pages;

namespace Application.Dtos.Subscriptions;

public class GetPageSubscriptionResponse
{
    public IEnumerable<SubscriptionListItem> Items { get; init; }
    
    public GetPageResponse Page { get; init; }
}