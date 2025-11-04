using System.Drawing; // Đảm bảo System.Drawing được import
using System.IO;     // THÊM MỚI: Để xử lý đường dẫn file

namespace Environmental_Monitoring
{
    public static class ThemeManager
    {
        // --- Màu Sắc Chung ---
        public static Color BackgroundColor { get; private set; }
        public static Color PanelColor { get; private set; }
        public static Color TextColor { get; private set; }
        public static Color SecondaryTextColor { get; private set; }
        public static Color AccentColor { get; private set; }

        // --- THÊM MỚI: Thuộc tính cho ảnh nền ---
        public static Image BackgroundImage { get; private set; }
        public static Image MainMenuLogo { get; private set; } // Nếu logo cũng đổi

        // --- Màu Chủ Đề Sáng (Light Mode) ---
        private static readonly Color light_Background = Color.FromArgb(236, 246, 239);
        private static readonly Color light_Panel = Color.White;
        private static readonly Color light_Text = Color.Black;
        private static readonly Color light_SecondaryText = Color.FromArgb(100, 100, 100);
        private static readonly Color light_Accent = Color.FromArgb(21, 137, 81);
        private static readonly Image light_BackgroundImage = Properties.Resources.lightmode; // Không có ảnh nền cho Light Mode
        private static readonly Image light_MainMenuLogo = Properties.Resources.d39e375b17e2b47022e116931a9df1af13e4f774; // Giả sử đây là logo sáng

        // --- Màu Chủ Đề Tối (Dark Mode) ---
        private static readonly Color dark_Background = Color.FromArgb(30, 30, 30);
        private static readonly Color dark_Panel = Color.FromArgb(50, 50, 50);
        private static readonly Color dark_Text = Color.White;
        private static readonly Color dark_SecondaryText = Color.FromArgb(180, 180, 180);
        private static readonly Color dark_Accent = Color.FromArgb(34, 191, 112);

        // --- CẬP NHẬT: THAY THẾ bằng ảnh nền Dark Mode của bạn ---
        // Đảm bảo bạn đã thêm Image 3 vào Resources của project.
        // Giả sử tên trong Resources là 'dark_background_image'
        private static readonly Image dark_BackgroundImage = Properties.Resources.darkmode;
        //private static readonly Image dark_MainMenuLogo = Properties.Resources.greenflow_logo_dark; // Nếu có logo darkmode

        /// <summary>
        /// Hàm này được gọi khi khởi động app và khi lưu Setting
        /// </summary>
        public static void ApplyTheme(string themeName)
        {
            if (themeName == "dark")
            {
                BackgroundColor = dark_Background;
                PanelColor = dark_Panel;
                TextColor = dark_Text;
                SecondaryTextColor = dark_SecondaryText;
                AccentColor = dark_Accent;
                BackgroundImage = dark_BackgroundImage; // Áp dụng ảnh nền tối
                //MainMenuLogo = dark_MainMenuLogo; // Áp dụng logo tối
            }
            else // Mặc định là SÁNG
            {
                BackgroundColor = light_Background;
                PanelColor = light_Panel;
                TextColor = light_Text;
                SecondaryTextColor = light_SecondaryText;
                AccentColor = light_Accent;
                BackgroundImage = light_BackgroundImage; // Không có ảnh nền cho sáng
                MainMenuLogo = light_MainMenuLogo; // Áp dụng logo sáng
            }
        }
    }
}