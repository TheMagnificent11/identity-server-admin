using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;

namespace IdentityServer.Admin.Configuration
{
    public class KebabCaseJsonNamingPolicy : JsonNamingPolicy
    {
        [SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "Culture does not matter here")]
        [SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "Format does not matter here")]
        public override string ConvertName(string name)
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
