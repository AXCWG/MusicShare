using System.Security.Cryptography;
using System.Text;

namespace MusicShare.Backend.Extensions;

public static class StringExtensions
{
    public static string Sha256HexHashString(this string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha256.ComputeHash(bytes);
        var hex = BitConverter.ToString(hash).Replace("-", "").ToLower();
        return hex;
    }
}