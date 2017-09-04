using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace IFormFileExtensions
{
    /// <summary>
    /// Provide Encrypt/Decrypt Methods to help you to encrtypt and decrypt the file.
    /// </summary>
    public static class SecurityExtensions
    {
        /// <summary>
        /// Encrypt a file
        /// </summary>
        /// <param name="InputFile">The Current file to encrypt</param>
        /// <param name="Key">The Encryption Key</param>
        /// <param name="Salt">Makes the encryption more secure</param>
        public static async Task<string> Encrypt(this IFormFile InputFile, string Key, string Salt)
        {
            // Locate a memory to write the bytes in. 
            using (MemoryStream MS = new MemoryStream())
            {
                // Advanced Encryption Standard Encryption Algorithm, it’s so powerful.
                AesManaged aes = new AesManaged();
                // Generate Encrypt Key, with a given key and pseudo byte array   
                using (PasswordDeriveBytes KeyGenerator = new PasswordDeriveBytes(Key, Encoding.UTF8.GetBytes(Salt)))
                {
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = KeyGenerator.GetBytes(aes.KeySize / 8);
                    aes.IV = KeyGenerator.GetBytes(aes.BlockSize / 8);
                }
                // Define a stream to link the data with Cryptographic transformation 
                using (CryptoStream cs = new CryptoStream(MS, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] localBytes = await cs.GetBytesAsync();
                    await cs.WriteAsync(localBytes, 0, localBytes.Length);
                }
                // Write the encrypted data into the text
                return BitConverter.ToString(await MS.GetBytesAsync());
            }
        }
        /// <summary>
        /// Decrypt a file
        /// </summary>
        /// <param name="InputFile">The Current file to decrypt</param>
        /// <param name="Key">The Encryption Key</param>
        /// <param name="Salt">Needed for decryption</param>
        public static async Task<string> Decrypt(this IFormFile InputFile, string Key, string Salt)
        {
            // Locate a memory to write the bytes in. 
            using (MemoryStream MS = new MemoryStream())
            {
                // Advanced Encryption Standard Encryption Algorithm, it’s so powerful.
                AesManaged aes = new AesManaged();
                // Generate Encrypt Key, with a given key and pseudo byte array   
                using (PasswordDeriveBytes KeyGenerator = new PasswordDeriveBytes(Key, Encoding.UTF8.GetBytes(Salt)))
                {
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = KeyGenerator.GetBytes(aes.KeySize / 8);
                    aes.IV = KeyGenerator.GetBytes(aes.BlockSize / 8);
                }
                // Define a stream to link the data with Cryptographic transformation 
                using (CryptoStream cs = new CryptoStream(MS, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    byte[] localBytes = await cs.GetBytesAsync();
                    await cs.WriteAsync(localBytes, 0, localBytes.Length);
                }
                return BitConverter.ToString(await MS.GetBytesAsync());
            }
        }
    }
}
