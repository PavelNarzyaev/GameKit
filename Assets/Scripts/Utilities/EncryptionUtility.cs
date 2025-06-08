using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GameKit
{

	public static class EncryptionUtility
	{
		private static readonly byte[] _key = Encoding.UTF8.GetBytes("SU6x2h36syQVdHV&Zx$t1%6flcXWBwgb");
		private static readonly byte[] _iv = Encoding.UTF8.GetBytes("9nZEMp**W#3bAYyh");

		public static string Encrypt(string plainText)
		{
			using var aes = Aes.Create();
			aes.Key = _key;
			aes.IV = _iv;

			using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
			using var memoryStream = new MemoryStream();
			using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
			using (var writer = new StreamWriter(cryptoStream))
				writer.Write(plainText);
			return Convert.ToBase64String(memoryStream.ToArray());
		}

		public static string Decrypt(string cipherText)
		{
			using var aes = Aes.Create();
			aes.Key = _key;
			aes.IV = _iv;

			using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
			using var memoryStream = new MemoryStream(Convert.FromBase64String(cipherText));
			using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
			using var reader = new StreamReader(cryptoStream);
			return reader.ReadToEnd();
		}
	}
}
