using Environmental_Monitoring.Model;
using System;

namespace Environmental_Monitoring.Controller
{
    public static class UserSession
    {
        public static Employee CurrentUser { get; private set; }

        public static void StartSession(Employee user)
        {
            CurrentUser = user;
        }

        public static void EndSession()
        {
            CurrentUser = null;
        }

        public static bool IsAdmin()
        {
            if (CurrentUser == null || CurrentUser.Role == null)
            {
                return false;
            }
            return CurrentUser.Role.RoleName.Equals("Admin", StringComparison.OrdinalIgnoreCase);
        }
    }
}