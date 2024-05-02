using Microsoft.AspNetCore.Mvc;

namespace Application.Dtos.Users;

public class BanUserRequest
{
    [FromRoute]
    public Guid IdUser { get; init; }
    
    public DateTime BlockingPeriod { get; init; }
    
    public string Reason { get; init; }
}