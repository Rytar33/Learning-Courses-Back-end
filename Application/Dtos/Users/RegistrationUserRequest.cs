namespace Application.Dtos.Users;

public class RegistrationUserRequest
{
    public string UserName { get; init; }

    public string Email { get; init; }

    public string FullName { get; init; }
    
    public string Password { get; init; }

    public string NumberPhone { get; init; }
}