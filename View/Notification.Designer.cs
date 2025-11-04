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
            txtSearch = new RoundedTextBox();
            lblTitle = new Label();
            lblPanelSoon = new RoundedButton();
            dgvQuaHan = new RoundedDataGridView();
            lblPanelOverdue = new RoundedButton();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSapQuaHan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvQuaHan).BeginInit();
            SuspendLayout();
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
            tableLayoutPanel1.Controls.Add(dgvSapQuaHan, 1, 3);
            tableLayoutPanel1.Controls.Add(txtSearch, 3, 1);
            tableLayoutPanel1.Controls.Add(lblTitle, 1, 1);
            tableLayoutPanel1.Controls.Add(lblPanelSoon, 1, 2);
            tableLayoutPanel1.Controls.Add(dgvQuaHan, 3, 3);
            tableLayoutPanel1.Controls.Add(lblPanelOverdue, 3, 2);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 2.999999F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.999997F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.999997F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 71.99998F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.00002956F));
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
            dgvSapQuaHan.Location = new Point(64, 164);
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
            dgvSapQuaHan.Size = new Size(531, 504);
            dgvSapQuaHan.TabIndex = 15;
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
            txtSearch.TabIndex = 19;
            txtSearch.UseSystemPasswordChar = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblTitle.Location = new Point(64, 21);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(211, 50);
            lblTitle.TabIndex = 20;
            lblTitle.Text = "Thông Báo";
            // 
            // lblPanelSoon
            // 
            lblPanelSoon.Anchor = AnchorStyles.None;
            lblPanelSoon.BackColor = Color.SeaGreen;
            lblPanelSoon.BaseColor = Color.SeaGreen;
            lblPanelSoon.BorderColor = Color.Transparent;
            lblPanelSoon.BorderRadius = 18;
            lblPanelSoon.BorderSize = 0;
            lblPanelSoon.FlatAppearance.BorderSize = 0;
            lblPanelSoon.FlatStyle = FlatStyle.Flat;
            lblPanelSoon.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPanelSoon.ForeColor = Color.Black;
            lblPanelSoon.HoverColor = Color.FromArgb(34, 139, 34);
            lblPanelSoon.Location = new Point(179, 101);
            lblPanelSoon.Name = "lblPanelSoon";
            lblPanelSoon.Size = new Size(300, 50);
            lblPanelSoon.TabIndex = 13;
            lblPanelSoon.Text = "Hợp Đồng Sắp Quá Hạn";
            lblPanelSoon.UseVisualStyleBackColor = false;
            lblPanelSoon.Click += btnSapQuaHan_Click_1;
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
            dgvQuaHan.Location = new Point(625, 164);
            dgvQuaHan.Name = "dgvQuaHan";
            dgvQuaHan.RowHeadersVisible = false;
            dgvQuaHan.RowHeadersWidth = 51;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(230, 101, 101);
            dataGridViewCellStyle5.SelectionBackColor = Color.Maroon;
            dgvQuaHan.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dgvQuaHan.Size = new Size(531, 504);
            dgvQuaHan.TabIndex = 17;
            // 
            // lblPanelOverdue
            // 
            lblPanelOverdue.Anchor = AnchorStyles.None;
            lblPanelOverdue.BackColor = Color.SeaGreen;
            lblPanelOverdue.BaseColor = Color.SeaGreen;
            lblPanelOverdue.BorderColor = Color.Transparent;
            lblPanelOverdue.BorderRadius = 18;
            lblPanelOverdue.BorderSize = 0;
            lblPanelOverdue.FlatAppearance.BorderSize = 0;
            lblPanelOverdue.FlatStyle = FlatStyle.Flat;
            lblPanelOverdue.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPanelOverdue.ForeColor = Color.Black;
            lblPanelOverdue.HoverColor = Color.FromArgb(34, 139, 34);
            lblPanelOverdue.Location = new Point(740, 101);
            lblPanelOverdue.Name = "lblPanelOverdue";
            lblPanelOverdue.Size = new Size(300, 50);
            lblPanelOverdue.TabIndex = 14;
            lblPanelOverdue.Text = "Hợp Đồng Quá Hạn";
            lblPanelOverdue.UseVisualStyleBackColor = false;
            lblPanelOverdue.Click += btnQuaHan_Click;
            // 
            // Notification
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(217, 244, 227);
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
        private RoundedButton lblPanelOverdue;
        private RoundedButton lblPanelSoon;
        private RoundedTextBox txtSearch;
        private Label lblTitle;
    }
}
