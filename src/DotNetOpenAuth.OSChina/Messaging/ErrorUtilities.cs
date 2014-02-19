using System;
using System.Globalization;

namespace DotNetOpenAuth.Messaging
{
    public static class ErrorUtilities
    {
        internal static void VerifyArgument(bool condition, string message, params object[] args)
        {
            Requires.NotNull(args, "args");
            if (!condition)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, message, args));
            }
        }
    }
}