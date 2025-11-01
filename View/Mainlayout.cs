using Environmental_Monitoring;
using Environmental_Monitoring.View;
using Environmental_Monitoring.View.Components; // <-- THÊM MỚI
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
        }

        private bool isMenuCollapsed = true;
        private int menuCollapsedWidth = 90;
        private int menuExpandedWidth = 190;

        private void btnToggleMenu_Click(object sender, EventArgs e)
        {
            // ... (Code của bạn đã đúng) ...
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
            // ... (Code của bạn đã đúng) ...
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
            // ... (Code của bạn đã đúng) ...
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
            // ... (Code của bạn đã đúng) ...
            currentActiveButton = selectedButton;
            selectedButton.IsSelected = true;
        }

        private void ResetAllButtons()
        {
            // ... (Code của bạn đã đúng) ...
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
            // ... (Code của bạn đã đúng) ...
            panelContent.Controls.Clear();
            pageToLoad.Dock = DockStyle.Fill;
            panelContent.Controls.Add(pageToLoad);
        }

        public void UpdateUIText()
        {
            // ... (Code của bạn đã đúng) ...
            SetMenuState(isMenuCollapsed);
        }
        public void UpdateAllChildLanguages()
        {
            // ... (Code của bạn đã đúng) ...
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
                    // <-- THAY ĐỔI: Sử dụng AlertPanel thay vì MessageBox -->
                    ShowGlobalAlert("Không thể cập nhật ngôn ngữ cho trang con: " + ex.Message, AlertPanel.AlertType.Error);
                }
            }
        }

        // <-- HÀM MỚI: Dùng để gọi AlertPanel từ bất kỳ đâu -->
        public void ShowGlobalAlert(string message, AlertPanel.AlertType type)
        {
            // 'globalAlertPanel' là tên bạn đã đặt cho AlertPanel
            // khi kéo nó vào MainLayout.cs [Design]
            if (globalAlertPanel != null)
            {
                globalAlertPanel.ShowAlert(message, type);
            }
            else
            {
                // Phương án dự phòng nếu 'globalAlertPanel' bị null
                MessageBox.Show(message);
            }
        }
    }
}