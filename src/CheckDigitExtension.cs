using System.Globalization;

#nullable enable
namespace BrightChain.Engine.Extensions;

/// <summary>
///     LUHN Modulo N / variable base for C#.
///     Tested in base 10 and 16.
///     Improved from https://stackoverflow.com/a/23640453.
/// </summary>
public static class CheckDigitExtension
{
    private const int MinBase = 2;
    private const int MaxBase = 16;

    private static void CheckBase(int value)
    {
        if (value is < MinBase or > MaxBase)
            throw new ArgumentOutOfRangeException(paramName: nameof(value),
                message: $"Base must be between {MinBase} and {MaxBase}.");
    }

    private static int[] NewResultInitializationVectorBuffer(int digitsBase = 10)
    {
        CheckBase(value: digitsBase);
        var result = new int[digitsBase];
        var offset = 0;
        for (var i = 0; i < digitsBase; i += 2) result[offset++] = i;

        for (var i = 1; i < digitsBase; i += 2) result[offset++] = i;

        return result;
    }

    #region extension methods for IList<int>

    /// <summary>
    ///     For a list of digits, compute the ending checkdigit
    /// </summary>
    /// <param name="digits">The list of digits for which to compute the check digit</param>
    /// <param name="digitsBase"></param>
    /// <returns>the check digit</returns>
    public static int CheckDigit(this IList<int> digits, int digitsBase = 10)
    {
        CheckBase(value: digitsBase);
        var results = NewResultInitializationVectorBuffer(digitsBase: digitsBase);
        var i = 0;
        var lengthMod = digits.Count % 2;
        return digits.Sum(selector: d => i++ % 2 == lengthMod ? d : results[d]) * (digitsBase - 1) % digitsBase;
    }

    /// <summary>
    ///     Return a list of digits including the check-digit
    /// </summary>
    /// <param name="digits">The original list of digits</param>
    /// <param name="digitsBase"></param>
    /// <returns>the new list of digits including check-digit</returns>
    public static IList<int> AppendCheckDigit(this IList<int> digits, int digitsBase = 10)
    {
        CheckBase(value: digitsBase);
        var result = new List<int>(collection: digits);
        result.Add(item: result.CheckDigit(digitsBase: digitsBase));
        return result;
    }

    /// <summary>
    ///     Returns true when a list of digits has a valid check-digit
    /// </summary>
    /// <param name="digits">The list of digits to check</param>
    /// <param name="digitsBase"></param>
    /// <returns>true/false depending on valid check-digit</returns>
    public static bool HasValidCheckDigit(this IList<int> digits, int digitsBase = 10)
    {
        CheckBase(value: digitsBase);
        return digits.Last() == CheckDigit(
            digits: digits.Take(count: digits.Count - 1).ToList(),
            digitsBase: digitsBase);
    }

    #endregion extension methods for IList<int>

    #region extension methods for strings

    /// <summary>
    ///     Internal conversion function to convert string into a list of ints
    /// </summary>
    /// <param name="digits">the original string</param>
    /// <param name="digitsBase"></param>
    /// <returns>the list of ints</returns>
    private static IList<int> ToDigitList(this string digits, int digitsBase = 10)
    {
        CheckBase(value: digitsBase);
        return digits.Select(
                selector: d => Convert.ToInt32(
                    value: d.ToString(),
                    fromBase: digitsBase))
            .ToList();
    }

    /// <summary>
    ///     For a string of digits, compute the ending check-digit
    /// </summary>
    /// <param name="digits">The string of digits for which to compute the check digit</param>
    /// <param name="digitsBase"></param>
    /// <returns>the check digit</returns>
    public static string CheckDigit(this string digits, int digitsBase = 10)
    {
        CheckBase(value: digitsBase);
        return Convert.ToString(
            value: digits
                .ToDigitList(digitsBase: digitsBase)
                .CheckDigit(digitsBase: digitsBase),
            toBase: digitsBase);
    }

    /// <summary>
    ///     Return a string of digits including the check-digit
    /// </summary>
    /// <param name="digits">The original string of digits</param>
    /// <param name="digitsBase"></param>
    /// <returns>the new string of digits including check-digit</returns>
    public static string AppendCheckDigit(this string digits, int digitsBase = 10)
    {
        CheckBase(value: digitsBase);
        return digits + digits.CheckDigit(digitsBase: digitsBase);
    }

    /// <summary>
    ///     Returns true when a string of digits has a valid check-digit
    /// </summary>
    /// <param name="digits">The string of digits to check</param>
    /// <param name="digitsBase"></param>
    /// <returns>true/false depending on valid check-digit</returns>
    public static bool HasValidCheckDigit(this string digits, int digitsBase = 10)
    {
        CheckBase(value: digitsBase);
        return digits
            .ToDigitList(digitsBase: digitsBase)
            .HasValidCheckDigit(digitsBase: digitsBase);
    }

    #endregion extension methods for strings

    #region extension methods for integers

    /// <summary>
    ///     Internal conversion function to convert int into a list of ints, one for each digit
    /// </summary>
    /// <param name="digits">the original int</param>
    /// <param name="digitsBase"></param>
    /// <returns>the list of ints</returns>
    private static IList<int> ToDigitList(this int digits, int digitsBase = 10)
    {
        CheckBase(value: digitsBase);
        return digits.ToString(
                format: $"{digits.ToString()}",
                provider: CultureInfo.InvariantCulture)
            .ToDigitList(digitsBase: digitsBase);
    }

    /// <summary>
    ///     For an integer, compute the ending check-digit
    /// </summary>
    /// <param name="digits">The integer for which to compute the check digit</param>
    /// <param name="digitsBase"></param>
    /// <returns>the check digit</returns>
    public static int CheckDigit(this int digits, int digitsBase = 10)
    {
        CheckBase(value: digitsBase);
        return digits
            .ToDigitList(digitsBase: digitsBase)
            .CheckDigit(digitsBase: digitsBase);
    }

    /// <summary>
    ///     Return an integer including the check-digit
    /// </summary>
    /// <param name="digits">The original integer</param>
    /// <param name="digitsBase"></param>
    /// <returns>the new integer including check-digit</returns>
    public static int AppendCheckDigit(this int digits, int digitsBase = 10)
    {
        CheckBase(value: digitsBase);
        return digits * digitsBase + digits.CheckDigit(digitsBase: digitsBase);
    }

    /// <summary>
    ///     Returns true when an integer has a valid check-digit
    /// </summary>
    /// <param name="digits">The integer to check</param>
    /// <param name="digitsBase"></param>
    /// <returns>true/false depending on valid check-digit</returns>
    public static bool HasValidCheckDigit(this int digits, int digitsBase = 10)
    {
        CheckBase(value: digitsBase);
        return digits
            .ToDigitList(digitsBase: digitsBase)
            .HasValidCheckDigit(digitsBase: digitsBase);
    }

    #endregion extension methods for integers

    #region extension methods for int64s

    /// <summary>
    ///     Internal conversion function to convert int into a list of ints, one for each digit
    /// </summary>
    /// <param name="digits">the original int</param>
    /// <param name="digitsBase"></param>
    /// <returns>the list of ints</returns>
    private static IList<int> ToDigitList(this long digits, int digitsBase = 10)
    {
        CheckBase(value: digitsBase);
        var resultDigits = new List<int>();
        while (digits > 0)
        {
            resultDigits.Add(item: (int) (digits % digitsBase));
            digits /= digitsBase;
        }

        return resultDigits;
    }

    /// <summary>
    ///     For an integer, compute the ending check-digit
    /// </summary>
    /// <param name="digits">The integer for which to compute the check digit</param>
    /// <param name="digitsBase"></param>
    /// <returns>the check digit</returns>
    public static int CheckDigit(this long digits, int digitsBase = 10)
    {
        CheckBase(value: digitsBase);
        return digits
            .ToDigitList(digitsBase: digitsBase)
            .CheckDigit(digitsBase: digitsBase);
    }

    /// <summary>
    ///     Return an integer including the check-digit
    /// </summary>
    /// <param name="digits">The original integer</param>
    /// <param name="digitsBase"></param>
    /// <returns>the new integer including check-digit</returns>
    public static long AppendCheckDigit(this long digits, int digitsBase = 10)
    {
        CheckBase(value: digitsBase);
        return digits * digitsBase + digits.CheckDigit(digitsBase: digitsBase);
    }

    /// <summary>
    ///     Returns true when an integer has a valid check-digit
    /// </summary>
    /// <param name="digits">The integer to check</param>
    /// <param name="digitsBase"></param>
    /// <returns>true/false depending on valid check-digit</returns>
    public static bool HasValidCheckDigit(this long digits, int digitsBase = 10)
    {
        CheckBase(value: digitsBase);
        return digits
            .ToDigitList(digitsBase: digitsBase)
            .HasValidCheckDigit(digitsBase: digitsBase);
    }

    #endregion extension methods for int64s
}