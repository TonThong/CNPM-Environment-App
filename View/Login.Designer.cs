namespace Environmental_Monitoring.View
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            roundedPanel1 = new Environmental_Monitoring.View.Components.RoundedPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            picShowPass = new PictureBox();
            pictureBox1 = new PictureBox();
            lbTaiKhoan = new Label();
            txtTaiKhoan = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtMatKhau = new Environmental_Monitoring.View.Components.RoundedTextBox();
            linkForgot = new LinkLabel();
            btnDangNhap = new Environmental_Monitoring.View.Components.RoundedButton();
            lbDangNhapFaceID = new Label();
            label1 = new Label();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            alertPanel = new Environmental_Monitoring.View.Components.AlertPanel();
            pbFlag = new PictureBox();
            panelLanguage = new Panel();
            btnEnglish = new Button();
            btnVietnamese = new Button();
            roundedPanel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picShowPass).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbFlag).BeginInit();
            panelLanguage.SuspendLayout();
            SuspendLayout();
            // 
            // roundedPanel1
            // 
            roundedPanel1.Anchor = AnchorStyles.None;
            roundedPanel1.BackColor = Color.Honeydew;
            roundedPanel1.BorderColor = Color.Transparent;
            roundedPanel1.BorderRadius = 20;
            roundedPanel1.BorderSize = 0;
            roundedPanel1.Controls.Add(tableLayoutPanel1);
            roundedPanel1.Location = new Point(515, 210);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(420, 620);
            roundedPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.Controls.Add(picShowPass, 2, 7);
            tableLayoutPanel1.Controls.Add(pictureBox1, 1, 1);
            tableLayoutPanel1.Controls.Add(lbTaiKhoan, 1, 3);
            tableLayoutPanel1.Controls.Add(txtTaiKhoan, 1, 5);
            tableLayoutPanel1.Controls.Add(txtMatKhau, 1, 7);
            tableLayoutPanel1.Controls.Add(linkForgot, 1, 9);
            tableLayoutPanel1.Controls.Add(btnDangNhap, 1, 11);
            tableLayoutPanel1.Controls.Add(lbDangNhapFaceID, 1, 13);
            tableLayoutPanel1.Location = new Point(6, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 15;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3.33333254F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 27.7777748F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3.33333254F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.55555439F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3.33333254F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.888887F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.55555439F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.888887F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3.33333254F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.55555439F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3.33333254F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.888887F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3.33333254F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.55555439F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3.33333254F));
            tableLayoutPanel1.Size = new Size(411, 614);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // picShowPass
            // 
            picShowPass.Cursor = Cursors.Hand;
            picShowPass.Image = Properties.Resources.eyeclose;
            picShowPass.Location = new Point(372, 355);
            picShowPass.Name = "picShowPass";
            picShowPass.Size = new Size(25, 25);
            picShowPass.SizeMode = PictureBoxSizeMode.Zoom;
            picShowPass.TabIndex = 2;
            picShowPass.TabStop = false;
            picShowPass.Click += picShowPass_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(44, 23);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Padding = new Padding(100, 0, 0, 0);
            pictureBox1.Size = new Size(322, 164);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // lbTaiKhoan
            // 
            lbTaiKhoan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbTaiKhoan.AutoSize = true;
            lbTaiKhoan.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lbTaiKhoan.Location = new Point(44, 210);
            lbTaiKhoan.Name = "lbTaiKhoan";
            lbTaiKhoan.Size = new Size(322, 34);
            lbTaiKhoan.TabIndex = 1;
            lbTaiKhoan.Text = "Tài Khoản";
            lbTaiKhoan.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtTaiKhoan
            // 
            txtTaiKhoan.BorderRadius = 15;
            txtTaiKhoan.BorderThickness = 2;
            txtTaiKhoan.Cursor = Cursors.IBeam;
            txtTaiKhoan.FocusBorderColor = Color.DimGray;
            txtTaiKhoan.HoverBorderColor = Color.DarkGray;
            txtTaiKhoan.Location = new Point(44, 267);
            txtTaiKhoan.Multiline = false;
            txtTaiKhoan.Name = "txtTaiKhoan";
            txtTaiKhoan.NormalBorderColor = Color.LightGray;
            txtTaiKhoan.Padding = new Padding(10, 13, 10, 10);
            txtTaiKhoan.PasswordChar = '\0';
            txtTaiKhoan.PlaceholderText = "Tài Khoản";
            txtTaiKhoan.ReadOnly = false;
            txtTaiKhoan.Size = new Size(322, 46);
            txtTaiKhoan.TabIndex = 2;
            txtTaiKhoan.UseSystemPasswordChar = false;
            // 
            // txtMatKhau
            // 
            txtMatKhau.BorderRadius = 15;
            txtMatKhau.BorderThickness = 2;
            txtMatKhau.Cursor = Cursors.IBeam;
            txtMatKhau.FocusBorderColor = Color.DimGray;
            txtMatKhau.HoverBorderColor = Color.DarkGray;
            txtMatKhau.Location = new Point(44, 355);
            txtMatKhau.Multiline = false;
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.NormalBorderColor = Color.LightGray;
            txtMatKhau.Padding = new Padding(10, 13, 10, 10);
            txtMatKhau.PasswordChar = '\0';
            txtMatKhau.PlaceholderText = "Mật Khẩu";
            txtMatKhau.ReadOnly = false;
            txtMatKhau.Size = new Size(322, 46);
            txtMatKhau.TabIndex = 3;
            txtMatKhau.UseSystemPasswordChar = false;
            // 
            // linkForgot
            // 
            linkForgot.ActiveLinkColor = SystemColors.ActiveCaptionText;
            linkForgot.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            linkForgot.AutoSize = true;
            linkForgot.Cursor = Cursors.Hand;
            linkForgot.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            linkForgot.LinkBehavior = LinkBehavior.HoverUnderline;
            linkForgot.LinkColor = Color.FromArgb(1, 42, 7);
            linkForgot.Location = new Point(44, 426);
            linkForgot.Name = "linkForgot";
            linkForgot.Size = new Size(322, 34);
            linkForgot.TabIndex = 4;
            linkForgot.TabStop = true;
            linkForgot.Text = "Quên Mật Khẩu";
            linkForgot.TextAlign = ContentAlignment.MiddleRight;
            linkForgot.LinkClicked += linkForgot_LinkClicked;
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = Color.FromArgb(0, 113, 0);
            btnDangNhap.BaseColor = Color.FromArgb(0, 113, 0);
            btnDangNhap.BorderColor = Color.Transparent;
            btnDangNhap.BorderRadius = 15;
            btnDangNhap.BorderSize = 0;
            btnDangNhap.Cursor = Cursors.Hand;
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.HoverColor = Color.FromArgb(34, 139, 34);
            btnDangNhap.Location = new Point(44, 483);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(322, 46);
            btnDangNhap.TabIndex = 5;
            btnDangNhap.Text = "Đăng Nhập";
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // lbDangNhapFaceID
            // 
            lbDangNhapFaceID.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbDangNhapFaceID.AutoSize = true;
            lbDangNhapFaceID.Cursor = Cursors.Hand;
            lbDangNhapFaceID.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lbDangNhapFaceID.ForeColor = Color.FromArgb(2, 85, 9);
            lbDangNhapFaceID.Location = new Point(44, 554);
            lbDangNhapFaceID.Name = "lbDangNhapFaceID";
            lbDangNhapFaceID.Size = new Size(322, 34);
            lbDangNhapFaceID.TabIndex = 6;
            lbDangNhapFaceID.Text = "Đăng Nhập Bằng Face ID";
            lbDangNhapFaceID.TextAlign = ContentAlignment.MiddleCenter;
            lbDangNhapFaceID.Click += lbDangNhapFaceID_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.ForeColor = Color.FromArgb(0, 65, 5);
            label1.Location = new Point(521, 62);
            label1.Name = "label1";
            label1.Size = new Size(411, 81);
            label1.TabIndex = 1;
            label1.Text = "GREEN FLOW";
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // alertPanel
            // 
            alertPanel.Location = new Point(1012, 10);
            alertPanel.Name = "alertPanel";
            alertPanel.Size = new Size(400, 100);
            alertPanel.TabIndex = 2;
            alertPanel.Visible = false;
            // 
            // pbFlag
            // 
            pbFlag.BorderStyle = BorderStyle.FixedSingle;
            pbFlag.Cursor = Cursors.Hand;
            pbFlag.Location = new Point(12, 12);
            pbFlag.Name = "pbFlag";
            pbFlag.Size = new Size(50, 34);
            pbFlag.SizeMode = PictureBoxSizeMode.Zoom;
            pbFlag.TabIndex = 3;
            pbFlag.TabStop = false;
            pbFlag.Click += pbFlag_Click;
            // 
            // panelLanguage
            // 
            panelLanguage.BorderStyle = BorderStyle.FixedSingle;
            panelLanguage.Controls.Add(btnEnglish);
            panelLanguage.Controls.Add(btnVietnamese);
            panelLanguage.Location = new Point(71, 37);
            panelLanguage.Name = "panelLanguage";
            panelLanguage.Size = new Size(160, 73);
            panelLanguage.TabIndex = 4;
            panelLanguage.Visible = false;
            // 
            // btnEnglish
            // 
            btnEnglish.Cursor = Cursors.Hand;
            btnEnglish.Dock = DockStyle.Top;
            btnEnglish.Location = new Point(0, 35);
            btnEnglish.Name = "btnEnglish";
            btnEnglish.Size = new Size(158, 35);
            btnEnglish.TabIndex = 6;
            btnEnglish.Text = "English";
            btnEnglish.UseVisualStyleBackColor = true;
            btnEnglish.Click += btnEnglish_Click;
            // 
            // btnVietnamese
            // 
            btnVietnamese.Cursor = Cursors.Hand;
            btnVietnamese.Dock = DockStyle.Top;
            btnVietnamese.Location = new Point(0, 0);
            btnVietnamese.Name = "btnVietnamese";
            btnVietnamese.Size = new Size(158, 35);
            btnVietnamese.TabIndex = 5;
            btnVietnamese.Text = "Tiếng Việt";
            btnVietnamese.UseVisualStyleBackColor = true;
            btnVietnamese.Click += btnVietnamese_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1421, 853);
            Controls.Add(panelLanguage);
            Controls.Add(pbFlag);
            Controls.Add(alertPanel);
            Controls.Add(label1);
            Controls.Add(roundedPanel1);
            DoubleBuffered = true;
            Name = "Login";
            Text = "Login";
            Load += Login_Load;
            roundedPanel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picShowPass).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbFlag).EndInit();
            panelLanguage.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Components.RoundedPanel roundedPanel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private PictureBox pictureBox1;
        private Label lbTaiKhoan;
        private Components.RoundedTextBox txtTaiKhoan;
        private Components.RoundedTextBox txtMatKhau;
        private LinkLabel linkForgot;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private Components.RoundedButton btnDangNhap;
        private Label lbDangNhapFaceID;
        private PictureBox picShowPass;
        private Components.AlertPanel alertPanel;
        private PictureBox pbFlag;
        private Panel panelLanguage;
        private Button btnEnglish;
        private Button btnVietnamese;
    }
}