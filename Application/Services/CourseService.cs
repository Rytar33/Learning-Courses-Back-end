using Application.Dtos.Courses;
using Application.Interfaces.Repositorys;
using Application.Repositorys;
using Application.Services.IOs;
using CourseDbContext;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class CourseService
{
    public CourseService()
        => _courseRepository = new CourseRepository();

    private ICourseRepository _courseRepository;
    
    public async Task Create(CreateCourseRequest createCourseRequest)
    {
        await using var db = new LearningCourseDataBaseContext();
        if (await db.Course.AnyAsync(c => c.Name == createCourseRequest.Name))
            throw new ValidationException("Такое название курса уже существует");
        if (createCourseRequest.IdUser != null
            & !await db.User.AnyAsync(u => u.Id == createCourseRequest.IdUser))
            throw new ArgumentNullException("Данного пользователя не было найденно");
        var course = new Course(
            createCourseRequest.Name,
            createCourseRequest.Description,
            createCourseRequest.IdUser);
        await _courseRepository.AddAsync(course);
        await _courseRepository.SaveChangesAsync();
        await ImageServices.ImportSingleFile(
            $"wwwroot/src/images/courses/{course.Id}",
            createCourseRequest.Image);
    }
}