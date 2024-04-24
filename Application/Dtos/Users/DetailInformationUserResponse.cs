namespace Application.Dtos.Users;

public class DetailInformationUserResponse : BaseUserDto
{
    public Guid IdUser { get; init; }
    
    public string? FullPathAvatar { get; init; }
    
    public DateTime DateTimeRegistration { get; init; }
}