namespace Environmental_Monitoring.View.ContractContent
{
    partial class AddEditParameterForm
    {
        private System.ComponentModel.IContainer components = null;
        private NumericUpDown numGioiHanMin;
        private NumericUpDown numGioiHanMax;
        private ComboBox cbbPhuTrach;
        private Button btnOK;
        private Button btnCancel;
        private Label lblTenThongSo;
        private Label lblDonVi;
        private Label lblGioiHanMin;
        private Label lblGioiHanMax;
        private Label lblPhuTrach;

        private void InitializeComponent()
        {
            numGioiHanMin = new NumericUpDown();
            numGioiHanMax = new NumericUpDown();
            cbbPhuTrach = new ComboBox();
            btnOK = new Button();
            btnCancel = new Button();
            lblTenThongSo = new Label();
            lblDonVi = new Label();
            lblGioiHanMin = new Label();
            lblGioiHanMax = new Label();
            lblPhuTrach = new Label();
            txtTenThongSo = new Environmental_Monitoring.View.Components.RoundedTextBox();
            txtDonVi = new Environmental_Monitoring.View.Components.RoundedTextBox();
            ((System.ComponentModel.ISupportInitialize)numGioiHanMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numGioiHanMax).BeginInit();
            SuspendLayout();
            // 
            // numGioiHanMin
            // 
            numGioiHanMin.DecimalPlaces = 2;
            numGioiHanMin.Location = new Point(150, 108);
            numGioiHanMin.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numGioiHanMin.Name = "numGioiHanMin";
            numGioiHanMin.Size = new Size(200, 25);
            numGioiHanMin.TabIndex = 5;
            // 
            // numGioiHanMax
            // 
            numGioiHanMax.DecimalPlaces = 2;
            numGioiHanMax.Location = new Point(150, 148);
            numGioiHanMax.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numGioiHanMax.Name = "numGioiHanMax";
            numGioiHanMax.Size = new Size(200, 25);
            numGioiHanMax.TabIndex = 7;
            // 
            // cbbPhuTrach
            // 
            cbbPhuTrach.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbPhuTrach.Location = new Point(150, 188);
            cbbPhuTrach.Name = "cbbPhuTrach";
            cbbPhuTrach.Size = new Size(200, 25);
            cbbPhuTrach.TabIndex = 9;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(80, 230);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(85, 40);
            btnOK.TabIndex = 10;
            btnOK.Text = "OK";
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(200, 230);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(85, 40);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Hủy";
            btnCancel.Click += btnCancel_Click;
            // 
            // lblTenThongSo
            // 
            lblTenThongSo.Location = new Point(20, 20);
            lblTenThongSo.Name = "lblTenThongSo";
            lblTenThongSo.Size = new Size(100, 23);
            lblTenThongSo.TabIndex = 0;
            lblTenThongSo.Text = "Tên thông số";
            // 
            // lblDonVi
            // 
            lblDonVi.Location = new Point(20, 65);
            lblDonVi.Name = "lblDonVi";
            lblDonVi.Size = new Size(100, 23);
            lblDonVi.TabIndex = 2;
            lblDonVi.Text = "Đơn vị";
            // 
            // lblGioiHanMin
            // 
            lblGioiHanMin.Location = new Point(20, 108);
            lblGioiHanMin.Name = "lblGioiHanMin";
            lblGioiHanMin.Size = new Size(100, 23);
            lblGioiHanMin.TabIndex = 4;
            lblGioiHanMin.Text = "Giới hạn Min";
            // 
            // lblGioiHanMax
            // 
            lblGioiHanMax.Location = new Point(20, 150);
            lblGioiHanMax.Name = "lblGioiHanMax";
            lblGioiHanMax.Size = new Size(100, 23);
            lblGioiHanMax.TabIndex = 6;
            lblGioiHanMax.Text = "Giới hạn Max";
            // 
            // lblPhuTrach
            // 
            lblPhuTrach.Location = new Point(20, 193);
            lblPhuTrach.Name = "lblPhuTrach";
            lblPhuTrach.Size = new Size(100, 23);
            lblPhuTrach.TabIndex = 8;
            lblPhuTrach.Text = "Phụ trách";
            // 
            // txtTenThongSo
            // 
            txtTenThongSo.BorderRadius = 15;
            txtTenThongSo.BorderThickness = 2;
            txtTenThongSo.FocusBorderColor = Color.HotPink;
            txtTenThongSo.HoverBorderColor = Color.DodgerBlue;
            txtTenThongSo.Location = new Point(150, 9);
            txtTenThongSo.Multiline = false;
            txtTenThongSo.Name = "txtTenThongSo";
            txtTenThongSo.NormalBorderColor = Color.Gray;
            txtTenThongSo.Padding = new Padding(10);
            txtTenThongSo.PasswordChar = '\0';
            txtTenThongSo.PlaceholderText = "";
            txtTenThongSo.ReadOnly = false;
            txtTenThongSo.Size = new Size(200, 38);
            txtTenThongSo.TabIndex = 12;
            txtTenThongSo.UseSystemPasswordChar = false;
            // 
            // txtDonVi
            // 
            txtDonVi.BorderRadius = 15;
            txtDonVi.BorderThickness = 2;
            txtDonVi.FocusBorderColor = Color.HotPink;
            txtDonVi.HoverBorderColor = Color.DodgerBlue;
            txtDonVi.Location = new Point(150, 54);
            txtDonVi.Multiline = false;
            txtDonVi.Name = "txtDonVi";
            txtDonVi.NormalBorderColor = Color.Gray;
            txtDonVi.Padding = new Padding(10);
            txtDonVi.PasswordChar = '\0';
            txtDonVi.PlaceholderText = "";
            txtDonVi.ReadOnly = false;
            txtDonVi.Size = new Size(200, 40);
            txtDonVi.TabIndex = 13;
            txtDonVi.UseSystemPasswordChar = false;
            // 
            // AddEditParameterForm
            // 
            ClientSize = new Size(380, 280);
            Controls.Add(txtDonVi);
            Controls.Add(txtTenThongSo);
            Controls.Add(lblTenThongSo);
            Controls.Add(lblDonVi);
            Controls.Add(lblGioiHanMin);
            Controls.Add(numGioiHanMin);
            Controls.Add(lblGioiHanMax);
            Controls.Add(numGioiHanMax);
            Controls.Add(lblPhuTrach);
            Controls.Add(cbbPhuTrach);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "AddEditParameterForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thêm/Chỉnh sửa Thông Số";
            ((System.ComponentModel.ISupportInitialize)numGioiHanMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)numGioiHanMax).EndInit();
            ResumeLayout(false);
        }
        private Components.RoundedTextBox txtTenThongSo;
        private Components.RoundedTextBox txtDonVi;
    }
}
