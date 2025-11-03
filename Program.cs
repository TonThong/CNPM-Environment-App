using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

using Environmental_Monitoring.View;

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
            https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            string cultureName = "vi";
            string savedLanguage = Properties.Settings.Default.Language;
            bool settingsNeedSave = false;

            if (string.IsNullOrEmpty(savedLanguage))
            {
                Properties.Settings.Default.Language = "Tiếng Việt";
                cultureName = "vi";
                settingsNeedSave = true;
            }
            else if (savedLanguage == "English")
            {
                cultureName = "en";
            }
            else
            {
                cultureName = "vi";
            }

            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            string savedTheme = Properties.Settings.Default.Theme;
            if (string.IsNullOrEmpty(savedTheme))
            {
                savedTheme = "light"; 
                Properties.Settings.Default.Theme = savedTheme;
                settingsNeedSave = true;
            }

            if (settingsNeedSave)
            {
                Properties.Settings.Default.Save();
            }

            ThemeManager.ApplyTheme(savedTheme);
            Application.Run(new Login());
        }
    }
}