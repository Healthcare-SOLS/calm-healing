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
            aes.Key = Encoding.UTF8.GetBytes(_configuration["AES:Key"].ToString());
            aes.IV = Encoding.UTF8.GetBytes(_configuration["AES:IV"].ToString());

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);
            sw.Write(plainText);

            return Convert.ToBase64String(ms.ToArray());
        }
        public string Decrypt(string cipherText)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_configuration["AES:Key"].ToString());
            aes.IV = Encoding.UTF8.GetBytes(_configuration["AES:IV"].ToString());

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}
