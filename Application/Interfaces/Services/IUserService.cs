using Application.Dtos.Users;

namespace Application.Interfaces.Services;

public interface IUserService
{
    Task<IResult> Registration(RegistrationUserRequest registrationUserRequest);

    Task<IResult> LogIn(LogInUserRequest logInUserRequest);

    Task<IResult> ChangeInformation(ChangeInformationUserRequest changeInformationUserRequest);

    Task<IResult> Ban(BanUserRequest banUserRequest);

    Task<IResult> CheckInformation(GetDetailUserRequest getDetailUserRequest);
}