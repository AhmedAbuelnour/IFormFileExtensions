using Microsoft.AspNetCore.Http;
using System;
using System.Security.Cryptography;

namespace IFormFileExtensions
{
    /// <summary>
    /// Provide 3 Hashing methods for different using
    /// </summary>
    public static class HashingExtensions
    {
        /// <summary>
        /// Compute the hash of a file (128 Letter) 
        /// </summary>
        /// <param name="Value">The Current file to get its hash</param>
        /// <returns>
        /// A strong hash (128 Letter) 
        /// </returns>
        public static string GetStrongHash128(this IFormFile Value)
        {
            return BitConverter.ToString(SHA512.Create().ComputeHash(Value.OpenReadStream())).Replace("-", string.Empty);
        }
        /// <summary>
        /// Compute the hash of a file (64 Letter) 
        /// </summary>
        /// <param name="Value">The Current file to get its hash</param>
        /// <returns>
        /// A strong hash (64 Letter) 
        /// </returns>
        public static string GetIntermediateHash64(this IFormFile Value)
        {
            return BitConverter.ToString(SHA256.Create().ComputeHash(Value.OpenReadStream())).Replace("-", string.Empty);
        }
        /// <summary>
        /// Compute the hash of a file (40 Letter) 
        /// </summary>
        /// <param name="Value">The Current file to get its hash</param>
        /// <returns>
        /// A strong hash (40 Letter) 
        /// </returns>
        public static string GetWeakHash40(this IFormFile Value)
        {
            return BitConverter.ToString(SHA1.Create().ComputeHash(Value.OpenReadStream())).Replace("-", string.Empty);
        }
    }
}
