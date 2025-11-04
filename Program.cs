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

            // --- CẬP NHẬT NGÔN NGỮ (Code gốc của bạn) ---
            string cultureName = "vi";
            string savedLanguage = Properties.Settings.Default.Language;
            bool settingsNeedSave = false; // Biến cờ để chỉ Save 1 lần

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

            // --- THÊM MỚI: ÁP DỤNG GIAO DIỆN KHI KHỞI ĐỘNG ---
            string savedTheme = Properties.Settings.Default.Theme;
            if (string.IsNullOrEmpty(savedTheme))
            {
                savedTheme = "light"; // Đặt Sáng ("light") làm mặc định
                Properties.Settings.Default.Theme = savedTheme;
                settingsNeedSave = true;
            }

            // Lưu lại các thay đổi mặc định (nếu có)
            if (settingsNeedSave)
            {
                Properties.Settings.Default.Save();
            }

            // Gọi hàm từ class ThemeManager để set màu
            ThemeManager.ApplyTheme(savedTheme);
            // -----------------------------------------------

            // Chạy Mainlayout (Code gốc của bạn)
            Application.Run(new Mainlayout());
        }
    }
}