using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using Environmental_Monitoring.View; // Đảm bảo using này
using Environmental_Monitoring.Controller; // Thêm using này cho ThemeManager

namespace Environmental_Monitoring
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Dòng này dành cho .NET 6+
            ApplicationConfiguration.Initialize();

            // === LOGIC NGÔN NGỮ ===
            string cultureName;
            string savedLanguage = Properties.Settings.Default.Language;
            bool settingsNeedSave = false;

            if (string.IsNullOrEmpty(savedLanguage))
            {
                cultureName = "vi";
                Properties.Settings.Default.Language = cultureName;
                settingsNeedSave = true;
            }
            else if (savedLanguage == "en" || savedLanguage == "vi")
            {
                cultureName = savedLanguage;
            }
            else
            {
                cultureName = "vi";
                Properties.Settings.Default.Language = cultureName;
                settingsNeedSave = true;
            }

            try
            {
                CultureInfo culture = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            catch (Exception)
            {
                CultureInfo culture = new CultureInfo("vi");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            string savedTheme = Properties.Settings.Default.Theme;
            if (string.IsNullOrEmpty(savedTheme))
            {
                savedTheme = "light";
                Properties.Settings.Default.Theme = savedTheme;
                settingsNeedSave = true;
            }
            ThemeManager.ApplyTheme(savedTheme);
            if (settingsNeedSave)
            {
                Properties.Settings.Default.Save();
            }

            Application.Run(new Login());
        }
    }
}