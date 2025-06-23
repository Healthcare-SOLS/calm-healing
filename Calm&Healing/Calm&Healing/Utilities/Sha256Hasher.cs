using System.Security.Cryptography;
using System.Text;

namespace Calm_Healing.Utilities
{
    public static class Sha256Hasher
    {
        public static string HashPassword(string email, string password)
        {
            string combined = email + password;

            using var sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(combined);
            byte[] hashBytes = sha256.ComputeHash(bytes);

            // Convert to hex string
            var sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.Append(b.ToString("x2")); // two-character hex
            }

            return sb.ToString(); // 64-character hex string
        }
    }
}
