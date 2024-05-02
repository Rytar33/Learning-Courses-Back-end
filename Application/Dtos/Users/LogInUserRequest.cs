using System.Reflection;

namespace Application.Dtos.Users;

public class LogInUserRequest
{
    public string EmailOrName { get; init; }
    
    public string Password { get; init; }
    
    public static ValueTask<LogInUserRequest?> BindAsync(HttpContext httpContext, ParameterInfo parameterInfo)
    {
        var result = new LogInUserRequest()
        {
            EmailOrName = httpContext.Request.Query["EmailOrName"]!,
            Password = httpContext.Request.Query["Password"]!
        };
        return ValueTask.FromResult<LogInUserRequest?>(result);
    }
}