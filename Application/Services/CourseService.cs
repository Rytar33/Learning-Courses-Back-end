using Application.Dtos.Courses;
using Application.Dtos.Pages;
using Application.Dtos.Ratings;
using LearningCourseDataBaseContext = CourseDbContext.LearningCourseDataBaseContext;

namespace Application.Services;

public class CourseService : ICourseService
{
    public CourseService(ICourseRepository courseRepository)
        => _courseRepository = courseRepository;

    private readonly ICourseRepository _courseRepository;

    public async Task<IResult> Create(CreateCourseRequest createCourseRequest)
    {
        await using var db = new LearningCourseDataBaseContext();
        if (await db.Course.AnyAsync(c => c.Name == createCourseRequest.Name))
            return Results.BadRequest("Такое название курса уже существует");
        if (createCourseRequest.IdUser != null
            & !await db.User.AnyAsync(u => u.Id == createCourseRequest.IdUser))
            return Results.NotFound("Данного пользователя не было найдено");
        try
        {
            var course = new Course(
                createCourseRequest.Name,
                createCourseRequest.Description,
                createCourseRequest.IdUser);
            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveChangesAsync();
            await FileLocalStorageServices.ImportSingleFile(
                $"wwwroot/src/images/courses/{course.Id}/main",
                createCourseRequest.Image);
            return Results.Created($"api/v1/Course/{course.Id}", course);
        }
        catch (ValidationException validationException)
        {
            return Results.BadRequest(validationException.Errors);
        }
    }

    public async Task<IResult> ChangeStatus(ChangeStatusCourseRequest changeStatusCourseRequest)
    {
        var course = await _courseRepository.GetByIdAsync(changeStatusCourseRequest.IdCourse);
        if (course == null)
            return Results.NotFound("Данного пользователя не было найдено");
        course.Update(isActive: changeStatusCourseRequest.IsActive);
        await _courseRepository.SaveChangesAsync();
        return Results.NoContent();
    }

    public async Task<IResult> GetInformation(GetDetailCourseRequest getDetailCourseRequest)
    {
        var course = await _courseRepository.GetByIdAsync(getDetailCourseRequest.IdCourse);
        if (course == null)
            return Results.NotFound();
        await using var db = new LearningCourseDataBaseContext();
        var sum = await db.Rating
            .Where(r => r.IdCourse == course.Id)
            .Select(r => r.QuantityScore)
            .SumAsync();
        return Results.Ok(new GetDetailCourseResponse
        {
            IdCourse = course.Id,
            Name = course.Name,
            Description = course.Description,
            PathImage = FileLocalStorageServices.ExportFullPathFile($"wwwroot/src/images/courses/{course.Id}/main")
                        ?? FileLocalStorageServices.ExportFullPathFile("wwwroot/src/images/courses"),
            AverageRating = sum != 0 
                ? (float)Math.Round((decimal)(sum / db.Rating.Count(r => r.IdCourse == course.Id)), 1) 
                : 0,
            PageRating = await PageService.GetPageRating(new GetPageRatingRequest
            {
                IdCourse = course.Id,
                Page = new GetPageRequest
                {
                    Page = getDetailCourseRequest.PageRating.Page.Page,
                    PageSize = getDetailCourseRequest.PageRating.Page.PageSize
                }
            }, new RatingRepository()),
            IsActive = course.IsActive
        });
    }
    
    public async Task<IResult> GetPageCourses(GetPageCourseRequest getPageCourseRequest)
    {
        var courses = await _courseRepository.GetAll();
        if (getPageCourseRequest.IdUserAuthor != null)
            courses = courses.Where(c => c.IdUser == getPageCourseRequest.IdUserAuthor);
        if (getPageCourseRequest.Search != null)
            courses = courses.Where(c => c.Name.Contains(getPageCourseRequest.Search) 
                                         | c.Description.Contains(getPageCourseRequest.Search));
        courses = courses
            .Skip((getPageCourseRequest.Page.Page - 1) * getPageCourseRequest.Page.PageSize)
            .Take(getPageCourseRequest.Page.PageSize);
        var pageCourseResponse = new GetPageCourseResponse
        {
            Items = courses.Select(c =>
            {
                using var db = new LearningCourseDataBaseContext();
                string? name = null;
                if (c.IdUser != null)
                    name = db.User.FirstAsync().GetAwaiter().GetResult().UserName;
                var sum = db.Rating
                    .Where(r => r.IdCourse == c.Id)
                    .Select(r => r.QuantityScore)
                    .Sum();
                return new CourseListItem
                {
                    IdCourse = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    PathImage = FileLocalStorageServices.ExportFullPathFile($"wwwroot/src/images/courses/{c.Id}/main")
                                ?? FileLocalStorageServices.ExportFullPathFile("wwwroot/src/images/courses"),
                    AverageScore = sum != 0 
                        ? (float)Math.Round((decimal)(sum / db.Rating.Count(r => r.IdCourse == c.Id)), 1) 
                        : sum,
                    IdUserAuthor = c.IdUser,
                    UserName = name,
                    IsActive = c.IsActive
                };
            }),
            Page = new GetPageResponse
            {
                Page = getPageCourseRequest.Page.Page,
                PageSize = getPageCourseRequest.Page.PageSize,
                Count = courses.Count()
            }
        };
        return pageCourseResponse.Items.Any()
            ? Results.Ok(pageCourseResponse) 
            : Results.BadRequest("На запрашиваемой страницы отсутствуют элементы");
    }
}