using FluentValidation;

namespace Domain.Validations;

public static class ValidateExtensions
{
    public static void ValidateWithErrors<T>(this IValidator<T> validator, T value)
    {
        var validationResult = validator.Validate(value);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }
}