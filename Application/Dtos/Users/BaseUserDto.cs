using Domain.Enums;

namespace Application.Dtos.Users;

public class BaseUserDto
{
    public string UserName { get; init; }

    public string Email { get; init; }

    public string FullName { get; init; }

    public string NumberPhone { get; init; }

    public UserType UserType { get; init; }
}