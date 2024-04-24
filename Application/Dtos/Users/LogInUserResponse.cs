namespace Application.Dtos.Users;

public class LogInUserResponse
{
    public string Token { get; init; }
    
    public Guid IdUser { get; init; }
}