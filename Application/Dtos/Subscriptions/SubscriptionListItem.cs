namespace Application.Dtos.Subscriptions;

public class SubscriptionListItem
{
    public Guid IdSubscription { get; init; }
    
    public Guid IdUser { get; init; }
    
    public string UserName { get; init; }
    
    public string? PathUserImage { get; init; }
    
    public Guid IdCourse { get; init; }
    
    public string CourseName { get; init; }
    
    public string? PathCourseImage { get; init; }
    
    public DateTime DateTimeStartSubscription { get; init; }
    
    public DateTime DateTimeEndSubscription { get; init; }

    public bool IsActive => DateTimeEndSubscription > DateTime.UtcNow ? true : false;
}