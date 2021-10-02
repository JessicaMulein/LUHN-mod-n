namespace BrightChain.Engine.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// LUHN Modulo N / variable base for C#.
    /// Tested in base 10 and 16.
    /// Improved from https://stackoverflow.com/a/23640453.
    /// </summary>
    public static class CheckDigitExtension
    {
        const int MAX_BASE = 16;

        static int[][] ResultsCacheByBase = new int[MAX_BASE + 1][];

        static int[] NewResultInitializationVectorBuffer(int stringBase = 10)
        {
            var cachedResult = ResultsCacheByBase[stringBase];
            if (cachedResult is not null)
            {
                return cachedResult;
            }

            int[] result = new int[stringBase];
            int offset = 0;
            for (int i = 0; i < stringBase; i += 2)
            {
                result[offset++] = i;
            }

            for (int i = 1; i < stringBase; i += 2)
            {
                result[offset++] = i;
            }

            ResultsCacheByBase[stringBase] = result;

            return result;
        }

        #region extension methods for IList<int>

        /// <summary>
        /// For a list of digits, compute the ending checkdigit 
        /// </summary>
        /// <param name="digits">The list of digits for which to compute the check digit</param>
        /// <returns>the check digit</returns>
        public static int CheckDigit(this IList<int> digits, int stringBase = 10)
        {
            var results = NewResultInitializationVectorBuffer(stringBase: stringBase);
            var i = 0;
            var lengthMod = digits.Count % 2;
            return (digits.Sum(d => i++ % 2 == lengthMod ? d : results[d]) * (stringBase - 1)) % stringBase;
        }

        /// <summary>
        /// Return a list of digits including the checkdigit
        /// </summary>
        /// <param name="digits">The original list of digits</param>
        /// <returns>the new list of digits including checkdigit</returns>
        public static IList<int> AppendCheckDigit(this IList<int> digits, int stringBase = 10)
        {
            var result = new List<int>(digits);
            result.Add(result.CheckDigit(stringBase: stringBase));
            return result;
        }

        /// <summary>
        /// Returns true when a list of digits has a valid checkdigit
        /// </summary>
        /// <param name="digits">The list of digits to check</param>
        /// <returns>true/false depending on valid checkdigit</returns>
        public static bool HasValidCheckDigit(this IList<int> digits, int stringBase = 10)
        {
            return digits.Last() == CheckDigit(
                digits: digits.Take(digits.Count - 1).ToList(),
                stringBase: stringBase);
        }

        #endregion extension methods for IList<int>

        #region extension methods for strings

        /// <summary>
        /// Internal conversion function to convert string into a list of ints
        /// </summary>
        /// <param name="digits">the original string</param>
        /// <returns>the list of ints</returns>
        private static IList<int> ToDigitList(this string digits, int stringBase = 10)
        {
            return digits.Select(
                d => Convert.ToInt32(
                    value: d.ToString(),
                    fromBase: stringBase))
                        .ToList();
        }

        /// <summary>
        /// For a string of digits, compute the ending checkdigit 
        /// </summary>
        /// <param name="digits">The string of digits for which to compute the check digit</param>
        /// <returns>the check digit</returns>
        public static string CheckDigit(this string digits, int stringBase = 10)
        {
            return Convert.ToString(
                value: digits
                    .ToDigitList(stringBase: stringBase)
                    .CheckDigit(stringBase: stringBase),
                toBase: stringBase);
        }

        /// <summary>
        /// Return a string of digits including the checkdigit
        /// </summary>
        /// <param name="digits">The original string of digits</param>
        /// <returns>the new string of digits including checkdigit</returns>
        public static string AppendCheckDigit(this string digits, int stringBase = 10)
        {
            return digits + digits.CheckDigit(stringBase: stringBase);
        }

        /// <summary>
        /// Returns true when a string of digits has a valid checkdigit
        /// </summary>
        /// <param name="digits">The string of digits to check</param>
        /// <returns>true/false depending on valid checkdigit</returns>
        public static bool HasValidCheckDigit(this string digits, int stringBase = 10)
        {
            return digits
                .ToDigitList(stringBase: stringBase)
                .HasValidCheckDigit(stringBase: stringBase);
        }

        #endregion extension methods for strings

        #region extension methods for integers

        /// <summary>
        /// Internal conversion function to convert int into a list of ints, one for each digit
        /// </summary>
        /// <param name="digits">the original int</param>
        /// <returns>the list of ints</returns>
        private static IList<int> ToDigitList(this int digits, int stringBase = 10)
        {
            return Convert.ToString(
                value: digits,
                toBase: stringBase)
                    .Select(
                        d => Convert.ToInt32(
                            value: d.ToString(),
                            fromBase: stringBase)).ToList();
        }

        /// <summary>
        /// For an integer, compute the ending checkdigit 
        /// </summary>
        /// <param name="digits">The integer for which to compute the check digit</param>
        /// <returns>the check digit</returns>
        public static int CheckDigit(this int digits, int stringBase = 10)
        {
            return digits
                .ToDigitList(stringBase: stringBase)
                .CheckDigit(stringBase: stringBase);
        }

        /// <summary>
        /// Return an integer including the checkdigit
        /// </summary>
        /// <param name="digits">The original integer</param>
        /// <returns>the new integer including checkdigit</returns>
        public static int AppendCheckDigit(this int digits, int stringBase = 10)
        {
            return digits * stringBase + digits.CheckDigit(stringBase: stringBase);
        }

        /// <summary>
        /// Returns true when an integer has a valid checkdigit
        /// </summary>
        /// <param name="digits">The integer to check</param>
        /// <returns>true/false depending on valid checkdigit</returns>
        public static bool HasValidCheckDigit(this int digits, int stringBase = 10)
        {
            return digits
                .ToDigitList(stringBase: stringBase)
                .HasValidCheckDigit(stringBase: stringBase);
        }

        #endregion extension methods for integers

        #region extension methods for int64s

        /// <summary>
        /// Internal conversion function to convert int into a list of ints, one for each digit
        /// </summary>
        /// <param name="digits">the original int</param>
        /// <returns>the list of ints</returns>
        private static IList<int> ToDigitList(this Int64 digits, int stringBase = 10)
        {
            return Convert.ToString(
                value: digits,
                toBase: stringBase)
                    .Select(
                        d => Convert.ToInt32(
                            value: d.ToString(),
                            fromBase: stringBase)).ToList();
        }

        /// <summary>
        /// For an integer, compute the ending checkdigit 
        /// </summary>
        /// <param name="digits">The integer for which to compute the check digit</param>
        /// <returns>the check digit</returns>
        public static int CheckDigit(this Int64 digits, int stringBase = 10)
        {
            return digits
                .ToDigitList(stringBase: stringBase)
                .CheckDigit(stringBase: stringBase);
        }

        /// <summary>
        /// Return an integer including the checkdigit
        /// </summary>
        /// <param name="digits">The original integer</param>
        /// <returns>the new integer including checkdigit</returns>
        public static Int64 AppendCheckDigit(this Int64 digits, int stringBase = 10)
        {
            return digits * stringBase + digits.CheckDigit(stringBase: stringBase);
        }

        /// <summary>
        /// Returns true when an integer has a valid checkdigit
        /// </summary>
        /// <param name="digits">The integer to check</param>
        /// <returns>true/false depending on valid checkdigit</returns>
        public static bool HasValidCheckDigit(this Int64 digits, int stringBase = 10)
        {
            return digits
                .ToDigitList(stringBase: stringBase)
                .HasValidCheckDigit(stringBase: stringBase);
        }

        #endregion extension methods for int64s
    }
}
