namespace Environmental_Monitoring.View.ContractContent
{
    partial class PlanContent
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
            lbContractID = new Environmental_Monitoring.View.Components.RoundedButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnAddParameter = new Environmental_Monitoring.View.Components.RoundedButton();
            label1 = new Label();
            checkedListBox1 = new CheckedListBox();
            label2 = new Label();
            roundedButton2 = new Environmental_Monitoring.View.Components.RoundedButton();
            roundedDataGridView1 = new Environmental_Monitoring.View.Components.RoundedDataGridView();
            btnCancel = new Environmental_Monitoring.View.Components.RoundedButton();
            btnContracts = new Environmental_Monitoring.View.Components.RoundedButton();
            lblParamNameValue = new Label();
            lblUnitValue = new Label();
            lblDeptValue = new Label();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView1).BeginInit();
            SuspendLayout();
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
            lbContractID.TabIndex = 52;
            lbContractID.Text = "Mã Hợp Đồng: ";
            lbContractID.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 7;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Controls.Add(btnAddParameter, 3, 5);
            tableLayoutPanel1.Controls.Add(label1, 1, 2);
            tableLayoutPanel1.Controls.Add(checkedListBox1, 1, 3);
            tableLayoutPanel1.Controls.Add(label2, 3, 2);
            tableLayoutPanel1.Controls.Add(roundedButton2, 4, 5);
            tableLayoutPanel1.Controls.Add(roundedDataGridView1, 3, 3);
            tableLayoutPanel1.Controls.Add(btnCancel, 5, 5);
            tableLayoutPanel1.Controls.Add(lbContractID, 5, 1);
            tableLayoutPanel1.Controls.Add(btnContracts, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Size = new Size(1215, 521);
            tableLayoutPanel1.TabIndex = 71;
            // 
            // btnAddParameter
            // 
            btnAddParameter.BackColor = Color.FromArgb(55, 63, 81);
            btnAddParameter.BaseColor = Color.FromArgb(55, 63, 81);
            btnAddParameter.BorderColor = Color.Transparent;
            btnAddParameter.BorderRadius = 25;
            btnAddParameter.BorderSize = 0;
            btnAddParameter.Cursor = Cursors.Hand;
            btnAddParameter.Dock = DockStyle.Fill;
            btnAddParameter.FlatAppearance.BorderSize = 0;
            btnAddParameter.FlatStyle = FlatStyle.Flat;
            btnAddParameter.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            btnAddParameter.ForeColor = Color.White;
            btnAddParameter.HoverColor = Color.FromArgb(64, 64, 64);
            btnAddParameter.Location = new Point(366, 418);
            btnAddParameter.Name = "btnAddParameter";
            btnAddParameter.Size = new Size(237, 72);
            btnAddParameter.TabIndex = 70;
            btnAddParameter.Text = "Thêm thông số";
            btnAddParameter.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            label1.Location = new Point(63, 104);
            label1.Name = "label1";
            label1.Size = new Size(252, 31);
            label1.TabIndex = 58;
            label1.Text = "Chọn mẫu môi trường";
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(63, 138);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(267, 136);
            checkedListBox1.TabIndex = 68;
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
            checkedListBox1.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            label2.Location = new Point(366, 104);
            label2.Name = "label2";
            label2.Size = new Size(206, 31);
            label2.TabIndex = 59;
            label2.Text = "Chọn thông số đo";
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.FromArgb(0, 113, 0);
            roundedButton2.BaseColor = Color.FromArgb(0, 113, 0);
            roundedButton2.BorderColor = Color.Transparent;
            roundedButton2.BorderRadius = 25;
            roundedButton2.BorderSize = 0;
            roundedButton2.Cursor = Cursors.Hand;
            roundedButton2.Dock = DockStyle.Fill;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            roundedButton2.ForeColor = Color.White;
            roundedButton2.HoverColor = Color.FromArgb(34, 139, 34);
            roundedButton2.Location = new Point(609, 418);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(267, 72);
            roundedButton2.TabIndex = 55;
            roundedButton2.Text = "Lưu";
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // roundedDataGridView1
            // 
            roundedDataGridView1.BackgroundColor = Color.White;
            roundedDataGridView1.BorderRadius = 20;
            roundedDataGridView1.BorderStyle = BorderStyle.None;
            roundedDataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            roundedDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel1.SetColumnSpan(roundedDataGridView1, 3);
            roundedDataGridView1.EnableHeadersVisualStyles = false;
            roundedDataGridView1.Location = new Point(366, 138);
            roundedDataGridView1.Name = "roundedDataGridView1";
            roundedDataGridView1.RowHeadersWidth = 51;
            roundedDataGridView1.Size = new Size(783, 254);
            roundedDataGridView1.TabIndex = 67;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Silver;
            btnCancel.BaseColor = Color.Silver;
            btnCancel.BorderColor = Color.Transparent;
            btnCancel.BorderRadius = 25;
            btnCancel.BorderSize = 0;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Dock = DockStyle.Fill;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Black;
            btnCancel.HoverColor = Color.DarkGray;
            btnCancel.Location = new Point(882, 418);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(267, 72);
            btnCancel.TabIndex = 56;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
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
            btnContracts.TabIndex = 57;
            btnContracts.Text = "Danh Sách Hợp Đồng";
            btnContracts.UseVisualStyleBackColor = false;
            btnContracts.Click += btnContracts_Click;
            // 
            // lblParamNameValue
            // 
            lblParamNameValue.AutoSize = true;
            lblParamNameValue.Font = new Font("Times New Roman", 11F);
            lblParamNameValue.Location = new Point(640, 370);
            lblParamNameValue.Name = "lblParamNameValue";
            lblParamNameValue.Size = new Size(0, 21);
            lblParamNameValue.TabIndex = 62;
            // 
            // lblUnitValue
            // 
            lblUnitValue.AutoSize = true;
            lblUnitValue.Font = new Font("Times New Roman", 11F);
            lblUnitValue.Location = new Point(640, 395);
            lblUnitValue.Name = "lblUnitValue";
            lblUnitValue.Size = new Size(0, 21);
            lblUnitValue.TabIndex = 64;
            // 
            // lblDeptValue
            // 
            lblDeptValue.AutoSize = true;
            lblDeptValue.Font = new Font("Times New Roman", 11F);
            lblDeptValue.Location = new Point(640, 420);
            lblDeptValue.Name = "lblDeptValue";
            lblDeptValue.Size = new Size(0, 21);
            lblDeptValue.TabIndex = 66;
            // 
            // PlanContent
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "PlanContent";
            Size = new Size(1215, 521);
            Load += PlanContent_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Components.RoundedButton lbContractID;
        private Components.RoundedDataGridView roundedDataGridView1;
        private Components.RoundedButton btnCancel;
        private Components.RoundedButton roundedButton2;
        private Components.RoundedButton btnContracts;
        private Label label2;
        private Label label1;
        private ComboBox cbbParameters;
        private Label lblParamTitle;
        private Label lblParamNameValue;
        private Label lblUnitTitle;
        private Label lblUnitValue;
        private Label lblDeptTitle;
        private Label lblDeptValue;
        private CheckedListBox checkedListBox1;
        private Components.RoundedButton btnAddParameter;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
