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
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblCustomer = new Label();
            lblContract = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            tableLayoutPanel2 = new TableLayoutPanel();
            txtboxCustomerName = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtboxIDContract = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtboxPhone = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtboxAddress = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtboxOwner = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtboxEmployee = new Environmental_Monitoring.View.Components.RoundedTextBox();
            btnCancel = new Environmental_Monitoring.View.Components.RoundedButton();
            btnSave = new Environmental_Monitoring.View.Components.RoundedButton();
            txtEmailCustomer = new Environmental_Monitoring.View.Components.RoundedTextBox();
            cmbContractType = new Environmental_Monitoring.View.Components.RoundedComboBox();
            dtpDueDate = new Environmental_Monitoring.View.Components.CustomDateTimePicker();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            tableLayoutPanel2.SetColumnSpan(lblTitle, 3);
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(63, 26);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(614, 41);
            lblTitle.TabIndex = 40;
            lblTitle.Text = "THÔNG TIN KHÁCH HÀNG VÀ HỢP ĐỒNG";
            // 
            // lblCustomer
            // 
            lblCustomer.AutoSize = true;
            lblCustomer.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            lblCustomer.ForeColor = Color.Black;
            lblCustomer.Location = new Point(63, 104);
            lblCustomer.Name = "lblCustomer";
            tableLayoutPanel2.SetRowSpan(lblCustomer, 2);
            lblCustomer.Size = new Size(144, 31);
            lblCustomer.TabIndex = 41;
            lblCustomer.Text = "Khách Hàng";
            // 
            // lblContract
            // 
            lblContract.AutoSize = true;
            lblContract.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            lblContract.ForeColor = Color.Black;
            lblContract.Location = new Point(670, 104);
            lblContract.Name = "lblContract";
            tableLayoutPanel2.SetRowSpan(lblContract, 2);
            lblContract.Size = new Size(125, 31);
            lblContract.TabIndex = 42;
            lblContract.Text = "Hợp Đồng";
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 6;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.Controls.Add(lblContract, 3, 3);
            tableLayoutPanel2.Controls.Add(lblTitle, 1, 1);
            tableLayoutPanel2.Controls.Add(txtboxCustomerName, 1, 5);
            tableLayoutPanel2.Controls.Add(txtboxIDContract, 3, 5);
            tableLayoutPanel2.Controls.Add(txtboxPhone, 1, 7);
            tableLayoutPanel2.Controls.Add(txtboxAddress, 1, 9);
            tableLayoutPanel2.Controls.Add(txtboxOwner, 1, 11);
            tableLayoutPanel2.Controls.Add(txtboxEmployee, 3, 11);
            tableLayoutPanel2.Controls.Add(lblCustomer, 1, 3);
            tableLayoutPanel2.Controls.Add(btnCancel, 4, 13);
            tableLayoutPanel2.Controls.Add(btnSave, 3, 13);
            tableLayoutPanel2.Controls.Add(txtEmailCustomer, 1, 13);
            tableLayoutPanel2.Controls.Add(cmbContractType, 3, 9);
            tableLayoutPanel2.Controls.Add(dtpDueDate, 3, 7);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 15;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.Size = new Size(1215, 521);
            tableLayoutPanel2.TabIndex = 54;
            // 
            // txtboxCustomerName
            // 
            txtboxCustomerName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtboxCustomerName.BorderRadius = 13;
            txtboxCustomerName.BorderThickness = 2;
            txtboxCustomerName.FocusBorderColor = Color.DimGray;
            txtboxCustomerName.Font = new Font("Segoe UI", 12F);
            txtboxCustomerName.ForeColor = Color.Black;
            txtboxCustomerName.HoverBorderColor = Color.Gray;
            txtboxCustomerName.Location = new Point(63, 153);
            txtboxCustomerName.Multiline = false;
            txtboxCustomerName.Name = "txtboxCustomerName";
            txtboxCustomerName.NormalBorderColor = Color.DarkGray;
            txtboxCustomerName.Padding = new Padding(9);
            txtboxCustomerName.PasswordChar = '\0';
            txtboxCustomerName.PlaceholderText = "";
            txtboxCustomerName.ReadOnly = false;
            tableLayoutPanel2.SetRowSpan(txtboxCustomerName, 2);
            txtboxCustomerName.Size = new Size(480, 46);
            txtboxCustomerName.TabIndex = 43;
            txtboxCustomerName.UseSystemPasswordChar = false;
            // 
            // txtboxIDContract
            // 
            txtboxIDContract.BorderRadius = 13;
            txtboxIDContract.BorderThickness = 2;
            tableLayoutPanel2.SetColumnSpan(txtboxIDContract, 2);
            txtboxIDContract.FocusBorderColor = Color.DimGray;
            txtboxIDContract.Font = new Font("Segoe UI", 12F);
            txtboxIDContract.ForeColor = Color.Black;
            txtboxIDContract.HoverBorderColor = Color.Gray;
            txtboxIDContract.Location = new Point(670, 153);
            txtboxIDContract.Multiline = false;
            txtboxIDContract.Name = "txtboxIDContract";
            txtboxIDContract.NormalBorderColor = Color.DarkGray;
            txtboxIDContract.Padding = new Padding(9);
            txtboxIDContract.PasswordChar = '\0';
            txtboxIDContract.PlaceholderText = "";
            txtboxIDContract.ReadOnly = false;
            tableLayoutPanel2.SetRowSpan(txtboxIDContract, 2);
            txtboxIDContract.Size = new Size(480, 46);
            txtboxIDContract.TabIndex = 44;
            txtboxIDContract.UseSystemPasswordChar = false;
            // 
            // txtboxPhone
            // 
            txtboxPhone.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtboxPhone.BorderRadius = 13;
            txtboxPhone.BorderThickness = 2;
            txtboxPhone.FocusBorderColor = Color.DimGray;
            txtboxPhone.Font = new Font("Segoe UI", 12F);
            txtboxPhone.ForeColor = Color.Black;
            txtboxPhone.HoverBorderColor = Color.Gray;
            txtboxPhone.Location = new Point(63, 225);
            txtboxPhone.Multiline = false;
            txtboxPhone.Name = "txtboxPhone";
            txtboxPhone.NormalBorderColor = Color.DarkGray;
            txtboxPhone.Padding = new Padding(9);
            txtboxPhone.PasswordChar = '\0';
            txtboxPhone.PlaceholderText = "";
            txtboxPhone.ReadOnly = false;
            tableLayoutPanel2.SetRowSpan(txtboxPhone, 2);
            txtboxPhone.Size = new Size(480, 46);
            txtboxPhone.TabIndex = 45;
            txtboxPhone.UseSystemPasswordChar = false;
            // 
            // txtboxAddress
            // 
            txtboxAddress.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtboxAddress.BorderRadius = 13;
            txtboxAddress.BorderThickness = 2;
            txtboxAddress.FocusBorderColor = Color.DimGray;
            txtboxAddress.Font = new Font("Segoe UI", 12F);
            txtboxAddress.ForeColor = Color.Black;
            txtboxAddress.HoverBorderColor = Color.Gray;
            txtboxAddress.Location = new Point(63, 297);
            txtboxAddress.Multiline = false;
            txtboxAddress.Name = "txtboxAddress";
            txtboxAddress.NormalBorderColor = Color.DarkGray;
            txtboxAddress.Padding = new Padding(9);
            txtboxAddress.PasswordChar = '\0';
            txtboxAddress.PlaceholderText = "";
            txtboxAddress.ReadOnly = false;
            tableLayoutPanel2.SetRowSpan(txtboxAddress, 2);
            txtboxAddress.Size = new Size(480, 46);
            txtboxAddress.TabIndex = 47;
            txtboxAddress.UseSystemPasswordChar = false;
            // 
            // txtboxOwner
            // 
            txtboxOwner.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtboxOwner.BorderRadius = 13;
            txtboxOwner.BorderThickness = 2;
            txtboxOwner.FocusBorderColor = Color.DimGray;
            txtboxOwner.Font = new Font("Segoe UI", 12F);
            txtboxOwner.ForeColor = Color.Black;
            txtboxOwner.HoverBorderColor = Color.Gray;
            txtboxOwner.Location = new Point(63, 369);
            txtboxOwner.Multiline = false;
            txtboxOwner.Name = "txtboxOwner";
            txtboxOwner.NormalBorderColor = Color.DarkGray;
            txtboxOwner.Padding = new Padding(9);
            txtboxOwner.PasswordChar = '\0';
            txtboxOwner.PlaceholderText = "";
            txtboxOwner.ReadOnly = false;
            tableLayoutPanel2.SetRowSpan(txtboxOwner, 2);
            txtboxOwner.Size = new Size(480, 46);
            txtboxOwner.TabIndex = 48;
            txtboxOwner.UseSystemPasswordChar = false;
            // 
            // txtboxEmployee
            // 
            txtboxEmployee.BorderRadius = 13;
            txtboxEmployee.BorderThickness = 2;
            tableLayoutPanel2.SetColumnSpan(txtboxEmployee, 2);
            txtboxEmployee.FocusBorderColor = Color.DimGray;
            txtboxEmployee.Font = new Font("Segoe UI", 12F);
            txtboxEmployee.ForeColor = Color.Black;
            txtboxEmployee.HoverBorderColor = Color.Gray;
            txtboxEmployee.Location = new Point(670, 369);
            txtboxEmployee.Multiline = false;
            txtboxEmployee.Name = "txtboxEmployee";
            txtboxEmployee.NormalBorderColor = Color.DarkGray;
            txtboxEmployee.Padding = new Padding(9);
            txtboxEmployee.PasswordChar = '\0';
            txtboxEmployee.PlaceholderText = "";
            txtboxEmployee.ReadOnly = false;
            tableLayoutPanel2.SetRowSpan(txtboxEmployee, 2);
            txtboxEmployee.Size = new Size(480, 46);
            txtboxEmployee.TabIndex = 49;
            txtboxEmployee.UseSystemPasswordChar = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Gray;
            btnCancel.BaseColor = Color.Gray;
            btnCancel.BorderColor = Color.Transparent;
            btnCancel.BorderRadius = 13;
            btnCancel.BorderSize = 0;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.HoverColor = Color.Silver;
            btnCancel.Location = new Point(913, 441);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(237, 46);
            btnCancel.TabIndex = 52;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(0, 113, 0);
            btnSave.BaseColor = Color.FromArgb(0, 113, 0);
            btnSave.BorderColor = Color.Transparent;
            btnSave.BorderRadius = 13;
            btnSave.BorderSize = 0;
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.HoverColor = Color.FromArgb(34, 139, 34);
            btnSave.Location = new Point(670, 441);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(237, 46);
            btnSave.TabIndex = 51;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // txtEmailCustomer
            // 
            txtEmailCustomer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtEmailCustomer.BorderRadius = 13;
            txtEmailCustomer.BorderThickness = 2;
            txtEmailCustomer.FocusBorderColor = Color.DimGray;
            txtEmailCustomer.Font = new Font("Segoe UI", 12F);
            txtEmailCustomer.ForeColor = Color.Black;
            txtEmailCustomer.HoverBorderColor = Color.Gray;
            txtEmailCustomer.Location = new Point(63, 441);
            txtEmailCustomer.Multiline = false;
            txtEmailCustomer.Name = "txtEmailCustomer";
            txtEmailCustomer.NormalBorderColor = Color.DarkGray;
            txtEmailCustomer.Padding = new Padding(9);
            txtEmailCustomer.PasswordChar = '\0';
            txtEmailCustomer.PlaceholderText = "";
            txtEmailCustomer.ReadOnly = false;
            tableLayoutPanel2.SetRowSpan(txtEmailCustomer, 2);
            txtEmailCustomer.Size = new Size(480, 46);
            txtEmailCustomer.TabIndex = 53;
            txtEmailCustomer.UseSystemPasswordChar = false;
            // 
            // cmbContractType
            // 
            cmbContractType.BorderRadius = 13;
            cmbContractType.BorderThickness = 2;
            tableLayoutPanel2.SetColumnSpan(cmbContractType, 2);
            cmbContractType.Cursor = Cursors.Hand;
            cmbContractType.DataSource = null;
            cmbContractType.DisplayMember = "";
            cmbContractType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbContractType.FocusBorderColor = Color.DimGray;
            cmbContractType.Font = new Font("Segoe UI", 12F);
            cmbContractType.ForeColor = Color.Black;
            cmbContractType.HoverBorderColor = Color.Gray;
            cmbContractType.Location = new Point(670, 297);
            cmbContractType.Name = "cmbContractType";
            cmbContractType.NormalBorderColor = Color.DarkGray;
            cmbContractType.SelectedIndex = -1;
            cmbContractType.SelectedItem = null;
            cmbContractType.SelectedValue = null;
            cmbContractType.Size = new Size(480, 46);
            cmbContractType.TabIndex = 46;
            cmbContractType.ValueMember = "";
            // 
            // dtpDueDate
            // 
            dtpDueDate.BorderColor = Color.FromArgb(171, 171, 171);
            dtpDueDate.BorderRadius = 13;
            dtpDueDate.BorderSize = 1;
            tableLayoutPanel2.SetColumnSpan(dtpDueDate, 2);
            dtpDueDate.Cursor = Cursors.Hand;
            dtpDueDate.CustomFormat = null;
            dtpDueDate.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 163);
            dtpDueDate.Format = DateTimePickerFormat.Short;
            dtpDueDate.Location = new Point(671, 226);
            dtpDueDate.Margin = new Padding(4);
            dtpDueDate.Name = "dtpDueDate";
            dtpDueDate.Size = new Size(478, 44);
            dtpDueDate.TabIndex = 50;
            dtpDueDate.Value = new DateTime(2025, 11, 15, 9, 57, 11, 361);
            // 
            // BusinessContent
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel2);
            Name = "BusinessContent";
            Size = new Size(1215, 521);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private Label lblCustomer;
        private Label lblContract;
        private Components.RoundedButton btnSave;
        private TableLayoutPanel tableLayoutPanel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        //private Components.RoundedButton btnSave;
        private Microsoft.Data.SqlClient.SqlCommandBuilder sqlCommandBuilder1;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private TableLayoutPanel tableLayoutPanel2;
        private Components.RoundedTextBox txtboxCustomerName;
        private Components.RoundedTextBox txtboxIDContract;
        private Components.RoundedTextBox txtboxPhone;
        private Components.RoundedComboBox cmbContractType;
        private Components.RoundedTextBox txtboxAddress;
        private Components.RoundedTextBox txtboxOwner;
        private Components.RoundedTextBox txtboxEmployee;
        private Components.CustomDateTimePicker dtpDueDate;
        private Components.RoundedButton btnCancel;
        private Components.RoundedTextBox txtEmailCustomer;
    }
}
