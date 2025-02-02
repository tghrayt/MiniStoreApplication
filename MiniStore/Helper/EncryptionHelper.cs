using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class EncryptionHelper
{
    public static string Encrypt(string plainText)
    {
        string base64Key = Environment.GetEnvironmentVariable("BASE64_KEY");
        string base64Iv = Environment.GetEnvironmentVariable("BASE64_IV");
        byte[] key = Convert.FromBase64String(base64Key);
        byte[] iv = Convert.FromBase64String(base64Iv);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt =
                       new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }
    
    public static string Decrypt(string cipherText)
    {
        string base64Key = Environment.GetEnvironmentVariable("BASE64_KEY");
        string base64Iv = Environment.GetEnvironmentVariable("BASE64_IV");
        byte[] key = Convert.FromBase64String(base64Key);
        byte[] iv = Convert.FromBase64String(base64Iv);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream csDecrypt =
                       new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}