using Application.Dtos.Users;
using Application.Interfaces.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController(IUserService userService) : Controller
{
    [Authorize(nameof(UserType.BaseUser))]
    [HttpGet("{IdUser}")]
    public async Task<IResult> GetDetailInformation([FromQuery] GetDetailUserRequest getDetailUserRequest)
        => await userService.CheckInformation(getDetailUserRequest);

    [Authorize(nameof(UserType.BaseUser))]
    [HttpPatch]
    public async Task<IResult> ChangeYourInformation([FromForm] ChangeInformationUserRequest changeInformationUserRequest)
        => await userService.ChangeInformation(changeInformationUserRequest);

    [Authorize(nameof(UserType.Administrator))]
    [HttpPatch("Ban/{IdUser}")]
    public async Task<IResult> Ban(BanUserRequest banUserRequest)
        => await userService.Ban(banUserRequest);
}