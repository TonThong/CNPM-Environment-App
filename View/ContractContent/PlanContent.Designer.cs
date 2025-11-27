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
            lblParamNameValue = new Label();
            lblUnitValue = new Label();
            lblDeptValue = new Label();
            roundedDataGridView1 = new Environmental_Monitoring.View.Components.RoundedDataGridView();
            btnContracts = new Environmental_Monitoring.View.Components.RoundedButton();
            lbContractID = new Environmental_Monitoring.View.Components.RoundedButton();
            btnCancel = new Environmental_Monitoring.View.Components.RoundedButton();
            roundedButton2 = new Environmental_Monitoring.View.Components.RoundedButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            mySqlCommand1 = new MySql.Data.MySqlClient.MySqlCommand();
            btnThemMau = new Environmental_Monitoring.View.Components.RoundedButton();
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
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
            // roundedDataGridView1
            // 
            roundedDataGridView1.BackgroundColor = Color.White;
            roundedDataGridView1.BorderRadius = 20;
            roundedDataGridView1.BorderStyle = BorderStyle.None;
            roundedDataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            roundedDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel1.SetColumnSpan(roundedDataGridView1, 5);
            roundedDataGridView1.EnableHeadersVisualStyles = false;
            roundedDataGridView1.Location = new Point(27, 119);
            roundedDataGridView1.Name = "roundedDataGridView1";
            roundedDataGridView1.RowHeadersWidth = 51;
            tableLayoutPanel1.SetRowSpan(roundedDataGridView1, 3);
            roundedDataGridView1.Size = new Size(1158, 355);
            roundedDataGridView1.TabIndex = 67;
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
            btnContracts.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            btnContracts.ForeColor = Color.White;
            btnContracts.HoverColor = Color.FromArgb(34, 139, 34);
            btnContracts.Location = new Point(27, 26);
            btnContracts.Name = "btnContracts";
            btnContracts.Size = new Size(273, 52);
            btnContracts.TabIndex = 57;
            btnContracts.Text = "Danh Sách Hợp Đồng";
            btnContracts.UseVisualStyleBackColor = false;
            btnContracts.Click += btnContracts_Click;
            // 
            // lbContractID
            // 
            lbContractID.Anchor = AnchorStyles.Right;
            lbContractID.BackColor = Color.FromArgb(217, 217, 217);
            lbContractID.BaseColor = Color.FromArgb(217, 217, 217);
            lbContractID.BorderColor = Color.Transparent;
            lbContractID.BorderRadius = 25;
            lbContractID.BorderSize = 0;
            tableLayoutPanel1.SetColumnSpan(lbContractID, 2);
            lbContractID.Enabled = false;
            lbContractID.FlatAppearance.BorderSize = 0;
            lbContractID.FlatStyle = FlatStyle.Flat;
            lbContractID.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            lbContractID.ForeColor = Color.Black;
            lbContractID.HoverColor = Color.FromArgb(34, 139, 34);
            lbContractID.Location = new Point(633, 26);
            lbContractID.Name = "lbContractID";
            lbContractID.Size = new Size(552, 52);
            lbContractID.TabIndex = 52;
            lbContractID.Text = "Khách Hàng:";
            lbContractID.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.None;
            btnCancel.BackColor = Color.Silver;
            btnCancel.BaseColor = Color.Silver;
            btnCancel.BorderColor = Color.Transparent;
            btnCancel.BorderRadius = 15;
            btnCancel.BorderSize = 0;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnCancel.ForeColor = Color.Black;
            btnCancel.HoverColor = Color.DarkGray;
            btnCancel.Location = new Point(959, 503);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(179, 52);
            btnCancel.TabIndex = 56;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // roundedButton2
            // 
            roundedButton2.Anchor = AnchorStyles.None;
            roundedButton2.BackColor = Color.FromArgb(0, 113, 0);
            roundedButton2.BaseColor = Color.FromArgb(0, 113, 0);
            roundedButton2.BorderColor = Color.Transparent;
            roundedButton2.BorderRadius = 15;
            roundedButton2.BorderSize = 0;
            roundedButton2.Cursor = Cursors.Hand;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            roundedButton2.ForeColor = Color.White;
            roundedButton2.HoverColor = Color.FromArgb(34, 139, 34);
            roundedButton2.Location = new Point(680, 503);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(179, 52);
            roundedButton2.TabIndex = 55;
            roundedButton2.Text = "Lưu Hợp Đồng";
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 7;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel1.Controls.Add(roundedButton2, 4, 7);
            tableLayoutPanel1.Controls.Add(btnContracts, 1, 1);
            tableLayoutPanel1.Controls.Add(btnCancel, 5, 7);
            tableLayoutPanel1.Controls.Add(lbContractID, 4, 1);
            tableLayoutPanel1.Controls.Add(roundedDataGridView1, 1, 3);
            tableLayoutPanel1.Controls.Add(btnThemMau, 3, 7);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 9;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 48F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.Size = new Size(1215, 584);
            tableLayoutPanel1.TabIndex = 71;
            // 
            // mySqlCommand1
            // 
            mySqlCommand1.CacheAge = 0;
            mySqlCommand1.Connection = null;
            mySqlCommand1.EnableCaching = false;
            mySqlCommand1.Transaction = null;
            // 
            // btnThemMau
            // 
            btnThemMau.Anchor = AnchorStyles.None;
            btnThemMau.BackColor = Color.FromArgb(0, 0, 64);
            btnThemMau.BaseColor = Color.FromArgb(0, 0, 64);
            btnThemMau.BorderColor = Color.Transparent;
            btnThemMau.BorderRadius = 15;
            btnThemMau.BorderSize = 0;
            btnThemMau.Cursor = Cursors.Hand;
            btnThemMau.FlatAppearance.BorderSize = 0;
            btnThemMau.FlatStyle = FlatStyle.Flat;
            btnThemMau.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            btnThemMau.ForeColor = Color.White;
            btnThemMau.HoverColor = Color.Navy;
            btnThemMau.Location = new Point(401, 503);
            btnThemMau.Name = "btnThemMau";
            btnThemMau.Size = new Size(179, 52);
            btnThemMau.TabIndex = 68;
            btnThemMau.Text = "Thêm Mẫu";
            btnThemMau.UseVisualStyleBackColor = false;
            // 
            // PlanContent
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "PlanContent";
            Size = new Size(1215, 584);
            Load += PlanContent_Load;
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ComboBox cbbParameters;
        private Label lblParamTitle;
        private Label lblParamNameValue;
        private Label lblUnitTitle;
        private Label lblUnitValue;
        private Label lblDeptTitle;
        private Label lblDeptValue;
        private Components.RoundedDataGridView roundedDataGridView1;
        private TableLayoutPanel tableLayoutPanel1;
        private Components.RoundedButton roundedButton2;
        private Components.RoundedButton btnCancel;
        private Components.RoundedButton lbContractID;
        private Components.RoundedButton btnContracts;
        private MySql.Data.MySqlClient.MySqlCommand mySqlCommand1;
        private Components.RoundedButton btnThemMau;
    }
}
