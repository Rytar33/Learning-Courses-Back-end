using Application.Dtos.Ratings;
using Application.Dtos.Subscriptions;

namespace Application.Dtos.Users;

public class GetDetailUserResponse
{
    public string? PathImage { get; init; }
    
    public Guid IdUser { get; init; }
    
    public string UserName { get; init; }
    
    public string FullName { get; init; }
    
    public string Email { get; init; }
    
    public UserType UserType { get; init; }
    
    public DateTime DateTimeRegistration { get; init; }
    
    public GetPageRatingResponse PageRating { get; init; }
    
    public GetPageSubscriptionResponse PageSubscription { get; init; }
}