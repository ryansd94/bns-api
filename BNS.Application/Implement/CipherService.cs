using BNS.Domain;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BNS.Service.Implement
{
    public class CipherService : ICipherService
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly MyConfiguration config;
        private readonly IDataProtector _protector;

        private static string key = "";
        public CipherService(IDataProtectionProvider DataProtectionProvider
            , IOptions<MyConfiguration> config)
        {
            _dataProtectionProvider = DataProtectionProvider;
            this.config = config.Value;
            key = this.config.Default.CipherKey;
            _protector = _dataProtectionProvider.CreateProtector(config.Value.Default.CipherKey);
        }
        public string Encrypt(string input)
        {
            return _protector.Protect(input);
        }

        public string Decrypt(string cipherText)
        {
            try
            {
                return _protector.Unprotect(cipherText);
            }
            catch
            {
                return string.Empty;
            }
        }

        public string EncryptString(string plainText)
        {
            return Encrypt(plainText);
            byte[] encryptedBytes;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] plaintextBytes = Encoding.UTF8.GetBytes(plainText);

                using (var encryptedStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(encryptedStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plaintextBytes, 0, plaintextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        encryptedBytes = encryptedStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        public async Task<string> DecryptString(string cipherText)
        {
            return Decrypt(cipherText);
            try
            {
                byte[] decryptedBytes;
                byte[] ciphertextBytes = Convert.FromBase64String(cipherText);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.Mode = CipherMode.CBC;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (var decryptedStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(decryptedStream, decryptor, CryptoStreamMode.Write))
                        {
                            await cryptoStream.WriteAsync(ciphertextBytes, 0, ciphertextBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            decryptedBytes = decryptedStream.ToArray();
                        }
                    }
                }

                return Encoding.UTF8.GetString(decryptedBytes);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
