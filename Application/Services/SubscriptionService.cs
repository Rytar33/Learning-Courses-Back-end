using Application.Dtos.Subscriptions;
using Application.Interfaces.Repositorys;
using Application.Repositorys;
using CourseDbContext;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class SubscriptionService
{
    public SubscriptionService()
        => _subscriptionRepository = new SubscriptionRepository();

    private readonly ISubscriptionRepository _subscriptionRepository;

    public async Task Subscribe(SubscribeRequest subscribeRequest)
    {
        await using var db = new LearningCourseDataBaseContext();
        if (!await db.User.AnyAsync(u => u.Id == subscribeRequest.IdUser))
            throw new ValidationException("Такого пользователя не существует");
        if (!await db.Course.AnyAsync(c => c.Id == subscribeRequest.IdCourse))
            throw new ValidationException("Такого курса не существует");
        if (await db.Subscription.AnyAsync(s =>
                s.IdUser == subscribeRequest.IdUser
                && s.IdCourse == subscribeRequest.IdCourse
                && s.DateTimeEndSubscription > DateTime.UtcNow))
            throw new ValidationException("У вас уже есть текущая не завершенная подписка");
        await _subscriptionRepository.AddAsync(
            new Subscription(
                subscribeRequest.IdUser,
                subscribeRequest.IdCourse,
                DateTime.UtcNow,
                subscribeRequest.DateTimeEndSubscription));
        await _subscriptionRepository.SaveChangesAsync();
    }

    public async Task ExtendSubscription(ExtendSubscriptionRequest extendSubscriptionRequest)
    {
        await using var db = new LearningCourseDataBaseContext();
        var userSubscriptionCourseLast = await db.Subscription
            .AsNoTracking()
            .LastOrDefaultAsync(s => 
                s.IdUser == extendSubscriptionRequest.IdUser
                & s.IdCourse == extendSubscriptionRequest.IdCourse) 
                                         ?? throw new ValidationException("Пользователь не подписан на курс");
        userSubscriptionCourseLast.Update(dateTimeEndSubscription: extendSubscriptionRequest.DateTimeExtend);
        await _subscriptionRepository.SaveChangesAsync();
    }
}