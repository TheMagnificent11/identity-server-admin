using System;
using System.Text.Json;

namespace IdentityServer.Admin.Configuration
{
    public class KebabCaseJsonNamingPolicy : JsonNamingPolicy
    {
        public static KebabCaseJsonNamingPolicy Instance => new KebabCaseJsonNamingPolicy();

        public override string ConvertName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return name.ToKebabCase();
        }
    }
}
