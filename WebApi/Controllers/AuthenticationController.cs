using Application.Dtos.Users;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthenticationController(IUserService userService) : Controller
{
    [HttpGet("User")]
    public async Task<IResult> LogInUser([FromQuery] LogInUserRequest logInUserRequest)
        => await userService.LogIn(logInUserRequest);
    
    [HttpPost("User/Registration")]
    public async Task<IResult> RegistrationUser(RegistrationUserRequest registrationUserRequest)
        => await userService.Registration(registrationUserRequest);
}