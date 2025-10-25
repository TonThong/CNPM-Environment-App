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
            btnSearch = new Environmental_Monitoring.View.Components.RoundedButton();
            btnSave = new Environmental_Monitoring.View.Components.RoundedButton();
            txtboxEmployee = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtboxEndDate = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtboxStartDate = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtboxIDContract = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtboxOwner = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtboxAddress = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtboxPhone = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtboxCustomerName = new Environmental_Monitoring.View.Components.RoundedTextBox();
            lbContract = new Label();
            lbCustomer = new Label();
            lbNamePage = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(txtboxEmployee);
            panel1.Controls.Add(txtboxEndDate);
            panel1.Controls.Add(txtboxStartDate);
            panel1.Controls.Add(txtboxIDContract);
            panel1.Controls.Add(txtboxOwner);
            panel1.Controls.Add(txtboxAddress);
            panel1.Controls.Add(txtboxPhone);
            panel1.Controls.Add(txtboxCustomerName);
            panel1.Controls.Add(lbContract);
            panel1.Controls.Add(lbCustomer);
            panel1.Controls.Add(lbNamePage);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1227, 507);
            panel1.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(217, 217, 217);
            btnSearch.BaseColor = Color.FromArgb(217, 217, 217);
            btnSearch.BorderColor = Color.Transparent;
            btnSearch.BorderRadius = 25;
            btnSearch.BorderSize = 0;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnSearch.ForeColor = Color.Black;
            btnSearch.HoverColor = Color.FromArgb(34, 139, 34);
            btnSearch.Location = new Point(703, 425);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(450, 50);
            btnSearch.TabIndex = 52;
            btnSearch.Text = "Trình Duyệt";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.SeaGreen;
            btnSave.BaseColor = Color.SeaGreen;
            btnSave.BorderColor = Color.Transparent;
            btnSave.BorderRadius = 25;
            btnSave.BorderSize = 0;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.HoverColor = Color.FromArgb(34, 139, 34);
            btnSave.Location = new Point(151, 425);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(450, 50);
            btnSave.TabIndex = 51;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // txtboxEmployee
            // 
            txtboxEmployee.BorderRadius = 15;
            txtboxEmployee.BorderThickness = 2;
            txtboxEmployee.FocusBorderColor = Color.HotPink;
            txtboxEmployee.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtboxEmployee.HoverBorderColor = Color.DodgerBlue;
            txtboxEmployee.Location = new Point(703, 351);
            txtboxEmployee.Multiline = false;
            txtboxEmployee.Name = "txtboxEmployee";
            txtboxEmployee.NormalBorderColor = Color.Gray;
            txtboxEmployee.Padding = new Padding(30, 15, 30, 15);
            txtboxEmployee.PasswordChar = '\0';
            txtboxEmployee.PlaceholderText = "Mã Nhân Viên Phụ Trách";
            txtboxEmployee.ReadOnly = false;
            txtboxEmployee.Size = new Size(450, 50);
            txtboxEmployee.TabIndex = 50;
            // 
            // txtboxEndDate
            // 
            txtboxEndDate.BorderRadius = 15;
            txtboxEndDate.BorderThickness = 2;
            txtboxEndDate.FocusBorderColor = Color.HotPink;
            txtboxEndDate.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtboxEndDate.HoverBorderColor = Color.DodgerBlue;
            txtboxEndDate.Location = new Point(703, 279);
            txtboxEndDate.Multiline = false;
            txtboxEndDate.Name = "txtboxEndDate";
            txtboxEndDate.NormalBorderColor = Color.Gray;
            txtboxEndDate.Padding = new Padding(30, 15, 30, 15);
            txtboxEndDate.PasswordChar = '\0';
            txtboxEndDate.PlaceholderText = "Ngày Dự Kiến Trả Kết Quả";
            txtboxEndDate.ReadOnly = false;
            txtboxEndDate.Size = new Size(450, 50);
            txtboxEndDate.TabIndex = 49;
            // 
            // txtboxStartDate
            // 
            txtboxStartDate.BorderRadius = 15;
            txtboxStartDate.BorderThickness = 2;
            txtboxStartDate.FocusBorderColor = Color.HotPink;
            txtboxStartDate.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtboxStartDate.HoverBorderColor = Color.DodgerBlue;
            txtboxStartDate.Location = new Point(703, 211);
            txtboxStartDate.Multiline = false;
            txtboxStartDate.Name = "txtboxStartDate";
            txtboxStartDate.NormalBorderColor = Color.Gray;
            txtboxStartDate.Padding = new Padding(30, 15, 30, 15);
            txtboxStartDate.PasswordChar = '\0';
            txtboxStartDate.PlaceholderText = "Ngày Lập Hợp Đồng";
            txtboxStartDate.ReadOnly = false;
            txtboxStartDate.Size = new Size(450, 50);
            txtboxStartDate.TabIndex = 48;
            // 
            // txtboxIDContract
            // 
            txtboxIDContract.BorderRadius = 15;
            txtboxIDContract.BorderThickness = 2;
            txtboxIDContract.FocusBorderColor = Color.HotPink;
            txtboxIDContract.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtboxIDContract.HoverBorderColor = Color.DodgerBlue;
            txtboxIDContract.Location = new Point(703, 141);
            txtboxIDContract.Multiline = false;
            txtboxIDContract.Name = "txtboxIDContract";
            txtboxIDContract.NormalBorderColor = Color.Gray;
            txtboxIDContract.Padding = new Padding(30, 15, 30, 15);
            txtboxIDContract.PasswordChar = '\0';
            txtboxIDContract.PlaceholderText = "Số Hợp Đồng";
            txtboxIDContract.ReadOnly = false;
            txtboxIDContract.Size = new Size(450, 50);
            txtboxIDContract.TabIndex = 47;
            // 
            // txtboxOwner
            // 
            txtboxOwner.BorderRadius = 15;
            txtboxOwner.BorderThickness = 2;
            txtboxOwner.FocusBorderColor = Color.HotPink;
            txtboxOwner.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtboxOwner.HoverBorderColor = Color.DodgerBlue;
            txtboxOwner.Location = new Point(151, 351);
            txtboxOwner.Multiline = false;
            txtboxOwner.Name = "txtboxOwner";
            txtboxOwner.NormalBorderColor = Color.Gray;
            txtboxOwner.Padding = new Padding(30, 15, 30, 15);
            txtboxOwner.PasswordChar = '\0';
            txtboxOwner.PlaceholderText = "Người Đại Diện";
            txtboxOwner.ReadOnly = false;
            txtboxOwner.Size = new Size(450, 50);
            txtboxOwner.TabIndex = 46;
            // 
            // txtboxAddress
            // 
            txtboxAddress.BorderRadius = 15;
            txtboxAddress.BorderThickness = 2;
            txtboxAddress.FocusBorderColor = Color.HotPink;
            txtboxAddress.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtboxAddress.HoverBorderColor = Color.DodgerBlue;
            txtboxAddress.Location = new Point(151, 279);
            txtboxAddress.Multiline = false;
            txtboxAddress.Name = "txtboxAddress";
            txtboxAddress.NormalBorderColor = Color.Gray;
            txtboxAddress.Padding = new Padding(30, 15, 30, 15);
            txtboxAddress.PasswordChar = '\0';
            txtboxAddress.PlaceholderText = "Địa Chỉ";
            txtboxAddress.ReadOnly = false;
            txtboxAddress.Size = new Size(450, 50);
            txtboxAddress.TabIndex = 45;
            // 
            // txtboxPhone
            // 
            txtboxPhone.BorderRadius = 15;
            txtboxPhone.BorderThickness = 2;
            txtboxPhone.FocusBorderColor = Color.HotPink;
            txtboxPhone.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtboxPhone.HoverBorderColor = Color.DodgerBlue;
            txtboxPhone.Location = new Point(151, 211);
            txtboxPhone.Multiline = false;
            txtboxPhone.Name = "txtboxPhone";
            txtboxPhone.NormalBorderColor = Color.Gray;
            txtboxPhone.Padding = new Padding(30, 15, 30, 15);
            txtboxPhone.PasswordChar = '\0';
            txtboxPhone.PlaceholderText = "Số Điện Thoại";
            txtboxPhone.ReadOnly = false;
            txtboxPhone.Size = new Size(450, 50);
            txtboxPhone.TabIndex = 44;
            // 
            // txtboxCustomerName
            // 
            txtboxCustomerName.BorderRadius = 15;
            txtboxCustomerName.BorderThickness = 2;
            txtboxCustomerName.FocusBorderColor = Color.HotPink;
            txtboxCustomerName.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtboxCustomerName.HoverBorderColor = Color.DodgerBlue;
            txtboxCustomerName.Location = new Point(151, 141);
            txtboxCustomerName.Multiline = false;
            txtboxCustomerName.Name = "txtboxCustomerName";
            txtboxCustomerName.NormalBorderColor = Color.Gray;
            txtboxCustomerName.Padding = new Padding(30, 15, 30, 15);
            txtboxCustomerName.PasswordChar = '\0';
            txtboxCustomerName.PlaceholderText = "Tên Khách Hàng / Doanh Nghiệp";
            txtboxCustomerName.ReadOnly = false;
            txtboxCustomerName.Size = new Size(450, 50);
            txtboxCustomerName.TabIndex = 43;
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
            // lbCustomer
            // 
            lbCustomer.AutoSize = true;
            lbCustomer.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbCustomer.Location = new Point(151, 96);
            lbCustomer.Name = "lbCustomer";
            lbCustomer.Size = new Size(140, 25);
            lbCustomer.TabIndex = 41;
            lbCustomer.Text = "Khách Hàng";
            // 
            // lbNamePage
            // 
            lbNamePage.AutoSize = true;
            lbNamePage.BackColor = Color.Transparent;
            lbNamePage.Font = new Font("Times New Roman", 24F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lbNamePage.ForeColor = Color.Black;
            lbNamePage.Location = new Point(111, 29);
            lbNamePage.Name = "lbNamePage";
            lbNamePage.Size = new Size(833, 45);
            lbNamePage.TabIndex = 40;
            lbNamePage.Text = "THÔNG TIN KHÁCH HÀNG VÀ HỢP ĐỒNG";
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
        private Components.RoundedTextBox txtboxEmployee;
        private Components.RoundedTextBox txtboxEndDate;
        private Components.RoundedTextBox txtboxStartDate;
        private Components.RoundedTextBox txtboxIDContract;
        private Components.RoundedButton btnSave;
        private Components.RoundedButton btnSearch;
        private Panel panel1;
    }
}
