using System.Text.RegularExpressions;

namespace Domain.Validations;

public class RegexPattern
{
    public static readonly Regex FullNameRegex 
        = new(@"^[А-ЯA-Z][а-яА-Яa-zA-Z]{0,49}\s[А-ЯA-Z][а-яА-Яa-zA-Z]{0,49}\s?[А-ЯA-Z]?[а-яА-Яa-zA-Z]{0,49}$");

    public static readonly Regex EmailRegex = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

    public static readonly Regex NumberPhoneRegex = new(@"^\+\d{11}$");
}