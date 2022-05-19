namespace Hairzone.CORE.Helpers;

internal static class PasswordHelper
{
    const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
    const string UPPER_CAES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string NUMBERS = "123456789";
    const string SPECIALS = @"!@£$%^&*()#€";

    public static string GeneratePassword(
        bool useLowercase = true, bool useUppercase = true,
        bool useNumbers = true, bool useSpecial = true,
        int passwordSize = 16)
    {
        char[] password = new char[passwordSize];
        string charSet = "";
        Random random = new Random();

        // Build up the character set to choose from
        if (useLowercase) charSet += LOWER_CASE;
        if (useUppercase) charSet += UPPER_CAES;
        if (useNumbers) charSet += NUMBERS;
        if (useSpecial) charSet += SPECIALS;

        foreach (var index in Enumerable.Range(0, passwordSize))
        {
            password[index] = charSet[random.Next(charSet.Length - 1)];
        }

        return string.Join(null, password);
    }
}
