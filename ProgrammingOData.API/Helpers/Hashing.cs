using System.Security.Cryptography;
using System.Text;

namespace ProgrammingOData.API.Helpers;

public class Hashing
{
    public string HashPasswordWithHMACSHA256(string password, string secretKey)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
        byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashBytes);
    }
}
