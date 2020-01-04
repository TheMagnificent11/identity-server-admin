using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace IdentityServer.Admin.Configuration
{
    public static class KebabCaseUtil
    {
        [SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "Culture does not matter here")]
        [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "Format does not matter here")]
        public static string ToKebabCase(this string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return string
                .Concat(name.Select(
                    (x, i) => i > 0 && char.IsUpper(x) ? $"-{x.ToString()}" : x.ToString()))
                .ToLower();
        }
    }
}
