namespace Environmental_Monitoring.View
{
    partial class ExperimentContent
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            roundedDataGridView2 = new Environmental_Monitoring.View.Components.RoundedDataGridView();
            btnCancel = new Environmental_Monitoring.View.Components.RoundedButton();
            btnSave = new Environmental_Monitoring.View.Components.RoundedButton();
            lbContractID = new Environmental_Monitoring.View.Components.RoundedButton();
            btnContracts = new Environmental_Monitoring.View.Components.RoundedButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView2).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // roundedDataGridView2
            // 
            roundedDataGridView2.BackgroundColor = Color.White;
            roundedDataGridView2.BorderRadius = 20;
            roundedDataGridView2.BorderStyle = BorderStyle.None;
            roundedDataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.None;
            roundedDataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel1.SetColumnSpan(roundedDataGridView2, 4);
            roundedDataGridView2.Dock = DockStyle.Fill;
            roundedDataGridView2.EnableHeadersVisualStyles = false;
            roundedDataGridView2.Location = new Point(27, 107);
            roundedDataGridView2.Name = "roundedDataGridView2";
            roundedDataGridView2.RowHeadersWidth = 51;
            roundedDataGridView2.Size = new Size(1158, 367);
            roundedDataGridView2.TabIndex = 70;
            roundedDataGridView2.CellContentClick += roundedDataGridView2_CellContentClick;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(217, 217, 217);
            btnCancel.BaseColor = Color.FromArgb(217, 217, 217);
            btnCancel.BorderColor = Color.Transparent;
            btnCancel.BorderRadius = 15;
            btnCancel.BorderSize = 0;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Dock = DockStyle.Fill;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Black;
            btnCancel.HoverColor = Color.FromArgb(34, 139, 34);
            btnCancel.Location = new Point(900, 503);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(285, 52);
            btnCancel.TabIndex = 69;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(0, 113, 0);
            btnSave.BaseColor = Color.FromArgb(0, 113, 0);
            btnSave.BorderColor = Color.Transparent;
            btnSave.BorderRadius = 15;
            btnSave.BorderSize = 0;
            btnSave.Cursor = Cursors.Hand;
            btnSave.Dock = DockStyle.Fill;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.HoverColor = Color.FromArgb(34, 139, 34);
            btnSave.Location = new Point(609, 503);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(285, 52);
            btnSave.TabIndex = 68;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // lbContractID
            // 
            lbContractID.BackColor = Color.FromArgb(217, 217, 217);
            lbContractID.BaseColor = Color.FromArgb(217, 217, 217);
            lbContractID.BorderColor = Color.Transparent;
            lbContractID.BorderRadius = 15;
            lbContractID.BorderSize = 0;
            tableLayoutPanel1.SetColumnSpan(lbContractID, 2);
            lbContractID.Enabled = false;
            lbContractID.FlatAppearance.BorderSize = 0;
            lbContractID.FlatStyle = FlatStyle.Flat;
            lbContractID.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lbContractID.ForeColor = Color.Black;
            lbContractID.HoverColor = Color.FromArgb(34, 139, 34);
            lbContractID.Location = new Point(609, 26);
            lbContractID.Name = "lbContractID";
            lbContractID.Size = new Size(576, 52);
            lbContractID.TabIndex = 67;
            lbContractID.Text = "Khách Hàng:";
            lbContractID.UseVisualStyleBackColor = false;
            // 
            // btnContracts
            // 
            btnContracts.BackColor = Color.FromArgb(0, 113, 0);
            btnContracts.BaseColor = Color.FromArgb(0, 113, 0);
            btnContracts.BorderColor = Color.Transparent;
            btnContracts.BorderRadius = 15;
            btnContracts.BorderSize = 0;
            btnContracts.Cursor = Cursors.Hand;
            btnContracts.Dock = DockStyle.Fill;
            btnContracts.FlatAppearance.BorderSize = 0;
            btnContracts.FlatStyle = FlatStyle.Flat;
            btnContracts.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnContracts.ForeColor = Color.White;
            btnContracts.HoverColor = Color.FromArgb(34, 139, 34);
            btnContracts.Location = new Point(27, 26);
            btnContracts.Name = "btnContracts";
            btnContracts.Size = new Size(285, 52);
            btnContracts.TabIndex = 71;
            btnContracts.Text = "Danh Sách Hợp Đồng";
            btnContracts.UseVisualStyleBackColor = false;
            btnContracts.Click += btnContracts_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel1.Controls.Add(btnSave, 3, 5);
            tableLayoutPanel1.Controls.Add(roundedDataGridView2, 1, 3);
            tableLayoutPanel1.Controls.Add(btnContracts, 1, 1);
            tableLayoutPanel1.Controls.Add(btnCancel, 4, 5);
            tableLayoutPanel1.Controls.Add(lbContractID, 3, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 64F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.Size = new Size(1215, 584);
            tableLayoutPanel1.TabIndex = 72;
            // 
            // ExperimentContent
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "ExperimentContent";
            Size = new Size(1215, 584);
            Load += ExperimentContent_Load_1;
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView2).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Components.RoundedDataGridView roundedDataGridView2;
        private Components.RoundedButton btnCancel;
        private Components.RoundedButton btnSave;
        private Components.RoundedButton lbContractID;
        private Components.RoundedButton btnContracts;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
