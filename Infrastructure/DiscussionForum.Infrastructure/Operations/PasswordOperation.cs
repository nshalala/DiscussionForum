using System.Security.Cryptography;
using System.Text;

namespace DiscussionForum.Infrastructure.Operations;

public class PasswordOperation
{
    public static string[] CalculateSha256Hash(string password)
    {
        string salt = GenerateSalt();
        byte[] inputBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltBytes = Convert.FromBase64String(salt);

        byte[] hashBytes;
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputWithSaltBytes = new byte[inputBytes.Length + saltBytes.Length];
            Buffer.BlockCopy(inputBytes, 0, inputWithSaltBytes, 0, inputBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, inputWithSaltBytes, inputBytes.Length, saltBytes.Length);
            hashBytes = sha256.ComputeHash(inputWithSaltBytes);
        }
        string hashedPassword = Convert.ToBase64String(hashBytes);
        
        return new[] { hashedPassword , salt };
    } 
    static string GenerateSalt()
    {
        byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
        return Convert.ToBase64String(saltBytes);
    }
    public static bool VerifyPassword(string inputPassword, string storedSalt, string storedHashedPassword)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(inputPassword);
        byte[] saltBytes = Convert.FromBase64String(storedSalt);

        byte[] hashBytes;
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputWithSaltBytes = new byte[inputBytes.Length + saltBytes.Length];
            Buffer.BlockCopy(inputBytes, 0, inputWithSaltBytes, 0, inputBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, inputWithSaltBytes, inputBytes.Length, saltBytes.Length);
            hashBytes = sha256.ComputeHash(inputWithSaltBytes);
        }
        string inputHashedPassword = Convert.ToBase64String(hashBytes);
        return storedHashedPassword.Equals(inputHashedPassword);
    }
}