using Environmental_Monitoring.View.Components;
using System.Windows.Forms;

namespace Environmental_Monitoring.View
{
    partial class AI
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
            lblTitle = new Label();
            lblPanelResign = new Label();
            roundedPanel5 = new RoundedPanel();
            customComboBox1 = new CustomComboBox();
            btnPredictPollution = new RoundedButton();
            lblPollutionLevel = new Label();
            lblPanelPollution = new Label();
            roundedPanel6 = new RoundedPanel();
            txtCustomerName = new RoundedTextBox();
            btnPredictResign = new RoundedButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            txtSearch = new RoundedTextBox();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            roundedPanel5.SuspendLayout();
            roundedPanel6.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblTitle.Location = new Point(64, 21);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(224, 50);
            lblTitle.TabIndex = 5;
            lblTitle.Text = "AI Dự Đoán";
            // 
            // lblPanelResign
            // 
            lblPanelResign.AutoSize = true;
            lblPanelResign.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblPanelResign.Location = new Point(30, 9);
            lblPanelResign.Name = "lblPanelResign";
            lblPanelResign.Size = new Size(94, 38);
            lblPanelResign.TabIndex = 0;
            lblPanelResign.Text = "Tái Ký";
            // 
            // roundedPanel5
            // 
            roundedPanel5.BackColor = Color.White;
            roundedPanel5.BorderColor = Color.Transparent;
            roundedPanel5.BorderRadius = 20;
            roundedPanel5.BorderSize = 0;
            roundedPanel5.Controls.Add(customComboBox1);
            roundedPanel5.Controls.Add(btnPredictPollution);
            roundedPanel5.Controls.Add(lblPollutionLevel);
            roundedPanel5.Controls.Add(lblPanelPollution);
            roundedPanel5.Dock = DockStyle.Fill;
            roundedPanel5.Location = new Point(625, 115);
            roundedPanel5.Name = "roundedPanel5";
            roundedPanel5.Size = new Size(531, 554);
            roundedPanel5.TabIndex = 9;
            // 
            // customComboBox1
            // 
            customComboBox1.ArrowColor = Color.DimGray;
            customComboBox1.BackColor = Color.White;
            customComboBox1.BorderRadius = 15;
            customComboBox1.BorderThickness = 2;
            customComboBox1.DropDownBackColor = Color.White;
            customComboBox1.DropDownHeight = 150;
            customComboBox1.DropDownHoverColor = Color.DodgerBlue;
            customComboBox1.FocusBorderColor = Color.HotPink;
            customComboBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            customComboBox1.ForeColor = Color.Black;
            customComboBox1.HoverBorderColor = Color.DodgerBlue;
            customComboBox1.Items.AddRange(new object[] { "Thành phố Hà Nội", "", "", "Thành phố Hải Phòng", "", "", "Thành phố Huế", "", "", "Thành phố Đà Nẵng", "", "", "Thành phố Hồ Chí Minh ", "Thành phố Cần Thơ ", "An Giang", "", "", "Bắc Ninh", "Cà Mau", "", "", "Cao Bằng", "", "", "Đắk Lắk", "", "", "Điện Biên", "", "", "Đồng Nai", "Đồng Tháp", "", "", "Gia Lai", "", "", "Hà Tĩnh", "", "", "Hưng Yên", "", "", "Khánh Hòa", "Lai Châu", "", "", "Lạng Sơn", "", "", "Lào Cai", "", "", "Lâm Đồng", "Nghệ An", "", "", "Ninh Bình", "", "", "Phú Thọ", "", "", "Quảng Ngãi ", "", "", "Quảng Ninh", "", "", "Quảng Trị", "", "", "Sơn La", "", "", "Tây Ninh", "Thái Nguyên", "", "", "Thanh Hóa", "", "", "Tuyên Quang", "Vĩnh Long" });
            customComboBox1.Location = new Point(40, 66);
            customComboBox1.Name = "customComboBox1";
            customComboBox1.NormalBorderColor = Color.Gray;
            customComboBox1.SelectedIndex = -1;
            customComboBox1.SelectedItem = null;
            customComboBox1.SelectedValue = null;
            customComboBox1.Size = new Size(278, 38);
            customComboBox1.TabIndex = 19;
            // 
            // btnPredictPollution
            // 
            btnPredictPollution.BackColor = Color.FromArgb(0, 113, 0);
            btnPredictPollution.BaseColor = Color.FromArgb(0, 113, 0);
            btnPredictPollution.BorderColor = Color.Transparent;
            btnPredictPollution.BorderRadius = 15;
            btnPredictPollution.BorderSize = 0;
            btnPredictPollution.FlatAppearance.BorderSize = 0;
            btnPredictPollution.FlatStyle = FlatStyle.Flat;
            btnPredictPollution.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnPredictPollution.ForeColor = Color.White;
            btnPredictPollution.HoverColor = Color.FromArgb(34, 139, 34);
            btnPredictPollution.Location = new Point(369, 66);
            btnPredictPollution.Name = "btnPredictPollution";
            btnPredictPollution.Size = new Size(120, 40);
            btnPredictPollution.TabIndex = 18;
            btnPredictPollution.Text = "Dự Đoán";
            btnPredictPollution.UseVisualStyleBackColor = false;
            // 
            // lblPollutionLevel
            // 
            lblPollutionLevel.AutoSize = true;
            lblPollutionLevel.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblPollutionLevel.Location = new Point(40, 117);
            lblPollutionLevel.Name = "lblPollutionLevel";
            lblPollutionLevel.Size = new Size(159, 25);
            lblPollutionLevel.TabIndex = 8;
            lblPollutionLevel.Text = "Mức Độ Ô Nhiễm";
            // 
            // lblPanelPollution
            // 
            lblPanelPollution.AutoSize = true;
            lblPanelPollution.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblPanelPollution.Location = new Point(30, 14);
            lblPanelPollution.Name = "lblPanelPollution";
            lblPanelPollution.Size = new Size(134, 38);
            lblPanelPollution.TabIndex = 1;
            lblPanelPollution.Text = "Ô Nhiễm";
            // 
            // roundedPanel6
            // 
            roundedPanel6.BackColor = Color.White;
            roundedPanel6.BorderColor = Color.Transparent;
            roundedPanel6.BorderRadius = 20;
            roundedPanel6.BorderSize = 0;
            roundedPanel6.Controls.Add(txtCustomerName);
            roundedPanel6.Controls.Add(btnPredictResign);
            roundedPanel6.Controls.Add(lblPanelResign);
            roundedPanel6.Dock = DockStyle.Fill;
            roundedPanel6.Location = new Point(64, 115);
            roundedPanel6.Name = "roundedPanel6";
            roundedPanel6.Size = new Size(531, 554);
            roundedPanel6.TabIndex = 8;
            // 
            // txtCustomerName
            // 
            txtCustomerName.BorderRadius = 15;
            txtCustomerName.BorderThickness = 2;
            txtCustomerName.FocusBorderColor = Color.DimGray;
            txtCustomerName.HoverBorderColor = Color.DarkGray;
            txtCustomerName.Location = new Point(30, 60);
            txtCustomerName.Multiline = false;
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.NormalBorderColor = Color.Gray;
            txtCustomerName.Padding = new Padding(10);
            txtCustomerName.PasswordChar = '\0';
            txtCustomerName.PlaceholderText = "Nhập Tên Khách Hàng";
            txtCustomerName.ReadOnly = false;
            txtCustomerName.Size = new Size(323, 40);
            txtCustomerName.TabIndex = 21;
            txtCustomerName.UseSystemPasswordChar = false;
            // 
            // btnPredictResign
            // 
            btnPredictResign.BackColor = Color.FromArgb(0, 113, 0);
            btnPredictResign.BaseColor = Color.FromArgb(0, 113, 0);
            btnPredictResign.BorderColor = Color.Transparent;
            btnPredictResign.BorderRadius = 15;
            btnPredictResign.BorderSize = 0;
            btnPredictResign.FlatAppearance.BorderSize = 0;
            btnPredictResign.FlatStyle = FlatStyle.Flat;
            btnPredictResign.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnPredictResign.ForeColor = Color.White;
            btnPredictResign.HoverColor = Color.FromArgb(34, 139, 34);
            btnPredictResign.Location = new Point(389, 60);
            btnPredictResign.Name = "btnPredictResign";
            btnPredictResign.Size = new Size(105, 40);
            btnPredictResign.TabIndex = 20;
            btnPredictResign.Text = "Dự Đoán";
            btnPredictResign.UseVisualStyleBackColor = false;
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
            tableLayoutPanel1.Controls.Add(lblTitle, 1, 1);
            tableLayoutPanel1.Controls.Add(roundedPanel5, 3, 3);
            tableLayoutPanel1.Controls.Add(roundedPanel6, 1, 3);
            tableLayoutPanel1.Controls.Add(txtSearch, 3, 1);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 79F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Size = new Size(1221, 709);
            tableLayoutPanel1.TabIndex = 10;
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
            txtSearch.TabIndex = 21;
            txtSearch.UseSystemPasswordChar = false;
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // AI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "AI";
            Size = new Size(1227, 715);
            roundedPanel5.ResumeLayout(false);
            roundedPanel5.PerformLayout();
            roundedPanel6.ResumeLayout(false);
            roundedPanel6.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label lblTitle;

        private Label lblPanelResign;
        private RoundedPanel roundedPanel5;
    
        private RoundedButton btnPredictPollution;
        private Label lblPollutionLevel;
        private Label lblPanelPollution;
        private RoundedPanel roundedPanel6;
        private RoundedButton btnPredictResign;

        private TableLayoutPanel tableLayoutPanel1;
        private RoundedTextBox txtSearch;
        private RoundedTextBox txtCustomerName;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private CustomComboBox customComboBox1;
    }
}