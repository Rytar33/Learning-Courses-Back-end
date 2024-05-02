using Application.Dtos.Subscriptions;

namespace Application.Interfaces.Services;

public interface ISubscriptionService
{
    Task<IResult> Subscribe(SubscribeRequest subscribeRequest);
}