using System;
using System.Security.Cryptography;

namespace XSquareCalculationsApi.Models
{
    public interface IPassword
    {
        string CreatePasswordHashBase64(string salt, string password);
        string CreateSaltBase64();
    }

    public class Password : IPassword
    {
        private readonly int saltSize = 32;
        private readonly int hashSize = 32;
        private readonly int iteration = 10000;

        public string CreatePasswordHashBase64(string salt, string password)
        {
            var saltBytes = Convert.FromBase64String(salt);
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, iteration))
            {
                var pBKDF2Hash = rfc2898DeriveBytes.GetBytes(hashSize);
                return Convert.ToBase64String(pBKDF2Hash);
            };
        }

        public string CreateSaltBase64()
        {
            var bytes = new byte[saltSize];
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetBytes(bytes);
            }

            return Convert.ToBase64String(bytes);
        }
    }
}
