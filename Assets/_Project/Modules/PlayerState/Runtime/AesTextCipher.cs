using System;
using System.IO;
using System.Security.Cryptography;
using JetBrains.Annotations;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class AesTextCipher : ITextCipher
    {
        public string Encrypt(string plainText)
        {
            using var aes = CreateConfiguredAes();
            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var memoryStream = new MemoryStream();
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            using (var writer = new StreamWriter(cryptoStream))
            {
                writer.Write(plainText);
            }

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            using var aes = CreateConfiguredAes();
            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var memoryStream = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }

        private static Aes CreateConfiguredAes()
        {
            var aes = Aes.Create();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Convert.FromBase64String(EncryptionKeys.KeyBase64);
            aes.IV = Convert.FromBase64String(EncryptionKeys.IvBase64);
            return aes;
        }
    }
}
