using System.Security.Cryptography;
using System.Text;

namespace Common.Helpers;

public class PasswordHasher
{
    public string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        string hashOfInput = HashPassword(password);
        return hashOfInput.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
    }
}
