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
            string homeText = collapsed ? "" : "Home";
            string userText = collapsed ? "" : "User";
            string contractsText = collapsed ? "" : "Contracts";
            string notificationText = collapsed ? "" : "Notification";
            string statsText = collapsed ? "" : "Stats";
            string settingText = collapsed ? "" : "Settings";
            string aiText = collapsed ? "" : "AI";
            string introduceText = collapsed ? "" : "Introduce";

            btnHome.Text = homeText;
            btnUser.Text = userText;
            btnContracts.Text = contractsText;
            btnNotification.Text = notificationText;
            btnStats.Text = statsText;
            btnSetting.Text = settingText;
            btnAI.Text = aiText;
            btnIntroduce.Text = introduceText;


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

            if (clickedButton == btnContracts)
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
    }
}
