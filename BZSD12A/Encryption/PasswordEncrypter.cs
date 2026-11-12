using BZSD12A.Encryption;
using BZSD12A.Services;
using Konscious.Security.Cryptography;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BZSD12A.Encryption
{
    public static class PasswordEncrypter
    {
            private const int SaltSize = 16; // 128 bit
            private const int HashSize = 32; // 256 bit

            public static string HashPassword(string password)
            {
                byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
                byte[] hash = ComputeArgon2Hash(password, salt);

                return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
            }

            public static bool VerifyPassword(string password, string storedHash)
            {
                string[] parts = storedHash.Split(':');
                if (parts.Length != 2) return false;

                byte[] salt = Convert.FromBase64String(parts[0]);
                byte[] originalHash = Convert.FromBase64String(parts[1]);

                byte[] newHash = ComputeArgon2Hash(password, salt);

                return CryptographicOperations.FixedTimeEquals(originalHash, newHash);
            }

            private static byte[] ComputeArgon2Hash(string password, byte[] salt)
            {
                using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
                argon2.Salt = salt;
                argon2.DegreeOfParallelism = 4; // Hány CPU szálat használjon
                argon2.MemorySize = 65536;      // 64 MB RAM használata (GPU támadások ellen)
                argon2.Iterations = 3;          // Futások száma

                return argon2.GetBytes(HashSize);
            }
    }
}
