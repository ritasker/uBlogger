using System.Text;

namespace uBlogger.Infrastructure.Security
{
    using System.Security.Cryptography;
    using BCrypt.Net;
    
    public class HashingService
    {
        public string HashPassword(string password)
        {
            var hashString = GetSha256Hash(password);
            return BCrypt.HashPassword(hashString, GetRandomSalt());
        }

        public bool ValidatePassword(string password, string correctHash)
        {
            var hashString = GetSha256Hash(password);
            return BCrypt.Verify(hashString, correctHash);
        }

        private static string GetSha256Hash(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            SHA256 hashAlgorithm = SHA256.Create();
            byte[] hash = hashAlgorithm.ComputeHash(bytes);
            return Encoding.UTF8.GetString(hash);
        }

        private static string GetRandomSalt()
        {
            return BCrypt.GenerateSalt(4);
        }
    }
}