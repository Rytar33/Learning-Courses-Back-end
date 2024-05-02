using Application.Dtos.Courses;

namespace Application.Interfaces.Services;

public interface ICourseService
{
    Task<IResult> Create(CreateCourseRequest createCourseRequest);

    Task<IResult> ChangeStatus(ChangeStatusCourseRequest changeStatusCourseRequest);

    Task<IResult> GetInformation(GetDetailCourseRequest getDetailCourseRequest);

    Task<IResult> GetPageCourses(GetPageCourseRequest getPageCourseRequest);
}