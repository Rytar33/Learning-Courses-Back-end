using FluentValidation;

namespace Domain.Validations.Validators;

public class RaitingValidator : AbstractValidator<Raiting>
{
    public RaitingValidator()
    {
        RuleFor(r => r);
    }
}
