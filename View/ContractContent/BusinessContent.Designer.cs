namespace Environmental_Monitoring.View.ContractContent
{
    partial class BusinessContent
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
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            lbNamePage = new Label();
            btnCancel = new Environmental_Monitoring.View.Components.RoundedButton();
            lbCustomer = new Label();
            txtboxEmployee = new Environmental_Monitoring.View.Components.RoundedTextBox();
            btnSave = new Environmental_Monitoring.View.Components.RoundedButton();
            txtboxEndDate = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtboxCustomerName = new Environmental_Monitoring.View.Components.RoundedTextBox();
            cmbContractType = new ComboBox();
            txtboxPhone = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtboxIDContract = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtboxAddress = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtboxOwner = new Environmental_Monitoring.View.Components.RoundedTextBox();
            lbContract = new Label();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Controls.Add(lbContract);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1227, 507);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Controls.Add(lbNamePage, 1, 1);
            tableLayoutPanel1.Controls.Add(btnCancel, 3, 13);
            tableLayoutPanel1.Controls.Add(lbCustomer, 1, 3);
            tableLayoutPanel1.Controls.Add(txtboxEmployee, 3, 11);
            tableLayoutPanel1.Controls.Add(btnSave, 1, 13);
            tableLayoutPanel1.Controls.Add(txtboxEndDate, 3, 9);
            tableLayoutPanel1.Controls.Add(txtboxCustomerName, 1, 5);
            tableLayoutPanel1.Controls.Add(cmbContractType, 3, 7);
            tableLayoutPanel1.Controls.Add(txtboxPhone, 1, 7);
            tableLayoutPanel1.Controls.Add(txtboxIDContract, 3, 5);
            tableLayoutPanel1.Controls.Add(txtboxAddress, 1, 9);
            tableLayoutPanel1.Controls.Add(txtboxOwner, 1, 11);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 15;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Size = new Size(1227, 507);
            tableLayoutPanel1.TabIndex = 53;
            // 
            // lbNamePage
            // 
            lbNamePage.AutoSize = true;
            lbNamePage.BackColor = Color.Transparent;
            tableLayoutPanel1.SetColumnSpan(lbNamePage, 3);
            lbNamePage.Font = new Font("Times New Roman", 24F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lbNamePage.ForeColor = Color.Black;
            lbNamePage.Location = new Point(64, 25);
            lbNamePage.Name = "lbNamePage";
            lbNamePage.Size = new Size(833, 45);
            lbNamePage.TabIndex = 40;
            lbNamePage.Text = "THÔNG TIN KHÁCH HÀNG VÀ HỢP ĐỒNG";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(217, 217, 217);
            btnCancel.BaseColor = Color.FromArgb(217, 217, 217);
            btnCancel.BorderColor = Color.Transparent;
            btnCancel.BorderRadius = 20;
            btnCancel.BorderSize = 0;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Black;
            btnCancel.HoverColor = Color.FromArgb(34, 139, 34);
            btnCancel.Location = new Point(676, 428);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(384, 44);
            btnCancel.TabIndex = 52;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // lbCustomer
            // 
            lbCustomer.AutoSize = true;
            lbCustomer.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbCustomer.Location = new Point(64, 100);
            lbCustomer.Name = "lbCustomer";
            lbCustomer.Size = new Size(140, 25);
            lbCustomer.TabIndex = 41;
            lbCustomer.Text = "Khách Hàng";
            // 
            // txtboxEmployee
            // 
            txtboxEmployee.BorderRadius = 15;
            txtboxEmployee.BorderThickness = 2;
            txtboxEmployee.FocusBorderColor = Color.HotPink;
            txtboxEmployee.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtboxEmployee.ForeColor = SystemColors.ActiveCaptionText;
            txtboxEmployee.HoverBorderColor = Color.DodgerBlue;
            txtboxEmployee.Location = new Point(676, 358);
            txtboxEmployee.Multiline = false;
            txtboxEmployee.Name = "txtboxEmployee";
            txtboxEmployee.NormalBorderColor = Color.LightGray;
            txtboxEmployee.Padding = new Padding(20, 7, 10, 10);
            txtboxEmployee.PasswordChar = '\0';
            txtboxEmployee.PlaceholderText = "Nhân Viên Thụ Lý";
            txtboxEmployee.ReadOnly = false;
            txtboxEmployee.Size = new Size(384, 44);
            txtboxEmployee.TabIndex = 50;
            txtboxEmployee.UseSystemPasswordChar = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.SeaGreen;
            btnSave.BaseColor = Color.SeaGreen;
            btnSave.BorderColor = Color.Transparent;
            btnSave.BorderRadius = 20;
            btnSave.BorderSize = 0;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.HoverColor = Color.FromArgb(34, 139, 34);
            btnSave.Location = new Point(64, 428);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(384, 44);
            btnSave.TabIndex = 51;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // txtboxEndDate
            // 
            txtboxEndDate.BorderRadius = 15;
            txtboxEndDate.BorderThickness = 2;
            txtboxEndDate.FocusBorderColor = Color.HotPink;
            txtboxEndDate.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtboxEndDate.ForeColor = SystemColors.ActiveCaptionText;
            txtboxEndDate.HoverBorderColor = Color.DodgerBlue;
            txtboxEndDate.Location = new Point(676, 288);
            txtboxEndDate.Multiline = false;
            txtboxEndDate.Name = "txtboxEndDate";
            txtboxEndDate.NormalBorderColor = Color.LightGray;
            txtboxEndDate.Padding = new Padding(20, 7, 10, 10);
            txtboxEndDate.PasswordChar = '\0';
            txtboxEndDate.PlaceholderText = "Ngày Dự Kiến Trả Kết Quả";
            txtboxEndDate.ReadOnly = false;
            txtboxEndDate.Size = new Size(384, 44);
            txtboxEndDate.TabIndex = 49;
            txtboxEndDate.UseSystemPasswordChar = false;
            // 
            // txtboxCustomerName
            // 
            txtboxCustomerName.BorderRadius = 15;
            txtboxCustomerName.BorderThickness = 2;
            txtboxCustomerName.FocusBorderColor = Color.HotPink;
            txtboxCustomerName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtboxCustomerName.ForeColor = SystemColors.ActiveCaptionText;
            txtboxCustomerName.HoverBorderColor = Color.DodgerBlue;
            txtboxCustomerName.Location = new Point(64, 148);
            txtboxCustomerName.Multiline = false;
            txtboxCustomerName.Name = "txtboxCustomerName";
            txtboxCustomerName.NormalBorderColor = Color.LightGray;
            txtboxCustomerName.Padding = new Padding(20, 7, 10, 10);
            txtboxCustomerName.PasswordChar = '\0';
            txtboxCustomerName.PlaceholderText = "Tên Khách Hàng / Doanh Nghiệp";
            txtboxCustomerName.ReadOnly = false;
            txtboxCustomerName.Size = new Size(384, 44);
            txtboxCustomerName.TabIndex = 43;
            txtboxCustomerName.UseSystemPasswordChar = false;
            // 
            // cmbContractType
            // 
            cmbContractType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbContractType.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            cmbContractType.FormattingEnabled = true;
            cmbContractType.Items.AddRange(new object[] {
            "Quy",
            "6 Thang"});
            cmbContractType.Location = new Point(676, 218);
            cmbContractType.Name = "cmbContractType";
            cmbContractType.Size = new Size(384, 44);
            cmbContractType.TabIndex = 48;
            // 
            // txtboxPhone
            // 
            txtboxPhone.BorderRadius = 15;
            txtboxPhone.BorderThickness = 2;
            txtboxPhone.FocusBorderColor = Color.HotPink;
            txtboxPhone.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtboxPhone.ForeColor = SystemColors.ActiveCaptionText;
            txtboxPhone.HoverBorderColor = Color.DodgerBlue;
            txtboxPhone.Location = new Point(64, 218);
            txtboxPhone.Multiline = false;
            txtboxPhone.Name = "txtboxPhone";
            txtboxPhone.NormalBorderColor = Color.LightGray;
            txtboxPhone.Padding = new Padding(20, 7, 10, 10);
            txtboxPhone.PasswordChar = '\0';
            txtboxPhone.PlaceholderText = "Ký hiệu doanh nghiệp";
            txtboxPhone.ReadOnly = false;
            txtboxPhone.Size = new Size(384, 44);
            txtboxPhone.TabIndex = 44;
            txtboxPhone.UseSystemPasswordChar = false;
            // 
            // txtboxIDContract
            // 
            txtboxIDContract.BorderRadius = 15;
            txtboxIDContract.BorderThickness = 2;
            txtboxIDContract.FocusBorderColor = Color.HotPink;
            txtboxIDContract.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtboxIDContract.ForeColor = SystemColors.ActiveCaptionText;
            txtboxIDContract.HoverBorderColor = Color.DodgerBlue;
            txtboxIDContract.Location = new Point(676, 148);
            txtboxIDContract.Multiline = false;
            txtboxIDContract.Name = "txtboxIDContract";
            txtboxIDContract.NormalBorderColor = Color.LightGray;
            txtboxIDContract.Padding = new Padding(20, 7, 10, 10);
            txtboxIDContract.PasswordChar = '\0';
            txtboxIDContract.PlaceholderText = "Số Hợp Đồng";
            txtboxIDContract.ReadOnly = false;
            txtboxIDContract.Size = new Size(384, 44);
            txtboxIDContract.TabIndex = 47;
            txtboxIDContract.UseSystemPasswordChar = false;
            // 
            // txtboxAddress
            // 
            txtboxAddress.BorderRadius = 15;
            txtboxAddress.BorderThickness = 2;
            txtboxAddress.FocusBorderColor = Color.HotPink;
            txtboxAddress.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtboxAddress.ForeColor = SystemColors.ActiveCaptionText;
            txtboxAddress.HoverBorderColor = Color.DodgerBlue;
            txtboxAddress.Location = new Point(64, 288);
            txtboxAddress.Multiline = false;
            txtboxAddress.Name = "txtboxAddress";
            txtboxAddress.NormalBorderColor = Color.LightGray;
            txtboxAddress.Padding = new Padding(20, 7, 10, 10);
            txtboxAddress.PasswordChar = '\0';
            txtboxAddress.PlaceholderText = "Địa Chỉ";
            txtboxAddress.ReadOnly = false;
            txtboxAddress.Size = new Size(384, 44);
            txtboxAddress.TabIndex = 45;
            txtboxAddress.UseSystemPasswordChar = false;
            // 
            // txtboxOwner
            // 
            txtboxOwner.BorderRadius = 15;
            txtboxOwner.BorderThickness = 2;
            txtboxOwner.FocusBorderColor = Color.HotPink;
            txtboxOwner.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtboxOwner.ForeColor = SystemColors.ActiveCaptionText;
            txtboxOwner.HoverBorderColor = Color.DodgerBlue;
            txtboxOwner.Location = new Point(64, 358);
            txtboxOwner.Multiline = false;
            txtboxOwner.Name = "txtboxOwner";
            txtboxOwner.NormalBorderColor = Color.LightGray;
            txtboxOwner.Padding = new Padding(20, 7, 10, 10);
            txtboxOwner.PasswordChar = '\0';
            txtboxOwner.PlaceholderText = "Người Đại Diện";
            txtboxOwner.ReadOnly = false;
            txtboxOwner.Size = new Size(384, 44);
            txtboxOwner.TabIndex = 46;
            txtboxOwner.UseSystemPasswordChar = false;
            // 
            // lbContract
            // 
            lbContract.AutoSize = true;
            lbContract.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbContract.Location = new Point(703, 96);
            lbContract.Name = "lbContract";
            lbContract.Size = new Size(116, 25);
            lbContract.TabIndex = 42;
            lbContract.Text = "Hợp Đồng";
            // 
            // BusinessContent
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "BusinessContent";
            Size = new Size(1227, 507);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lbNamePage;
        private Label lbCustomer;
        private Label lbContract;
        private Components.RoundedTextBox txtboxCustomerName;
        private Components.RoundedTextBox txtboxPhone;
        private Components.RoundedTextBox txtboxAddress;
        private Components.RoundedTextBox txtboxOwner;
        private Components.RoundedTextBox txtboxEndDate;
        private Components.RoundedTextBox txtboxIDContract;
        private Components.RoundedButton btnSave;
        private Components.RoundedButton btnCancel;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Components.RoundedTextBox txtboxEmployee;
        private ComboBox cmbContractType;
    }
}
