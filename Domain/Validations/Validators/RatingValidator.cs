﻿using FluentValidation;

namespace Domain.Validations.Validators;

public class RatingValidator : AbstractValidator<Rating>
{
    public RatingValidator()
    {
        RuleFor(r => r.QuantityScore)
            .Must(score => score >= 1 & score <= 5)
            .WithMessage(string.Format(ErrorMessage.OutDiapason, nameof(Rating.QuantityScore), "1", "5"));

        RuleFor(r => r.Comment)
            .MaximumLength(250).WithMessage("Комментарий не может быть больше 250-ти символов");
    }
}
