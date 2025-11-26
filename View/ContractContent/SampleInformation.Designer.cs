namespace Environmental_Monitoring.View.ContractContent
{
    partial class SampleInformation
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
            txtNenMau = new Environmental_Monitoring.View.Components.RoundedTextBox();
            label3 = new Label();
            dgvParams = new Environmental_Monitoring.View.Components.RoundedDataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnLuuMau = new Environmental_Monitoring.View.Components.RoundedButton();
            btnHuy = new Environmental_Monitoring.View.Components.RoundedButton();
            cbbThongSo = new Environmental_Monitoring.View.Components.RoundedComboBox();
            label4 = new Label();
            txtToaDo = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtViTri = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtKyHieu = new Environmental_Monitoring.View.Components.RoundedTextBox();
            label5 = new Label();
            btnThemThongSo = new Environmental_Monitoring.View.Components.RoundedButton();
            ((System.ComponentModel.ISupportInitialize)dgvParams).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(32, 30);
            label1.Name = "label1";
            label1.Size = new Size(96, 20);
            label1.TabIndex = 0;
            label1.Text = "Tên Nền Mẫu";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(44, 96);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.TabIndex = 1;
            label2.Text = "Thông Số";
            // 
            // txtNenMau
            // 
            txtNenMau.BorderRadius = 15;
            txtNenMau.BorderThickness = 2;
            txtNenMau.FocusBorderColor = Color.HotPink;
            txtNenMau.HoverBorderColor = Color.DodgerBlue;
            txtNenMau.Location = new Point(163, 18);
            txtNenMau.Multiline = false;
            txtNenMau.Name = "txtNenMau";
            txtNenMau.NormalBorderColor = Color.Gray;
            txtNenMau.Padding = new Padding(10);
            txtNenMau.PasswordChar = '\0';
            txtNenMau.PlaceholderText = "";
            txtNenMau.ReadOnly = false;
            txtNenMau.Size = new Size(214, 45);
            txtNenMau.TabIndex = 2;
            txtNenMau.UseSystemPasswordChar = false;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point(415, 20);
            label3.Name = "label3";
            label3.Size = new Size(89, 40);
            label3.TabIndex = 4;
            label3.Text = " Vị Trí Quan Trắc";
            // 
            // dgvParams
            // 
            dgvParams.BackgroundColor = Color.White;
            dgvParams.BorderRadius = 20;
            dgvParams.BorderStyle = BorderStyle.None;
            dgvParams.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvParams.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel1.SetColumnSpan(dgvParams, 9);
            dgvParams.EnableHeadersVisualStyles = false;
            dgvParams.Location = new Point(23, 150);
            dgvParams.Name = "dgvParams";
            dgvParams.RowHeadersWidth = 51;
            dgvParams.Size = new Size(954, 291);
            dgvParams.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 11;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel1.Controls.Add(dgvParams, 1, 5);
            tableLayoutPanel1.Controls.Add(txtNenMau, 3, 1);
            tableLayoutPanel1.Controls.Add(label1, 1, 1);
            tableLayoutPanel1.Controls.Add(label2, 1, 3);
            tableLayoutPanel1.Controls.Add(btnHuy, 9, 6);
            tableLayoutPanel1.Controls.Add(cbbThongSo, 3, 3);
            tableLayoutPanel1.Controls.Add(label4, 5, 3);
            tableLayoutPanel1.Controls.Add(label3, 5, 1);
            tableLayoutPanel1.Controls.Add(txtToaDo, 7, 3);
            tableLayoutPanel1.Controls.Add(txtViTri, 7, 1);
            tableLayoutPanel1.Controls.Add(txtKyHieu, 9, 1);
            tableLayoutPanel1.Controls.Add(label5, 8, 1);
            tableLayoutPanel1.Controls.Add(btnThemThongSo, 9, 3);
            tableLayoutPanel1.Controls.Add(btnLuuMau, 7, 6);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 58F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.Size = new Size(1000, 515);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // btnLuuMau
            // 
            btnLuuMau.Anchor = AnchorStyles.Right;
            btnLuuMau.BackColor = Color.SeaGreen;
            btnLuuMau.BaseColor = Color.SeaGreen;
            btnLuuMau.BorderColor = Color.Transparent;
            btnLuuMau.BorderRadius = 15;
            btnLuuMau.BorderSize = 0;
            btnLuuMau.FlatAppearance.BorderSize = 0;
            btnLuuMau.FlatStyle = FlatStyle.Flat;
            btnLuuMau.ForeColor = Color.White;
            btnLuuMau.HoverColor = Color.FromArgb(34, 139, 34);
            btnLuuMau.Location = new Point(563, 448);
            btnLuuMau.Name = "btnLuuMau";
            btnLuuMau.Size = new Size(134, 45);
            btnLuuMau.TabIndex = 11;
            btnLuuMau.Text = "Lưu Mẫu";
            btnLuuMau.UseVisualStyleBackColor = false;
            // 
            // btnHuy
            // 
            btnHuy.Anchor = AnchorStyles.Left;
            btnHuy.BackColor = Color.Gray;
            btnHuy.BaseColor = Color.Gray;
            btnHuy.BorderColor = Color.Transparent;
            btnHuy.BorderRadius = 15;
            btnHuy.BorderSize = 0;
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.ForeColor = Color.White;
            btnHuy.HoverColor = Color.DarkGray;
            btnHuy.Location = new Point(823, 448);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(134, 45);
            btnHuy.TabIndex = 13;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            // 
            // cbbThongSo
            // 
            cbbThongSo.BorderRadius = 12;
            cbbThongSo.BorderThickness = 2;
            cbbThongSo.DataSource = null;
            cbbThongSo.DisplayMember = "";
            cbbThongSo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbThongSo.FocusBorderColor = Color.FromArgb(52, 168, 83);
            cbbThongSo.HoverBorderColor = Color.FromArgb(52, 168, 83);
            cbbThongSo.Location = new Point(163, 84);
            cbbThongSo.Name = "cbbThongSo";
            cbbThongSo.NormalBorderColor = Color.LightGray;
            cbbThongSo.SelectedIndex = -1;
            cbbThongSo.SelectedItem = null;
            cbbThongSo.SelectedValue = null;
            cbbThongSo.Size = new Size(214, 45);
            cbbThongSo.TabIndex = 16;
            cbbThongSo.ValueMember = "";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Location = new Point(432, 96);
            label4.Name = "label4";
            label4.Size = new Size(56, 20);
            label4.TabIndex = 6;
            label4.Text = "Tọa độ";
            // 
            // txtToaDo
            // 
            txtToaDo.BorderRadius = 15;
            txtToaDo.BorderThickness = 2;
            txtToaDo.FocusBorderColor = Color.HotPink;
            txtToaDo.HoverBorderColor = Color.DodgerBlue;
            txtToaDo.Location = new Point(543, 84);
            txtToaDo.Multiline = false;
            txtToaDo.Name = "txtToaDo";
            txtToaDo.NormalBorderColor = Color.Gray;
            txtToaDo.Padding = new Padding(10);
            txtToaDo.PasswordChar = '\0';
            txtToaDo.PlaceholderText = "";
            txtToaDo.ReadOnly = false;
            txtToaDo.Size = new Size(154, 45);
            txtToaDo.TabIndex = 10;
            txtToaDo.UseSystemPasswordChar = false;
            // 
            // txtViTri
            // 
            txtViTri.BorderRadius = 15;
            txtViTri.BorderThickness = 2;
            txtViTri.FocusBorderColor = Color.HotPink;
            txtViTri.HoverBorderColor = Color.DodgerBlue;
            txtViTri.Location = new Point(543, 18);
            txtViTri.Multiline = false;
            txtViTri.Name = "txtViTri";
            txtViTri.NormalBorderColor = Color.Gray;
            txtViTri.Padding = new Padding(10);
            txtViTri.PasswordChar = '\0';
            txtViTri.PlaceholderText = "";
            txtViTri.ReadOnly = false;
            txtViTri.Size = new Size(154, 45);
            txtViTri.TabIndex = 7;
            txtViTri.UseSystemPasswordChar = false;
            // 
            // txtKyHieu
            // 
            txtKyHieu.BorderRadius = 15;
            txtKyHieu.BorderThickness = 2;
            txtKyHieu.FocusBorderColor = Color.HotPink;
            txtKyHieu.HoverBorderColor = Color.DodgerBlue;
            txtKyHieu.Location = new Point(823, 18);
            txtKyHieu.Multiline = false;
            txtKyHieu.Name = "txtKyHieu";
            txtKyHieu.NormalBorderColor = Color.Gray;
            txtKyHieu.Padding = new Padding(10);
            txtKyHieu.PasswordChar = '\0';
            txtKyHieu.PlaceholderText = "";
            txtKyHieu.ReadOnly = false;
            txtKyHieu.Size = new Size(154, 45);
            txtKyHieu.TabIndex = 14;
            txtKyHieu.UseSystemPasswordChar = false;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Location = new Point(730, 30);
            label5.Name = "label5";
            label5.Size = new Size(59, 20);
            label5.TabIndex = 17;
            label5.Text = "Ký Hiệu";
            // 
            // btnThemThongSo
            // 
            btnThemThongSo.Anchor = AnchorStyles.None;
            btnThemThongSo.BackColor = Color.FromArgb(0, 0, 64);
            btnThemThongSo.BaseColor = Color.FromArgb(0, 0, 64);
            btnThemThongSo.BorderColor = Color.Transparent;
            btnThemThongSo.BorderRadius = 15;
            btnThemThongSo.BorderSize = 0;
            btnThemThongSo.FlatAppearance.BorderSize = 0;
            btnThemThongSo.FlatStyle = FlatStyle.Flat;
            btnThemThongSo.ForeColor = Color.White;
            btnThemThongSo.HoverColor = Color.FromArgb(34, 139, 34);
            btnThemThongSo.Location = new Point(823, 84);
            btnThemThongSo.Name = "btnThemThongSo";
            btnThemThongSo.Size = new Size(154, 45);
            btnThemThongSo.TabIndex = 12;
            btnThemThongSo.Text = "Thêm Thông Số";
            btnThemThongSo.UseVisualStyleBackColor = false;
            btnThemThongSo.Click += btnThemThongSo_Click_1;
            // 
            // SampleInformation
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 515);
            Controls.Add(tableLayoutPanel1);
            Name = "SampleInformation";
            Text = "SampleInformation";
            ((System.ComponentModel.ISupportInitialize)dgvParams).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Components.RoundedTextBox txtNenMau;
        private Label label3;
        private Components.RoundedDataGridView dgvParams;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label4;
        private Components.RoundedTextBox txtViTri;
        private Components.RoundedTextBox txtToaDo;
        private Components.RoundedButton btnHuy;
        private Components.RoundedButton btnLuuMau;
        private Components.RoundedButton btnThemThongSo;
        private Components.RoundedTextBox txtKyHieu;
        private Components.RoundedComboBox cbbThongSo;
        private Label label5;
    }
}