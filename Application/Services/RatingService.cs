using Application.Dtos.Ratings;
using Application.Interfaces.Repositorys;
using Application.Repositorys;
using CourseDbContext;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class RatingService
{
    public RatingService()
        => _ratingRepository = new RatingRepository();

    private readonly IRatingRepository _ratingRepository;

    public async Task SendRatingForCourse(SendRatingRequest sendRatingRequest)
    {
        await using var db = new LearningCourseDataBaseContext();
        if (!await db.User.AnyAsync(u => u.Id == sendRatingRequest.IdUser))
            throw new ValidationException("Пользователя не было найденно");
        if (!await db.Course.AnyAsync(c => c.Id == sendRatingRequest.IdCourse))
            throw new ValidationException("Курс не был найден");
        if (!await db.Subscription.AnyAsync(s => 
                s.IdUser == sendRatingRequest.IdUser
                & s.IdCourse == sendRatingRequest.IdCourse))
            throw new ValidationException("На курс пользователь не был подписан");
        if (await db.Rating.AnyAsync(r =>
                r.IdUser == sendRatingRequest.IdUser
                & r.IdCourse == sendRatingRequest.IdCourse))
            throw new ValidationException("Отзыв уже поставлен на этот курс");
        await _ratingRepository.AddAsync(new Rating(
            sendRatingRequest.IdCourse,
            sendRatingRequest.IdUser,
            sendRatingRequest.QuantityScore,
            sendRatingRequest.Comment));
        await _ratingRepository.SaveChangesAsync();
    }
}