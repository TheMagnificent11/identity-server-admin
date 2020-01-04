﻿using System;

namespace IdentityServer4.Quickstart.UI
{
    public static class AccountOptions
    {
        public static bool AllowLocalLogin => true;

        public static bool AllowRememberLogin => true;

        public static TimeSpan RememberMeLoginDuration => TimeSpan.FromDays(30);

        public static bool ShowLogoutPrompt => true;

        public static bool AutomaticRedirectAfterSignOut => false;

        // specify the Windows authentication scheme being used
        public static string WindowsAuthenticationSchemeName => Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme;

        // if user uses windows auth, should we load the groups from windows
        public static bool IncludeWindowsGroups => false;

        public static string InvalidCredentialsErrorMessage => "Invalid username or password";
    }
}
