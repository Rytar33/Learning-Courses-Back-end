namespace Application.Dtos.Users;

public class BanUserRequest
{
    public Guid IdUser { get; init; }
    
    public DateTime BlockingPeriod { get; init; }
    
    public string Reason { get; init; }
}