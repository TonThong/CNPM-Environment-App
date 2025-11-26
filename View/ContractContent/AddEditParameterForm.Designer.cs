namespace Environmental_Monitoring.View.ContractContent
{
    partial class AddEditParameterForm
    {
        private System.ComponentModel.IContainer components = null;
        private NumericUpDown numGioiHanMin;
        private NumericUpDown numGioiHanMax;
        private Label lblTenThongSo;
        private Label lblDonVi;
        private Label lblGioiHanMin;
        private Label lblGioiHanMax;
        private Label lblPhuTrach;

        private void InitializeComponent()
        {
            numGioiHanMin = new NumericUpDown();
            numGioiHanMax = new NumericUpDown();
            lblTenThongSo = new Label();
            lblDonVi = new Label();
            lblGioiHanMin = new Label();
            lblGioiHanMax = new Label();
            lblPhuTrach = new Label();
            txtTenThongSo = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtDonVi = new Environmental_Monitoring.View.Components.RoundedTextBox();
            btnOK = new Environmental_Monitoring.View.Components.RoundedButton();
            btnCancel = new Environmental_Monitoring.View.Components.RoundedButton();
            cbbPhuTrach = new Environmental_Monitoring.View.Components.RoundedComboBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblTemplate = new Label();
            txtPhuongPhap = new Environmental_Monitoring.View.Components.RoundedTextBox();
            ((System.ComponentModel.ISupportInitialize)numGioiHanMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numGioiHanMax).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // numGioiHanMin
            // 
            numGioiHanMin.Anchor = AnchorStyles.Left;
            tableLayoutPanel1.SetColumnSpan(numGioiHanMin, 2);
            numGioiHanMin.DecimalPlaces = 2;
            numGioiHanMin.Location = new Point(190, 145);
            numGioiHanMin.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numGioiHanMin.Name = "numGioiHanMin";
            numGioiHanMin.Size = new Size(254, 25);
            numGioiHanMin.TabIndex = 5;
            // 
            // numGioiHanMax
            // 
            numGioiHanMax.Anchor = AnchorStyles.Left;
            tableLayoutPanel1.SetColumnSpan(numGioiHanMax, 2);
            numGioiHanMax.DecimalPlaces = 2;
            numGioiHanMax.Location = new Point(190, 202);
            numGioiHanMax.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numGioiHanMax.Name = "numGioiHanMax";
            numGioiHanMax.Size = new Size(254, 25);
            numGioiHanMax.TabIndex = 7;
            // 
            // lblTenThongSo
            // 
            lblTenThongSo.Dock = DockStyle.Fill;
            lblTenThongSo.Location = new Point(26, 22);
            lblTenThongSo.Name = "lblTenThongSo";
            lblTenThongSo.Size = new Size(135, 44);
            lblTenThongSo.TabIndex = 0;
            lblTenThongSo.Text = "Tên thông số";
            lblTenThongSo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDonVi
            // 
            lblDonVi.Dock = DockStyle.Fill;
            lblDonVi.Location = new Point(26, 79);
            lblDonVi.Name = "lblDonVi";
            lblDonVi.Size = new Size(135, 44);
            lblDonVi.TabIndex = 2;
            lblDonVi.Text = "Đơn vị";
            lblDonVi.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGioiHanMin
            // 
            lblGioiHanMin.Dock = DockStyle.Fill;
            lblGioiHanMin.Location = new Point(26, 136);
            lblGioiHanMin.Name = "lblGioiHanMin";
            lblGioiHanMin.Size = new Size(135, 44);
            lblGioiHanMin.TabIndex = 4;
            lblGioiHanMin.Text = "Giới hạn Min";
            lblGioiHanMin.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGioiHanMax
            // 
            lblGioiHanMax.Dock = DockStyle.Fill;
            lblGioiHanMax.Location = new Point(26, 193);
            lblGioiHanMax.Name = "lblGioiHanMax";
            lblGioiHanMax.Size = new Size(135, 44);
            lblGioiHanMax.TabIndex = 6;
            lblGioiHanMax.Text = "Giới hạn Max";
            lblGioiHanMax.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPhuTrach
            // 
            lblPhuTrach.Dock = DockStyle.Fill;
            lblPhuTrach.Location = new Point(26, 250);
            lblPhuTrach.Name = "lblPhuTrach";
            lblPhuTrach.Size = new Size(135, 44);
            lblPhuTrach.TabIndex = 8;
            lblPhuTrach.Text = "Phụ trách";
            lblPhuTrach.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtTenThongSo
            // 
            txtTenThongSo.BorderRadius = 15;
            txtTenThongSo.BorderThickness = 2;
            tableLayoutPanel1.SetColumnSpan(txtTenThongSo, 2);
            txtTenThongSo.Dock = DockStyle.Fill;
            txtTenThongSo.FocusBorderColor = Color.DimGray;
            txtTenThongSo.HoverBorderColor = Color.DarkGray;
            txtTenThongSo.Location = new Point(190, 25);
            txtTenThongSo.Multiline = false;
            txtTenThongSo.Name = "txtTenThongSo";
            txtTenThongSo.NormalBorderColor = Color.Gray;
            txtTenThongSo.Padding = new Padding(10);
            txtTenThongSo.PasswordChar = '\0';
            txtTenThongSo.PlaceholderText = "";
            txtTenThongSo.ReadOnly = false;
            txtTenThongSo.Size = new Size(254, 38);
            txtTenThongSo.TabIndex = 12;
            txtTenThongSo.UseSystemPasswordChar = false;
            // 
            // txtDonVi
            // 
            txtDonVi.BorderRadius = 15;
            txtDonVi.BorderThickness = 2;
            tableLayoutPanel1.SetColumnSpan(txtDonVi, 2);
            txtDonVi.Dock = DockStyle.Fill;
            txtDonVi.FocusBorderColor = Color.DimGray;
            txtDonVi.HoverBorderColor = Color.DarkGray;
            txtDonVi.Location = new Point(190, 82);
            txtDonVi.Multiline = false;
            txtDonVi.Name = "txtDonVi";
            txtDonVi.NormalBorderColor = Color.Gray;
            txtDonVi.Padding = new Padding(10);
            txtDonVi.PasswordChar = '\0';
            txtDonVi.PlaceholderText = "";
            txtDonVi.ReadOnly = false;
            txtDonVi.Size = new Size(254, 38);
            txtDonVi.TabIndex = 13;
            txtDonVi.UseSystemPasswordChar = false;
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.FromArgb(0, 113, 0);
            btnOK.BaseColor = Color.FromArgb(0, 113, 0);
            btnOK.BorderColor = Color.Transparent;
            btnOK.BorderRadius = 8;
            btnOK.BorderSize = 0;
            btnOK.Cursor = Cursors.Hand;
            btnOK.Dock = DockStyle.Fill;
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.ForeColor = Color.White;
            btnOK.HoverColor = Color.FromArgb(34, 139, 34);
            btnOK.Location = new Point(190, 376);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(124, 38);
            btnOK.TabIndex = 14;
            btnOK.Text = "Lưu";
            btnOK.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Silver;
            btnCancel.BaseColor = Color.Silver;
            btnCancel.BorderColor = Color.Transparent;
            btnCancel.BorderRadius = 8;
            btnCancel.BorderSize = 0;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Dock = DockStyle.Fill;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.HoverColor = Color.DarkGray;
            btnCancel.Location = new Point(320, 376);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(124, 38);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // cbbPhuTrach
            // 
            cbbPhuTrach.BackColor = Color.White;
            cbbPhuTrach.BorderRadius = 12;
            cbbPhuTrach.BorderThickness = 2;
            tableLayoutPanel1.SetColumnSpan(cbbPhuTrach, 2);
            cbbPhuTrach.Cursor = Cursors.Hand;
            cbbPhuTrach.DataSource = null;
            cbbPhuTrach.DisplayMember = "";
            cbbPhuTrach.Dock = DockStyle.Fill;
            cbbPhuTrach.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbPhuTrach.FocusBorderColor = Color.DimGray;
            cbbPhuTrach.HoverBorderColor = Color.DarkGray;
            cbbPhuTrach.Location = new Point(190, 253);
            cbbPhuTrach.Name = "cbbPhuTrach";
            cbbPhuTrach.NormalBorderColor = Color.Gray;
            cbbPhuTrach.SelectedIndex = -1;
            cbbPhuTrach.SelectedItem = null;
            cbbPhuTrach.SelectedValue = null;
            cbbPhuTrach.Size = new Size(254, 38);
            cbbPhuTrach.TabIndex = 17;
            cbbPhuTrach.ValueMember = "";
            //cbbPhuTrach.SelectedIndexChanged += cbbPhuTrach_SelectedIndexChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Controls.Add(lblTenThongSo, 1, 1);
            tableLayoutPanel1.Controls.Add(lblDonVi, 1, 3);
            tableLayoutPanel1.Controls.Add(cbbPhuTrach, 3, 9);
            tableLayoutPanel1.Controls.Add(lblGioiHanMin, 1, 5);
            tableLayoutPanel1.Controls.Add(lblGioiHanMax, 1, 7);
            tableLayoutPanel1.Controls.Add(lblPhuTrach, 1, 9);
            tableLayoutPanel1.Controls.Add(numGioiHanMax, 3, 7);
            tableLayoutPanel1.Controls.Add(numGioiHanMin, 3, 5);
            tableLayoutPanel1.Controls.Add(txtDonVi, 3, 3);
            tableLayoutPanel1.Controls.Add(lblTemplate, 1, 11);
            tableLayoutPanel1.Controls.Add(txtTenThongSo, 3, 1);
            tableLayoutPanel1.Controls.Add(btnOK, 3, 13);
            tableLayoutPanel1.Controls.Add(btnCancel, 4, 13);
            tableLayoutPanel1.Controls.Add(txtPhuongPhap, 3, 11);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 15;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Size = new Size(473, 442);
            tableLayoutPanel1.TabIndex = 19;
            // 
            // lblTemplate
            // 
            lblTemplate.Dock = DockStyle.Fill;
            lblTemplate.Location = new Point(26, 307);
            lblTemplate.Name = "lblTemplate";
            lblTemplate.Size = new Size(135, 44);
            lblTemplate.TabIndex = 16;
            lblTemplate.Text = "Phương Pháp";
            lblTemplate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPhuongPhap
            // 
            txtPhuongPhap.BorderRadius = 15;
            txtPhuongPhap.BorderThickness = 2;
            tableLayoutPanel1.SetColumnSpan(txtPhuongPhap, 2);
            txtPhuongPhap.FocusBorderColor = Color.HotPink;
            txtPhuongPhap.HoverBorderColor = Color.DodgerBlue;
            txtPhuongPhap.Location = new Point(190, 310);
            txtPhuongPhap.Multiline = false;
            txtPhuongPhap.Name = "txtPhuongPhap";
            txtPhuongPhap.NormalBorderColor = Color.Gray;
            txtPhuongPhap.Padding = new Padding(10);
            txtPhuongPhap.PasswordChar = '\0';
            txtPhuongPhap.PlaceholderText = "";
            txtPhuongPhap.ReadOnly = false;
            txtPhuongPhap.Size = new Size(254, 38);
            txtPhuongPhap.TabIndex = 18;
            txtPhuongPhap.UseSystemPasswordChar = false;
            // 
            // AddEditParameterForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(473, 442);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "AddEditParameterForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thêm/Chỉnh sửa Thông Số";
            ((System.ComponentModel.ISupportInitialize)numGioiHanMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)numGioiHanMax).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }
        private Components.RoundedTextBox txtTenThongSo;
        private Components.RoundedTextBox txtDonVi;
        private Components.RoundedButton btnOK;
        private Components.RoundedButton btnCancel;
        private Components.RoundedComboBox cbbPhuTrach;
        private TableLayoutPanel tableLayoutPanel1;
        private Label lblTemplate;
        private Components.RoundedTextBox txtPhuongPhap;
    }
}
