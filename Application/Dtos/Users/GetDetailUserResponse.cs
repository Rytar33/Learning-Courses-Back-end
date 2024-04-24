using Domain.Enums;

namespace Application.Dtos.Users;

public class GetDetailUserResponse
{
    public string PathImage { get; init; }
    
    public Guid IdUser { get; init; }
    
    public string UserName { get; init; }
    
    public string Email { get; init; }
    
    public UserType UserType { get; init; }
    
    public DateTime DateTimeRegistration { get; init; }
}