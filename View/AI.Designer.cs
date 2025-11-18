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
            panelPollution = new RoundedPanel();
            btnPredictPollution = new RoundedButton();
            lblPollutionLevel = new Label();
            lblPanelPollution = new Label();
            cmbLocation = new CustomComboBox();
            panelResign = new RoundedPanel();
            txtCustomerName = new RoundedTextBox();
            btnPredictResign = new RoundedButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            txtSearch = new RoundedTextBox();
            panelPollution.SuspendLayout();
            panelResign.SuspendLayout();
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
            // panelPollution
            // 
            panelPollution.BackColor = Color.White;
            panelPollution.BorderColor = Color.Transparent;
            panelPollution.BorderRadius = 20;
            panelPollution.BorderSize = 0;
            panelPollution.Controls.Add(btnPredictPollution);
            panelPollution.Controls.Add(lblPollutionLevel);
            panelPollution.Controls.Add(lblPanelPollution);
            panelPollution.Controls.Add(cmbLocation);
            panelPollution.Dock = DockStyle.Fill;
            panelPollution.Location = new Point(625, 115);
            panelPollution.Name = "panelPollution";
            panelPollution.Size = new Size(531, 554);
            panelPollution.TabIndex = 9;
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
            btnPredictPollution.Click += btnPredictPollution_Click;
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
            // cmbLocation
            // 
            cmbLocation.ArrowColor = Color.DimGray;
            cmbLocation.BackColor = Color.White;
            cmbLocation.BorderRadius = 10;
            cmbLocation.BorderThickness = 2;
            cmbLocation.DropDownBackColor = Color.White;
            cmbLocation.DropDownHeight = 150;
            cmbLocation.DropDownHoverColor = Color.DodgerBlue;
            cmbLocation.FocusBorderColor = Color.HotPink;
            cmbLocation.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbLocation.ForeColor = Color.Black;
            cmbLocation.HoverBorderColor = Color.DodgerBlue;
            cmbLocation.Location = new Point(40, 66);
            cmbLocation.Name = "cmbLocation";
            cmbLocation.NormalBorderColor = Color.Gray;
            cmbLocation.SelectedIndex = -1;
            cmbLocation.SelectedItem = null;
            cmbLocation.SelectedValue = null;
            cmbLocation.Size = new Size(278, 40);
            cmbLocation.TabIndex = 22;
            // 
            // panelResign
            // 
            panelResign.BackColor = Color.White;
            panelResign.BorderColor = Color.Transparent;
            panelResign.BorderRadius = 20;
            panelResign.BorderSize = 0;
            panelResign.Controls.Add(txtCustomerName);
            panelResign.Controls.Add(btnPredictResign);
            panelResign.Controls.Add(lblPanelResign);
            panelResign.Dock = DockStyle.Fill;
            panelResign.Location = new Point(64, 115);
            panelResign.Name = "panelResign";
            panelResign.Size = new Size(531, 554);
            panelResign.TabIndex = 8;
            // 
            // txtCustomerName
            // 
            txtCustomerName.BorderRadius = 10;
            txtCustomerName.BorderThickness = 2;
            txtCustomerName.FocusBorderColor = Color.DimGray;
            txtCustomerName.HoverBorderColor = Color.DarkGray;
            txtCustomerName.Location = new Point(30, 60);
            txtCustomerName.Multiline = false;
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.NormalBorderColor = Color.Gray;
            txtCustomerName.Padding = new Padding(7, 10, 7, 7);
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
            btnPredictResign.Click += btnPredictResign_Click;
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
            tableLayoutPanel1.Controls.Add(panelPollution, 3, 3);
            tableLayoutPanel1.Controls.Add(panelResign, 1, 3);
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
            // AI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(tableLayoutPanel1);
            Name = "AI";
            Size = new Size(1227, 715);
            panelPollution.ResumeLayout(false);
            panelPollution.PerformLayout();
            panelResign.ResumeLayout(false);
            panelResign.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label lblTitle;

        private Label lblPanelResign;
        private RoundedPanel panelPollution;
    
        private RoundedButton btnPredictPollution;
        private Label lblPollutionLevel;
        private Label lblPanelPollution;
        private RoundedPanel panelResign;
        private RoundedButton btnPredictResign;

        private TableLayoutPanel tableLayoutPanel1;
        private RoundedTextBox txtSearch;
        private RoundedTextBox txtCustomerName;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPollution;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartResign;
        private CustomComboBox cmbLocation;
        private CustomDateTimePicker customDateTimePicker1;
    }
}