using Application.Dtos.Courses;
using Application.Interfaces.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CourseController(ICourseService courseService) : Controller
{
    [HttpGet]
    public async Task<IResult> GetPage([FromQuery] GetPageCourseRequest getPageCourseRequest)
        => await courseService.GetPageCourses(getPageCourseRequest);

    [Authorize]
    [HttpGet("{IdCourse}")]
    public async Task<IResult> GetDetailInformation([FromQuery] GetDetailCourseRequest getDetailCourseRequest)
        => await courseService.GetInformation(getDetailCourseRequest);

    [Authorize(nameof(UserType.Administrator))]
    [HttpPost]
    public async Task<IResult> Create([FromForm] CreateCourseRequest createCourseRequest)
        => await courseService.Create(createCourseRequest);
    
    [Authorize(nameof(UserType.Administrator))]
    [HttpPatch]
    public async Task<IResult> ChangeStatus([FromBody] ChangeStatusCourseRequest changeStatusCourseRequest)
        => await courseService.ChangeStatus(changeStatusCourseRequest);
    
}