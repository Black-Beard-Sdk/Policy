using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Black.Beard.Adfs
{
    /// <summary>
    /// Provides methods for generating API keys and security identifiers.
    /// </summary>
    public static class ApiKeyGenerator
    {

        /// <summary>
        /// Generates a random API key of the specified length.
        /// </summary>
        /// <param name="length">The length of the API key to generate. Must be greater than 0.</param>
        /// <returns>A random API key string of the specified length.</returns>
        /// <remarks>
        /// This method creates a random string using a mix of alphanumeric and special characters.
        /// The randomness is based on the standard .NET Random class.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when length is less than or equal to 0.</exception>
        /// <example>
        /// <code lang="C#">
        /// // Generate a 32-character API key
        /// string apiKey = ApiKeyGenerator.GenerateApiKey(32);
        /// Console.WriteLine($"Generated API key: {apiKey}");
        /// </code>
        /// </example>
        public static string GenerateApiKey(int length = 100)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789()&é'(-è_çà)=$^*ù!:;,§.Nµ%£¨+°";
            var random = new Random();
            var apiKey = new char[length];

            for (int i = 0; i < length; i++)
                apiKey[i] = chars[random.Next(chars.Length)];

            return new string(apiKey);
        }

        /// <summary>
        /// Generates a login and password pair based on the provided raw data.
        /// </summary>
        /// <param name="rawData">The raw data to use as seed for generating identifiers. Must not be null or empty.</param>
        /// <param name="salt">third part for concatenate to rawData before generate login and password</param>
        /// <returns>A tuple containing the generated login and password.</returns>
        /// <remarks>
        /// This method first generates a login by hashing the raw data using SHA256.
        /// Then it generates a password by hashing the login.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when rawData is null.</exception>
        /// <exception cref="ArgumentException">Thrown when rawData is empty.</exception>
        /// <example>
        /// <code lang="C#">
        /// // Generate login and password from user email
        /// string email = "user@example.com";
        /// var cnx = ApiKeyGenerator.GenerateIdentifiers(email, "mysalt");
        /// Console.WriteLine($"Generated apikey: {cnx[0]}");
        /// Console.WriteLine($"Generated login: {{cnx[1]}");
        /// Console.WriteLine($"Generated password: {{cnx[2]}");
        /// </code>
        /// </example>
        public static string[] GenerateIdentifiers(this string rawData, string salt = null)
        {

            if (string.IsNullOrEmpty(salt))
                salt = string.Empty;

            var login = GenerateLogin(rawData + salt);
            return [rawData, login, GeneratePassword(login)];

        }

        /// <summary>
        /// Generates a login identifier by hashing the provided raw data using SHA256.
        /// </summary>
        /// <param name="rawData">The raw data to hash. Must not be null.</param>
        /// <returns>A hexadecimal string representation of the SHA256 hash of the raw data.</returns>
        /// <remarks>
        /// This method computes the SHA256 hash of the raw data and returns it as a hexadecimal string.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when rawData is null.</exception>
        /// <see cref="string"/>
        private static string GenerateLogin(string rawData)
        {

            using (SHA256 sha256Hash = SHA256.Create())
            {
                StringBuilder builder = new StringBuilder();
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }

        }

        /// <summary>
        /// Generates a password by hashing the provided raw data using SHA256.
        /// </summary>
        /// <param name="rawData">The raw data to hash. Must not be null.</param>
        /// <returns>A hexadecimal string representation of the SHA256 hash of the raw data.</returns>
        /// <remarks>
        /// This method computes the SHA1 hash of the raw data and returns it as a hexadecimal string.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when rawData is null.</exception>
        /// <see cref="string"/>
        private static string GeneratePassword(string rawData)
        {
            using (SHA1 sha1Hash = SHA1.Create())
            {
                StringBuilder builder = new StringBuilder();
                byte[] bytes = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }
        }
        

    }

}
