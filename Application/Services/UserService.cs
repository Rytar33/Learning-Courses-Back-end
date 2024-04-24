using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos.Users;
using Application.Interfaces.Repositorys;
using Application.Repositorys;
using Application.Services.IOs;
using CourseDbContext;
using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class UserService
{
    public UserService()
        => _userRepository = new UserRepository();
    
    private IUserRepository _userRepository;

    public async Task Registration(RegistrationUserRequest registrationUserRequest)
    {
        await using var db = new LearningCourseDataBaseContext();
        if (await db.User.AnyAsync(u => u.UserName == registrationUserRequest.UserName))
            throw new ValidationException("Пользователь с данным именем уже существует");
        if (await db.User.AnyAsync(u => u.NumberPhone == registrationUserRequest.NumberPhone))
            throw new ValidationException("Пользователь с данным телефоном уже существует");
        if (await db.User.AnyAsync(u => u.Email == registrationUserRequest.Email))
            throw new ValidationException("Пользователь с данной почтой уже существует");

        await _userRepository.AddAsync(
            new User(
                registrationUserRequest.UserName,
                registrationUserRequest.Email,
                registrationUserRequest.FullName,
                registrationUserRequest.Password,
                registrationUserRequest.NumberPhone,
                UserType.BaseUser,
                DateTime.UtcNow));
        await _userRepository.SaveChangesAsync();
    }

    public async Task<string> LogIn(LogInUserRequest logInUserRequest)
    {
        await using var db = new LearningCourseDataBaseContext();
        var user = await db.User.AsNoTracking().FirstOrDefaultAsync(u =>
            (u.Email == logInUserRequest.EmailOrName || u.UserName == logInUserRequest.EmailOrName)
            && u.Password == logInUserRequest.Password) 
                   ?? throw new ArgumentNullException("Неверный логин и/или пароль");
        var jwt = new JwtSecurityToken(
            issuer: "Ныкыта",
            audience: user.UserName,
            notBefore: DateTime.UtcNow,
            claims: Enum.GetNames(typeof(UserType)).Select(e => new Claim(e, user.UserType.ToString())),
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("You think there was going to be a secret key in here? But it was me! DIO!")),
                SecurityAlgorithms.HmacSha256));
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public async Task ChangeInformation(ChangeInformationUserRequest changeInformationUserRequest)
    {
        var user = await _userRepository.GetByIdAsync(changeInformationUserRequest.IdUser);
        user.Update(changeInformationUserRequest.UserName);
        await _userRepository.SaveChangesAsync();
        if (changeInformationUserRequest.Image != null)
            await ImageServices.ImportSingleFile(
                $"wwwroot/src/images/users/{user.Id}",
                changeInformationUserRequest.Image);
    }

    public async Task Ban(BanUserRequest banUserRequest)
    {
        var user = await _userRepository.GetByIdAsync(banUserRequest.IdUser);
        user.Update(userType: UserType.Blocked);
        await _userRepository.SaveChangesAsync();
    }
}