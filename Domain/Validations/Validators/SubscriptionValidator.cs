using FluentValidation;

namespace Domain.Validations.Validators;

public class SubscriptionValidator : AbstractValidator<Subscription>
{
    public SubscriptionValidator()
    {
        RuleFor(s => s.DateTimePayment)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage(string.Format(ErrorMessage.FutureDate, nameof(Subscription.DateTimePayment)));
        
        RuleFor(s => s.DateTimePayment)
            .LessThanOrEqualTo(s => s.DateTimeEndSubscription)
            .WithMessage("Дата оплаты не может быть позже даты окончания подписки.");
    }
}
