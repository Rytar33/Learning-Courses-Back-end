using System.Text.Json.Serialization;

namespace Domain.Enums;

[JsonConverter(typeof(string))]
public enum UserType
{
    BaseUser = 0,
    Blocked = 1,
    Administrator = 2
}
