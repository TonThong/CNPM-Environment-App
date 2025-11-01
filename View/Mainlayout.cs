using Environmental_Monitoring;
using Environmental_Monitoring.View;
using Environmental_Monitoring.View.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Resources;


namespace Environmental_Monitoring.View
{

    public partial class Mainlayout : Form
    {

        private MenuButton currentActiveButton;

        public Mainlayout()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            btnToggleMenu.Click += new EventHandler(btnToggleMenu_Click);
            btnHome.Click += new EventHandler(MenuButton_Click);
            btnUser.Click += new EventHandler(MenuButton_Click);
            btnContracts.Click += new EventHandler(MenuButton_Click);
            btnNotification.Click += new EventHandler(MenuButton_Click);
            btnStats.Click += new EventHandler(MenuButton_Click);
            btnSetting.Click += new EventHandler(MenuButton_Click);
            btnAI.Click += new EventHandler(MenuButton_Click);
            btnIntroduce.Click += new EventHandler(MenuButton_Click);

            HighlightButton(btnNotification);
            LoadPage(new Notification());

            panelMenu.Width = menuCollapsedWidth;

            SetMenuState(isMenuCollapsed);

            // --- THÊM MỚI: Áp dụng giao diện ngay khi khởi động ---
            ApplyTheme();
        }

        // --- HÀM ĐÃ CẬP NHẬT: Thêm ảnh nền và set màu nút bằng vòng lặp ---
        private void ApplyTheme()
        {
            // Áp dụng màu cho nền Form
            this.BackColor = ThemeManager.BackgroundColor;

            // --- CẬP NHẬT: Thêm ảnh nền ---
            this.BackgroundImage = ThemeManager.BackgroundImage;
            this.BackgroundImageLayout = ImageLayout.Stretch; // Hoặc Zoom, Center...

            // Áp dụng màu cho thanh Header (Nơi có chữ GREEN FLOW)
            //panelHeadder.BackColor = ThemeManager.PanelColor;
            // (Bạn cần thêm code để đổi màu chữ 'GREEN FLOW' nếu nó là Label)
            // lblAppName.ForeColor = ThemeManager.TextColor; 
            // (Nếu logo cũng đổi theo Theme, giả sử tên là picLogo)
            // picLogo.Image = ThemeManager.MainMenuLogo;

            // Áp dụng màu cho thanh Menu (Bên trái)
            panelMenu.BackColor = ThemeManager.PanelColor;

            // --- CẬP NHẬT: Áp dụng màu cho các nút menu bằng vòng lặp ---
            foreach (Control ctrl in panelMenu.Controls)
            {
                if (ctrl is MenuButton btn)
                {
                    // Màu chữ mặc định (khi không được chọn)
                    btn.ForeColor = ThemeManager.SecondaryTextColor;
                    btn.BackColor = ThemeManager.PanelColor;
                }
            }

            // Áp dụng màu nền cho khu vực nội dung (phía sau các trang)
            // Đây là code SỬA LỖI CÓ Ô MÀU TRẮNG Ở BACKGROUND
            panelContent.BackColor = ThemeManager.BackgroundColor;
        }


        private bool isMenuCollapsed = true;
        private int menuCollapsedWidth = 90;
        private int menuExpandedWidth = 190;

        private void btnToggleMenu_Click(object sender, EventArgs e)
        {
            // (Code của bạn đã đúng)
            isMenuCollapsed = !isMenuCollapsed;
            int oldMenuWidth = panelMenu.Width;
            int newMenuWidth = isMenuCollapsed ? menuCollapsedWidth : menuExpandedWidth;
            int widthDifference = newMenuWidth - oldMenuWidth;
            this.SuspendLayout();
            panelMenu.Width = newMenuWidth;
            panelContent.Left += widthDifference;
            panelHeadder.Left += widthDifference;
            SetMenuState(isMenuCollapsed);
            this.ResumeLayout();
        }

        private void SetMenuState(bool collapsed)
        {
            // (Code của bạn đã đúng)
            try
            {
                ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Mainlayout).Assembly);
                CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
                string homeText = collapsed ? "" : rm.GetString("Menu_Home", culture);
                string userText = collapsed ? "" : rm.GetString("Menu_User", culture);
                string contractsText = collapsed ? "" : rm.GetString("Menu_Contracts", culture);
                string notificationText = collapsed ? "" : rm.GetString("Menu_Notification", culture);
                string statsText = collapsed ? "" : rm.GetString("Menu_Stats", culture);
                string settingText = collapsed ? "" : rm.GetString("Menu_Settings", culture);
                string aiText = collapsed ? "" : rm.GetString("Menu_AI", culture);
                string introduceText = collapsed ? "" : rm.GetString("Menu_Introduce", culture);
                btnHome.Text = homeText;
                btnUser.Text = userText;
                btnContracts.Text = contractsText;
                btnNotification.Text = notificationText;
                btnStats.Text = statsText;
                btnSetting.Text = settingText;
                btnAI.Text = aiText;
                btnIntroduce.Text = introduceText;
            }
            catch (Exception ex)
            {
            }
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            // (Code của bạn đã đúng)
            MenuButton oldActiveButton = currentActiveButton;
            ResetAllButtons();
            if (oldActiveButton != null)
            {
                oldActiveButton.Invalidate();
            }
            MenuButton clickedButton = (MenuButton)sender;
            HighlightButton(clickedButton);
            if (clickedButton == btnUser)
            {
                LoadPage(new Employee());
            }
            else if (clickedButton == btnContracts)
            {
                LoadPage(new Contract());
            }
            else if (clickedButton == btnNotification)
            {
                LoadPage(new Notification());
            }
            else if (clickedButton == btnStats)
            {
                LoadPage(new Stats());
            }
            else if (clickedButton == btnSetting)
            {
                LoadPage(new Setting());
            }
            else if (clickedButton == btnAI)
            {
                LoadPage(new AI());
            }
            else if (clickedButton == btnIntroduce)
            {
                LoadPage(new Introduce());
            }
        }
        private void HighlightButton(MenuButton selectedButton)
        {
            // (Code của bạn đã đúng)
            currentActiveButton = selectedButton;
            selectedButton.IsSelected = true;
        }

        private void ResetAllButtons()
        {
            // (Code của bạn đã đúng)
            foreach (Control ctrl in panelMenu.Controls)
            {
                if (ctrl is MenuButton btn)
                {
                    btn.IsSelected = false;
                }
            }
        }

        private void LoadPage(UserControl pageToLoad)
        {
            // (Code của bạn đã đúng)
            panelContent.Controls.Clear();
            pageToLoad.Dock = DockStyle.Fill;
            panelContent.Controls.Add(pageToLoad);
        }

        // --- ĐÃ CẬP NHẬT (Thêm logic áp dụng Theme) ---
        public void UpdateUIText()
        {
            // 1. TẢI NGÔN NGỮ (Code này bạn đã có)
            SetMenuState(isMenuCollapsed);

            // 2. ÁP DỤNG GIAO DIỆN (Code mới)
            ApplyTheme();
        }

        public void UpdateAllChildLanguages()
        {
            // (Code của bạn đã đúng)
            this.UpdateUIText(); // Hàm này giờ đã bao gồm cả Ngôn ngữ VÀ Giao diện
            if (panelContent.Controls.Count > 0)
            {
                var currentPage = panelContent.Controls[0];
                try
                {
                    currentPage.GetType().GetMethod("UpdateUIText")?.Invoke(currentPage, null);
                }
                catch (Exception ex)
                {
                    ShowGlobalAlert("Không thể cập nhật ngôn ngữ/giao diện cho trang con: " + ex.Message, AlertPanel.AlertType.Error);
                }
            }
        }

        public void ShowGlobalAlert(string message, AlertPanel.AlertType type)
        {
            // (Code của bạn đã đúng)
            if (globalAlertPanel != null)
            {
                globalAlertPanel.ShowAlert(message, type);
            }
            else
            {
                MessageBox.Show(message);
            }
        }
    }
}