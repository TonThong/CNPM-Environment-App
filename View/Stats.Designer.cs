﻿using Environmental_Monitoring.View.Components;

namespace Environmental_Monitoring.View
{
    partial class Stats
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            lblOrderCountTitle = new Label();
            lblOnTimeRateTitle = new Label();
            txtSearch = new RoundedTextBox();
            roundedPanel4 = new RoundedPanel();
            cmbNam = new CustomComboBox();
            cmbQuy = new CustomComboBox();
            btnApply = new RoundedButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblTitle = new Label();
            chartOrderQuantity = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartOnTimeRate = new System.Windows.Forms.DataVisualization.Charting.Chart();
            roundedPanel4.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartOrderQuantity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartOnTimeRate).BeginInit();
            SuspendLayout();
            // 
            // lblOrderCountTitle
            // 
            lblOrderCountTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblOrderCountTitle.AutoSize = true;
            lblOrderCountTitle.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblOrderCountTitle.Location = new Point(64, 182);
            lblOrderCountTitle.Name = "lblOrderCountTitle";
            lblOrderCountTitle.Size = new Size(531, 70);
            lblOrderCountTitle.TabIndex = 16;
            lblOrderCountTitle.Text = "Số Lượng Đơn Hàng";
            lblOrderCountTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblOnTimeRateTitle
            // 
            lblOnTimeRateTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblOnTimeRateTitle.AutoSize = true;
            lblOnTimeRateTitle.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblOnTimeRateTitle.Location = new Point(625, 182);
            lblOnTimeRateTitle.Name = "lblOnTimeRateTitle";
            lblOnTimeRateTitle.Size = new Size(531, 70);
            lblOnTimeRateTitle.TabIndex = 19;
            lblOnTimeRateTitle.Text = "Tỷ Lệ Đúng/Trễ Hẹn";
            lblOnTimeRateTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.White;
            txtSearch.BorderRadius = 15;
            txtSearch.BorderThickness = 1;
            txtSearch.FocusBorderColor = Color.DimGray;
            txtSearch.HoverBorderColor = Color.DarkGray;
            txtSearch.Location = new Point(625, 24);
            txtSearch.Multiline = false;
            txtSearch.Name = "txtSearch";
            txtSearch.NormalBorderColor = Color.LightGray;
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
            roundedPanel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            roundedPanel4.BackColor = Color.White;
            roundedPanel4.BorderColor = Color.Transparent;
            roundedPanel4.BorderRadius = 20;
            roundedPanel4.BorderSize = 0;
            roundedPanel4.Controls.Add(cmbNam);
            roundedPanel4.Controls.Add(cmbQuy);
            roundedPanel4.Controls.Add(btnApply);
            roundedPanel4.Location = new Point(64, 115);
            roundedPanel4.Name = "roundedPanel4";
            roundedPanel4.Size = new Size(531, 64);
            roundedPanel4.TabIndex = 18;
            // 
            // cmbNam
            // 
            cmbNam.ArrowColor = Color.DimGray;
            cmbNam.BackColor = Color.White;
            cmbNam.BorderRadius = 15;
            cmbNam.BorderThickness = 2;
            cmbNam.DropDownBackColor = Color.White;
            cmbNam.DropDownHeight = 150;
            cmbNam.DropDownHoverColor = Color.Gainsboro;
            cmbNam.FocusBorderColor = Color.DimGray;
            cmbNam.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbNam.ForeColor = Color.Black;
            cmbNam.HoverBorderColor = Color.DarkGray;
            cmbNam.Location = new Point(199, 9);
            cmbNam.Name = "cmbNam";
            cmbNam.NormalBorderColor = Color.LightGray;
            cmbNam.SelectedIndex = -1;
            cmbNam.SelectedItem = null;
            cmbNam.SelectedValue = null;
            cmbNam.Size = new Size(144, 45);
            cmbNam.TabIndex = 18;
            // 
            // cmbQuy
            // 
            cmbQuy.ArrowColor = Color.DimGray;
            cmbQuy.BackColor = Color.White;
            cmbQuy.BorderRadius = 15;
            cmbQuy.BorderThickness = 2;
            cmbQuy.DropDownBackColor = Color.White;
            cmbQuy.DropDownHeight = 150;
            cmbQuy.DropDownHoverColor = Color.Gainsboro;
            cmbQuy.FocusBorderColor = Color.DimGray;
            cmbQuy.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbQuy.ForeColor = Color.Black;
            cmbQuy.HoverBorderColor = Color.DarkGray;
            cmbQuy.Location = new Point(28, 9);
            cmbQuy.Name = "cmbQuy";
            cmbQuy.NormalBorderColor = Color.LightGray;
            cmbQuy.SelectedIndex = -1;
            cmbQuy.SelectedItem = null;
            cmbQuy.SelectedValue = null;
            cmbQuy.Size = new Size(144, 45);
            cmbQuy.TabIndex = 17;
            // 
            // btnApply
            // 
            btnApply.BackColor = Color.FromArgb(0, 113, 0);
            btnApply.BaseColor = Color.FromArgb(0, 113, 0);
            btnApply.BorderColor = Color.Transparent;
            btnApply.BorderRadius = 15;
            btnApply.BorderSize = 0;
            btnApply.FlatAppearance.BorderSize = 0;
            btnApply.FlatStyle = FlatStyle.Flat;
            btnApply.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnApply.ForeColor = Color.White;
            btnApply.HoverColor = Color.FromArgb(34, 139, 34);
            btnApply.Location = new Point(400, 9);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(110, 45);
            btnApply.TabIndex = 16;
            btnApply.Text = "Áp Dụng";
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += btnApply_Click;
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
            tableLayoutPanel1.Controls.Add(roundedPanel4, 1, 3);
            tableLayoutPanel1.Controls.Add(txtSearch, 3, 1);
            tableLayoutPanel1.Controls.Add(lblOnTimeRateTitle, 3, 4);
            tableLayoutPanel1.Controls.Add(lblOrderCountTitle, 1, 4);
            tableLayoutPanel1.Controls.Add(chartOrderQuantity, 1, 5);
            tableLayoutPanel1.Controls.Add(chartOnTimeRate, 3, 5);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 59F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Size = new Size(1221, 709);
            tableLayoutPanel1.TabIndex = 26;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblTitle.Location = new Point(64, 21);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(531, 70);
            lblTitle.TabIndex = 5;
            lblTitle.Text = "Báo Cáo Và Thống Kê";
            // 
            // chartOrderQuantity
            // 
            chartArea3.Name = "ChartArea1";
            chartOrderQuantity.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            chartOrderQuantity.Legends.Add(legend3);
            chartOrderQuantity.Location = new Point(64, 255);
            chartOrderQuantity.Name = "chartOrderQuantity";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            chartOrderQuantity.Series.Add(series3);
            chartOrderQuantity.Size = new Size(531, 412);
            chartOrderQuantity.TabIndex = 21;
            chartOrderQuantity.Text = "chartOrderQuantity";
            // 
            // chartOnTimeRate
            // 
            chartArea4.Name = "ChartArea1";
            chartOnTimeRate.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            chartOnTimeRate.Legends.Add(legend4);
            chartOnTimeRate.Location = new Point(625, 255);
            chartOnTimeRate.Name = "chartOnTimeRate";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            chartOnTimeRate.Series.Add(series4);
            chartOnTimeRate.Size = new Size(531, 412);
            chartOnTimeRate.TabIndex = 22;
            chartOnTimeRate.Text = "chartOnTimeRate";
            // 
            // Stats
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "Stats";
            Size = new Size(1227, 715);
            Load += Stats_Load;
            roundedPanel4.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartOrderQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartOnTimeRate).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblOrderCountTitle;
        private Label lblOnTimeRateTitle;
        private RoundedTextBox txtSearch;
        private RoundedPanel roundedPanel4;
        private CustomComboBox cmbNam;
        private CustomComboBox cmbQuy;
        private RoundedButton btnApply;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblTitle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartOrderQuantity;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartOnTimeRate;
    }
}