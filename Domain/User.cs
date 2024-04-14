using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain;

[Table("user")]
public class User : BaseEntity
{
    public User(
        string userName,
        string email,
        string fullName,
        string password,
        string numberPhone,
        UserType userType,
        DateTime dateTimeRegistration) 
    {
        UserName = userName;
        Email = email;
        FullName = fullName;
        Password = password;
        NumberPhone = numberPhone;
        UserType = userType;
        DateTimeRegistration = dateTimeRegistration;
    }

    [Column("user_name")]
    public string UserName { get; init; }

    [Column("email")]
    public string Email { get; init; }

    [Column("full_name")]
    public string FullName { get; init; }

    [Column("password")]
    public string Password { get; init; }

    [Column("number_phone")]
    public string NumberPhone { get; init; }

    [Column("user_type")]
    public UserType UserType { get; init; }

    [Column("date_time_registration")]
    public DateTime DateTimeRegistration { get; init; }
}