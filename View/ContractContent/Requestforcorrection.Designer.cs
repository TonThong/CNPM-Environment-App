namespace Environmental_Monitoring.View.ContractContent
{
    partial class Requestforcorrection
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
            radHienTruong = new RadioButton();
            radThiNghiem = new RadioButton();
            btnSave = new Environmental_Monitoring.View.Components.RoundedButton();
            btnCancel = new Environmental_Monitoring.View.Components.RoundedButton();
            label1 = new Label();
            SuspendLayout();
            // 
            // radHienTruong
            // 
            radHienTruong.AutoSize = true;
            radHienTruong.Font = new Font("Segoe UI", 12F);
            radHienTruong.Location = new Point(68, 77);
            radHienTruong.Name = "radHienTruong";
            radHienTruong.Size = new Size(201, 32);
            radHienTruong.TabIndex = 0;
            radHienTruong.TabStop = true;
            radHienTruong.Text = "Phòng Hiện trường";
            radHienTruong.UseVisualStyleBackColor = true;
            // 
            // radThiNghiem
            // 
            radThiNghiem.AutoSize = true;
            radThiNghiem.Font = new Font("Segoe UI", 12F);
            radThiNghiem.Location = new Point(68, 129);
            radThiNghiem.Name = "radThiNghiem";
            radThiNghiem.Size = new Size(192, 32);
            radThiNghiem.TabIndex = 1;
            radThiNghiem.TabStop = true;
            radThiNghiem.Text = "Phòng Thí nghiệm";
            radThiNghiem.UseVisualStyleBackColor = true;
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
            btnSave.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.HoverColor = Color.FromArgb(34, 139, 34);
            btnSave.Location = new Point(68, 202);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 40);
            btnSave.TabIndex = 2;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.DarkGray;
            btnCancel.BaseColor = Color.DarkGray;
            btnCancel.BorderColor = Color.Transparent;
            btnCancel.BorderRadius = 10;
            btnCancel.BorderSize = 0;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.HoverColor = Color.Gray;
            btnCancel.Location = new Point(208, 202);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 40);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.Location = new Point(48, 9);
            label1.Name = "label1";
            label1.Size = new Size(303, 28);
            label1.TabIndex = 4;
            label1.Text = "Chọn phòng ban cần chỉnh sửa";
            // 
            // Requestforcorrection
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(382, 253);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(radThiNghiem);
            Controls.Add(radHienTruong);
            Name = "Requestforcorrection";
            Text = "Requestforcorrection";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton radHienTruong;
        private RadioButton radThiNghiem;
        private Components.RoundedButton btnSave;
        private Components.RoundedButton btnCancel;
        private Label label1;
    }
}