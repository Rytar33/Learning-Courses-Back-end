namespace Application.Dtos.Users;

public class ChangeInformationUserRequest
{
    public Guid IdUser { get; init; }
    
    public string? UserName { get; init; }
    
    public IFormFile? Image { get; init; }
}