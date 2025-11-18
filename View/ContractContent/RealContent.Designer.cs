namespace Environmental_Monitoring.View.ContractContent
{
    partial class RealContent
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
            btnCancel = new Environmental_Monitoring.View.Components.RoundedButton();
            btnSave = new Environmental_Monitoring.View.Components.RoundedButton();
            lbContractID = new Environmental_Monitoring.View.Components.RoundedButton();
            roundedDataGridView2 = new Environmental_Monitoring.View.Components.RoundedDataGridView();
            btnContracts = new Environmental_Monitoring.View.Components.RoundedButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView2).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(217, 217, 217);
            btnCancel.BaseColor = Color.FromArgb(217, 217, 217);
            btnCancel.BorderColor = Color.Transparent;
            btnCancel.BorderRadius = 25;
            btnCancel.BorderSize = 0;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Dock = DockStyle.Fill;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Black;
            btnCancel.HoverColor = Color.FromArgb(34, 139, 34);
            btnCancel.Location = new Point(882, 419);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(267, 72);
            btnCancel.TabIndex = 64;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(0, 113, 0);
            btnSave.BaseColor = Color.FromArgb(0, 113, 0);
            btnSave.BorderColor = Color.Transparent;
            btnSave.BorderRadius = 25;
            btnSave.BorderSize = 0;
            btnSave.Cursor = Cursors.Hand;
            btnSave.Dock = DockStyle.Fill;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.HoverColor = Color.FromArgb(34, 139, 34);
            btnSave.Location = new Point(609, 419);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(267, 72);
            btnSave.TabIndex = 63;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // lbContractID
            // 
            lbContractID.BackColor = Color.FromArgb(217, 217, 217);
            lbContractID.BaseColor = Color.FromArgb(217, 217, 217);
            lbContractID.BorderColor = Color.Transparent;
            lbContractID.BorderRadius = 25;
            lbContractID.BorderSize = 0;
            lbContractID.Dock = DockStyle.Fill;
            lbContractID.Enabled = false;
            lbContractID.FlatAppearance.BorderSize = 0;
            lbContractID.FlatStyle = FlatStyle.Flat;
            lbContractID.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            lbContractID.ForeColor = Color.Black;
            lbContractID.HoverColor = Color.FromArgb(34, 139, 34);
            lbContractID.Location = new Point(882, 29);
            lbContractID.Name = "lbContractID";
            lbContractID.Size = new Size(267, 72);
            lbContractID.TabIndex = 60;
            lbContractID.Text = "Mã Hợp Đồng: ";
            lbContractID.UseVisualStyleBackColor = false;
            // 
            // roundedDataGridView2
            // 
            roundedDataGridView2.BackgroundColor = Color.White;
            roundedDataGridView2.BorderRadius = 20;
            roundedDataGridView2.BorderStyle = BorderStyle.None;
            roundedDataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.None;
            roundedDataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel1.SetColumnSpan(roundedDataGridView2, 4);
            roundedDataGridView2.EnableHeadersVisualStyles = false;
            roundedDataGridView2.Location = new Point(63, 133);
            roundedDataGridView2.Name = "roundedDataGridView2";
            roundedDataGridView2.RowHeadersWidth = 51;
            roundedDataGridView2.Size = new Size(1086, 254);
            roundedDataGridView2.TabIndex = 65;
            // 
            // btnContracts
            // 
            btnContracts.BackColor = Color.FromArgb(0, 113, 0);
            btnContracts.BaseColor = Color.FromArgb(0, 113, 0);
            btnContracts.BorderColor = Color.Transparent;
            btnContracts.BorderRadius = 25;
            btnContracts.BorderSize = 0;
            btnContracts.Cursor = Cursors.Hand;
            btnContracts.Dock = DockStyle.Fill;
            btnContracts.FlatAppearance.BorderSize = 0;
            btnContracts.FlatStyle = FlatStyle.Flat;
            btnContracts.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            btnContracts.ForeColor = Color.White;
            btnContracts.HoverColor = Color.FromArgb(34, 139, 34);
            btnContracts.Location = new Point(63, 29);
            btnContracts.Name = "btnContracts";
            btnContracts.Size = new Size(267, 72);
            btnContracts.TabIndex = 66;
            btnContracts.Text = "Danh Sách Hợp Đồng";
            btnContracts.UseVisualStyleBackColor = false;
            btnContracts.Click += btnContracts_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Controls.Add(lbContractID, 4, 1);
            tableLayoutPanel1.Controls.Add(roundedDataGridView2, 1, 3);
            tableLayoutPanel1.Controls.Add(btnContracts, 1, 1);
            tableLayoutPanel1.Controls.Add(btnCancel, 4, 5);
            tableLayoutPanel1.Controls.Add(btnSave, 3, 5);
            tableLayoutPanel1.Cursor = Cursors.Default;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Size = new Size(1215, 521);
            tableLayoutPanel1.TabIndex = 67;
            // 
            // RealContent
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Cursor = Cursors.Hand;
            Name = "RealContent";
            Size = new Size(1215, 521);
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView2).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Components.RoundedButton btnCancel;
        private Components.RoundedButton btnSave;
        private Components.RoundedButton lbContractID;
        private Components.RoundedDataGridView roundedDataGridView2;
        private Components.RoundedButton btnContracts;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
