using FluentValidation;

namespace Domain.Validations.Validators;

public class CourseValidator : AbstractValidator<Course>
{
    public CourseValidator()
    {
        RuleFor(c => c.Name)
            .NotNull().WithMessage(string.Format(ErrorMessage.IsNull, nameof(Course.Name)))
            .NotEmpty().WithMessage(string.Format(ErrorMessage.IsEmpty, nameof(Course.Name)))
            .MaximumLength(25).WithMessage(
                string.Format(ErrorMessage.OutDiapason, nameof(Course.Description), "1-го", "25-ти"));
        
        RuleFor(c => c.Description)
            .MinimumLength(20).MaximumLength(250)
            .WithMessage(string.Format(ErrorMessage.OutDiapason, nameof(Course.Description), "20-ти", "250-ти"));
    }
}