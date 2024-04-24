namespace Application.Dtos.Subscriptions;

public class ExtendSubscriptionRequest
{
    public Guid IdUser { get; init; }
    
    public Guid IdCourse { get; init; }
    
    public DateTime DateTimeExtend { get; init; }
}