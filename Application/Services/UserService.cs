using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos.Pages;
using Application.Dtos.Ratings;
using Application.Dtos.Subscriptions;
using Application.Dtos.Users;
using Domain.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class UserService : IUserService
{
    public UserService(IUserRepository userRepository)
        => _userRepository = userRepository;
    
    private readonly IUserRepository _userRepository;

    public async Task<IResult> Registration(RegistrationUserRequest registrationUserRequest)
    {
        await using var db = new LearningCourseDataBaseContext();
        if (await db.User.AnyAsync(u => u.UserName == registrationUserRequest.UserName))
            return Results.BadRequest("Пользователь с данным именем уже существует");
        if (await db.User.AnyAsync(u => u.NumberPhone == registrationUserRequest.NumberPhone))
            return Results.BadRequest("Пользователь с данным телефоном уже существует");
        if (await db.User.AnyAsync(u => u.Email == registrationUserRequest.Email))
            return Results.BadRequest("Пользователь с данной почтой уже существует");

        try
        {
            var user = new User(
                registrationUserRequest.UserName,
                registrationUserRequest.Email,
                registrationUserRequest.FullName,
                registrationUserRequest.Password.GetSha256(),
                registrationUserRequest.NumberPhone,
                UserType.BaseUser,
                DateTime.UtcNow);
        
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return Results.Created($"api/v1/User/{user.Id}", user);
        }
        catch (ValidationException validationException)
        {
            return Results.BadRequest(validationException.Errors);
        }
    }

    public async Task<IResult> LogIn(LogInUserRequest logInUserRequest)
    {
        await using var db = new LearningCourseDataBaseContext();
        var user = await db.User.AsNoTracking().FirstOrDefaultAsync(u =>
            (u.Email == logInUserRequest.EmailOrName || u.UserName == logInUserRequest.EmailOrName)
            && u.Password == logInUserRequest.Password.GetSha256());
        if (user == null)
            return Results.BadRequest("Неверно введён логин, почта и/или пароль");
        
        var jwt = new JwtSecurityToken(
            
            issuer: "Ныкыта",
            audience: user.UserName,
            notBefore: DateTime.UtcNow,
            claims: [new Claim(nameof(UserType), user.UserType.ToString())],
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("You think there was going to be a secret key in here? But it was me! DIO!")),
                SecurityAlgorithms.HmacSha256));
        return Results.Ok(
            new LogInUserResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                IdUser = user.Id
            });
    }

    public async Task<IResult> ChangeInformation(ChangeInformationUserRequest changeInformationUserRequest)
    {
        var user = await _userRepository.GetByIdAsync(changeInformationUserRequest.IdUser);
        if (user == null)
            return Results.NotFound("Данного пользователя не было найденно");
        try
        {
            user.Update(changeInformationUserRequest.UserName);
            await _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            if (changeInformationUserRequest.Image != null)
                await FileLocalStorageServices.ImportSingleFile(
                    $"wwwroot/src/images/users/{user.Id}",
                    changeInformationUserRequest.Image);
            return Results.NoContent();
        }
        catch (ValidationException validationException)
        {
            return Results.BadRequest(validationException.Errors);
        }
    }

    public async Task<IResult> Ban(BanUserRequest banUserRequest)
    {
        var user = await _userRepository.GetByIdAsync(banUserRequest.IdUser);
        if (user == null)
            return Results.NotFound("Данного пользователя не было найденно");
        user.Update(userType: UserType.Blocked);
        await _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
        return Results.NoContent();
    }

    public async Task<IResult> CheckInformation(GetDetailUserRequest getDetailUserRequest)
    {
        var user = await _userRepository.GetByIdAsync(getDetailUserRequest.IdUser);
        if (user == null)
            return Results.NotFound("Данного пользователя не было найденно");
        return Results.Ok(new GetDetailUserResponse 
        { 
            IdUser = user.Id,
            UserName = user.UserName,
            FullName = user.FullName,
            UserType = user.UserType,
            DateTimeRegistration = user.DateTimeRegistration,
            PathImage = FileLocalStorageServices.ExportFullPathFile($"wwwroot/src/images/users/{user.Id}"),
            PageRating = await PageService.GetPageRating(
                new GetPageRatingRequest
                {
                    IdUser = user.Id,
                    Page = new GetPageRequest
                    {
                        Page = getDetailUserRequest.PageRating.Page.Page,
                        PageSize = getDetailUserRequest.PageRating.Page.PageSize
                    }
                }, new RatingRepository()), 
            PageSubscription = await PageService.GetPageSubscription(
                new GetPageSubscriptionRequest 
                {
                    IdUser = user.Id,
                    Page = new GetPageRequest
                    {
                        Page = getDetailUserRequest.PageRating.Page.Page,
                        PageSize = getDetailUserRequest.PageRating.Page.PageSize
                    }
                }, new SubscriptionRepository())
        });
    }
}