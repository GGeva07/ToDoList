using System.Security.Cryptography;
using System.Text;

namespace ToDoListAPI.Core.Application.Helpers
{
    public static class Auth
    {
        public static string Hash(string pass, out string salt)
        {
            salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
            var pbkdf2 = new Rfc2898DeriveBytes(pass, Convert.FromBase64String(salt), 100_000, HashAlgorithmName.SHA256);
            return Convert.ToBase64String(pbkdf2.GetBytes(32));
        }

        public static bool Verify(string pass, string hash, string salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(
                pass,
                Convert.FromBase64String(salt),
                100_000,
                HashAlgorithmName.SHA256
            );
            var pass2 = Convert.ToBase64String(pbkdf2.GetBytes(32));
            return hash == pass2;
        }
    }
}
