using Application.Dtos.Ratings;

namespace Application.Services;

public class RatingService : IRatingService
{
    public RatingService(IRatingRepository ratingRepository)
        => _ratingRepository = ratingRepository;

    private readonly IRatingRepository _ratingRepository;

    public async Task<IResult> SendRatingForCourse(SendRatingRequest sendRatingRequest)
    {
        await using var db = new LearningCourseDataBaseContext();
        if (!await db.User.AnyAsync(u => u.Id == sendRatingRequest.IdUser))
            return Results.NotFound("Пользователя не было найденно");
        if (!await db.Course.AnyAsync(c => c.Id == sendRatingRequest.IdCourse))
            return Results.NotFound("Курс не был найден");
        if (!await db.Subscription.AnyAsync(s => 
                s.IdUser == sendRatingRequest.IdUser
                & s.IdCourse == sendRatingRequest.IdCourse))
            return Results.BadRequest("На курс пользователь не был подписан");
        if (await db.Rating.AnyAsync(r =>
                r.IdUser == sendRatingRequest.IdUser
                & r.IdCourse == sendRatingRequest.IdCourse))
            return Results.BadRequest("Отзыв уже поставлен на этот курс");
        try
        {
            var rating = new Rating(
                sendRatingRequest.IdCourse,
                sendRatingRequest.IdUser,
                sendRatingRequest.QuantityScore,
                sendRatingRequest.Comment);
            await _ratingRepository.AddAsync(rating);
            await _ratingRepository.SaveChangesAsync();
            return Results.Created();
        }
        catch (ValidationException validationException)
        {
            return Results.BadRequest(validationException.Errors);
        }
    }

    public async Task<IResult> Remove(Guid idRating)
    {
        var rating = await _ratingRepository.GetByIdAsync(idRating);
        if (rating == null)
            return Results.NotFound("Данного курса не было найденно");
        await _ratingRepository.Delete(rating);
        await _ratingRepository.SaveChangesAsync();
        return Results.NoContent();
    }
}