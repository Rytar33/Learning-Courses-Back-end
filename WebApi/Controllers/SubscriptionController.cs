using Application.Dtos.Subscriptions;
using Application.Interfaces.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SubscriptionController(ISubscriptionService subscriptionService) : Controller
{
    [Authorize(nameof(UserType.BaseUser))]
    [HttpPost]
    public async Task<IResult> Subscribe(SubscribeRequest subscribeRequest)
        => await subscriptionService.Subscribe(subscribeRequest);
}