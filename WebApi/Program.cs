using System.Text;
using Application.Interfaces.Repositorys;
using Application.Interfaces.Services;
using Application.Repositorys;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration
                            .GetSection("JwtOptions")
                            .GetSection("SecretKey").Value!))
                };
            });
        builder.Services.AddAuthorization(options =>
        {
            foreach (var userType in Enum.GetNames(typeof(UserType)))
            {
                options.AddPolicy(userType, policy =>
                {
                    policy.RequireClaim(nameof(UserType), userType);
                });
            }
        });
        builder.Services.AddSwaggerGen(option =>
        {
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Description = "Введите существующий токен",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            });
        });
        builder.Services.AddScoped<ICourseService, CourseService>();
        builder.Services.AddScoped<IRatingService, RatingService>();
        builder.Services.AddScoped<SenderService>();
        builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
        builder.Services.AddScoped<IUserService, UserService>();
        
        builder.Services.AddScoped<ICourseRepository, CourseRepository>();
        builder.Services.AddScoped<IRatingRepository, RatingRepository>();
        builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        
        
        var app = builder.Build();
        
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        
        /*
        // User
        app.MapPost("api/v1/User/Registration", async (RegistrationUserRequest registrationUserRequest) 
            => await new UserService().Registration(registrationUserRequest));
        app.MapGet("api/v1/User/Login", async ([AsParameters] LogInUserRequest logInUserRequest) 
            => await new UserService().LogIn(logInUserRequest));
        app.MapPatch("api/v1/User/Ban", [Authorize("Administrator")] async (BanUserRequest banUserRequest) 
            => await new UserService().Ban(banUserRequest));
        app.MapGet("api/v1/User", [Authorize] async ([FromQuery] GetDetailUserRequest getDetailUserRequest) 
            => await new UserService().CheckInformation(getDetailUserRequest));
        app.MapPatch("api/v1/User", [Authorize] async ([FromForm] ChangeInformationUserRequest changeInformationUserRequest) 
            => await new UserService().ChangeInformation(changeInformationUserRequest));
        
        // Course
        app.MapGet("api/v1/Courses", async ([AsParameters] GetPageCourseRequest getPageCourseRequest) 
            => await new CourseService().GetPageCourses(getPageCourseRequest));
        app.MapGet("api/v1/Course/{getDetailCourseRequest.IdUser}", 
            async ([AsParameters] GetDetailCourseRequest getDetailCourseRequest) 
            => await new CourseService().GetInformation(getDetailCourseRequest));
        app.MapPatch("api/v1/Course/Status", [Authorize("Administrator")] 
            async (ChangeStatusCourseRequest changeStatusCourseRequest) 
            => await new CourseService().ChangeStatus(changeStatusCourseRequest));
        app.MapPost("api/v1/Course", [Authorize("Administrator")] 
            async ([FromForm] CreateCourseRequest createCourseRequest) 
            => await new CourseService().Create(createCourseRequest));
        
        // Rating
        app.MapPost("api/v1/Rating", [Authorize("BaseUser")] async (SendRatingRequest sendRatingRequest) 
            => await new RatingService().SendRatingForCourse(sendRatingRequest));
        app.MapDelete("api/v1/Rating", [Authorize("BaseUser")] [Authorize("Administrator")] async (Guid idRating)
            => await new RatingService().Remove(idRating));
        
        // Subscription
        app.MapPost("api/v1/Subscription", [Authorize("BaseUser")] 
            async (SubscribeRequest subscribeRequest) 
            => await new SubscriptionService().Subscribe(subscribeRequest));
        */
        
        
        app.Run();
    }
}