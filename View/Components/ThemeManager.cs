using System.Drawing;
using System.IO;


namespace Environmental_Monitoring
{
    
        public static class ThemeManager
        {
            // === Light Mode ===
            private static Color light_Background = Color.FromArgb(240, 245, 240); // Màu nền xanh lá rất nhạt
            private static Color light_Panel = Color.White; // Màu panel trắng
            private static Color light_SecondaryPanel = Color.FromArgb(230, 230, 230); // Màu xám nhạt cho header
            private static Color light_Text = Color.Black;
            private static Color light_SecondaryText = Color.FromArgb(64, 64, 64);
            private static Color light_Accent = Color.FromArgb(42, 165, 90); // Màu xanh lá cây đậm (Green Flow)
            private static Color light_Border = Color.LightGray;

            // === Dark Mode ===
            private static Color dark_Background = Color.FromArgb(30, 30, 30); // Màu nền tối
            private static Color dark_Panel = Color.FromArgb(45, 45, 48); // Màu panel tối
            private static Color dark_SecondaryPanel = Color.FromArgb(60, 60, 60); // Màu header tối
            private static Color dark_Text = Color.White;
            private static Color dark_SecondaryText = Color.FromArgb(200, 200, 200);
            private static Color dark_Accent = Color.FromArgb(60, 179, 113); // Màu xanh lá cây sáng (MediumSeaGreen)
            private static Color dark_Border = Color.FromArgb(80, 80, 80);

            public static Color BackgroundColor { get; private set; }
            public static Color PanelColor { get; private set; }
            public static Color SecondaryPanelColor { get; private set; }
            public static Color TextColor { get; private set; }
            public static Color SecondaryTextColor { get; private set; }
            public static Color AccentColor { get; private set; }
            public static Color BorderColor { get; private set; }
            public static Image BackgroundImage { get; private set; } 
            static ThemeManager()
            {
                ApplyTheme("light");
            }

            public static void ApplyTheme(string themeName)
            {
                if (themeName.ToLower() == "dark")
                {
                    BackgroundColor = dark_Background;
                    PanelColor = dark_Panel;
                    SecondaryPanelColor = dark_SecondaryPanel;
                    TextColor = dark_Text;
                    SecondaryTextColor = dark_SecondaryText;
                    AccentColor = dark_Accent;
                    BorderColor = dark_Border;
                    BackgroundImage = Properties.Resources.darkmode; 
                }
                else 
                {
                    BackgroundColor = light_Background;
                    PanelColor = light_Panel;
                    SecondaryPanelColor = light_SecondaryPanel;
                    TextColor = light_Text;
                    SecondaryTextColor = light_SecondaryText;
                    AccentColor = light_Accent;
                    BorderColor = light_Border;
                    BackgroundImage = Properties.Resources.lightmode; 
                }
            }
        }
}