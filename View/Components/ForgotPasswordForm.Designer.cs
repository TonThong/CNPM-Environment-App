namespace Environmental_Monitoring.View
{
    partial class ForgotPasswordForm
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
            txtMatKhauMoi = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtXacNhanMatKhauMoi = new Environmental_Monitoring.View.Components.RoundedTextBox();
            btnSave = new Environmental_Monitoring.View.Components.RoundedButton();
            btnSendCode = new Environmental_Monitoring.View.Components.RoundedButton();
            btnVerifyCode = new Environmental_Monitoring.View.Components.RoundedButton();
            txtSoDienThoai = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtOTP = new Environmental_Monitoring.View.Components.RoundedTextBox();
            picShowPassNew = new PictureBox();
            picShowPassConfirm = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picShowPassNew).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picShowPassConfirm).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.Location = new Point(83, 9);
            label1.Name = "label1";
            label1.Size = new Size(223, 38);
            label1.TabIndex = 0;
            label1.Text = "Quên Mật Khẩu";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label2.Location = new Point(38, 70);
            label2.Name = "label2";
            label2.Size = new Size(222, 31);
            label2.TabIndex = 1;
            label2.Text = "Xác Thực Tài Khoản";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label3.Location = new Point(38, 70);
            label3.Name = "label3";
            label3.Size = new Size(211, 31);
            label3.TabIndex = 2;
            label3.Text = "Đặt Mật Khẩu Mới";
            // 
            // txtMatKhauMoi
            // 
            txtMatKhauMoi.BorderRadius = 15;
            txtMatKhauMoi.BorderThickness = 2;
            txtMatKhauMoi.Cursor = Cursors.IBeam;
            txtMatKhauMoi.FocusBorderColor = Color.DimGray;
            txtMatKhauMoi.HoverBorderColor = Color.DarkGray;
            txtMatKhauMoi.Location = new Point(67, 124);
            txtMatKhauMoi.Multiline = false;
            txtMatKhauMoi.Name = "txtMatKhauMoi";
            txtMatKhauMoi.NormalBorderColor = Color.LightGray;
            txtMatKhauMoi.Padding = new Padding(10);
            txtMatKhauMoi.PasswordChar = '●';
            txtMatKhauMoi.PlaceholderText = "";
            txtMatKhauMoi.ReadOnly = false;
            txtMatKhauMoi.Size = new Size(260, 40);
            txtMatKhauMoi.TabIndex = 5;
            txtMatKhauMoi.UseSystemPasswordChar = false;
            txtMatKhauMoi.TextChanged += txtMatKhauMoi_TextChanged;
            // 
            // txtXacNhanMatKhauMoi
            // 
            txtXacNhanMatKhauMoi.BorderRadius = 15;
            txtXacNhanMatKhauMoi.BorderThickness = 2;
            txtXacNhanMatKhauMoi.Cursor = Cursors.IBeam;
            txtXacNhanMatKhauMoi.FocusBorderColor = Color.DimGray;
            txtXacNhanMatKhauMoi.HoverBorderColor = Color.DarkGray;
            txtXacNhanMatKhauMoi.Location = new Point(67, 180);
            txtXacNhanMatKhauMoi.Multiline = false;
            txtXacNhanMatKhauMoi.Name = "txtXacNhanMatKhauMoi";
            txtXacNhanMatKhauMoi.NormalBorderColor = Color.LightGray;
            txtXacNhanMatKhauMoi.Padding = new Padding(10);
            txtXacNhanMatKhauMoi.PasswordChar = '●';
            txtXacNhanMatKhauMoi.PlaceholderText = "";
            txtXacNhanMatKhauMoi.ReadOnly = false;
            txtXacNhanMatKhauMoi.Size = new Size(260, 40);
            txtXacNhanMatKhauMoi.TabIndex = 6;
            txtXacNhanMatKhauMoi.UseSystemPasswordChar = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.SeaGreen;
            btnSave.BaseColor = Color.SeaGreen;
            btnSave.BorderColor = Color.Transparent;
            btnSave.BorderRadius = 10;
            btnSave.BorderSize = 0;
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnSave.ForeColor = Color.White;
            btnSave.HoverColor = Color.FromArgb(34, 139, 34);
            btnSave.Location = new Point(115, 240);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(160, 65);
            btnSave.TabIndex = 7;
            btnSave.Text = "Xác Nhận";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnSendCode
            // 
            btnSendCode.BackColor = Color.SeaGreen;
            btnSendCode.BaseColor = Color.SeaGreen;
            btnSendCode.BorderColor = Color.Transparent;
            btnSendCode.BorderRadius = 10;
            btnSendCode.BorderSize = 0;
            btnSendCode.Cursor = Cursors.Hand;
            btnSendCode.FlatAppearance.BorderSize = 0;
            btnSendCode.FlatStyle = FlatStyle.Flat;
            btnSendCode.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnSendCode.ForeColor = Color.White;
            btnSendCode.HoverColor = Color.FromArgb(34, 139, 34);
            btnSendCode.Location = new Point(115, 240);
            btnSendCode.Name = "btnSendCode";
            btnSendCode.Size = new Size(160, 65);
            btnSendCode.TabIndex = 2;
            btnSendCode.Text = "Gửi Mã";
            btnSendCode.UseVisualStyleBackColor = false;
            btnSendCode.Click += btnSendCode_Click;
            // 
            // btnVerifyCode
            // 
            btnVerifyCode.BackColor = Color.SeaGreen;
            btnVerifyCode.BaseColor = Color.SeaGreen;
            btnVerifyCode.BorderColor = Color.Transparent;
            btnVerifyCode.BorderRadius = 10;
            btnVerifyCode.BorderSize = 0;
            btnVerifyCode.Cursor = Cursors.Hand;
            btnVerifyCode.FlatAppearance.BorderSize = 0;
            btnVerifyCode.FlatStyle = FlatStyle.Flat;
            btnVerifyCode.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnVerifyCode.ForeColor = Color.White;
            btnVerifyCode.HoverColor = Color.FromArgb(34, 139, 34);
            btnVerifyCode.Location = new Point(115, 240);
            btnVerifyCode.Name = "btnVerifyCode";
            btnVerifyCode.Size = new Size(160, 65);
            btnVerifyCode.TabIndex = 8;
            btnVerifyCode.Text = "Xác Thực";
            btnVerifyCode.UseVisualStyleBackColor = false;
            btnVerifyCode.Click += btnVerifyCode_Click;
            // 
            // txtSoDienThoai
            // 
            txtSoDienThoai.BorderRadius = 15;
            txtSoDienThoai.BorderThickness = 2;
            txtSoDienThoai.Cursor = Cursors.IBeam;
            txtSoDienThoai.FocusBorderColor = Color.DimGray;
            txtSoDienThoai.HoverBorderColor = Color.DarkGray;
            txtSoDienThoai.Location = new Point(67, 157);
            txtSoDienThoai.Multiline = false;
            txtSoDienThoai.Name = "txtSoDienThoai";
            txtSoDienThoai.NormalBorderColor = Color.LightGray;
            txtSoDienThoai.Padding = new Padding(10);
            txtSoDienThoai.PasswordChar = '\0';
            txtSoDienThoai.PlaceholderText = "";
            txtSoDienThoai.ReadOnly = false;
            txtSoDienThoai.Size = new Size(260, 40);
            txtSoDienThoai.TabIndex = 4;
            txtSoDienThoai.UseSystemPasswordChar = false;
            txtSoDienThoai.TextChanged += txtSoDienThoai_TextChanged;
            // 
            // txtOTP
            // 
            txtOTP.BorderRadius = 15;
            txtOTP.BorderThickness = 2;
            txtOTP.FocusBorderColor = Color.DimGray;
            txtOTP.HoverBorderColor = Color.DarkGray;
            txtOTP.Location = new Point(67, 157);
            txtOTP.Multiline = false;
            txtOTP.Name = "txtOTP";
            txtOTP.NormalBorderColor = Color.LightGray;
            txtOTP.Padding = new Padding(10);
            txtOTP.PasswordChar = '\0';
            txtOTP.PlaceholderText = "";
            txtOTP.ReadOnly = false;
            txtOTP.Size = new Size(260, 40);
            txtOTP.TabIndex = 3;
            txtOTP.UseSystemPasswordChar = false;
            // 
            // picShowPassNew
            // 
            picShowPassNew.Cursor = Cursors.Hand;
            picShowPassNew.Location = new Point(295, 132);
            picShowPassNew.Name = "picShowPassNew";
            picShowPassNew.Size = new Size(24, 24);
            picShowPassNew.SizeMode = PictureBoxSizeMode.Zoom;
            picShowPassNew.TabIndex = 10;
            picShowPassNew.TabStop = false;
            picShowPassNew.Click += picShowPassNew_Click;
            // 
            // picShowPassConfirm
            // 
            picShowPassConfirm.Cursor = Cursors.Hand;
            picShowPassConfirm.Location = new Point(295, 187);
            picShowPassConfirm.Name = "picShowPassConfirm";
            picShowPassConfirm.Size = new Size(24, 24);
            picShowPassConfirm.SizeMode = PictureBoxSizeMode.Zoom;
            picShowPassConfirm.TabIndex = 11;
            picShowPassConfirm.TabStop = false;
            picShowPassConfirm.Click += picShowPassConfirm_Click;
            // 
            // ForgotPasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(217, 244, 227);
            ClientSize = new Size(395, 342);
            Controls.Add(picShowPassConfirm);
            Controls.Add(picShowPassNew);
            Controls.Add(btnVerifyCode);
            Controls.Add(btnSendCode);
            Controls.Add(btnSave);
            Controls.Add(txtXacNhanMatKhauMoi);
            Controls.Add(txtMatKhauMoi);
            Controls.Add(txtSoDienThoai);
            Controls.Add(txtOTP);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ForgotPasswordForm";
            Text = "ForgotPasswordForm";
            ((System.ComponentModel.ISupportInitialize)picShowPassNew).EndInit();
            ((System.ComponentModel.ISupportInitialize)picShowPassConfirm).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // Khai báo các control
        private Components.RoundedButton btnSendCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Components.RoundedTextBox txtSoDienThoai;
        private Components.RoundedTextBox txtMatKhauMoi;
        private Components.RoundedTextBox txtOTP;
        private Components.RoundedTextBox txtXacNhanMatKhauMoi;
        private Components.RoundedButton btnVerifyCode;
        private Components.RoundedButton btnSave;
        private System.Windows.Forms.PictureBox picShowPassNew;
        private System.Windows.Forms.PictureBox picShowPassConfirm;
    }
}