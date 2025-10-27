using Environmental_Monitoring.View.Components;

namespace Environmental_Monitoring.View
{
    partial class Mainlayout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used. 
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainlayout));
            lblBigName = new Label();
            panelMenu = new Panel();
            btnToggleMenu = new MenuButton();
            btnNotification = new MenuButton();
            btnStats = new MenuButton();
            btnContracts = new MenuButton();
            btnIntroduce = new MenuButton();
            btnAI = new MenuButton();
            btnHome = new MenuButton();
            btnSetting = new MenuButton();
            btnUser = new MenuButton();
            btnMenu = new MenuButton();
            panelContent = new RoundedPanel();
            pictureBox1 = new PictureBox();
            btnBell = new MenuButton();
            panelHeadder = new Panel();
            panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelHeadder.SuspendLayout();
            SuspendLayout();
            // 
            // lblBigName
            // 
            lblBigName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblBigName.AutoSize = true;
            lblBigName.BackColor = Color.Transparent;
            lblBigName.Font = new Font("Times New Roman", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblBigName.ForeColor = Color.DarkGreen;
            lblBigName.Location = new Point(140, 45);
            lblBigName.Name = "lblBigName";
            lblBigName.Size = new Size(346, 53);
            lblBigName.TabIndex = 18;
            lblBigName.Text = "GREEN FLOW";
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(217, 217, 217);
            panelMenu.Controls.Add(btnToggleMenu);
            panelMenu.Controls.Add(btnNotification);
            panelMenu.Controls.Add(btnStats);
            panelMenu.Controls.Add(btnContracts);
            panelMenu.Controls.Add(btnIntroduce);
            panelMenu.Controls.Add(btnAI);
            panelMenu.Controls.Add(btnHome);
            panelMenu.Controls.Add(btnSetting);
            panelMenu.Controls.Add(btnUser);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(190, 853);
            panelMenu.TabIndex = 15;
            // 
            // btnToggleMenu
            // 
            btnToggleMenu.ActiveBackColor = Color.FromArgb(220, 240, 220);
            btnToggleMenu.ActiveBorderColor = Color.FromArgb(0, 100, 0);
            btnToggleMenu.BackColor = Color.Transparent;
            btnToggleMenu.BackgroundImage = (Image)resources.GetObject("btnToggleMenu.BackgroundImage");
            btnToggleMenu.BackgroundImageLayout = ImageLayout.Stretch;
            btnToggleMenu.BorderLeftSize = 5;
            btnToggleMenu.FlatAppearance.BorderSize = 0;
            btnToggleMenu.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnToggleMenu.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnToggleMenu.FlatStyle = FlatStyle.Flat;
            btnToggleMenu.HoverBackColor = Color.FromArgb(230, 230, 230);
            btnToggleMenu.InactiveBackColor = Color.Transparent;
            btnToggleMenu.IsSelected = false;
            btnToggleMenu.Location = new Point(0, 0);
            btnToggleMenu.Name = "btnToggleMenu";
            btnToggleMenu.PressedBackColor = Color.FromArgb(200, 200, 200);
            btnToggleMenu.Size = new Size(190, 90);
            btnToggleMenu.TabIndex = 10;
            btnToggleMenu.UseVisualStyleBackColor = false;
            // 
            // btnNotification
            // 
            btnNotification.ActiveBackColor = Color.FromArgb(220, 240, 220);
            btnNotification.ActiveBorderColor = Color.FromArgb(0, 100, 0);
            btnNotification.BackColor = Color.Transparent;
            btnNotification.BackgroundImage = (Image)resources.GetObject("btnNotification.BackgroundImage");
            btnNotification.BackgroundImageLayout = ImageLayout.Stretch;
            btnNotification.BorderLeftSize = 5;
            btnNotification.FlatAppearance.BorderSize = 0;
            btnNotification.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnNotification.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnNotification.FlatStyle = FlatStyle.Flat;
            btnNotification.HoverBackColor = Color.FromArgb(230, 230, 230);
            btnNotification.InactiveBackColor = Color.Transparent;
            btnNotification.IsSelected = false;
            btnNotification.Location = new Point(0, 360);
            btnNotification.Name = "btnNotification";
            btnNotification.PressedBackColor = Color.FromArgb(200, 200, 200);
            btnNotification.Size = new Size(190, 90);
            btnNotification.TabIndex = 9;
            btnNotification.UseVisualStyleBackColor = false;
            // 
            // btnStats
            // 
            btnStats.ActiveBackColor = Color.FromArgb(220, 240, 220);
            btnStats.ActiveBorderColor = Color.FromArgb(0, 100, 0);
            btnStats.BackColor = Color.Transparent;
            btnStats.BackgroundImage = (Image)resources.GetObject("btnStats.BackgroundImage");
            btnStats.BackgroundImageLayout = ImageLayout.Stretch;
            btnStats.BorderLeftSize = 5;
            btnStats.FlatAppearance.BorderSize = 0;
            btnStats.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnStats.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnStats.FlatStyle = FlatStyle.Flat;
            btnStats.HoverBackColor = Color.FromArgb(230, 230, 230);
            btnStats.InactiveBackColor = Color.Transparent;
            btnStats.IsSelected = false;
            btnStats.Location = new Point(0, 450);
            btnStats.Name = "btnStats";
            btnStats.PressedBackColor = Color.FromArgb(200, 200, 200);
            btnStats.Size = new Size(190, 90);
            btnStats.TabIndex = 3;
            btnStats.UseVisualStyleBackColor = false;
            // 
            // btnContracts
            // 
            btnContracts.ActiveBackColor = Color.FromArgb(220, 240, 220);
            btnContracts.ActiveBorderColor = Color.FromArgb(0, 100, 0);
            btnContracts.BackColor = Color.Transparent;
            btnContracts.BackgroundImage = (Image)resources.GetObject("btnContracts.BackgroundImage");
            btnContracts.BackgroundImageLayout = ImageLayout.Stretch;
            btnContracts.BorderLeftSize = 5;
            btnContracts.FlatAppearance.BorderSize = 0;
            btnContracts.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnContracts.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnContracts.FlatStyle = FlatStyle.Flat;
            btnContracts.HoverBackColor = Color.FromArgb(230, 230, 230);
            btnContracts.InactiveBackColor = Color.Transparent;
            btnContracts.IsSelected = false;
            btnContracts.Location = new Point(0, 270);
            btnContracts.Name = "btnContracts";
            btnContracts.PressedBackColor = Color.FromArgb(200, 200, 200);
            btnContracts.Size = new Size(190, 90);
            btnContracts.TabIndex = 1;
            btnContracts.UseVisualStyleBackColor = false;
            // 
            // btnIntroduce
            // 
            btnIntroduce.ActiveBackColor = Color.FromArgb(220, 240, 220);
            btnIntroduce.ActiveBorderColor = Color.FromArgb(0, 100, 0);
            btnIntroduce.BackColor = Color.Transparent;
            btnIntroduce.BackgroundImage = (Image)resources.GetObject("btnIntroduce.BackgroundImage");
            btnIntroduce.BackgroundImageLayout = ImageLayout.Stretch;
            btnIntroduce.BorderLeftSize = 5;
            btnIntroduce.FlatAppearance.BorderSize = 0;
            btnIntroduce.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnIntroduce.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnIntroduce.FlatStyle = FlatStyle.Flat;
            btnIntroduce.HoverBackColor = Color.FromArgb(230, 230, 230);
            btnIntroduce.InactiveBackColor = Color.Transparent;
            btnIntroduce.IsSelected = false;
            btnIntroduce.Location = new Point(0, 720);
            btnIntroduce.Name = "btnIntroduce";
            btnIntroduce.PressedBackColor = Color.FromArgb(200, 200, 200);
            btnIntroduce.Size = new Size(190, 90);
            btnIntroduce.TabIndex = 8;
            btnIntroduce.UseVisualStyleBackColor = false;
            // 
            // btnAI
            // 
            btnAI.ActiveBackColor = Color.FromArgb(220, 240, 220);
            btnAI.ActiveBorderColor = Color.FromArgb(0, 100, 0);
            btnAI.BackColor = Color.Transparent;
            btnAI.BackgroundImage = (Image)resources.GetObject("btnAI.BackgroundImage");
            btnAI.BackgroundImageLayout = ImageLayout.Stretch;
            btnAI.BorderLeftSize = 5;
            btnAI.FlatAppearance.BorderSize = 0;
            btnAI.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnAI.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnAI.FlatStyle = FlatStyle.Flat;
            btnAI.HoverBackColor = Color.FromArgb(230, 230, 230);
            btnAI.InactiveBackColor = Color.Transparent;
            btnAI.IsSelected = false;
            btnAI.Location = new Point(0, 630);
            btnAI.Name = "btnAI";
            btnAI.PressedBackColor = Color.FromArgb(200, 200, 200);
            btnAI.Size = new Size(190, 90);
            btnAI.TabIndex = 7;
            btnAI.UseVisualStyleBackColor = false;
            // 
            // btnHome
            // 
            btnHome.ActiveBackColor = Color.FromArgb(220, 240, 220);
            btnHome.ActiveBorderColor = Color.FromArgb(0, 100, 0);
            btnHome.BackColor = Color.Transparent;
            btnHome.BackgroundImage = (Image)resources.GetObject("btnHome.BackgroundImage");
            btnHome.BackgroundImageLayout = ImageLayout.Stretch;
            btnHome.BorderLeftSize = 5;
            btnHome.FlatAppearance.BorderSize = 0;
            btnHome.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnHome.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnHome.FlatStyle = FlatStyle.Flat;
            btnHome.HoverBackColor = Color.FromArgb(230, 230, 230);
            btnHome.InactiveBackColor = Color.Transparent;
            btnHome.IsSelected = false;
            btnHome.Location = new Point(0, 90);
            btnHome.Name = "btnHome";
            btnHome.PressedBackColor = Color.FromArgb(200, 200, 200);
            btnHome.Size = new Size(190, 90);
            btnHome.TabIndex = 5;
            btnHome.UseVisualStyleBackColor = false;
            // 
            // btnSetting
            // 
            btnSetting.ActiveBackColor = Color.FromArgb(220, 240, 220);
            btnSetting.ActiveBorderColor = Color.FromArgb(0, 100, 0);
            btnSetting.BackColor = Color.Transparent;
            btnSetting.BackgroundImage = (Image)resources.GetObject("btnSetting.BackgroundImage");
            btnSetting.BackgroundImageLayout = ImageLayout.Stretch;
            btnSetting.BorderLeftSize = 5;
            btnSetting.FlatAppearance.BorderSize = 0;
            btnSetting.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnSetting.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnSetting.FlatStyle = FlatStyle.Flat;
            btnSetting.HoverBackColor = Color.FromArgb(230, 230, 230);
            btnSetting.InactiveBackColor = Color.Transparent;
            btnSetting.IsSelected = false;
            btnSetting.Location = new Point(0, 540);
            btnSetting.Name = "btnSetting";
            btnSetting.PressedBackColor = Color.FromArgb(200, 200, 200);
            btnSetting.Size = new Size(190, 90);
            btnSetting.TabIndex = 4;
            btnSetting.UseVisualStyleBackColor = false;
            // 
            // btnUser
            // 
            btnUser.ActiveBackColor = Color.FromArgb(220, 240, 220);
            btnUser.ActiveBorderColor = Color.FromArgb(0, 100, 0);
            btnUser.BackColor = Color.Transparent;
            btnUser.BackgroundImage = (Image)resources.GetObject("btnUser.BackgroundImage");
            btnUser.BackgroundImageLayout = ImageLayout.Stretch;
            btnUser.BorderLeftSize = 5;
            btnUser.FlatAppearance.BorderSize = 0;
            btnUser.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnUser.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnUser.FlatStyle = FlatStyle.Flat;
            btnUser.HoverBackColor = Color.FromArgb(230, 230, 230);
            btnUser.InactiveBackColor = Color.Transparent;
            btnUser.IsSelected = false;
            btnUser.Location = new Point(0, 180);
            btnUser.Name = "btnUser";
            btnUser.PressedBackColor = Color.FromArgb(200, 200, 200);
            btnUser.Size = new Size(190, 90);
            btnUser.TabIndex = 2;
            btnUser.UseVisualStyleBackColor = false;
            // 
            // btnMenu
            // 
            btnMenu.ActiveBackColor = Color.FromArgb(220, 240, 220);
            btnMenu.ActiveBorderColor = Color.FromArgb(0, 100, 0);
            btnMenu.BackColor = Color.Transparent;
            btnMenu.BackgroundImageLayout = ImageLayout.Stretch;
            btnMenu.BorderLeftSize = 5;
            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnMenu.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnMenu.FlatStyle = FlatStyle.Flat;
            btnMenu.HoverBackColor = Color.FromArgb(230, 230, 230);
            btnMenu.InactiveBackColor = Color.Transparent;
            btnMenu.IsSelected = false;
            btnMenu.Location = new Point(90, 30);
            btnMenu.Name = "btnMenu";
            btnMenu.PressedBackColor = Color.FromArgb(200, 200, 200);
            btnMenu.Size = new Size(90, 90);
            btnMenu.TabIndex = 6;
            btnMenu.UseVisualStyleBackColor = false;
            // 
            // panelContent
            // 
            panelContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelContent.BackColor = Color.FromArgb(217, 244, 227);
            panelContent.BorderColor = Color.Transparent;
            panelContent.BorderRadius = 20;
            panelContent.BorderSize = 0;
            panelContent.Location = new Point(90, 140);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(1227, 715);
            panelContent.TabIndex = 19;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(140, 140);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 20;
            pictureBox1.TabStop = false;
            // 
            // btnBell
            // 
            btnBell.ActiveBackColor = Color.FromArgb(220, 240, 220);
            btnBell.ActiveBorderColor = Color.FromArgb(0, 100, 0);
            btnBell.Anchor = AnchorStyles.Right;
            btnBell.BackColor = Color.Transparent;
            btnBell.BackgroundImage = (Image)resources.GetObject("btnBell.BackgroundImage");
            btnBell.BackgroundImageLayout = ImageLayout.Stretch;
            btnBell.BorderLeftSize = 5;
            btnBell.FlatAppearance.BorderSize = 0;
            btnBell.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnBell.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnBell.FlatStyle = FlatStyle.Flat;
            btnBell.HoverBackColor = Color.LightGray;
            btnBell.InactiveBackColor = Color.Transparent;
            btnBell.IsSelected = false;
            btnBell.Location = new Point(1137, 25);
            btnBell.Name = "btnBell";
            btnBell.PressedBackColor = Color.Silver;
            btnBell.Size = new Size(90, 90);
            btnBell.TabIndex = 9;
            btnBell.UseVisualStyleBackColor = false;
            // 
            // panelHeadder
            // 
            panelHeadder.BackColor = Color.Transparent;
            panelHeadder.Controls.Add(lblBigName);
            panelHeadder.Controls.Add(pictureBox1);
            panelHeadder.Controls.Add(btnBell);
            panelHeadder.Location = new Point(90, 0);
            panelHeadder.Name = "panelHeadder";
            panelHeadder.Size = new Size(1230, 140);
            panelHeadder.TabIndex = 21;
            // 
            // Mainlayout
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1421, 853);
            Controls.Add(panelHeadder);
            Controls.Add(panelContent);
            Controls.Add(panelMenu);
            Controls.Add(btnMenu);
            Name = "Mainlayout";
            panelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelHeadder.ResumeLayout(false);
            panelHeadder.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label labelName;
        private Label lblBigName;
        private Panel panelMenu;
        private Button btnNotify;
        private RoundedPanel panelContent;
        private MenuButton btnContracts;
        private MenuButton btnStats;
        private MenuButton btnUser;
        private MenuButton btnSetting;
        private MenuButton btnAI;
        private MenuButton btnMenu;
        private MenuButton btnHome;
        private MenuButton btnIntroduce;
        private PictureBox pictureBox1;
        private MenuButton btnBell;
        private MenuButton btnNotification;
        private MenuButton btnToggleMenu;
        private Panel panelHeadder;
    }
}