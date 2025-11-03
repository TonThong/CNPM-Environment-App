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

        private bool isDragging = false;
        private Point lastLocation;

        public Mainlayout()
        {
            InitializeComponent();

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

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

            panel.MouseDown += new MouseEventHandler(panelHeadder_MouseDown);
            panel.MouseMove += new MouseEventHandler(panelHeadder_MouseMove);
            panel.MouseUp += new MouseEventHandler(panelHeadder_MouseUp);


            HighlightButton(btnNotification);
            LoadPage(new Notification());

            panelMenu.Width = menuCollapsedWidth;

            SetMenuState(isMenuCollapsed);

            ApplyTheme();
        }

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


        private bool isMenuCollapsed = true;
        private int menuCollapsedWidth = 90;
        private int menuExpandedWidth = 190;

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