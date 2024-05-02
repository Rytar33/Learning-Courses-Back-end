using Application.Dtos.Ratings;

namespace Application.Interfaces.Services;

public interface IRatingService
{
    Task<IResult> SendRatingForCourse(SendRatingRequest sendRatingRequest);

    Task<IResult> Remove(Guid idRating);
}