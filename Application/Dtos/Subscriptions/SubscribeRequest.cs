namespace Application.Dtos.Subscriptions;

public class SubscribeRequest
{
    public Guid IdUser { get; init; }
    
    public Guid IdCourse { get; init; }
    
    public DateTime DateTimeEndSubscription { get; init; }
}