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

        // === BIẾN MỚI ĐỂ XỬ LÝ KÉO-THẢ CỬA SỔ ===
        private bool isDragging = false;
        private Point lastLocation;
        // ======================================

        public Mainlayout()
        {
            InitializeComponent();

            // === CẬP NHẬT ĐỂ XÓA TITLE BAR ===
            //this.AutoSize = false; // Giữ lại từ lần sửa trước
            //this.FormBorderStyle = FormBorderStyle.None; // Xóa title bar
            // ================================

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

            // === GẮN SỰ KIỆN KÉO-THẢ CHO HEADER ===
            // (Giả sử panel header của bạn tên là 'panelHeadder')
            panel.MouseDown += new MouseEventHandler(panelHeadder_MouseDown);
            panel.MouseMove += new MouseEventHandler(panelHeadder_MouseMove);
            panel.MouseUp += new MouseEventHandler(panelHeadder_MouseUp);

            // LƯU Ý: Bạn cũng nên vào Designer và gán sự kiện 
            // MouseDown, MouseMove, MouseUp của logo và label "GREEN FLOW"
            // trỏ vào 3 hàm này, để kéo-thả mượt hơn.
            // =====================================

            HighlightButton(btnNotification);
            LoadPage(new Notification());

            panelMenu.Width = menuCollapsedWidth;

            SetMenuState(isMenuCollapsed);

            ApplyTheme();
        }

        // === CÁC HÀM MỚI ĐỂ KÉO-THẢ CỬA SỔ ===
        private void panelHeadder_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastLocation = e.Location;
            }
        }

        private void panelHeadder_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void panelHeadder_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
        // =====================================

        private void ApplyTheme()
        {
            this.BackColor = ThemeManager.BackgroundColor;

            this.BackgroundImage = ThemeManager.BackgroundImage;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // Áp dụng màu cho thanh Header (Nơi có chữ GREEN FLOW)
            //panelHeadder.BackColor = ThemeManager.PanelColor;
            // (Bạn cần thêm code để đổi màu chữ 'GREEN FLOW' nếu nó là Label)
            // lblAppName.ForeColor = ThemeManager.TextColor; 
            // (Nếu logo cũng đổi theo Theme, giả sử tên là picLogo)
            // picLogo.Image = ThemeManager.MainMenuLogo;

            panelMenu.BackColor = ThemeManager.PanelColor;

            foreach (Control ctrl in panelMenu.Controls)
            {
                if (ctrl is MenuButton btn)
                {
                    btn.ForeColor = ThemeManager.SecondaryTextColor;
                    btn.BackColor = ThemeManager.PanelColor;
                }
            }

            panelContent.BackColor = ThemeManager.BackgroundColor;
        }


        private bool isMenuCollapsed = true;
        private int menuCollapsedWidth = 90;
        private int menuExpandedWidth = 190;

        // === HÀM NÀY GIỮ NGUYÊN NHƯ LẦN TRƯỚC ===
        private void btnToggleMenu_Click(object sender, EventArgs e)
        {
            isMenuCollapsed = !isMenuCollapsed;
            int newMenuWidth = isMenuCollapsed ? menuCollapsedWidth : menuExpandedWidth;

            this.SuspendLayout();

            // 1. Cập nhật Menu Panel
            panelMenu.Width = newMenuWidth;

            // 2. Cập nhật Header Panel (panelHeadder)
            panel.Left = newMenuWidth;
            panel.Width = this.ClientSize.Width - newMenuWidth;
            // panelHeadder.Top = 0; // Đã giả định Top = 0

            // 3. Cập nhật Content Panel (panelContent)
            panelContent.Left = newMenuWidth;
            // DÒNG QUAN TRỌNG NHẤT ĐỂ SỬA LỖI:
            panelContent.Top = panel.Height; // Reset Top về vị trí cố định

            panelContent.Width = this.ClientSize.Width - newMenuWidth;
            panelContent.Height = this.ClientSize.Height - panel.Height;

            // 4. Cập nhật trạng thái text
            SetMenuState(isMenuCollapsed);

            this.ResumeLayout(true); // Dùng true để áp dụng layout ngay
        }
        // ===============================================

        private void SetMenuState(bool collapsed)
        {
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
            currentActiveButton = selectedButton;
            selectedButton.IsSelected = true;
        }

        private void ResetAllButtons()
        {
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
            panelContent.Controls.Clear();
            pageToLoad.Dock = DockStyle.Fill;
            panelContent.Controls.Add(pageToLoad);
        }
        public void UpdateUIText()
        {
            SetMenuState(isMenuCollapsed);

            ApplyTheme();
        }

        public void UpdateAllChildLanguages()
        {
            this.UpdateUIText();
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
            if (globalAlertPanel != null)
            {
                globalAlertPanel.ShowAlert(message, type);
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}