using Application.Dtos.Subscriptions;

namespace Application.Services;

public class SubscriptionService : ISubscriptionService
{
    public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        => _subscriptionRepository = subscriptionRepository;

    private readonly ISubscriptionRepository _subscriptionRepository;

    public async Task<IResult> Subscribe(SubscribeRequest subscribeRequest)
    {
        await using var db = new LearningCourseDataBaseContext();
        if (!await db.User.AnyAsync(u => u.Id == subscribeRequest.IdUser))
            return Results.NotFound("Такого пользователя не существует");
        if (!await db.Course.AnyAsync(c => c.Id == subscribeRequest.IdCourse))
            return Results.NotFound("Такого курса не существует");
        if (await db.Subscription.AnyAsync(s =>
                s.IdUser == subscribeRequest.IdUser
                && s.IdCourse == subscribeRequest.IdCourse
                && s.DateTimeEndSubscription > DateTime.UtcNow))
            return Results.BadRequest("У вас уже есть текущая не завершенная подписка");
        if (await db.Course.AnyAsync(c => c.Id == subscribeRequest.IdCourse && !c.IsActive))
            return Results.BadRequest("Данный курс не активен, и на него нельзя подписаться");
        try
        {
            var subscription = new Subscription(
                subscribeRequest.IdUser,
                subscribeRequest.IdCourse,
                DateTime.UtcNow,
                subscribeRequest.DateTimeEndSubscription);
            await _subscriptionRepository.AddAsync(subscription);
            await _subscriptionRepository.SaveChangesAsync();
            return Results.Created($"api/v1/Subscription/{subscription.Id}", subscription);
        }
        catch (ValidationException validationException)
        {
            return Results.BadRequest(validationException.Errors);
        }
    }

    public async Task<IResult> ExtendSubscription(ExtendSubscriptionRequest extendSubscriptionRequest)
    {
        await using var db = new LearningCourseDataBaseContext();
        var userSubscriptionCourseLast = await db.Subscription
            .AsNoTracking()
            .LastOrDefaultAsync(s => 
                s.IdUser == extendSubscriptionRequest.IdUser
                && s.IdCourse == extendSubscriptionRequest.IdCourse
                && DateTime.UtcNow < s.DateTimeEndSubscription);
        if (userSubscriptionCourseLast == null)
            return Results.BadRequest("Пользователь не подписан на курс");
        try
        {
            userSubscriptionCourseLast.Update(dateTimeEndSubscription: extendSubscriptionRequest.DateTimeExtend);
            await _subscriptionRepository.SaveChangesAsync();
            return Results.NoContent();
        }
        catch (ValidationException validationException)
        {
            return Results.BadRequest(validationException.Errors);
        }
    }
}