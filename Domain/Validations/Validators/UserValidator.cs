using FluentValidation;

namespace Domain.Validations.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.FullName)
            .Matches(RegexPattern.FullNameRegex).WithMessage(
                string.Format(ErrorMessage.OnlyLetters, nameof(User.FullName)));

        RuleFor(u => u.Email)
            .Matches(RegexPattern.EmailRegex).WithMessage(ErrorMessage.EmailNoCompiledRegex);

        RuleFor(u => u.NumberPhone)
            .Matches(RegexPattern.NumberPhoneRegex).WithMessage(ErrorMessage.IncorrectNumberPhone);

        RuleFor(u => u.DateTimeRegistration)
            .GreaterThan(DateTime.UtcNow).WithMessage(ErrorMessage.FutureDate);

        RuleFor(u => u.UserName)
            .MinimumLength(4).MaximumLength(20)
            .WithMessage(string.Format(ErrorMessage.OutDiapason, nameof(User.UserName), "4-х", "20-ти"));
    }
}
