using Environmental_Monitoring.View.Components;

namespace Environmental_Monitoring
{
    partial class Notification
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            tableLayoutPanel1 = new TableLayoutPanel();
            dgvSapQuaHan = new RoundedDataGridView();
            roundedTextBox1 = new RoundedTextBox();
            label3 = new Label();
            btnSapQuaHan = new RoundedButton();
            dgvQuaHan = new RoundedDataGridView();
            btnQuaHan = new RoundedButton();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSapQuaHan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvQuaHan).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(217, 244, 227);
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.80769253F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44.23077F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.92307687F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44.23077F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.80769253F));
            tableLayoutPanel1.Controls.Add(dgvSapQuaHan, 1, 3);
            tableLayoutPanel1.Controls.Add(roundedTextBox1, 3, 1);
            tableLayoutPanel1.Controls.Add(label3, 1, 1);
            tableLayoutPanel1.Controls.Add(btnSapQuaHan, 1, 2);
            tableLayoutPanel1.Controls.Add(dgvQuaHan, 3, 3);
            tableLayoutPanel1.Controls.Add(btnQuaHan, 3, 2);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3.06124282F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.2041445F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.2041445F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 71.53044F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.000031F));
            tableLayoutPanel1.Size = new Size(1221, 709);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // dgvSapQuaHan
            // 
            dgvSapQuaHan.BackgroundColor = Color.FromArgb(237, 230, 147);
            dgvSapQuaHan.BorderRadius = 20;
            dgvSapQuaHan.BorderStyle = BorderStyle.None;
            dgvSapQuaHan.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(229, 227, 109);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ButtonShadow;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvSapQuaHan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvSapQuaHan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSapQuaHan.Dock = DockStyle.Fill;
            dgvSapQuaHan.EnableHeadersVisualStyles = false;
            dgvSapQuaHan.Location = new Point(61, 168);
            dgvSapQuaHan.Name = "dgvSapQuaHan";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(237, 230, 147);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = Color.Goldenrod;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvSapQuaHan.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvSapQuaHan.RowHeadersVisible = false;
            dgvSapQuaHan.RowHeadersWidth = 51;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(237, 230, 147);
            dataGridViewCellStyle3.SelectionBackColor = Color.Goldenrod;
            dgvSapQuaHan.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dgvSapQuaHan.Size = new Size(534, 501);
            dgvSapQuaHan.TabIndex = 15;
            // 
            // roundedTextBox1
            // 
            roundedTextBox1.BackColor = Color.White;
            roundedTextBox1.BorderRadius = 15;
            roundedTextBox1.BorderThickness = 1;
            roundedTextBox1.FocusBorderColor = SystemColors.ControlDark;
            roundedTextBox1.HoverBorderColor = Color.DarkGray;
            roundedTextBox1.Location = new Point(624, 24);
            roundedTextBox1.Multiline = false;
            roundedTextBox1.Name = "roundedTextBox1";
            roundedTextBox1.NormalBorderColor = Color.DarkGray;
            roundedTextBox1.Padding = new Padding(9, 12, 9, 9);
            roundedTextBox1.PasswordChar = '\0';
            roundedTextBox1.PlaceholderText = "Tìm Kiếm...";
            roundedTextBox1.ReadOnly = false;
            roundedTextBox1.Size = new Size(534, 45);
            roundedTextBox1.TabIndex = 19;
            roundedTextBox1.UseSystemPasswordChar = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label3.Location = new Point(61, 21);
            label3.Name = "label3";
            label3.Size = new Size(211, 50);
            label3.TabIndex = 20;
            label3.Text = "Thông Báo";
            // 
            // btnSapQuaHan
            // 
            btnSapQuaHan.Anchor = AnchorStyles.None;
            btnSapQuaHan.BackColor = Color.SeaGreen;
            btnSapQuaHan.BaseColor = Color.SeaGreen;
            btnSapQuaHan.BorderColor = Color.Transparent;
            btnSapQuaHan.BorderRadius = 18;
            btnSapQuaHan.BorderSize = 0;
            btnSapQuaHan.FlatAppearance.BorderSize = 0;
            btnSapQuaHan.FlatStyle = FlatStyle.Flat;
            btnSapQuaHan.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnSapQuaHan.ForeColor = Color.Black;
            btnSapQuaHan.HoverColor = Color.FromArgb(34, 139, 34);
            btnSapQuaHan.Location = new Point(178, 104);
            btnSapQuaHan.Name = "btnSapQuaHan";
            btnSapQuaHan.Size = new Size(300, 50);
            btnSapQuaHan.TabIndex = 13;
            btnSapQuaHan.Text = "Hợp Đồng Sắp Quá Hạn";
            btnSapQuaHan.UseVisualStyleBackColor = false;
            btnSapQuaHan.Click += btnSapQuaHan_Click_1;
            // 
            // dgvQuaHan
            // 
            dgvQuaHan.BackgroundColor = Color.FromArgb(230, 101, 101);
            dgvQuaHan.BorderRadius = 20;
            dgvQuaHan.BorderStyle = BorderStyle.None;
            dgvQuaHan.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(239, 69, 69);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.ButtonShadow;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvQuaHan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvQuaHan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvQuaHan.Dock = DockStyle.Fill;
            dgvQuaHan.EnableHeadersVisualStyles = false;
            dgvQuaHan.Location = new Point(624, 168);
            dgvQuaHan.Name = "dgvQuaHan";
            dgvQuaHan.RowHeadersVisible = false;
            dgvQuaHan.RowHeadersWidth = 51;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(230, 101, 101);
            dataGridViewCellStyle5.SelectionBackColor = Color.Maroon;
            dgvQuaHan.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dgvQuaHan.Size = new Size(534, 501);
            dgvQuaHan.TabIndex = 17;
            // 
            // btnQuaHan
            // 
            btnQuaHan.Anchor = AnchorStyles.None;
            btnQuaHan.BackColor = Color.SeaGreen;
            btnQuaHan.BaseColor = Color.SeaGreen;
            btnQuaHan.BorderColor = Color.Transparent;
            btnQuaHan.BorderRadius = 18;
            btnQuaHan.BorderSize = 0;
            btnQuaHan.FlatAppearance.BorderSize = 0;
            btnQuaHan.FlatStyle = FlatStyle.Flat;
            btnQuaHan.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnQuaHan.ForeColor = Color.Black;
            btnQuaHan.HoverColor = Color.FromArgb(34, 139, 34);
            btnQuaHan.Location = new Point(741, 104);
            btnQuaHan.Name = "btnQuaHan";
            btnQuaHan.Size = new Size(300, 50);
            btnQuaHan.TabIndex = 14;
            btnQuaHan.Text = "Hợp Đồng Quá Hạn";
            btnQuaHan.UseVisualStyleBackColor = false;
            btnQuaHan.Click += btnQuaHan_Click;
            // 
            // Notification
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "Notification";
            Size = new Size(1227, 715);
            Load += Notification_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSapQuaHan).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvQuaHan).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private RoundedDataGridView roundedDataGridView2;
        private TableLayoutPanel tableLayoutPanel1;
        private RoundedDataGridView dgvQuaHan;
        private RoundedDataGridView dgvSapQuaHan;
        private RoundedButton btnQuaHan;
        private RoundedButton btnSapQuaHan;
        private RoundedTextBox roundedTextBox1;
        private Label label3;
    }
}
