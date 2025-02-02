using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class EncryptionHelper
{

    private const string base64Key = "VtTIv2IHtY7EM3jR9Zj34/LofXN+xWbVQ4X4lZy+0rk=";
    private const string base64Iv = "ZvN4bGg7kSPpT9yc8rD6zQ==";

    // Chiffrement avec des clés en Base64
    public static string Encrypt(string plainText)
    {
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

    // Déchiffrement avec des clés en Base64
    public static string Decrypt(string cipherText)
    {
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