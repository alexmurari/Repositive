namespace KraftCore.Shared.Security.Hash
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    ///     Provides static methods to compute and verify hash values.
    /// </summary>
    public static class Hasher
    {
        /// <summary>
        ///     The random number generator.
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        ///     Computes the hash of the provided string.
        ///     A salt byte array is generated if not provided.
        /// </summary>
        /// <param name="plainText">The value to be computed.</param>
        /// <param name="saltBytes">The salt bytes.</param>
        /// <returns>The computed hash.</returns>
        public static string ComputeHash(string plainText, byte[] saltBytes = null)
        {
            // If salt is not specified, generate it.
            if (saltBytes == null)
            {
                // Generate a random number for the size of the salt.
                var saltSize = Random.Next(0x10, 0x20);

                // Allocate a byte array, which will hold the salt.
                saltBytes = new byte[saltSize];

                // Initialize a random number generator.
                using (var rng = new RNGCryptoServiceProvider())
                {
                    // Fill the salt with cryptographically strong byte values.
                    rng.GetNonZeroBytes(saltBytes);
                }
            }

            // Convert plain text into a byte array.
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Allocate array, which will hold plain text and salt.
            var plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            Array.Copy(plainTextBytes, plainTextWithSaltBytes, plainTextBytes.Length);

            // Append salt bytes to the resulting array.
            Array.Copy(saltBytes, 0, plainTextWithSaltBytes, plainTextBytes.Length, saltBytes.Length);

            // Compute hash value of our plain text with appended salt.
            using (var sha256Managed = new SHA256Managed())
            {
                var hashBytes = sha256Managed.ComputeHash(plainTextWithSaltBytes);

                // Create array which will hold hash and original salt bytes.
                var hashWithSaltBytes = new byte[hashBytes.Length + saltBytes.Length];

                // Copy hash bytes into resulting array.
                Array.Copy(hashBytes, hashWithSaltBytes, hashBytes.Length);

                // Append salt bytes to the result.
                Array.Copy(saltBytes, 0, hashWithSaltBytes, hashBytes.Length, saltBytes.Length);

                // Convert result into a base64-encoded string.
                var hashValue = Convert.ToBase64String(hashWithSaltBytes);

                // Return the result.
                return hashValue;
            }
        }

        /// <summary>
        ///     Computes and compares the hash value of the provided plain text string with the provided hash value string.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <param name="hashValue">The hash value.</param>
        /// <returns>True if the computed hash matches the provided hash; otherwise, false.</returns>
        public static bool VerifyHash(string plainText, string hashValue)
        {
            // Convert base64-encoded hash value into a byte array.
            var hashWithSaltBytes = Convert.FromBase64String(hashValue);

            using (var sha256Managed = new SHA256Managed())
            {
                // We must know size of hash (without salt) in bytes.
                var hashSizeInBytes = sha256Managed.HashSize / 8; // Bits -> divide by 8 -> bytes. :)

                // Make sure that the specified hash value is long enough.
                if (hashWithSaltBytes.Length < hashSizeInBytes)
                    return false;

                // Allocate array to hold original salt bytes retrieved from hash.
                var saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

                // Copy salt from the end of the hash to the new array.
                Array.Copy(hashWithSaltBytes, hashSizeInBytes, saltBytes, 0, saltBytes.Length);

                // Compute a new hash string.
                var expectedHashString = ComputeHash(plainText, saltBytes);

                // If the computed hash matches the specified hash,
                // the plain text value must be correct.
                return hashValue.Equals(expectedHashString);
            }
        }
    }
}