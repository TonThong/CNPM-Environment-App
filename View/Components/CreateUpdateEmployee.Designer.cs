namespace Environmental_Monitoring.View
{
    partial class CreateUpdateEmployee
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
            lblMaNV = new Label();
            lblYear = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            lblDepartment = new Label();
            lblHoTen = new Label();
            label9 = new Label();
            label10 = new Label();
            lblRole = new Label();
            lblSDT = new Label();
            label13 = new Label();
            label14 = new Label();
            lblEmail = new Label();
            lblAddress = new Label();
            label17 = new Label();
            lblPass = new Label();
            btnSave = new Environmental_Monitoring.View.Components.RoundedButton();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            txtMaNV = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtNamSinh = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtDiaChi = new Environmental_Monitoring.View.Components.RoundedTextBox();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            tableLayoutPanel1 = new TableLayoutPanel();
            txtHoTen = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtEmail = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtMatKhau = new Environmental_Monitoring.View.Components.RoundedTextBox();
            cbbRole = new Environmental_Monitoring.View.Components.RoundedComboBox();
            txtSDT = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtPhong = new Environmental_Monitoring.View.Components.RoundedTextBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnCancel = new Environmental_Monitoring.View.Components.RoundedButton();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // lblMaNV
            // 
            lblMaNV.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblMaNV.AutoSize = true;
            lblMaNV.Location = new Point(49, 26);
            lblMaNV.Name = "lblMaNV";
            lblMaNV.Size = new Size(104, 47);
            lblMaNV.TabIndex = 0;
            lblMaNV.Text = "Mã nhân viên";
            lblMaNV.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblYear
            // 
            lblYear.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblYear.AutoSize = true;
            lblYear.Location = new Point(508, 26);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(104, 47);
            lblYear.TabIndex = 2;
            lblYear.Text = "Năm sinh";
            lblYear.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Red;
            label3.Location = new Point(389, 26);
            label3.Name = "label3";
            label3.Size = new Size(21, 28);
            label3.TabIndex = 4;
            label3.Text = "*";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Red;
            label4.Location = new Point(389, 120);
            label4.Name = "label4";
            label4.Size = new Size(21, 28);
            label4.TabIndex = 5;
            label4.Text = "*";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Red;
            label5.Location = new Point(848, 120);
            label5.Name = "label5";
            label5.Size = new Size(21, 28);
            label5.TabIndex = 11;
            label5.Text = "*";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Red;
            label6.Location = new Point(848, 26);
            label6.Name = "label6";
            label6.Size = new Size(21, 28);
            label6.TabIndex = 10;
            label6.Text = "*";
            // 
            // lblDepartment
            // 
            lblDepartment.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblDepartment.AutoSize = true;
            lblDepartment.Location = new Point(508, 120);
            lblDepartment.Name = "lblDepartment";
            lblDepartment.Size = new Size(104, 47);
            lblDepartment.TabIndex = 8;
            lblDepartment.Text = "Phòng ban";
            lblDepartment.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblHoTen
            // 
            lblHoTen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(49, 120);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(104, 47);
            lblHoTen.TabIndex = 6;
            lblHoTen.Text = "Họ tên";
            lblHoTen.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.Red;
            label9.Location = new Point(848, 308);
            label9.Name = "label9";
            label9.Size = new Size(21, 28);
            label9.TabIndex = 23;
            label9.Text = "*";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.Red;
            label10.Location = new Point(848, 214);
            label10.Name = "label10";
            label10.Size = new Size(21, 28);
            label10.TabIndex = 22;
            label10.Text = "*";
            // 
            // lblRole
            // 
            lblRole.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblRole.AutoSize = true;
            lblRole.Location = new Point(508, 308);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(104, 47);
            lblRole.TabIndex = 20;
            lblRole.Text = "Vai trò";
            lblRole.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSDT
            // 
            lblSDT.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblSDT.AutoSize = true;
            lblSDT.Location = new Point(508, 214);
            lblSDT.Name = "lblSDT";
            lblSDT.Size = new Size(104, 47);
            lblSDT.TabIndex = 18;
            lblSDT.Text = "Số điện thoại";
            lblSDT.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.Red;
            label13.Location = new Point(389, 308);
            label13.Name = "label13";
            label13.Size = new Size(21, 28);
            label13.TabIndex = 17;
            label13.Text = "*";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = Color.Red;
            label14.Location = new Point(389, 214);
            label14.Name = "label14";
            label14.Size = new Size(21, 28);
            label14.TabIndex = 16;
            label14.Text = "*";
            // 
            // lblEmail
            // 
            lblEmail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(49, 308);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(104, 47);
            lblEmail.TabIndex = 14;
            lblEmail.Text = "Email";
            lblEmail.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblAddress
            // 
            lblAddress.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(49, 214);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(104, 47);
            lblAddress.TabIndex = 12;
            lblAddress.Text = "Địa chỉ";
            lblAddress.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label17.ForeColor = Color.Red;
            label17.Location = new Point(389, 402);
            label17.Name = "label17";
            label17.Size = new Size(21, 28);
            label17.TabIndex = 26;
            label17.Text = "*";
            // 
            // lblPass
            // 
            lblPass.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblPass.AutoSize = true;
            lblPass.Location = new Point(49, 402);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(104, 47);
            lblPass.TabIndex = 24;
            lblPass.Text = "Mật khẩu";
            lblPass.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.BackColor = Color.SeaGreen;
            btnSave.BaseColor = Color.SeaGreen;
            btnSave.BorderColor = Color.Transparent;
            btnSave.BorderRadius = 10;
            btnSave.BorderSize = 0;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.ForeColor = Color.White;
            btnSave.HoverColor = Color.FromArgb(34, 139, 34);
            btnSave.ImageAlign = ContentAlignment.MiddleLeft;
            btnSave.Location = new Point(190, 4);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(168, 53);
            btnSave.TabIndex = 28;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // txtMaNV
            // 
            txtMaNV.BackColor = Color.White;
            txtMaNV.BorderRadius = 5;
            txtMaNV.BorderThickness = 2;
            txtMaNV.Dock = DockStyle.Fill;
            txtMaNV.FocusBorderColor = Color.DimGray;
            txtMaNV.HoverBorderColor = Color.DarkGray;
            txtMaNV.Location = new Point(159, 29);
            txtMaNV.Multiline = false;
            txtMaNV.Name = "txtMaNV";
            txtMaNV.NormalBorderColor = Color.LightGray;
            txtMaNV.Padding = new Padding(5);
            txtMaNV.PasswordChar = '\0';
            txtMaNV.PlaceholderText = "Nhập Mã NV";
            txtMaNV.ReadOnly = false;
            txtMaNV.Size = new Size(224, 41);
            txtMaNV.TabIndex = 30;
            txtMaNV.UseSystemPasswordChar = false;
            // 
            // txtNamSinh
            // 
            txtNamSinh.BackColor = Color.White;
            txtNamSinh.BorderRadius = 5;
            txtNamSinh.BorderThickness = 2;
            txtNamSinh.Dock = DockStyle.Fill;
            txtNamSinh.FocusBorderColor = Color.DimGray;
            txtNamSinh.HoverBorderColor = Color.DarkGray;
            txtNamSinh.Location = new Point(618, 29);
            txtNamSinh.Multiline = false;
            txtNamSinh.Name = "txtNamSinh";
            txtNamSinh.NormalBorderColor = Color.LightGray;
            txtNamSinh.Padding = new Padding(5);
            txtNamSinh.PasswordChar = '\0';
            txtNamSinh.PlaceholderText = "Nhập Năm Sinh";
            txtNamSinh.ReadOnly = false;
            txtNamSinh.Size = new Size(224, 41);
            txtNamSinh.TabIndex = 31;
            txtNamSinh.UseSystemPasswordChar = false;
            // 
            // txtDiaChi
            // 
            txtDiaChi.BackColor = Color.White;
            txtDiaChi.BorderRadius = 5;
            txtDiaChi.BorderThickness = 2;
            txtDiaChi.Dock = DockStyle.Fill;
            txtDiaChi.FocusBorderColor = Color.DimGray;
            txtDiaChi.HoverBorderColor = Color.DarkGray;
            txtDiaChi.Location = new Point(159, 217);
            txtDiaChi.Multiline = false;
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.NormalBorderColor = Color.LightGray;
            txtDiaChi.Padding = new Padding(5);
            txtDiaChi.PasswordChar = '\0';
            txtDiaChi.PlaceholderText = "Nhập Địa Chỉ";
            txtDiaChi.ReadOnly = false;
            txtDiaChi.Size = new Size(224, 41);
            txtDiaChi.TabIndex = 32;
            txtDiaChi.UseSystemPasswordChar = false;
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 9;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Controls.Add(txtHoTen, 2, 3);
            tableLayoutPanel1.Controls.Add(txtDiaChi, 2, 5);
            tableLayoutPanel1.Controls.Add(lblRole, 5, 7);
            tableLayoutPanel1.Controls.Add(txtMaNV, 2, 1);
            tableLayoutPanel1.Controls.Add(lblAddress, 1, 5);
            tableLayoutPanel1.Controls.Add(lblSDT, 5, 5);
            tableLayoutPanel1.Controls.Add(lblDepartment, 5, 3);
            tableLayoutPanel1.Controls.Add(lblEmail, 1, 7);
            tableLayoutPanel1.Controls.Add(lblPass, 1, 9);
            tableLayoutPanel1.Controls.Add(lblHoTen, 1, 3);
            tableLayoutPanel1.Controls.Add(lblYear, 5, 1);
            tableLayoutPanel1.Controls.Add(label4, 3, 3);
            tableLayoutPanel1.Controls.Add(label3, 3, 1);
            tableLayoutPanel1.Controls.Add(lblMaNV, 1, 1);
            tableLayoutPanel1.Controls.Add(label14, 3, 5);
            tableLayoutPanel1.Controls.Add(txtEmail, 2, 7);
            tableLayoutPanel1.Controls.Add(label13, 3, 7);
            tableLayoutPanel1.Controls.Add(txtMatKhau, 2, 9);
            tableLayoutPanel1.Controls.Add(label17, 3, 9);
            tableLayoutPanel1.Controls.Add(label9, 7, 7);
            tableLayoutPanel1.Controls.Add(cbbRole, 6, 7);
            tableLayoutPanel1.Controls.Add(txtSDT, 6, 5);
            tableLayoutPanel1.Controls.Add(label10, 7, 5);
            tableLayoutPanel1.Controls.Add(txtPhong, 6, 3);
            tableLayoutPanel1.Controls.Add(label5, 7, 3);
            tableLayoutPanel1.Controls.Add(txtNamSinh, 6, 1);
            tableLayoutPanel1.Controls.Add(label6, 7, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 5, 9);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 12;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Size = new Size(922, 523);
            tableLayoutPanel1.TabIndex = 33;
            // 
            // txtHoTen
            // 
            txtHoTen.BackColor = Color.White;
            txtHoTen.BorderRadius = 5;
            txtHoTen.BorderThickness = 2;
            txtHoTen.Dock = DockStyle.Fill;
            txtHoTen.FocusBorderColor = Color.DimGray;
            txtHoTen.HoverBorderColor = Color.DarkGray;
            txtHoTen.Location = new Point(159, 123);
            txtHoTen.Multiline = false;
            txtHoTen.Name = "txtHoTen";
            txtHoTen.NormalBorderColor = Color.LightGray;
            txtHoTen.Padding = new Padding(5);
            txtHoTen.PasswordChar = '\0';
            txtHoTen.PlaceholderText = "Nhập Họ Và Tên";
            txtHoTen.ReadOnly = false;
            txtHoTen.Size = new Size(224, 41);
            txtHoTen.TabIndex = 35;
            txtHoTen.UseSystemPasswordChar = false;
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.White;
            txtEmail.BorderRadius = 5;
            txtEmail.BorderThickness = 2;
            txtEmail.Dock = DockStyle.Fill;
            txtEmail.FocusBorderColor = Color.DimGray;
            txtEmail.HoverBorderColor = Color.DarkGray;
            txtEmail.Location = new Point(159, 311);
            txtEmail.Multiline = false;
            txtEmail.Name = "txtEmail";
            txtEmail.NormalBorderColor = Color.LightGray;
            txtEmail.Padding = new Padding(5);
            txtEmail.PasswordChar = '\0';
            txtEmail.PlaceholderText = "Nhập Email";
            txtEmail.ReadOnly = false;
            txtEmail.Size = new Size(224, 41);
            txtEmail.TabIndex = 33;
            txtEmail.UseSystemPasswordChar = false;
            // 
            // txtMatKhau
            // 
            txtMatKhau.BackColor = Color.White;
            txtMatKhau.BorderRadius = 5;
            txtMatKhau.BorderThickness = 2;
            txtMatKhau.Dock = DockStyle.Fill;
            txtMatKhau.FocusBorderColor = Color.DimGray;
            txtMatKhau.HoverBorderColor = Color.DarkGray;
            txtMatKhau.Location = new Point(159, 405);
            txtMatKhau.Multiline = false;
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.NormalBorderColor = Color.LightGray;
            txtMatKhau.Padding = new Padding(5);
            txtMatKhau.PasswordChar = '\0';
            txtMatKhau.PlaceholderText = "Nhập Mật Khẩu";
            txtMatKhau.ReadOnly = false;
            txtMatKhau.Size = new Size(224, 41);
            txtMatKhau.TabIndex = 34;
            txtMatKhau.UseSystemPasswordChar = false;
            // 
            // cbbRole
            // 
            cbbRole.BackColor = Color.White;
            cbbRole.BorderRadius = 5;
            cbbRole.BorderThickness = 2;
            cbbRole.DataSource = null;
            cbbRole.DisplayMember = "";
            cbbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbRole.FocusBorderColor = Color.DimGray;
            cbbRole.HoverBorderColor = Color.DarkGray;
            cbbRole.Location = new Point(618, 311);
            cbbRole.Name = "cbbRole";
            cbbRole.NormalBorderColor = Color.LightGray;
            cbbRole.Padding = new Padding(5, 10, 5, 10);
            cbbRole.SelectedIndex = -1;
            cbbRole.SelectedItem = null;
            cbbRole.SelectedValue = null;
            cbbRole.Size = new Size(224, 41);
            cbbRole.TabIndex = 39;
            cbbRole.ValueMember = "";
            // 
            // txtSDT
            // 
            txtSDT.BackColor = Color.White;
            txtSDT.BorderRadius = 5;
            txtSDT.BorderThickness = 2;
            txtSDT.Dock = DockStyle.Fill;
            txtSDT.FocusBorderColor = Color.DimGray;
            txtSDT.HoverBorderColor = Color.DarkGray;
            txtSDT.Location = new Point(618, 217);
            txtSDT.Multiline = false;
            txtSDT.Name = "txtSDT";
            txtSDT.NormalBorderColor = Color.LightGray;
            txtSDT.Padding = new Padding(5);
            txtSDT.PasswordChar = '\0';
            txtSDT.PlaceholderText = "Nhập Số Điện Thoại";
            txtSDT.ReadOnly = false;
            txtSDT.Size = new Size(224, 41);
            txtSDT.TabIndex = 37;
            txtSDT.UseSystemPasswordChar = false;
            // 
            // txtPhong
            // 
            txtPhong.BackColor = Color.White;
            txtPhong.BorderRadius = 5;
            txtPhong.BorderThickness = 2;
            txtPhong.Dock = DockStyle.Fill;
            txtPhong.FocusBorderColor = Color.DimGray;
            txtPhong.HoverBorderColor = Color.DarkGray;
            txtPhong.Location = new Point(618, 123);
            txtPhong.Multiline = false;
            txtPhong.Name = "txtPhong";
            txtPhong.NormalBorderColor = Color.LightGray;
            txtPhong.Padding = new Padding(5);
            txtPhong.PasswordChar = '\0';
            txtPhong.PlaceholderText = "Nhập Phòng Ban";
            txtPhong.ReadOnly = false;
            txtPhong.Size = new Size(224, 41);
            txtPhong.TabIndex = 36;
            txtPhong.UseSystemPasswordChar = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 3);
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
            tableLayoutPanel2.Controls.Add(btnCancel, 0, 0);
            tableLayoutPanel2.Controls.Add(btnSave, 2, 0);
            tableLayoutPanel2.Location = new Point(508, 405);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel1.SetRowSpan(tableLayoutPanel2, 2);
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(361, 61);
            tableLayoutPanel2.TabIndex = 41;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = SystemColors.ButtonShadow;
            btnCancel.BaseColor = SystemColors.ButtonShadow;
            btnCancel.BorderColor = Color.Transparent;
            btnCancel.BorderRadius = 10;
            btnCancel.BorderSize = 0;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.ForeColor = Color.White;
            btnCancel.HoverColor = Color.FromArgb(34, 139, 34);
            btnCancel.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancel.Location = new Point(3, 4);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(167, 53);
            btnCancel.TabIndex = 40;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // CreateUpdateEmployee
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(217, 244, 227);
            ClientSize = new Size(922, 523);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(3, 4, 3, 4);
            Name = "CreateUpdateEmployee";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CreateUpdateEmployee";
            Load += CreateUpdateEmployee_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblMaNV;
        private Label lblYear;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label lblDepartment;
        private Label lblHoTen;
        private Label label9;
        private Label label10;
        private Label lblRole;
        private Label lblSDT;
        private Label label13;
        private Label label14;
        private Label lblEmail;
        private Label lblAddress;
        private Label label17;
        private Label lblPass;
        private Components.RoundedButton btnSave;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Components.RoundedTextBox txtMaNV;
        private Components.RoundedTextBox txtNamSinh;
        private Components.RoundedTextBox txtDiaChi;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private TableLayoutPanel tableLayoutPanel1;
        private Components.RoundedTextBox txtEmail;
        private Components.RoundedTextBox txtMatKhau;
        private Components.RoundedTextBox txtHoTen;
        private Components.RoundedTextBox txtPhong;
        private Components.RoundedTextBox txtSDT;
        private Components.RoundedComboBox cbbRole;
        private Components.RoundedButton btnCancel;
        private TableLayoutPanel tableLayoutPanel2;
    }
}