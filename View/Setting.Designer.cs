using Environmental_Monitoring.View.Components;
using System.Windows.Forms;

namespace Environmental_Monitoring
{
    partial class Setting
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
            roundedPanel3 = new RoundedPanel();
            roundedButton1 = new RoundedButton();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            label2 = new Label();
            label10 = new Label();
            label13 = new Label();
            label15 = new Label();
            roundedPanel2 = new RoundedPanel();
            label12 = new Label();
            label8 = new Label();
            label7 = new Label();
            label5 = new Label();
            label6 = new Label();
            label4 = new Label();
            label3 = new Label();
            lblBaoCao = new Label();
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            roundedPanel3.SuspendLayout();
            roundedPanel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // roundedPanel3
            // 
            roundedPanel3.BackColor = Color.White;
            roundedPanel3.BorderColor = Color.Transparent;
            roundedPanel3.BorderRadius = 20;
            roundedPanel3.BorderSize = 0;
            roundedPanel3.Controls.Add(roundedButton1);
            roundedPanel3.Controls.Add(checkBox2);
            roundedPanel3.Controls.Add(checkBox1);
            roundedPanel3.Controls.Add(comboBox2);
            roundedPanel3.Controls.Add(comboBox1);
            roundedPanel3.Controls.Add(label2);
            roundedPanel3.Controls.Add(label10);
            roundedPanel3.Controls.Add(label13);
            roundedPanel3.Controls.Add(label15);
            roundedPanel3.Location = new Point(637, 164);
            roundedPanel3.Name = "roundedPanel3";
            roundedPanel3.Size = new Size(519, 504);
            roundedPanel3.TabIndex = 17;
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.FromArgb(0, 113, 0);
            roundedButton1.BaseColor = Color.FromArgb(0, 113, 0);
            roundedButton1.BorderColor = Color.Transparent;
            roundedButton1.BorderRadius = 15;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            roundedButton1.ForeColor = Color.White;
            roundedButton1.HoverColor = Color.FromArgb(34, 139, 34);
            roundedButton1.Location = new Point(307, 370);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(148, 37);
            roundedButton1.TabIndex = 21;
            roundedButton1.Text = "Lưu Cài Đặt";
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(102, 380);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(99, 24);
            checkBox2.TabIndex = 20;
            checkBox2.Text = "Ứng Dụng";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(102, 336);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(68, 24);
            checkBox1.TabIndex = 19;
            checkBox1.Text = "Email";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(102, 230);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(151, 28);
            comboBox2.TabIndex = 18;
            comboBox2.Text = "Sáng ";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(102, 125);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 17;
            comboBox1.Text = "Tiếng Việt";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            label2.Location = new Point(147, 21);
            label2.Name = "label2";
            label2.Size = new Size(248, 38);
            label2.TabIndex = 16;
            label2.Text = "Cài Đặt Hệ Thống";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label10.Location = new Point(60, 284);
            label10.Name = "label10";
            label10.Size = new Size(130, 31);
            label10.TabIndex = 5;
            label10.Text = "Thông Báo";
            label10.Click += label10_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label13.Location = new Point(60, 182);
            label13.Name = "label13";
            label13.Size = new Size(202, 31);
            label13.TabIndex = 3;
            label13.Text = "Chủ Đề Giao Diện";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label15.Location = new Point(60, 77);
            label15.Name = "label15";
            label15.Size = new Size(127, 31);
            label15.TabIndex = 1;
            label15.Text = "Ngôn Ngữ";
            // 
            // roundedPanel2
            // 
            roundedPanel2.BackColor = Color.White;
            roundedPanel2.BorderColor = Color.Transparent;
            roundedPanel2.BorderRadius = 20;
            roundedPanel2.BorderSize = 0;
            roundedPanel2.Controls.Add(label12);
            roundedPanel2.Controls.Add(label8);
            roundedPanel2.Controls.Add(label7);
            roundedPanel2.Controls.Add(label5);
            roundedPanel2.Controls.Add(label6);
            roundedPanel2.Controls.Add(label4);
            roundedPanel2.Controls.Add(label3);
            roundedPanel2.Location = new Point(64, 164);
            roundedPanel2.Name = "roundedPanel2";
            roundedPanel2.Size = new Size(519, 504);
            roundedPanel2.TabIndex = 8;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            label12.Location = new Point(109, 21);
            label12.Name = "label12";
            label12.Size = new Size(278, 38);
            label12.TabIndex = 16;
            label12.Text = "Hỗ Trợ Người Dùng";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label8.Location = new Point(60, 359);
            label8.Name = "label8";
            label8.Size = new Size(249, 31);
            label8.TabIndex = 6;
            label8.Text = "Email: QTMT@vn.com";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label7.Location = new Point(60, 290);
            label7.Name = "label7";
            label7.Size = new Size(229, 31);
            label7.TabIndex = 5;
            label7.Text = "HotLine: 012345678";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label5.ForeColor = Color.Blue;
            label5.Location = new Point(93, 229);
            label5.Name = "label5";
            label5.Size = new Size(190, 25);
            label5.TabIndex = 4;
            label5.Text = "Câu Hỏi Thường Gặp";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label6.Location = new Point(60, 182);
            label6.Name = "label6";
            label6.Size = new Size(58, 31);
            label6.TabIndex = 3;
            label6.Text = "FAQ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label4.ForeColor = Color.Blue;
            label4.Location = new Point(93, 131);
            label4.Name = "label4";
            label4.Size = new Size(119, 25);
            label4.TabIndex = 2;
            label4.Text = "Xem Tài Liệu";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label3.Location = new Point(60, 84);
            label3.Name = "label3";
            label3.Size = new Size(237, 31);
            label3.TabIndex = 1;
            label3.Text = "Hướng Dẫn Sử Dụng";
            label3.Click += label3_Click;
            // 
            // lblBaoCao
            // 
            lblBaoCao.AutoSize = true;
            lblBaoCao.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblBaoCao.Location = new Point(64, 21);
            lblBaoCao.Name = "lblBaoCao";
            lblBaoCao.Size = new Size(327, 50);
            lblBaoCao.TabIndex = 5;
            lblBaoCao.Text = "Hỗ Trợ Và Cài Đặt";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            label1.Location = new Point(93, 14);
            label1.Name = "label1";
            label1.Size = new Size(127, 38);
            label1.TabIndex = 0;
            label1.Text = "Trợ giúp";
            label1.Click += label1_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(217, 244, 227);
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Controls.Add(roundedPanel2, 1, 3);
            tableLayoutPanel1.Controls.Add(roundedPanel3, 3, 3);
            tableLayoutPanel1.Controls.Add(lblBaoCao, 1, 1);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 72F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Size = new Size(1221, 709);
            tableLayoutPanel1.TabIndex = 18;
            // 
            // Setting
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "Setting";
            Size = new Size(1229, 720);
            roundedPanel3.ResumeLayout(false);
            roundedPanel3.PerformLayout();
            roundedPanel2.ResumeLayout(false);
            roundedPanel2.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private RoundedPanel roundedPanel2;
        private Label label1;

        private Label lblBaoCao;
        private Label label4;
        private Label label3;
        private Label label8;
        private Label label7;
        private Label label5;
        private Label label6;
        private Label label12;
        private RoundedPanel roundedPanel3;
        private ComboBox comboBox1;
        private Label label2;
        private Label label10;
        private Label label13;
        private Label label15;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private ComboBox comboBox2;
        private RoundedButton roundedButton1;
        private TableLayoutPanel tableLayoutPanel1;
    }
}