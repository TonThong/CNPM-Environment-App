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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            string cultureName = "vi"; 
       
            string savedLanguage = Properties.Settings.Default.Language;

            if (string.IsNullOrEmpty(savedLanguage))
            {
             
                Properties.Settings.Default.Language = "Tiếng Việt";
                Properties.Settings.Default.Save();
                cultureName = "vi";
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
            Application.Run(new Mainlayout());
        }
    }
}