using System.Security.Cryptography;
using System.Text;

namespace Domain.Extensions;

public static class StringExtensions
{
    public static string GetSha256(this string input)
        => Convert.ToHexString(
            SHA256.HashData(
                Encoding.UTF8.GetBytes(input)));
}