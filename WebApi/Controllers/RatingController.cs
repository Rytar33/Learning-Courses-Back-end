using Application.Dtos.Ratings;
using Application.Interfaces.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RatingController(IRatingService ratingService) : Controller
{
    [Authorize(nameof(UserType.BaseUser))]
    [HttpPost]
    public async Task<IResult> SendRatingForCourse(SendRatingRequest sendRatingRequest)
        => await ratingService.SendRatingForCourse(sendRatingRequest);

    [Authorize(nameof(UserType.Administrator))]
    [HttpDelete("{idRating}")]
    public async Task<IResult> Remove([FromRoute] Guid idRating)
        => await ratingService.Remove(idRating);
}