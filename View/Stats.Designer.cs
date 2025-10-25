using Environmental_Monitoring.View.Components;

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
            comboBox1 = new ComboBox();
            cmbQuy = new ComboBox();
            lblBaoCao = new Label();
            lblTyLe = new Label();
            lblSoLuongDonHang = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            label3 = new Label();
            roundedPanel4 = new RoundedPanel();
            comboBox2 = new ComboBox();
            comboBox3 = new ComboBox();
            roundedButton2 = new RoundedButton();
            label2 = new Label();
            label4 = new Label();
            roundedTextBox1 = new RoundedTextBox();
            label1 = new Label();
            tableLayoutPanel1.SuspendLayout();
            roundedPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(209, 21);
            comboBox1.Margin = new Padding(10, 3, 3, 3);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 1;
            comboBox1.Text = "Năm";
            // 
            // cmbQuy
            // 
            cmbQuy.FormattingEnabled = true;
            cmbQuy.Location = new Point(32, 21);
            cmbQuy.Margin = new Padding(10, 3, 3, 3);
            cmbQuy.Name = "cmbQuy";
            cmbQuy.Size = new Size(151, 28);
            cmbQuy.TabIndex = 0;
            cmbQuy.Text = "Quý";
            // 
            // lblBaoCao
            // 
            lblBaoCao.AutoSize = true;
            lblBaoCao.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblBaoCao.Location = new Point(25, 18);
            lblBaoCao.Name = "lblBaoCao";
            lblBaoCao.Size = new Size(358, 46);
            lblBaoCao.TabIndex = 5;
            lblBaoCao.Text = "Báo Cáo Và Thống Kê";
            // 
            // lblTyLe
            // 
            lblTyLe.AutoSize = true;
            lblTyLe.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblTyLe.Location = new Point(474, 165);
            lblTyLe.Name = "lblTyLe";
            lblTyLe.Size = new Size(226, 31);
            lblTyLe.TabIndex = 4;
            lblTyLe.Text = "Tỷ Lệ Đúng/Trễ Hẹn";
            // 
            // lblSoLuongDonHang
            // 
            lblSoLuongDonHang.AutoSize = true;
            lblSoLuongDonHang.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblSoLuongDonHang.Location = new Point(25, 165);
            lblSoLuongDonHang.Name = "lblSoLuongDonHang";
            lblSoLuongDonHang.Size = new Size(231, 31);
            lblSoLuongDonHang.TabIndex = 3;
            lblSoLuongDonHang.Text = "Số Lượng Đơn Hàng";
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
            tableLayoutPanel1.Controls.Add(label3, 1, 1);
            tableLayoutPanel1.Controls.Add(roundedPanel4, 1, 3);
            tableLayoutPanel1.Controls.Add(label2, 1, 4);
            tableLayoutPanel1.Controls.Add(label4, 3, 4);
            tableLayoutPanel1.Controls.Add(roundedTextBox1, 3, 1);
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
            tableLayoutPanel1.TabIndex = 19;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label3.Location = new Point(64, 21);
            label3.Name = "label3";
            label3.Size = new Size(531, 70);
            label3.TabIndex = 5;
            label3.Text = "Báo Cáo Và Thống Kê";
            // 
            // roundedPanel4
            // 
            roundedPanel4.BackColor = Color.White;
            roundedPanel4.BorderColor = Color.Transparent;
            roundedPanel4.BorderRadius = 20;
            roundedPanel4.BorderSize = 0;
            roundedPanel4.Controls.Add(comboBox2);
            roundedPanel4.Controls.Add(comboBox3);
            roundedPanel4.Controls.Add(roundedButton2);
            roundedPanel4.Dock = DockStyle.Fill;
            roundedPanel4.Location = new Point(64, 115);
            roundedPanel4.Name = "roundedPanel4";
            roundedPanel4.Size = new Size(531, 64);
            roundedPanel4.TabIndex = 18;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(205, 18);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(151, 28);
            comboBox2.TabIndex = 18;
            comboBox2.Text = "Năm";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(21, 18);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(151, 28);
            comboBox3.TabIndex = 17;
            comboBox3.Text = "Quý";
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.FromArgb(0, 113, 0);
            roundedButton2.BaseColor = Color.FromArgb(0, 113, 0);
            roundedButton2.BorderColor = Color.Transparent;
            roundedButton2.BorderRadius = 15;
            roundedButton2.BorderSize = 0;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            roundedButton2.ForeColor = Color.White;
            roundedButton2.HoverColor = Color.FromArgb(34, 139, 34);
            roundedButton2.Location = new Point(400, 9);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(110, 45);
            roundedButton2.TabIndex = 16;
            roundedButton2.Text = "Áp Dụng";
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            label2.Location = new Point(64, 182);
            label2.Name = "label2";
            label2.Size = new Size(531, 70);
            label2.TabIndex = 16;
            label2.Text = "Số Lượng Đơn Hàng";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            label4.Location = new Point(625, 182);
            label4.Name = "label4";
            label4.Size = new Size(531, 70);
            label4.TabIndex = 19;
            label4.Text = "Tỷ Lệ Đúng/Trễ Hẹn";
            label4.TextAlign = ContentAlignment.MiddleLeft;
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
            roundedTextBox1.TabIndex = 20;
            roundedTextBox1.UseSystemPasswordChar = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.Location = new Point(21, 176);
            label1.Name = "label1";
            label1.Size = new Size(231, 31);
            label1.TabIndex = 15;
            label1.Text = "Số Lượng Đơn Hàng";
            // 
            // Stats
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "Stats";
            Size = new Size(1227, 715);
            Load += Report_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            roundedPanel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSoLuongDonHang;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Label lblBaoCao;
        private Label lblTyLe;
        private Label lblSoLuongDonHang;
        private ComboBox comboBox1;
        private ComboBox cmbQuy;
      
        private Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart5;
        private TableLayoutPanel tableLayoutPanel1;
        private RoundedPanel roundedPanel4;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private RoundedButton roundedButton2;
        private Label label2;
        private Label label4;
        private RoundedTextBox roundedTextBox1;
    }
}