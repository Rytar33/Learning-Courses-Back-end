namespace Domain.Validations;

public static class ErrorMessage
{
    /// <summary>
    /// Сообщение об ошибке формата - только буквы
    /// </summary>
    public const string OnlyLetters = "{0} должен содержать только буквы";

    /// <summary>
    /// Сообщение об ошибке даты
    /// </summary>
    public const string FutureDate = "{0} не может быть в будущем";

    /// <summary>
    /// Сообщение об ошибке даты рождения
    /// </summary>
    public const string OldDate = "{0} слишком старая дата";

    /// <summary>
    /// Сообщение об исключении null
    /// </summary>
    public const string IsNull = "{0} отсутствует значение";

    /// <summary>
    /// Сообщение об исключении empty
    /// </summary>
    public const string IsEmpty = "{0} пустой";
}