using Environmental_Monitoring.View.Components;

namespace Environmental_Monitoring.View
{
    partial class Employee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Employee));
            tableLayoutPanel1 = new TableLayoutPanel();
            pnlPagination = new FlowLayoutPanel();
            btnFirst = new Button();
            btnPrevious = new Button();
            lblPageInfo = new Label();
            btnNext = new Button();
            btnLast = new Button();
            dgvEmployee = new RoundedDataGridView();
            btnAdd = new RoundedButton();
            lblTitle = new Label();
            txtSearch = new RoundedTextBox();
            tableLayoutPanel1.SuspendLayout();
            pnlPagination.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmployee).BeginInit();
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
            tableLayoutPanel1.Controls.Add(pnlPagination, 1, 4);
            tableLayoutPanel1.Controls.Add(dgvEmployee, 1, 3);
            tableLayoutPanel1.Controls.Add(btnAdd, 1, 2);
            tableLayoutPanel1.Controls.Add(lblTitle, 1, 1);
            tableLayoutPanel1.Controls.Add(txtSearch, 3, 1);
            tableLayoutPanel1.Location = new Point(3, 4);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 64F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.Size = new Size(1221, 710);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // pnlPagination
            // 
            pnlPagination.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            pnlPagination.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(pnlPagination, 3);
            pnlPagination.Controls.Add(btnFirst);
            pnlPagination.Controls.Add(btnPrevious);
            pnlPagination.Controls.Add(lblPageInfo);
            pnlPagination.Controls.Add(btnNext);
            pnlPagination.Controls.Add(btnLast);
            pnlPagination.Location = new Point(331, 621);
            pnlPagination.Margin = new Padding(3, 4, 3, 4);
            pnlPagination.Name = "pnlPagination";
            pnlPagination.Size = new Size(557, 63);
            pnlPagination.TabIndex = 25;
            // 
            // btnFirst
            // 
            btnFirst.Location = new Point(3, 4);
            btnFirst.Margin = new Padding(3, 4, 3, 4);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(86, 53);
            btnFirst.TabIndex = 0;
            btnFirst.Text = "<<";
            btnFirst.UseVisualStyleBackColor = true;
            // 
            // btnPrevious
            // 
            btnPrevious.Location = new Point(95, 4);
            btnPrevious.Margin = new Padding(3, 4, 3, 4);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(86, 53);
            btnPrevious.TabIndex = 1;
            btnPrevious.Text = "<";
            btnPrevious.UseVisualStyleBackColor = true;
            // 
            // lblPageInfo
            // 
            lblPageInfo.Anchor = AnchorStyles.None;
            lblPageInfo.Location = new Point(187, 4);
            lblPageInfo.Name = "lblPageInfo";
            lblPageInfo.Size = new Size(183, 53);
            lblPageInfo.TabIndex = 2;
            lblPageInfo.Text = "Page 1 / 10";
            lblPageInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(376, 4);
            btnNext.Margin = new Padding(3, 4, 3, 4);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(86, 53);
            btnNext.TabIndex = 3;
            btnNext.Text = ">";
            btnNext.UseVisualStyleBackColor = true;
            // 
            // btnLast
            // 
            btnLast.Location = new Point(468, 4);
            btnLast.Margin = new Padding(3, 4, 3, 4);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(86, 53);
            btnLast.TabIndex = 4;
            btnLast.Text = ">>";
            btnLast.UseVisualStyleBackColor = true;
            // 
            // dgvEmployee
            // 
            dgvEmployee.BackgroundColor = Color.White;
            dgvEmployee.BorderRadius = 20;
            dgvEmployee.BorderStyle = BorderStyle.None;
            dgvEmployee.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvEmployee.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel1.SetColumnSpan(dgvEmployee, 3);
            dgvEmployee.EnableHeadersVisualStyles = false;
            dgvEmployee.Location = new Point(64, 167);
            dgvEmployee.Margin = new Padding(3, 4, 3, 4);
            dgvEmployee.Name = "dgvEmployee";
            dgvEmployee.RowHeadersVisible = false;
            dgvEmployee.RowHeadersWidth = 51;
            dgvEmployee.RowTemplate.Height = 35;
            dgvEmployee.Size = new Size(1092, 446);
            dgvEmployee.TabIndex = 24;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(0, 113, 0);
            btnAdd.BaseColor = Color.FromArgb(0, 113, 0);
            btnAdd.BorderColor = Color.Transparent;
            btnAdd.BorderRadius = 15;
            btnAdd.BorderSize = 0;
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdd.ForeColor = Color.White;
            btnAdd.HoverColor = Color.FromArgb(0, 192, 0);
            btnAdd.Image = (Image)resources.GetObject("btnAdd.Image");
            btnAdd.ImageAlign = ContentAlignment.MiddleLeft;
            btnAdd.Location = new Point(64, 96);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(243, 61);
            btnAdd.TabIndex = 23;
            btnAdd.Text = "Thêm nhân viên";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(64, 21);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(334, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Quản lý nhân viên";
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.White;
            txtSearch.BorderRadius = 15;
            txtSearch.BorderThickness = 1;
            txtSearch.Cursor = Cursors.IBeam;
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
            txtSearch.TabIndex = 26;
            txtSearch.UseSystemPasswordChar = false;
            // 
            // Employee
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(tableLayoutPanel1);
            Name = "Employee";
            Size = new Size(1229, 720);
            Load += Employee_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            pnlPagination.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvEmployee).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel pnlPagination;
        private Button btnFirst;
        private Button btnPrevious;
        private Label lblPageInfo;
        private Button btnNext;
        private Button btnLast;
        private RoundedDataGridView dgvEmployee;
        private RoundedButton btnAdd;
        private Label lblTitle;
        private RoundedTextBox txtSearch;
    }
}
