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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 30D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 70D);
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            lblAI = new Label();
            chartTaiKy = new System.Windows.Forms.DataVisualization.Charting.Chart();
            lblTaiKy = new Label();
            roundedPanel5 = new RoundedPanel();
            chartONhiem = new System.Windows.Forms.DataVisualization.Charting.Chart();
            btnGuess2 = new RoundedButton();
            cmbDiaPhuong = new ComboBox();
            lblMucDoONhiem = new Label();
            lblONhiem = new Label();
            roundedPanel6 = new RoundedPanel();
            btnGuess1 = new RoundedButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            roundedTextBox1 = new RoundedTextBox();
            ((System.ComponentModel.ISupportInitialize)chartTaiKy).BeginInit();
            roundedPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartONhiem).BeginInit();
            roundedPanel6.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblAI
            // 
            lblAI.AutoSize = true;
            lblAI.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblAI.Location = new Point(64, 21);
            lblAI.Name = "lblAI";
            lblAI.Size = new Size(224, 50);
            lblAI.TabIndex = 5;
            lblAI.Text = "AI Dự Đoán";
            // 
            // chartTaiKy
            // 
            chartArea1.Name = "ChartArea1";
            chartTaiKy.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartTaiKy.Legends.Add(legend1);
            chartTaiKy.Location = new Point(3, 136);
            chartTaiKy.Name = "chartTaiKy";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            dataPoint1.Color = Color.FromArgb(0, 192, 192);
            dataPoint1.CustomProperties = "PieLabelStyle=Disabled";
            dataPoint1.Label = "   ";
            dataPoint2.Color = Color.FromArgb(255, 125, 125);
            dataPoint2.CustomProperties = "PieLabelStyle=Disabled";
            dataPoint2.Label = "Khả Năng Tái kí";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            chartTaiKy.Series.Add(series1);
            chartTaiKy.Size = new Size(523, 395);
            chartTaiKy.TabIndex = 17;
            chartTaiKy.Text = "chart3";
            // 
            // lblTaiKy
            // 
            lblTaiKy.AutoSize = true;
            lblTaiKy.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblTaiKy.Location = new Point(28, 9);
            lblTaiKy.Name = "lblTaiKy";
            lblTaiKy.Size = new Size(94, 38);
            lblTaiKy.TabIndex = 0;
            lblTaiKy.Text = "Tái Ký";
            // 
            // roundedPanel5
            // 
            roundedPanel5.BackColor = Color.White;
            roundedPanel5.BorderColor = Color.Transparent;
            roundedPanel5.BorderRadius = 20;
            roundedPanel5.BorderSize = 0;
            roundedPanel5.Controls.Add(chartONhiem);
            roundedPanel5.Controls.Add(btnGuess2);
            roundedPanel5.Controls.Add(cmbDiaPhuong);
            roundedPanel5.Controls.Add(lblMucDoONhiem);
            roundedPanel5.Controls.Add(lblONhiem);
            roundedPanel5.Dock = DockStyle.Fill;
            roundedPanel5.Location = new Point(625, 115);
            roundedPanel5.Name = "roundedPanel5";
            roundedPanel5.Size = new Size(531, 554);
            roundedPanel5.TabIndex = 9;
            // 
            // chartONhiem
            // 
            chartArea2.Name = "ChartArea1";
            chartONhiem.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chartONhiem.Legends.Add(legend2);
            chartONhiem.Location = new Point(3, 162);
            chartONhiem.Name = "chartONhiem";
            series2.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            series2.BorderColor = Color.DarkGray;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.EmptyPointStyle.IsValueShownAsLabel = true;
            series2.LabelBorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            series2.Legend = "Legend1";
            series2.Name = "2025";
            series2.SmartLabelStyle.CalloutLineAnchorCapStyle = System.Windows.Forms.DataVisualization.Charting.LineAnchorCapStyle.None;
            chartONhiem.Series.Add(series2);
            chartONhiem.Size = new Size(523, 369);
            chartONhiem.TabIndex = 19;
            chartONhiem.Text = "chart4";
            // 
            // btnGuess2
            // 
            btnGuess2.BackColor = Color.FromArgb(0, 113, 0);
            btnGuess2.BaseColor = Color.FromArgb(0, 113, 0);
            btnGuess2.BorderColor = Color.Transparent;
            btnGuess2.BorderRadius = 15;
            btnGuess2.BorderSize = 0;
            btnGuess2.FlatAppearance.BorderSize = 0;
            btnGuess2.FlatStyle = FlatStyle.Flat;
            btnGuess2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnGuess2.ForeColor = Color.White;
            btnGuess2.HoverColor = Color.FromArgb(34, 139, 34);
            btnGuess2.Location = new Point(367, 110);
            btnGuess2.Name = "btnGuess2";
            btnGuess2.Size = new Size(121, 37);
            btnGuess2.TabIndex = 18;
            btnGuess2.Text = "Dự Đoán";
            btnGuess2.UseVisualStyleBackColor = false;
            // 
            // cmbDiaPhuong
            // 
            cmbDiaPhuong.ForeColor = SystemColors.WindowText;
            cmbDiaPhuong.FormattingEnabled = true;
            cmbDiaPhuong.Location = new Point(79, 69);
            cmbDiaPhuong.Name = "cmbDiaPhuong";
            cmbDiaPhuong.Size = new Size(225, 28);
            cmbDiaPhuong.TabIndex = 15;
            cmbDiaPhuong.Text = "Hà Nội";
            // 
            // lblMucDoONhiem
            // 
            lblMucDoONhiem.AutoSize = true;
            lblMucDoONhiem.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblMucDoONhiem.Location = new Point(40, 117);
            lblMucDoONhiem.Name = "lblMucDoONhiem";
            lblMucDoONhiem.Size = new Size(159, 25);
            lblMucDoONhiem.TabIndex = 8;
            lblMucDoONhiem.Text = "Mức Độ Ô Nhiễm";
            // 
            // lblONhiem
            // 
            lblONhiem.AutoSize = true;
            lblONhiem.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblONhiem.Location = new Point(40, 14);
            lblONhiem.Name = "lblONhiem";
            lblONhiem.Size = new Size(134, 38);
            lblONhiem.TabIndex = 1;
            lblONhiem.Text = "Ô Nhiễm";
            // 
            // roundedPanel6
            // 
            roundedPanel6.BackColor = Color.White;
            roundedPanel6.BorderColor = Color.Transparent;
            roundedPanel6.BorderRadius = 20;
            roundedPanel6.BorderSize = 0;
            roundedPanel6.Controls.Add(btnGuess1);
            roundedPanel6.Controls.Add(chartTaiKy);
            roundedPanel6.Controls.Add(lblTaiKy);
            roundedPanel6.Dock = DockStyle.Fill;
            roundedPanel6.Location = new Point(64, 115);
            roundedPanel6.Name = "roundedPanel6";
            roundedPanel6.Size = new Size(531, 554);
            roundedPanel6.TabIndex = 8;
            // 
            // btnGuess1
            // 
            btnGuess1.BackColor = Color.FromArgb(0, 113, 0);
            btnGuess1.BaseColor = Color.FromArgb(0, 113, 0);
            btnGuess1.BorderColor = Color.Transparent;
            btnGuess1.BorderRadius = 15;
            btnGuess1.BorderSize = 0;
            btnGuess1.FlatAppearance.BorderSize = 0;
            btnGuess1.FlatStyle = FlatStyle.Flat;
            btnGuess1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnGuess1.ForeColor = Color.White;
            btnGuess1.HoverColor = Color.FromArgb(34, 139, 34);
            btnGuess1.Location = new Point(379, 61);
            btnGuess1.Name = "btnGuess1";
            btnGuess1.Size = new Size(105, 37);
            btnGuess1.TabIndex = 20;
            btnGuess1.Text = "Dự Đoán";
            btnGuess1.UseVisualStyleBackColor = false;
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
            tableLayoutPanel1.Controls.Add(lblAI, 1, 1);
            tableLayoutPanel1.Controls.Add(roundedPanel5, 3, 3);
            tableLayoutPanel1.Controls.Add(roundedPanel6, 1, 3);
            tableLayoutPanel1.Controls.Add(roundedTextBox1, 3, 1);
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
            // roundedTextBox1
            // 
            roundedTextBox1.BackColor = Color.White;
            roundedTextBox1.BorderRadius = 15;
            roundedTextBox1.BorderThickness = 1;
            roundedTextBox1.FocusBorderColor = SystemColors.ControlDark;
            roundedTextBox1.HoverBorderColor = Color.DarkGray;
            roundedTextBox1.Location = new Point(625, 24);
            roundedTextBox1.Multiline = false;
            roundedTextBox1.Name = "roundedTextBox1";
            roundedTextBox1.NormalBorderColor = Color.DarkGray;
            roundedTextBox1.Padding = new Padding(9, 12, 9, 9);
            roundedTextBox1.PasswordChar = '\0';
            roundedTextBox1.PlaceholderText = "Tìm Kiếm...";
            roundedTextBox1.ReadOnly = false;
            roundedTextBox1.Size = new Size(531, 45);
            roundedTextBox1.TabIndex = 21;
            // 
            // AI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "AI";
            Size = new Size(1227, 715);
            Load += AIguess_Load;
            ((System.ComponentModel.ISupportInitialize)chartTaiKy).EndInit();
            roundedPanel5.ResumeLayout(false);
            roundedPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartONhiem).EndInit();
            roundedPanel6.ResumeLayout(false);
            roundedPanel6.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label lblAI;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTaiKy;
        private Label lblTaiKy;
        private RoundedPanel roundedPanel5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartONhiem;
        private RoundedButton btnGuess2;
        private ComboBox cmbDiaPhuong;
        private Label lblMucDoONhiem;
        private Label lblONhiem;
        private RoundedPanel roundedPanel6;
        private RoundedButton btnGuess1;

        private TableLayoutPanel tableLayoutPanel1;
        private RoundedTextBox roundedTextBox1;
    }
}