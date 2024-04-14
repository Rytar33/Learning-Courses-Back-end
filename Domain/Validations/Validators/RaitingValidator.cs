using FluentValidation;

namespace Domain.Validations.Validators;

public class RaitingValidator : AbstractValidator<Rating>
{
    public RaitingValidator()
    {
        RuleFor(r => r);
    }
}
