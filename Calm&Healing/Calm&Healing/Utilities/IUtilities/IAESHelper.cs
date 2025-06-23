namespace Calm_Healing.Utilities.IUtilities
{
    public interface IAESHelper
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
