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
            label3 = new Label();
            dgvParams = new Environmental_Monitoring.View.Components.RoundedDataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            cbbThongSo = new Environmental_Monitoring.View.Components.RoundedComboBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnThemThongSo = new Environmental_Monitoring.View.Components.RoundedButton();
            btnLuuMau = new Environmental_Monitoring.View.Components.RoundedButton();
            btnHuy = new Environmental_Monitoring.View.Components.RoundedButton();
            txtToaDo = new Environmental_Monitoring.View.Components.RoundedTextBox();
            label4 = new Label();
            label5 = new Label();
            txtKyHieu = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtViTri = new Environmental_Monitoring.View.Components.RoundedTextBox();
            cbbMoiTruong = new Environmental_Monitoring.View.Components.RoundedComboBox();
            txtNenMau = new Environmental_Monitoring.View.Components.RoundedTextBox();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvParams).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(412, 33);
            label1.Name = "label1";
            label1.Size = new Size(96, 20);
            label1.TabIndex = 0;
            label1.Text = "Tên Nền Mẫu";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(44, 99);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.TabIndex = 1;
            label2.Text = "Thông Số";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point(415, 89);
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
            tableLayoutPanel1.Controls.Add(label2, 1, 3);
            tableLayoutPanel1.Controls.Add(cbbThongSo, 3, 3);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 6);
            tableLayoutPanel1.Controls.Add(txtToaDo, 9, 3);
            tableLayoutPanel1.Controls.Add(label4, 8, 3);
            tableLayoutPanel1.Controls.Add(label5, 8, 1);
            tableLayoutPanel1.Controls.Add(txtKyHieu, 9, 1);
            tableLayoutPanel1.Controls.Add(txtViTri, 7, 3);
            tableLayoutPanel1.Controls.Add(label3, 5, 3);
            tableLayoutPanel1.Controls.Add(cbbMoiTruong, 3, 1);
            tableLayoutPanel1.Controls.Add(txtNenMau, 7, 1);
            tableLayoutPanel1.Controls.Add(label1, 5, 1);
            tableLayoutPanel1.Controls.Add(label6, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 2F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 2F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 58F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.Size = new Size(1000, 515);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // cbbThongSo
            // 
            cbbThongSo.Anchor = AnchorStyles.None;
            cbbThongSo.BackColor = Color.White;
            cbbThongSo.BorderRadius = 12;
            cbbThongSo.BorderThickness = 2;
            cbbThongSo.Cursor = Cursors.Hand;
            cbbThongSo.DataSource = null;
            cbbThongSo.DisplayMember = "";
            cbbThongSo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbThongSo.FocusBorderColor = Color.DimGray;
            cbbThongSo.HoverBorderColor = Color.DarkGray;
            cbbThongSo.Location = new Point(163, 87);
            cbbThongSo.Name = "cbbThongSo";
            cbbThongSo.NormalBorderColor = Color.Gray;
            cbbThongSo.SelectedIndex = -1;
            cbbThongSo.SelectedItem = null;
            cbbThongSo.SelectedValue = null;
            cbbThongSo.Size = new Size(214, 43);
            cbbThongSo.TabIndex = 16;
            cbbThongSo.ValueMember = "";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 9);
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.Controls.Add(btnThemThongSo, 1, 0);
            tableLayoutPanel2.Controls.Add(btnLuuMau, 2, 0);
            tableLayoutPanel2.Controls.Add(btnHuy, 3, 0);
            tableLayoutPanel2.Location = new Point(23, 448);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(954, 45);
            tableLayoutPanel2.TabIndex = 19;
            // 
            // btnThemThongSo
            // 
            btnThemThongSo.Anchor = AnchorStyles.None;
            btnThemThongSo.BackColor = Color.FromArgb(0, 0, 64);
            btnThemThongSo.BaseColor = Color.FromArgb(0, 0, 64);
            btnThemThongSo.BorderColor = Color.Transparent;
            btnThemThongSo.BorderRadius = 15;
            btnThemThongSo.BorderSize = 0;
            btnThemThongSo.Cursor = Cursors.Hand;
            btnThemThongSo.FlatAppearance.BorderSize = 0;
            btnThemThongSo.FlatStyle = FlatStyle.Flat;
            btnThemThongSo.ForeColor = Color.White;
            btnThemThongSo.HoverColor = Color.Navy;
            btnThemThongSo.Location = new Point(280, 3);
            btnThemThongSo.Name = "btnThemThongSo";
            btnThemThongSo.Size = new Size(154, 39);
            btnThemThongSo.TabIndex = 12;
            btnThemThongSo.Text = "Thêm Thông Số";
            btnThemThongSo.UseVisualStyleBackColor = false;
            btnThemThongSo.Click += btnThemThongSo_Click_1;
            // 
            // btnLuuMau
            // 
            btnLuuMau.Anchor = AnchorStyles.None;
            btnLuuMau.BackColor = Color.SeaGreen;
            btnLuuMau.BaseColor = Color.SeaGreen;
            btnLuuMau.BorderColor = Color.Transparent;
            btnLuuMau.BorderRadius = 15;
            btnLuuMau.BorderSize = 0;
            btnLuuMau.Cursor = Cursors.Hand;
            btnLuuMau.FlatAppearance.BorderSize = 0;
            btnLuuMau.FlatStyle = FlatStyle.Flat;
            btnLuuMau.ForeColor = Color.White;
            btnLuuMau.HoverColor = Color.FromArgb(34, 139, 34);
            btnLuuMau.Location = new Point(527, 3);
            btnLuuMau.Name = "btnLuuMau";
            btnLuuMau.Size = new Size(135, 39);
            btnLuuMau.TabIndex = 11;
            btnLuuMau.Text = "Lưu Mẫu";
            btnLuuMau.UseVisualStyleBackColor = false;
            // 
            // btnHuy
            // 
            btnHuy.Anchor = AnchorStyles.None;
            btnHuy.BackColor = Color.Gray;
            btnHuy.BaseColor = Color.Gray;
            btnHuy.BorderColor = Color.Transparent;
            btnHuy.BorderRadius = 15;
            btnHuy.BorderSize = 0;
            btnHuy.Cursor = Cursors.Hand;
            btnHuy.FlatAppearance.BorderSize = 0;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.ForeColor = Color.White;
            btnHuy.HoverColor = Color.DarkGray;
            btnHuy.Location = new Point(766, 3);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(135, 39);
            btnHuy.TabIndex = 13;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = false;
            // 
            // txtToaDo
            // 
            txtToaDo.Anchor = AnchorStyles.None;
            txtToaDo.BackColor = Color.White;
            txtToaDo.BorderRadius = 15;
            txtToaDo.BorderThickness = 2;
            txtToaDo.FocusBorderColor = Color.DimGray;
            txtToaDo.HoverBorderColor = Color.DarkGray;
            txtToaDo.Location = new Point(823, 87);
            txtToaDo.Multiline = false;
            txtToaDo.Name = "txtToaDo";
            txtToaDo.NormalBorderColor = Color.Gray;
            txtToaDo.Padding = new Padding(10);
            txtToaDo.PasswordChar = '\0';
            txtToaDo.PlaceholderText = "";
            txtToaDo.ReadOnly = false;
            txtToaDo.Size = new Size(154, 43);
            txtToaDo.TabIndex = 10;
            txtToaDo.UseSystemPasswordChar = false;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Location = new Point(732, 99);
            label4.Name = "label4";
            label4.Size = new Size(56, 20);
            label4.TabIndex = 6;
            label4.Text = "Tọa độ";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Location = new Point(730, 33);
            label5.Name = "label5";
            label5.Size = new Size(59, 20);
            label5.TabIndex = 17;
            label5.Text = "Ký Hiệu";
            // 
            // txtKyHieu
            // 
            txtKyHieu.Anchor = AnchorStyles.None;
            txtKyHieu.BackColor = Color.White;
            txtKyHieu.BorderRadius = 15;
            txtKyHieu.BorderThickness = 2;
            txtKyHieu.FocusBorderColor = Color.DimGray;
            txtKyHieu.HoverBorderColor = Color.DarkGray;
            txtKyHieu.Location = new Point(823, 21);
            txtKyHieu.Multiline = false;
            txtKyHieu.Name = "txtKyHieu";
            txtKyHieu.NormalBorderColor = Color.Gray;
            txtKyHieu.Padding = new Padding(10);
            txtKyHieu.PasswordChar = '\0';
            txtKyHieu.PlaceholderText = "";
            txtKyHieu.ReadOnly = false;
            txtKyHieu.Size = new Size(154, 43);
            txtKyHieu.TabIndex = 14;
            txtKyHieu.UseSystemPasswordChar = false;
            // 
            // txtViTri
            // 
            txtViTri.Anchor = AnchorStyles.None;
            txtViTri.BackColor = Color.White;
            txtViTri.BorderRadius = 15;
            txtViTri.BorderThickness = 2;
            txtViTri.FocusBorderColor = Color.DimGray;
            txtViTri.HoverBorderColor = Color.DarkGray;
            txtViTri.Location = new Point(543, 87);
            txtViTri.Multiline = false;
            txtViTri.Name = "txtViTri";
            txtViTri.NormalBorderColor = Color.Gray;
            txtViTri.Padding = new Padding(10);
            txtViTri.PasswordChar = '\0';
            txtViTri.PlaceholderText = "";
            txtViTri.ReadOnly = false;
            txtViTri.Size = new Size(154, 43);
            txtViTri.TabIndex = 7;
            txtViTri.UseSystemPasswordChar = false;
            // 
            // cbbMoiTruong
            // 
            cbbMoiTruong.Anchor = AnchorStyles.None;
            cbbMoiTruong.BackColor = Color.White;
            cbbMoiTruong.BorderRadius = 12;
            cbbMoiTruong.BorderThickness = 2;
            cbbMoiTruong.Cursor = Cursors.Hand;
            cbbMoiTruong.DataSource = null;
            cbbMoiTruong.DisplayMember = "";
            cbbMoiTruong.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbMoiTruong.FocusBorderColor = Color.DimGray;
            cbbMoiTruong.HoverBorderColor = Color.DarkGray;
            cbbMoiTruong.Location = new Point(163, 21);
            cbbMoiTruong.Name = "cbbMoiTruong";
            cbbMoiTruong.NormalBorderColor = Color.Gray;
            cbbMoiTruong.SelectedIndex = -1;
            cbbMoiTruong.SelectedItem = null;
            cbbMoiTruong.SelectedValue = null;
            cbbMoiTruong.Size = new Size(214, 43);
            cbbMoiTruong.TabIndex = 20;
            cbbMoiTruong.ValueMember = "";
            // 
            // txtNenMau
            // 
            txtNenMau.Anchor = AnchorStyles.None;
            txtNenMau.BackColor = Color.White;
            txtNenMau.BorderRadius = 15;
            txtNenMau.BorderThickness = 2;
            txtNenMau.FocusBorderColor = Color.DimGray;
            txtNenMau.HoverBorderColor = Color.DarkGray;
            txtNenMau.Location = new Point(543, 21);
            txtNenMau.Multiline = false;
            txtNenMau.Name = "txtNenMau";
            txtNenMau.NormalBorderColor = Color.Gray;
            txtNenMau.Padding = new Padding(10);
            txtNenMau.PasswordChar = '\0';
            txtNenMau.PlaceholderText = "";
            txtNenMau.ReadOnly = false;
            txtNenMau.Size = new Size(154, 43);
            txtNenMau.TabIndex = 21;
            txtNenMau.UseSystemPasswordChar = false;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Location = new Point(37, 33);
            label6.Name = "label6";
            label6.Size = new Size(86, 20);
            label6.TabIndex = 22;
            label6.Text = "Môi Trường";
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
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
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
        private TableLayoutPanel tableLayoutPanel2;
        private Components.RoundedComboBox cbbMoiTruong;
        private Components.RoundedTextBox txtNenMau;
        private Label label6;
    }
}