using Domain.Enums;

namespace Domain;

public class User : BaseEntity
{
    public User(
        string userName,
        string email,
        string fullName,
        string password,
        string numberPhone,
        UserType userType) 
    {
        UserName = userName;
        Email = email;
        FullName = fullName;
        Password = password;
        NumberPhone = numberPhone;
        UserType = userType;
    }

    public string UserName { get; init; }

    public string Email { get; init; }

    public string FullName { get; init; }

    public string Password { get; init; }

    public string NumberPhone { get; init; }

    public UserType UserType { get; init; }

    public DateTime DateTimeRegistration { get; init; }
}