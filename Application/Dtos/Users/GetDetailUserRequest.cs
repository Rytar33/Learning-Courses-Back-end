using Application.Dtos.Ratings;
using Application.Dtos.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace Application.Dtos.Users;

public class GetDetailUserRequest
{
    public GetDetailUserRequest()
    {
        PageRating = new GetPageRatingRequest { IdUser = IdUser };
        PageSubscription = new GetPageSubscriptionRequest { IdUser = IdUser };
    }
    
    [FromRoute]
    public Guid IdUser { get; init; }
    
    public GetPageRatingRequest PageRating { get; init; }
    
    public GetPageSubscriptionRequest PageSubscription { get; init; }
}