using Environmental_Monitoring.View.Components;
using System.Windows.Forms;

namespace Environmental_Monitoring
{
    partial class Setting
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
            roundedPanel3 = new RoundedPanel();
            roundedPanel2 = new RoundedPanel();
            lblUserSupport = new Label();
            label8 = new Label();
            label7 = new Label();
            lblViewDocument = new Label();
            lblUserManual = new Label();
            lblBaoCao = new Label();
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            txtSearch = new RoundedTextBox();
            roundedPanel4 = new RoundedPanel();
            cmbGiaoDien = new CustomComboBox();
            lblTheme = new Label();
            roundedPanel1 = new RoundedPanel();
            cmbNgonNgu = new CustomComboBox();
            lblLanguage = new Label();
            lblSystemSettings = new Label();
            btnSave = new RoundedButton();
            roundedPanel5 = new RoundedPanel();
            btnRegisterFace = new RoundedButton();
            lblFaceID = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            roundedPanel6 = new RoundedPanel();
            roundedPanel7 = new RoundedPanel();
            label2 = new Label();
            label3 = new Label();
            roundedPanel3.SuspendLayout();
            roundedPanel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            roundedPanel4.SuspendLayout();
            roundedPanel1.SuspendLayout();
            roundedPanel5.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            roundedPanel6.SuspendLayout();
            roundedPanel7.SuspendLayout();
            SuspendLayout();
            // 
            // roundedPanel3
            // 
            roundedPanel3.BackColor = Color.White;
            roundedPanel3.BorderColor = Color.Transparent;
            roundedPanel3.BorderRadius = 20;
            roundedPanel3.BorderSize = 0;
            tableLayoutPanel1.SetColumnSpan(roundedPanel3, 3);
            roundedPanel3.Controls.Add(tableLayoutPanel2);
            roundedPanel3.Location = new Point(64, 115);
            roundedPanel3.Name = "roundedPanel3";
            roundedPanel3.Size = new Size(1092, 298);
            roundedPanel3.TabIndex = 17;
            // 
            // roundedPanel2
            // 
            roundedPanel2.BackColor = Color.White;
            roundedPanel2.BorderColor = Color.Transparent;
            roundedPanel2.BorderRadius = 20;
            roundedPanel2.BorderSize = 0;
            tableLayoutPanel1.SetColumnSpan(roundedPanel2, 3);
            roundedPanel2.Controls.Add(tableLayoutPanel3);
            roundedPanel2.Location = new Point(64, 440);
            roundedPanel2.Name = "roundedPanel2";
            roundedPanel2.Size = new Size(1092, 242);
            roundedPanel2.TabIndex = 8;
            // 
            // lblUserSupport
            // 
            lblUserSupport.Anchor = AnchorStyles.None;
            lblUserSupport.AutoSize = true;
            tableLayoutPanel3.SetColumnSpan(lblUserSupport, 3);
            lblUserSupport.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblUserSupport.Location = new Point(405, 9);
            lblUserSupport.Name = "lblUserSupport";
            lblUserSupport.Size = new Size(278, 38);
            lblUserSupport.TabIndex = 16;
            lblUserSupport.Text = "Hỗ Trợ Người Dùng";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label8.Location = new Point(39, 106);
            label8.Name = "label8";
            label8.Size = new Size(249, 31);
            label8.TabIndex = 6;
            label8.Text = "Email: QTMT@vn.com";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label7.Location = new Point(39, 28);
            label7.Name = "label7";
            label7.Size = new Size(229, 31);
            label7.TabIndex = 5;
            label7.Text = "HotLine: 012345678";
            // 
            // lblViewDocument
            // 
            lblViewDocument.AutoSize = true;
            lblViewDocument.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblViewDocument.ForeColor = Color.Blue;
            lblViewDocument.Location = new Point(350, 28);
            lblViewDocument.Name = "lblViewDocument";
            lblViewDocument.Size = new Size(119, 25);
            lblViewDocument.TabIndex = 2;
            lblViewDocument.Text = "Xem Tài Liệu";
            lblViewDocument.Click += lblViewDocument_Click;
            // 
            // lblUserManual
            // 
            lblUserManual.AutoSize = true;
            lblUserManual.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblUserManual.Location = new Point(31, 23);
            lblUserManual.Name = "lblUserManual";
            lblUserManual.Size = new Size(237, 31);
            lblUserManual.TabIndex = 1;
            lblUserManual.Text = "Hướng Dẫn Sử Dụng";
            // 
            // lblBaoCao
            // 
            lblBaoCao.AutoSize = true;
            lblBaoCao.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblBaoCao.Location = new Point(64, 21);
            lblBaoCao.Name = "lblBaoCao";
            lblBaoCao.Size = new Size(327, 50);
            lblBaoCao.TabIndex = 5;
            lblBaoCao.Text = "Hỗ Trợ Và Cài Đặt";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            label1.Location = new Point(93, 14);
            label1.Name = "label1";
            label1.Size = new Size(127, 38);
            label1.TabIndex = 0;
            label1.Text = "Trợ giúp";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(217, 244, 227);
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Controls.Add(lblBaoCao, 1, 1);
            tableLayoutPanel1.Controls.Add(txtSearch, 3, 1);
            tableLayoutPanel1.Controls.Add(roundedPanel3, 1, 3);
            tableLayoutPanel1.Controls.Add(roundedPanel2, 1, 5);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 43F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.Size = new Size(1221, 709);
            tableLayoutPanel1.TabIndex = 18;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.White;
            txtSearch.BorderRadius = 15;
            txtSearch.BorderThickness = 1;
            txtSearch.FocusBorderColor = SystemColors.ControlDark;
            txtSearch.HoverBorderColor = Color.DarkGray;
            txtSearch.Location = new Point(625, 24);
            txtSearch.Multiline = false;
            txtSearch.Name = "txtSearch";
            txtSearch.NormalBorderColor = Color.DarkGray;
            txtSearch.Padding = new Padding(9, 12, 9, 9);
            txtSearch.PasswordChar = '\0';
            txtSearch.PlaceholderText = "Tìm Kiếm...";
            txtSearch.ReadOnly = false;
            txtSearch.Size = new Size(531, 45);
            txtSearch.TabIndex = 20;
            txtSearch.UseSystemPasswordChar = false;
            // 
            // roundedPanel4
            // 
            roundedPanel4.BackColor = Color.WhiteSmoke;
            roundedPanel4.BorderColor = Color.Transparent;
            roundedPanel4.BorderRadius = 10;
            roundedPanel4.BorderSize = 0;
            roundedPanel4.Controls.Add(lblTheme);
            roundedPanel4.Controls.Add(cmbGiaoDien);
            roundedPanel4.Dock = DockStyle.Fill;
            roundedPanel4.Location = new Point(57, 117);
            roundedPanel4.Name = "roundedPanel4";
            roundedPanel4.Size = new Size(976, 50);
            roundedPanel4.TabIndex = 26;
            // 
            // cmbGiaoDien
            // 
            cmbGiaoDien.ArrowColor = Color.DimGray;
            cmbGiaoDien.BackColor = Color.White;
            cmbGiaoDien.BorderRadius = 15;
            cmbGiaoDien.BorderThickness = 2;
            cmbGiaoDien.DropDownBackColor = Color.White;
            cmbGiaoDien.DropDownHeight = 150;
            cmbGiaoDien.DropDownHoverColor = Color.DarkGray;
            cmbGiaoDien.FocusBorderColor = Color.FromArgb(64, 64, 64);
            cmbGiaoDien.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbGiaoDien.ForeColor = Color.Black;
            cmbGiaoDien.HoverBorderColor = Color.DimGray;
            cmbGiaoDien.Items.AddRange(new object[] { "Dark", "Light" });
            cmbGiaoDien.Location = new Point(629, 4);
            cmbGiaoDien.Name = "cmbGiaoDien";
            cmbGiaoDien.NormalBorderColor = Color.Gray;
            cmbGiaoDien.SelectedIndex = -1;
            cmbGiaoDien.SelectedItem = null;
            cmbGiaoDien.SelectedValue = null;
            cmbGiaoDien.Size = new Size(288, 35);
            cmbGiaoDien.TabIndex = 23;
            // 
            // lblTheme
            // 
            lblTheme.Anchor = AnchorStyles.Left;
            lblTheme.AutoSize = true;
            lblTheme.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblTheme.Location = new Point(48, 7);
            lblTheme.Name = "lblTheme";
            lblTheme.Size = new Size(202, 31);
            lblTheme.TabIndex = 3;
            lblTheme.Text = "Chủ Đề Giao Diện";
            lblTheme.Click += lblTheme_Click;
            // 
            // roundedPanel1
            // 
            roundedPanel1.BackColor = Color.WhiteSmoke;
            roundedPanel1.BorderColor = Color.Black;
            roundedPanel1.BorderRadius = 10;
            roundedPanel1.BorderSize = 0;
            roundedPanel1.Controls.Add(lblLanguage);
            roundedPanel1.Controls.Add(cmbNgonNgu);
            roundedPanel1.Dock = DockStyle.Fill;
            roundedPanel1.Location = new Point(57, 54);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(976, 50);
            roundedPanel1.TabIndex = 26;
            // 
            // cmbNgonNgu
            // 
            cmbNgonNgu.ArrowColor = Color.DimGray;
            cmbNgonNgu.BackColor = Color.White;
            cmbNgonNgu.BorderRadius = 15;
            cmbNgonNgu.BorderThickness = 2;
            cmbNgonNgu.DropDownBackColor = Color.White;
            cmbNgonNgu.DropDownHeight = 150;
            cmbNgonNgu.DropDownHoverColor = Color.DarkGray;
            cmbNgonNgu.FocusBorderColor = Color.FromArgb(64, 64, 64);
            cmbNgonNgu.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbNgonNgu.ForeColor = Color.Black;
            cmbNgonNgu.HoverBorderColor = Color.DimGray;
            cmbNgonNgu.Items.AddRange(new object[] { "Tiếng Việt", "English" });
            cmbNgonNgu.Location = new Point(629, 7);
            cmbNgonNgu.Name = "cmbNgonNgu";
            cmbNgonNgu.NormalBorderColor = Color.Gray;
            cmbNgonNgu.SelectedIndex = -1;
            cmbNgonNgu.SelectedItem = null;
            cmbNgonNgu.SelectedValue = null;
            cmbNgonNgu.Size = new Size(288, 35);
            cmbNgonNgu.TabIndex = 22;
            // 
            // lblLanguage
            // 
            lblLanguage.Anchor = AnchorStyles.Left;
            lblLanguage.AutoSize = true;
            lblLanguage.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblLanguage.Location = new Point(48, 9);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Size = new Size(127, 31);
            lblLanguage.TabIndex = 1;
            lblLanguage.Text = "Ngôn Ngữ";
            // 
            // lblSystemSettings
            // 
            lblSystemSettings.Anchor = AnchorStyles.None;
            lblSystemSettings.AutoSize = true;
            lblSystemSettings.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblSystemSettings.Location = new Point(421, 6);
            lblSystemSettings.Name = "lblSystemSettings";
            lblSystemSettings.Size = new Size(248, 38);
            lblSystemSettings.TabIndex = 16;
            lblSystemSettings.Text = "Cài Đặt Hệ Thống";
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.None;
            btnSave.BackColor = Color.FromArgb(0, 113, 0);
            btnSave.BaseColor = Color.FromArgb(0, 113, 0);
            btnSave.BorderColor = Color.Transparent;
            btnSave.BorderRadius = 10;
            btnSave.BorderSize = 0;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnSave.ForeColor = Color.White;
            btnSave.HoverColor = Color.FromArgb(34, 139, 34);
            btnSave.Location = new Point(471, 245);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(148, 35);
            btnSave.TabIndex = 21;
            btnSave.Text = "Lưu Cài Đặt";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // roundedPanel5
            // 
            roundedPanel5.BackColor = Color.WhiteSmoke;
            roundedPanel5.BorderColor = Color.Transparent;
            roundedPanel5.BorderRadius = 10;
            roundedPanel5.BorderSize = 0;
            roundedPanel5.Controls.Add(lblFaceID);
            roundedPanel5.Controls.Add(btnRegisterFace);
            roundedPanel5.Dock = DockStyle.Fill;
            roundedPanel5.Location = new Point(57, 180);
            roundedPanel5.Name = "roundedPanel5";
            roundedPanel5.Size = new Size(976, 50);
            roundedPanel5.TabIndex = 28;
            // 
            // btnRegisterFace
            // 
            btnRegisterFace.BackColor = Color.DarkGray;
            btnRegisterFace.BaseColor = Color.DarkGray;
            btnRegisterFace.BorderColor = Color.Transparent;
            btnRegisterFace.BorderRadius = 10;
            btnRegisterFace.BorderSize = 0;
            btnRegisterFace.FlatAppearance.BorderSize = 0;
            btnRegisterFace.FlatStyle = FlatStyle.Flat;
            btnRegisterFace.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 163);
            btnRegisterFace.ForeColor = Color.Black;
            btnRegisterFace.HoverColor = Color.Gray;
            btnRegisterFace.Location = new Point(629, 5);
            btnRegisterFace.Name = "btnRegisterFace";
            btnRegisterFace.Size = new Size(288, 36);
            btnRegisterFace.TabIndex = 25;
            btnRegisterFace.Text = "Cài Đặt Face ID";
            btnRegisterFace.UseVisualStyleBackColor = false;
            // 
            // lblFaceID
            // 
            lblFaceID.Anchor = AnchorStyles.Left;
            lblFaceID.AutoSize = true;
            lblFaceID.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblFaceID.Location = new Point(48, 11);
            lblFaceID.Name = "lblFaceID";
            lblFaceID.Size = new Size(90, 31);
            lblFaceID.TabIndex = 24;
            lblFaceID.Text = "Face ID";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 90F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.Controls.Add(roundedPanel5, 1, 7);
            tableLayoutPanel2.Controls.Add(btnSave, 1, 9);
            tableLayoutPanel2.Controls.Add(lblSystemSettings, 1, 1);
            tableLayoutPanel2.Controls.Add(roundedPanel1, 1, 3);
            tableLayoutPanel2.Controls.Add(roundedPanel4, 1, 5);
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 11;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 1.26582289F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 15.1898727F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 1.26582265F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 18.987339F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 2.5316453F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 18.987339F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 2.5316453F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 18.987339F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 2.5316453F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 15.1898727F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 2.5316453F));
            tableLayoutPanel2.Size = new Size(1092, 298);
            tableLayoutPanel2.TabIndex = 27;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 5;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 47F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 47F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel3.Controls.Add(roundedPanel6, 1, 3);
            tableLayoutPanel3.Controls.Add(lblUserSupport, 1, 1);
            tableLayoutPanel3.Controls.Add(roundedPanel7, 3, 3);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 5;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 2F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel3.Size = new Size(1092, 242);
            tableLayoutPanel3.TabIndex = 17;
            // 
            // roundedPanel6
            // 
            roundedPanel6.BackColor = Color.WhiteSmoke;
            roundedPanel6.BorderColor = Color.Transparent;
            roundedPanel6.BorderRadius = 20;
            roundedPanel6.BorderSize = 0;
            roundedPanel6.Controls.Add(label2);
            roundedPanel6.Controls.Add(label3);
            roundedPanel6.Controls.Add(lblUserManual);
            roundedPanel6.Controls.Add(lblViewDocument);
            roundedPanel6.Dock = DockStyle.Fill;
            roundedPanel6.Location = new Point(24, 67);
            roundedPanel6.Name = "roundedPanel6";
            roundedPanel6.Size = new Size(507, 163);
            roundedPanel6.TabIndex = 0;
            // 
            // roundedPanel7
            // 
            roundedPanel7.BackColor = Color.WhiteSmoke;
            roundedPanel7.BorderColor = Color.Transparent;
            roundedPanel7.BorderRadius = 20;
            roundedPanel7.BorderSize = 0;
            roundedPanel7.Controls.Add(label8);
            roundedPanel7.Controls.Add(label7);
            roundedPanel7.Dock = DockStyle.Fill;
            roundedPanel7.Location = new Point(558, 67);
            roundedPanel7.Name = "roundedPanel7";
            roundedPanel7.Size = new Size(507, 163);
            roundedPanel7.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label2.Location = new Point(31, 106);
            label2.Name = "label2";
            label2.Size = new Size(58, 31);
            label2.TabIndex = 3;
            label2.Text = "FAQ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label3.ForeColor = Color.Blue;
            label3.Location = new Point(285, 111);
            label3.Name = "label3";
            label3.Size = new Size(190, 25);
            label3.TabIndex = 4;
            label3.Text = "Câu Hỏi Thường Gặp";
            // 
            // Setting
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(tableLayoutPanel1);
            Name = "Setting";
            Size = new Size(1229, 720);
            roundedPanel3.ResumeLayout(false);
            roundedPanel2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            roundedPanel4.ResumeLayout(false);
            roundedPanel4.PerformLayout();
            roundedPanel1.ResumeLayout(false);
            roundedPanel1.PerformLayout();
            roundedPanel5.ResumeLayout(false);
            roundedPanel5.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            roundedPanel6.ResumeLayout(false);
            roundedPanel6.PerformLayout();
            roundedPanel7.ResumeLayout(false);
            roundedPanel7.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private RoundedPanel roundedPanel2;
        private Label label1;

        private Label lblBaoCao;
        private Label lblViewDocument;
        private Label lblUserManual;
        private Label label8;
        private Label label7;
        private Label lblUserSupport;
        private RoundedPanel roundedPanel3;
        private TableLayoutPanel tableLayoutPanel1;
        private RoundedTextBox txtSearch;
        private TableLayoutPanel tableLayoutPanel3;
        private RoundedPanel roundedPanel6;
        private RoundedPanel roundedPanel7;
        private TableLayoutPanel tableLayoutPanel2;
        private RoundedPanel roundedPanel5;
        private Label lblFaceID;
        private RoundedButton btnRegisterFace;
        private RoundedButton btnSave;
        private Label lblSystemSettings;
        private RoundedPanel roundedPanel1;
        private Label lblLanguage;
        private CustomComboBox cmbNgonNgu;
        private RoundedPanel roundedPanel4;
        private Label lblTheme;
        private CustomComboBox cmbGiaoDien;
        private Label label2;
        private Label label3;
    }
}