using Calm_Healing.Utilities.IUtilities;
using System.Security.Cryptography;
using System.Text;

namespace Calm_Healing.Utilities
{
    public class AESHelper : IAESHelper
    {
        private readonly IConfiguration _configuration;

        public AESHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();

            aes.Key = Encoding.UTF8.GetBytes(_configuration["AES:Key"]);
            aes.IV = Encoding.UTF8.GetBytes(_configuration["AES:IV"]);

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();

            // Use CryptoStream with Write mode
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            // Ensure the encrypted data is complete before converting to Base64
            return Convert.ToBase64String(ms.ToArray());
        }
        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrWhiteSpace(cipherText))
                return string.Empty;

            try
            {
                // Attempt Base64 decode first
                byte[] buffer = Convert.FromBase64String(cipherText);

                using var aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(_configuration["AES:Key"]);
                aes.IV = Encoding.UTF8.GetBytes(_configuration["AES:IV"]);

                using var decryptor = aes.CreateDecryptor();
                using var ms = new MemoryStream(buffer);
                using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using var sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch (FormatException)
            {
                return cipherText;
            }
            catch (CryptographicException)
            {
                return cipherText;
            }
            catch
            {
                return cipherText;
            }
        }
    }
}
