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
using Environmental_Monitoring.Controller;

namespace Environmental_Monitoring.View
{
    public partial class Mainlayout : Form
    {
        #region Fields & Properties

        private MenuButton currentActiveButton;
        private bool isDragging = false;
        private Point lastLocation;

        private bool isMenuCollapsed = true;
        private int menuCollapsedWidth = 90;
        private int menuExpandedWidth = 190;

        private Notification notificationPage;
        private Contract contractPage;
        private Employee employeePage;
        private Stats statsPage;
        private Setting settingPage;
        private AI aiPage;
        private Introduce introducePage;

        #endregion

        #region Initialization

        public Mainlayout()
        {
            InitializeComponent();
            InitializeFormProperties();
            RegisterEventHandlers();

            panelMenu.Width = menuCollapsedWidth;
            SetMenuState(isMenuCollapsed);
            ApplyTheme();
        }

        private void InitializeFormProperties()
        {
            this.Load += new System.EventHandler(this.Mainlayout_Load);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.DoubleBuffered = true;
        }

        private void RegisterEventHandlers()
        {
            btnToggleMenu.Click += new EventHandler(btnToggleMenu_Click);
            btnHome.Click += new EventHandler(MenuButton_Click);
            btnUser.Click += new EventHandler(MenuButton_Click);
            btnContracts.Click += new EventHandler(MenuButton_Click);
            btnNotification.Click += new EventHandler(MenuButton_Click);
            btnStats.Click += new EventHandler(MenuButton_Click);
            btnSetting.Click += new EventHandler(MenuButton_Click);
            btnAI.Click += new EventHandler(MenuButton_Click);
            btnIntroduce.Click += new EventHandler(MenuButton_Click);

            panel.MouseDown += new MouseEventHandler(panelHeadder_MouseDown);
            panel.MouseMove += new MouseEventHandler(panelHeadder_MouseMove);
            panel.MouseUp += new MouseEventHandler(panelHeadder_MouseUp);
        }

        private void Mainlayout_Load(object sender, EventArgs e)
        {
            ApplyPermissions();
            LoadDefaultPageForRole();
        }

        #endregion

        #region Permissions and Role Logic

        private void ApplyPermissions()
        {
            if (UserSession.IsAdmin())
            {
                return;
            }
            btnUser.Enabled = false;
        }

        private void LoadDefaultPageForRole()
        {
            UserControl defaultPage = GetOrCreatePage(ref notificationPage);
            MenuButton defaultButton = btnNotification;

            LoadPage(defaultPage);
            ResetAllButtons();
            HighlightButton(defaultButton);
        }

        private void LoadHomePageForRole()
        {
            UserControl homePage = null;
            MenuButton homeButton;

            string roleName = UserSession.CurrentUser?.Role?.RoleName ?? "";

            switch (roleName.ToLowerInvariant())
            {
                case "admin":
                    homePage = GetOrCreatePage(ref notificationPage);
                    homeButton = btnNotification;
                    break;

                case "plan":
                case "business":
                case "scene":
                case "lab":
                case "result":
                    homePage = GetOrCreatePage(ref contractPage);
                    homeButton = btnContracts;
                    break;

                default:
                    homePage = GetOrCreatePage(ref notificationPage);
                    homeButton = btnNotification;
                    break;
            }

            LoadPage(homePage);
            ResetAllButtons();
            HighlightButton(homeButton);
        }


        #endregion

        #region Page Navigation & Loading

        private T GetOrCreatePage<T>(ref T pageInstance) where T : UserControl, new()
        {
            if (pageInstance == null || pageInstance.IsDisposed)
            {
                pageInstance = new T();
                pageInstance.Dock = DockStyle.Fill;
                panelContent.Controls.Add(pageInstance);
            }
            return pageInstance;
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            MenuButton clickedButton = (MenuButton)sender;

            if (clickedButton == btnHome)
            {
                LoadHomePageForRole();
                return;
            }

            MenuButton oldActiveButton = currentActiveButton;
            ResetAllButtons();
            if (oldActiveButton != null)
            {
                oldActiveButton.Invalidate();
            }

            HighlightButton(clickedButton);

            if (clickedButton == btnUser)
            {
                LoadPage(GetOrCreatePage(ref employeePage));
            }
            else if (clickedButton == btnContracts)
            {
                LoadPage(GetOrCreatePage(ref contractPage));
            }
            else if (clickedButton == btnNotification)
            {
                LoadPage(GetOrCreatePage(ref notificationPage));
            }
            else if (clickedButton == btnStats)
            {
                LoadPage(GetOrCreatePage(ref statsPage));
            }
            else if (clickedButton == btnSetting)
            {
                LoadPage(GetOrCreatePage(ref settingPage));
            }
            else if (clickedButton == btnAI)
            {
                LoadPage(GetOrCreatePage(ref aiPage));
            }
            else if (clickedButton == btnIntroduce)
            {
                LoadPage(GetOrCreatePage(ref introducePage));
            }
        }

        private void LoadPage(UserControl pageToLoad)
        {
            if (pageToLoad == null) return;
            pageToLoad.BringToFront();
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

        #endregion

        #region Menu Collapse/Expand Logic

        private void btnToggleMenu_Click(object sender, EventArgs e)
        {
            isMenuCollapsed = !isMenuCollapsed;
            int newMenuWidth = isMenuCollapsed ? menuCollapsedWidth : menuExpandedWidth;

            this.SuspendLayout();

            panelMenu.Width = newMenuWidth;

            panel.Left = newMenuWidth;
            panel.Width = this.ClientSize.Width - newMenuWidth;
            panelContent.Left = newMenuWidth;
            panelContent.Top = panel.Height;
            panelContent.Width = this.ClientSize.Width - newMenuWidth;
            panelContent.Height = this.ClientSize.Height - panel.Height;

            SetMenuState(isMenuCollapsed);

            this.ResumeLayout(true);
        }

        private void SetMenuState(bool collapsed)
        {
            try
            {
                ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Mainlayout).Assembly);
                CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

                btnHome.Text = collapsed ? "" : rm.GetString("Menu_Home", culture);
                btnUser.Text = collapsed ? "" : rm.GetString("Menu_User", culture);
                btnContracts.Text = collapsed ? "" : rm.GetString("Menu_Contracts", culture);
                btnNotification.Text = collapsed ? "" : rm.GetString("Menu_Notification", culture);
                btnStats.Text = collapsed ? "" : rm.GetString("Menu_Stats", culture);
                btnSetting.Text = collapsed ? "" : rm.GetString("Menu_Settings", culture);
                btnAI.Text = collapsed ? "" : rm.GetString("Menu_AI", culture);
                btnIntroduce.Text = collapsed ? "" : rm.GetString("Menu_Introduce", culture);
            }
            catch (Exception ex)
            {
                ShowGlobalAlert("Lỗi tải tài nguyên ngôn ngữ: " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        #endregion

        #region Borderless Form Dragging

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

        #endregion

        #region Theme & Language

        private void ApplyTheme()
        {
            this.BackColor = ThemeManager.BackgroundColor;
            this.BackgroundImage = ThemeManager.BackgroundImage;
            this.BackgroundImageLayout = ImageLayout.Stretch;

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
                // Lặp qua TẤT CẢ các control (các trang) trong panelContent
                foreach (Control ctl in panelContent.Controls)
                {
                    try
                    {
                        // Thử gọi hàm UpdateUIText của mỗi trang
                        var method = ctl.GetType().GetMethod("UpdateUIText");
                        if (method != null)
                        {
                            method.Invoke(ctl, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Hiển thị lỗi nếu không cập nhật được trang cụ thể
                        ShowGlobalAlert($"Không thể cập nhật ngôn ngữ cho trang {ctl.Name}: {ex.Message}", AlertPanel.AlertType.Error);
                    }
                }
            }
        }


        #endregion

        #region Utilities & Form Events

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

        #endregion
    }
}

