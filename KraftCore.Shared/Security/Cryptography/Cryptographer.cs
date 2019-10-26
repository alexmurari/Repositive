namespace KraftCore.Shared.Security.Cryptography
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    ///     Provides methods for crypt and decrypt <see cref="string" /> objects.
    ///     This class cannot be inherited.
    /// </summary>
    internal sealed class Cryptographer : IDisposable
    {
        /// <summary>
        ///     The cipher algorithm.
        /// </summary>
        private readonly RijndaelManaged _cipher;

        /// <summary>
        ///     The secret key for the algorithm.
        /// </summary>
        private readonly byte[] _key;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Cryptographer" /> class with the specified password for the cypher
        ///     key.
        /// </summary>
        /// <param name="password">The password for the cypher.</param>
        internal Cryptographer(string password)
        {
            _cipher = new RijndaelManaged();
            password = Convert.ToBase64String(Encoding.UTF8.GetBytes(password.PadRight((password.Length + 3) & ~3, '=')));

            using (var sha = SHA256.Create())
            {
                _cipher.Key = _key = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _cipher?.Dispose();
        }

        /// <summary>
        ///     Encrypts the provided <see cref="string" /> object.
        /// </summary>
        /// <param name="cleanText">The <see cref="string" /> object with the text to be encrypted.</param>
        /// <returns>The <see cref="string" /> object with the encrypted text.</returns>
        internal string Encrypt(string cleanText)
        {
            using (_cipher)
            {
                var initVectorId = GetIvId(cleanText);
                _cipher.IV = GetIv(initVectorId);
                var buf = Encoding.UTF8.GetBytes(cleanText);

                using (var ms = new MemoryStream())
                {
                    ms.WriteByte(initVectorId);

                    using (var stream = new CryptoStream(ms, _cipher.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        stream.Write(buf, 0, buf.Length);
                    }

                    buf = ms.ToArray();
                    return Base64Encode(buf);
                }
            }
        }

        /// <summary>
        ///     Decrypts the provided <see cref="string" /> object.
        /// </summary>
        /// <param name="cypherText">The <see cref="string" /> object with the text to be decrypted.</param>
        /// <returns>The <see cref="string" /> object with the decrypted text.</returns>
        internal string Decrypt(string cypherText)
        {
            using (_cipher)
            {
                using (var ms = new MemoryStream(Base64Decode(cypherText)))
                {
                    var initVectorId = (byte)ms.ReadByte();
                    _cipher.IV = GetIv(initVectorId);

                    using (var result = new MemoryStream())
                    {
                        using (var stream = new CryptoStream(ms, _cipher.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            stream.CopyTo(result);
                        }

                        return Encoding.UTF8.GetString(result.ToArray());
                    }
                }
            }
        }

        /// <summary>
        ///     Sanitizes and encodes the provided <see cref="byte" /> array object to base-64 digits.
        /// </summary>
        /// <param name="data">The array containing the data to be encoded.</param>
        /// <returns>The base 64 encoded string.</returns>
        private static string Base64Encode(byte[] data)
        {
            return Convert.ToBase64String(data).Trim('=').Replace('+', '$').Replace('/', '_');
        }

        /// <summary>
        ///     Sanitizes and decodes the provided <see cref="string" /> object from base-64 digits.
        /// </summary>
        /// <param name="str">The base-64 encoded string.</param>
        /// <returns>The array containing the data.</returns>
        private static byte[] Base64Decode(string str)
        {
            str = str.Replace('$', '+').Replace('_', '/').PadRight((str.Length + 3) & ~3, '=');
            return Convert.FromBase64String(str);
        }

        /// <summary>
        ///     Extracts the initialization vector ID from the provided <see cref="string" /> object.
        /// </summary>
        /// <param name="str">The string containing the initialization vector.</param>
        /// <returns>The initialization vector id.</returns>
        private static byte GetIvId(string str)
        {
            var x = (byte)str[0];
            for (var i = 1; i < str.Length; i++)
                x = (byte)((x * 0x180) + (byte)str[i]);

            return x;
        }

        /// <summary>
        ///     Computes the initialization vector byte array using the provided ID.
        /// </summary>
        /// <param name="id">The initialization vector id value.</param>
        /// <returns>The initialization vector.</returns>
        private byte[] GetIv(byte id)
        {
            var iv = new byte[_cipher.BlockSize / 8];
            for (var i = 0; i < iv.Length; i++)
                iv[i] = (byte)(id ^ _key[i]);

            return iv;
        }
    }
}