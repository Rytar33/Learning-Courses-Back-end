using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Domain.Validations;
using Domain.Validations.Validators;

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
        new UserValidator().ValidateWithErrors(this);
    }

    [Column("user_name")]
    public string UserName { get; private set; }

    [Column("email")]
    public string Email { get; private set; }

    [Column("full_name")]
    public string FullName { get; private set; }

    [Column("password")]
    public string Password { get; private set; }

    [Column("number_phone")]
    public string NumberPhone { get; private set; }

    [Column("user_type")]
    public UserType UserType { get; private set; }

    [Column("date_time_registration")]
    public DateTime DateTimeRegistration { get; init; }

    public User Update(
        string? userName = null,
        string? email = null,
        string? fullName = null,
        string? password = null,
        string? numberPhone = null,
        UserType? userType = null)
    {
        if (userName != null)
            UserName = userName;
        if (email != null)
            Email = email;
        if (fullName != null)
            FullName = fullName;
        if (password != null)
            Password = password;
        if (numberPhone != null)
            NumberPhone = numberPhone;
        if (userType != null)
            UserType = userType.Value;
        new UserValidator().ValidateWithErrors(this);
        return this;
    }
}