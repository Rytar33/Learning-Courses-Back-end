using Application.Dtos.Pages;
using Application.Dtos.Ratings;
using Application.Dtos.Subscriptions;

namespace Application.Services;

public static class PageService
{
    public static async Task<GetPageRatingResponse> GetPageRating(
        GetPageRatingRequest getPageRatingRequest,
        IRatingRepository ratingRepository)
    {
        var ratingItems = await ratingRepository.GetAll();

        if (getPageRatingRequest.IdCourse != null)
            ratingItems = ratingItems.Where(r => r.IdCourse == getPageRatingRequest.IdCourse);
        if (getPageRatingRequest.IdUser != null)
            ratingItems = ratingItems.Where(r => r.IdUser == getPageRatingRequest.IdUser);
        
        ratingItems = ratingItems
            .Skip((getPageRatingRequest.Page.Page - 1) * getPageRatingRequest.Page.PageSize)
            .Take(getPageRatingRequest.Page.PageSize);
        ratingItems = ratingItems.ToList();
        
        return new GetPageRatingResponse
        {
            Items = ratingItems.Select(r => 
            { 
                using var db = new LearningCourseDataBaseContext();
                var user = db.User.FirstAsync(u => u.Id == r.IdUser).GetAwaiter().GetResult();
                var course = db.Course.FirstAsync(c => c.Id == r.IdCourse).GetAwaiter().GetResult();
                return new RatingListItem
                {
                    IdRating = r.Id,
                    IdCourse = course.Id,
                    CourseName = course.Name,
                    IdUser = r.IdUser,
                    UserName = user.UserName,
                    PathImage = FileLocalStorageServices.ExportFullPathFile($"wwwroot/src/images/users/{user.Id}")
                                ?? FileLocalStorageServices.ExportFullPathFile("wwwroot/src/images/users"),
                    QuantityScore = r.QuantityScore,
                    Comment = r.Comment
                };
            }),
            Page = new GetPageResponse
            {
                Count = ratingItems.Count(),
                Page = getPageRatingRequest.Page.Page,
                PageSize = getPageRatingRequest.Page.PageSize
            }
        };
    }

    public static async Task<GetPageSubscriptionResponse> GetPageSubscription(
        GetPageSubscriptionRequest getPageSubscriptionRequest,
        ISubscriptionRepository subscriptionRepository)
    {
        var subscriptionItems = await subscriptionRepository.GetAll();
        
        if (getPageSubscriptionRequest.IdCourse != null)
            subscriptionItems = subscriptionItems.Where(r => r.IdCourse == getPageSubscriptionRequest.IdCourse);
        if (getPageSubscriptionRequest.IdUser != null)
            subscriptionItems = subscriptionItems.Where(r => r.IdUser == getPageSubscriptionRequest.IdUser);
        
        subscriptionItems = subscriptionItems
            .Skip((getPageSubscriptionRequest.Page.Page - 1) * getPageSubscriptionRequest.Page.PageSize)
            .Take(getPageSubscriptionRequest.Page.PageSize);
        subscriptionItems = subscriptionItems.ToList();

        return new GetPageSubscriptionResponse
        {
            Items = subscriptionItems.Select(s =>
            {
                using var db = new LearningCourseDataBaseContext();
                var user = db.User.FirstAsync().GetAwaiter().GetResult();
                var course = db.Course.FirstAsync(c => c.Id == s.IdCourse).GetAwaiter().GetResult();
                return new SubscriptionListItem
                {
                    IdSubscription = s.Id,
                    IdCourse = s.IdCourse,
                    CourseName = course.Name,
                    PathCourseImage = FileLocalStorageServices
                        .ExportFullPathFile($"wwwroot/src/images/courses/{course.Id}/main"),
                    IdUser = s.IdUser,
                    UserName = user.UserName,
                    PathUserImage = FileLocalStorageServices
                        .ExportFullPathFile($"wwwroot/src/images/users/{user.Id}"),
                    DateTimeStartSubscription = s.DateTimePayment,
                    DateTimeEndSubscription = s.DateTimeEndSubscription
                };
            }),
            Page = new GetPageResponse
            {
                Count = subscriptionItems.Count(),
                Page = getPageSubscriptionRequest.Page.Page,
                PageSize = getPageSubscriptionRequest.Page.PageSize
            }
        };
    }
}