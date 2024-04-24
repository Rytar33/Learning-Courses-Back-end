namespace Application.Dtos.Users;

public class LogInUserRequest
{
    public string EmailOrName { get; init; }
    
    public string Password { get; init; }
}