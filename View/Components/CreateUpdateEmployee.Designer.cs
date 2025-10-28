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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label16 = new Label();
            label17 = new Label();
            label18 = new Label();
            btnSave = new Environmental_Monitoring.View.Components.RoundedButton();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            txtMaNV = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtNamSinh = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtDiaChi = new Environmental_Monitoring.View.Components.RoundedTextBox();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            tableLayoutPanel1 = new TableLayoutPanel();
            txtSDT = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtPhong = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtHoTen = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtMatKhau = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtEmail = new Environmental_Monitoring.View.Components.RoundedTextBox();
            cbbRole = new Environmental_Monitoring.View.Components.RoundedComboBox();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(49, 26);
            label1.Name = "label1";
            label1.Size = new Size(104, 47);
            label1.TabIndex = 0;
            label1.Text = "Mã nhân viên";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(508, 26);
            label2.Name = "label2";
            label2.Size = new Size(104, 47);
            label2.TabIndex = 2;
            label2.Text = "Năm sinh";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Red;
            label3.Location = new Point(159, 26);
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
            label4.Location = new Point(159, 120);
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
            label5.Location = new Point(618, 120);
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
            label6.Location = new Point(618, 26);
            label6.Name = "label6";
            label6.Size = new Size(21, 28);
            label6.TabIndex = 10;
            label6.Text = "*";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(508, 120);
            label7.Name = "label7";
            label7.Size = new Size(104, 47);
            label7.TabIndex = 8;
            label7.Text = "Phòng ban";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(49, 120);
            label8.Name = "label8";
            label8.Size = new Size(104, 47);
            label8.TabIndex = 6;
            label8.Text = "Họ tên";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.Red;
            label9.Location = new Point(618, 308);
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
            label10.Location = new Point(618, 214);
            label10.Name = "label10";
            label10.Size = new Size(21, 28);
            label10.TabIndex = 22;
            label10.Text = "*";
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label11.AutoSize = true;
            label11.Location = new Point(508, 308);
            label11.Name = "label11";
            label11.Size = new Size(104, 47);
            label11.TabIndex = 20;
            label11.Text = "Vai trò";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label12.AutoSize = true;
            label12.Location = new Point(508, 214);
            label12.Name = "label12";
            label12.Size = new Size(104, 47);
            label12.TabIndex = 18;
            label12.Text = "Số điện thoại";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.Red;
            label13.Location = new Point(159, 308);
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
            label14.Location = new Point(159, 214);
            label14.Name = "label14";
            label14.Size = new Size(21, 28);
            label14.TabIndex = 16;
            label14.Text = "*";
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label15.AutoSize = true;
            label15.Location = new Point(49, 308);
            label15.Name = "label15";
            label15.Size = new Size(104, 47);
            label15.TabIndex = 14;
            label15.Text = "Email";
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label16.AutoSize = true;
            label16.Location = new Point(49, 214);
            label16.Name = "label16";
            label16.Size = new Size(104, 47);
            label16.TabIndex = 12;
            label16.Text = "Địa chỉ";
            label16.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label17.ForeColor = Color.Red;
            label17.Location = new Point(159, 402);
            label17.Name = "label17";
            label17.Size = new Size(21, 28);
            label17.TabIndex = 26;
            label17.Text = "*";
            // 
            // label18
            // 
            label18.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label18.AutoSize = true;
            label18.Location = new Point(49, 402);
            label18.Name = "label18";
            label18.Size = new Size(104, 47);
            label18.TabIndex = 24;
            label18.Text = "Mật khẩu";
            label18.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
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
            btnSave.Location = new Point(645, 406);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(224, 39);
            btnSave.TabIndex = 28;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // txtMaNV
            // 
            txtMaNV.BackColor = Color.White;
            txtMaNV.BorderRadius = 15;
            txtMaNV.BorderThickness = 2;
            txtMaNV.Dock = DockStyle.Fill;
            txtMaNV.FocusBorderColor = Color.HotPink;
            txtMaNV.HoverBorderColor = Color.DodgerBlue;
            txtMaNV.Location = new Point(186, 29);
            txtMaNV.Multiline = false;
            txtMaNV.Name = "txtMaNV";
            txtMaNV.NormalBorderColor = Color.Gray;
            txtMaNV.Padding = new Padding(10);
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
            txtNamSinh.BorderRadius = 15;
            txtNamSinh.BorderThickness = 2;
            txtNamSinh.Dock = DockStyle.Fill;
            txtNamSinh.FocusBorderColor = Color.HotPink;
            txtNamSinh.HoverBorderColor = Color.DodgerBlue;
            txtNamSinh.Location = new Point(645, 29);
            txtNamSinh.Multiline = false;
            txtNamSinh.Name = "txtNamSinh";
            txtNamSinh.NormalBorderColor = Color.Gray;
            txtNamSinh.Padding = new Padding(10);
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
            txtDiaChi.BorderRadius = 15;
            txtDiaChi.BorderThickness = 2;
            txtDiaChi.Dock = DockStyle.Fill;
            txtDiaChi.FocusBorderColor = Color.HotPink;
            txtDiaChi.HoverBorderColor = Color.DodgerBlue;
            txtDiaChi.Location = new Point(186, 217);
            txtDiaChi.Multiline = false;
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.NormalBorderColor = Color.Gray;
            txtDiaChi.Padding = new Padding(10);
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
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Controls.Add(txtSDT, 7, 5);
            tableLayoutPanel1.Controls.Add(txtPhong, 7, 3);
            tableLayoutPanel1.Controls.Add(txtHoTen, 3, 3);
            tableLayoutPanel1.Controls.Add(txtMatKhau, 3, 9);
            tableLayoutPanel1.Controls.Add(txtEmail, 3, 7);
            tableLayoutPanel1.Controls.Add(label1, 1, 1);
            tableLayoutPanel1.Controls.Add(txtDiaChi, 3, 5);
            tableLayoutPanel1.Controls.Add(label3, 2, 1);
            tableLayoutPanel1.Controls.Add(label10, 6, 5);
            tableLayoutPanel1.Controls.Add(txtNamSinh, 7, 1);
            tableLayoutPanel1.Controls.Add(label11, 5, 7);
            tableLayoutPanel1.Controls.Add(label5, 6, 3);
            tableLayoutPanel1.Controls.Add(txtMaNV, 3, 1);
            tableLayoutPanel1.Controls.Add(label6, 6, 1);
            tableLayoutPanel1.Controls.Add(label16, 1, 5);
            tableLayoutPanel1.Controls.Add(label12, 5, 5);
            tableLayoutPanel1.Controls.Add(label17, 2, 9);
            tableLayoutPanel1.Controls.Add(label4, 2, 3);
            tableLayoutPanel1.Controls.Add(label14, 2, 5);
            tableLayoutPanel1.Controls.Add(label7, 5, 3);
            tableLayoutPanel1.Controls.Add(label13, 2, 7);
            tableLayoutPanel1.Controls.Add(label15, 1, 7);
            tableLayoutPanel1.Controls.Add(label18, 1, 9);
            tableLayoutPanel1.Controls.Add(label9, 6, 7);
            tableLayoutPanel1.Controls.Add(btnSave, 7, 9);
            tableLayoutPanel1.Controls.Add(label8, 1, 3);
            tableLayoutPanel1.Controls.Add(label2, 5, 1);
            tableLayoutPanel1.Controls.Add(cbbRole, 7, 7);
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
            // txtSDT
            // 
            txtSDT.BackColor = Color.White;
            txtSDT.BorderRadius = 15;
            txtSDT.BorderThickness = 2;
            txtSDT.Dock = DockStyle.Fill;
            txtSDT.FocusBorderColor = Color.HotPink;
            txtSDT.HoverBorderColor = Color.DodgerBlue;
            txtSDT.Location = new Point(645, 217);
            txtSDT.Multiline = false;
            txtSDT.Name = "txtSDT";
            txtSDT.NormalBorderColor = Color.Gray;
            txtSDT.Padding = new Padding(10);
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
            txtPhong.BorderRadius = 15;
            txtPhong.BorderThickness = 2;
            txtPhong.Dock = DockStyle.Fill;
            txtPhong.FocusBorderColor = Color.HotPink;
            txtPhong.HoverBorderColor = Color.DodgerBlue;
            txtPhong.Location = new Point(645, 123);
            txtPhong.Multiline = false;
            txtPhong.Name = "txtPhong";
            txtPhong.NormalBorderColor = Color.Gray;
            txtPhong.Padding = new Padding(10);
            txtPhong.PasswordChar = '\0';
            txtPhong.PlaceholderText = "Nhập Năm Sinh";
            txtPhong.ReadOnly = false;
            txtPhong.Size = new Size(224, 41);
            txtPhong.TabIndex = 36;
            txtPhong.UseSystemPasswordChar = false;
            // 
            // txtHoTen
            // 
            txtHoTen.BackColor = Color.White;
            txtHoTen.BorderRadius = 15;
            txtHoTen.BorderThickness = 2;
            txtHoTen.Dock = DockStyle.Fill;
            txtHoTen.FocusBorderColor = Color.HotPink;
            txtHoTen.HoverBorderColor = Color.DodgerBlue;
            txtHoTen.Location = new Point(186, 123);
            txtHoTen.Multiline = false;
            txtHoTen.Name = "txtHoTen";
            txtHoTen.NormalBorderColor = Color.Gray;
            txtHoTen.Padding = new Padding(10);
            txtHoTen.PasswordChar = '\0';
            txtHoTen.PlaceholderText = "Nhập Họ Và Tên";
            txtHoTen.ReadOnly = false;
            txtHoTen.Size = new Size(224, 41);
            txtHoTen.TabIndex = 35;
            txtHoTen.UseSystemPasswordChar = false;
            // 
            // txtMatKhau
            // 
            txtMatKhau.BackColor = Color.White;
            txtMatKhau.BorderRadius = 15;
            txtMatKhau.BorderThickness = 2;
            txtMatKhau.Dock = DockStyle.Fill;
            txtMatKhau.FocusBorderColor = Color.HotPink;
            txtMatKhau.HoverBorderColor = Color.DodgerBlue;
            txtMatKhau.Location = new Point(186, 405);
            txtMatKhau.Multiline = false;
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.NormalBorderColor = Color.Gray;
            txtMatKhau.Padding = new Padding(10);
            txtMatKhau.PasswordChar = '\0';
            txtMatKhau.PlaceholderText = "Nhập Mật Khẩu";
            txtMatKhau.ReadOnly = false;
            txtMatKhau.Size = new Size(224, 41);
            txtMatKhau.TabIndex = 34;
            txtMatKhau.UseSystemPasswordChar = false;
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.White;
            txtEmail.BorderRadius = 15;
            txtEmail.BorderThickness = 2;
            txtEmail.Dock = DockStyle.Fill;
            txtEmail.FocusBorderColor = Color.HotPink;
            txtEmail.HoverBorderColor = Color.DodgerBlue;
            txtEmail.Location = new Point(186, 311);
            txtEmail.Multiline = false;
            txtEmail.Name = "txtEmail";
            txtEmail.NormalBorderColor = Color.Gray;
            txtEmail.Padding = new Padding(10);
            txtEmail.PasswordChar = '\0';
            txtEmail.PlaceholderText = "Nhập Email";
            txtEmail.ReadOnly = false;
            txtEmail.Size = new Size(224, 41);
            txtEmail.TabIndex = 33;
            txtEmail.UseSystemPasswordChar = false;
            // 
            // cbbRole
            // 
            cbbRole.BackColor = Color.White;
            cbbRole.BorderRadius = 15;
            cbbRole.BorderThickness = 2;
            cbbRole.DataSource = null;
            cbbRole.DisplayMember = "";
            cbbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbRole.FocusBorderColor = Color.HotPink;
            cbbRole.HoverBorderColor = Color.DodgerBlue;
            cbbRole.Location = new Point(645, 311);
            cbbRole.Name = "cbbRole";
            cbbRole.NormalBorderColor = Color.Gray;
            cbbRole.Padding = new Padding(10, 4, 10, 4);
            cbbRole.SelectedIndex = -1;
            cbbRole.SelectedItem = null;
            cbbRole.SelectedValue = null;
            cbbRole.Size = new Size(224, 41);
            cbbRole.TabIndex = 39;
            cbbRole.ValueMember = "";
            // 
            // CreateUpdateEmployee
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(217, 244, 227);
            ClientSize = new Size(922, 523);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "CreateUpdateEmployee";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CreateUpdateEmployee";
            Load += CreateUpdateEmployee_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
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
    }
}