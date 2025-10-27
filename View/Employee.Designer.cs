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
            btnAdd = new RoundedButton();
            dgvEmployee = new RoundedDataGridView();
            pnlPagination = new FlowLayoutPanel();
            btnFirst = new Button();
            btnPrevious = new Button();
            lblPageInfo = new Label();
            btnNext = new Button();
            btnLast = new Button();
            headerPanel = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            txbSearch = new RoundedTextBox();
            labelTitle = new Label();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmployee).BeginInit();
            pnlPagination.SuspendLayout();
            headerPanel.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.754032F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 93.2459641F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 48F));
            tableLayoutPanel1.Controls.Add(btnAdd, 1, 0);
            tableLayoutPanel1.Controls.Add(dgvEmployee, 1, 1);
            tableLayoutPanel1.Controls.Add(pnlPagination, 1, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 80);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 107F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 73F));
            tableLayoutPanel1.Size = new Size(1182, 635);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.SeaGreen;
            btnAdd.BaseColor = Color.SeaGreen;
            btnAdd.BorderColor = Color.Transparent;
            btnAdd.BorderRadius = 15;
            btnAdd.BorderSize = 0;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdd.ForeColor = Color.White;
            btnAdd.HoverColor = Color.FromArgb(34, 139, 34);
            btnAdd.Image = (Image)resources.GetObject("btnAdd.Image");
            btnAdd.ImageAlign = ContentAlignment.MiddleLeft;
            btnAdd.Location = new Point(79, 4);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(207, 60);
            btnAdd.TabIndex = 23;
            btnAdd.Text = "Thêm nhân viên";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // dgvEmployee
            // 
            dgvEmployee.BackgroundColor = Color.White;
            dgvEmployee.BorderRadius = 20;
            dgvEmployee.BorderStyle = BorderStyle.None;
            dgvEmployee.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvEmployee.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEmployee.Dock = DockStyle.Fill;
            dgvEmployee.EnableHeadersVisualStyles = false;
            dgvEmployee.Location = new Point(79, 111);
            dgvEmployee.Margin = new Padding(3, 4, 3, 4);
            dgvEmployee.Name = "dgvEmployee";
            dgvEmployee.RowHeadersWidth = 51;
            dgvEmployee.Size = new Size(1051, 447);
            dgvEmployee.TabIndex = 24;
            dgvEmployee.CellContentClick += dgvEmployee_CellContentClick;
            // 
            // pnlPagination
            // 
            pnlPagination.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            pnlPagination.AutoSize = true;
            pnlPagination.Controls.Add(btnFirst);
            pnlPagination.Controls.Add(btnPrevious);
            pnlPagination.Controls.Add(lblPageInfo);
            pnlPagination.Controls.Add(btnNext);
            pnlPagination.Controls.Add(btnLast);
            pnlPagination.Location = new Point(326, 566);
            pnlPagination.Margin = new Padding(3, 4, 3, 4);
            pnlPagination.Name = "pnlPagination";
            pnlPagination.Size = new Size(557, 65);
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
            // headerPanel
            // 
            headerPanel.BackColor = Color.WhiteSmoke;
            headerPanel.Controls.Add(tableLayoutPanel2);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Margin = new Padding(3, 4, 3, 4);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new Padding(23, 13, 23, 13);
            headerPanel.Size = new Size(1182, 80);
            headerPanel.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57.95699F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42.04301F));
            tableLayoutPanel2.Controls.Add(txbSearch, 1, 0);
            tableLayoutPanel2.Controls.Add(labelTitle, 0, 0);
            tableLayoutPanel2.Location = new Point(73, 17);
            tableLayoutPanel2.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 46.1538467F));
            tableLayoutPanel2.Size = new Size(1063, 52);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // txbSearch
            // 
            txbSearch.BackColor = Color.White;
            txbSearch.BorderRadius = 15;
            txbSearch.BorderThickness = 1;
            txbSearch.FocusBorderColor = SystemColors.ControlDark;
            txbSearch.HoverBorderColor = Color.DarkGray;
            txbSearch.Location = new Point(619, 3);
            txbSearch.Multiline = false;
            txbSearch.Name = "txbSearch";
            txbSearch.NormalBorderColor = Color.DarkGray;
            txbSearch.Padding = new Padding(9, 12, 9, 9);
            txbSearch.PasswordChar = '\0';
            txbSearch.PlaceholderText = "Tìm Kiếm...";
            txbSearch.ReadOnly = false;
            txbSearch.Size = new Size(440, 45);
            txbSearch.TabIndex = 21;
            txbSearch.TextChanged += txbSearch_TextChanged;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            labelTitle.ForeColor = Color.Black;
            labelTitle.Location = new Point(3, 0);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(308, 46);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Quản lý nhân viên";
            // 
            // Employee
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(headerPanel);
            Name = "Employee";
            Size = new Size(1182, 715);
            Load += Employee_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmployee).EndInit();
            pnlPagination.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel headerPanel;
        private Label labelTitle;
        private RoundedButton btnAdd;
        private RoundedDataGridView dgvEmployee;
        private TableLayoutPanel tableLayoutPanel2;
        private RoundedTextBox txbSearch;

        private FlowLayoutPanel pnlPagination;
        private Button btnFirst;
        private Button btnPrevious;
        private Label lblPageInfo;
        private Button btnNext;
        private Button btnLast;
    }
}
