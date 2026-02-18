using shortid;
using shortid.Configuration;

namespace TelegramBot.Application.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Generates a random URL‑safe string of letters and/or digits.
        /// </summary>
        /// <param name="length">Length of the generated string (8–15).</param>
        /// <param name="useDigits">Include digits.</param>
        /// <param name="useSpecialCharacters">Include _ - ~ . ! @ $ % ^ & * ( ) + = ? </param>
        /// <returns>Random string.</returns>
        public static string GenerateRandomId(
            int length = 10,
            bool useDigits = true, bool useSpecialCharacters = true)
        {
            var options = new GenerationOptions(useDigits, useSpecialCharacters, length);

            return ShortId.Generate(options);
        }

        public static string? GetStartReferral(this string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return null;

            // "/start" or "/start ref_xxx"
            var parts = message.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
                return null;

            return parts[1];
        }

    }

}
