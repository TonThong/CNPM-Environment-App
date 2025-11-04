namespace Environmental_Monitoring.View.ContractContent
{
    partial class AddEditParameterForm
    {
        private System.ComponentModel.IContainer components = null;
        private Environmental_Monitoring.View.Components.RoundedTextBox txtTenThongSo;
        private Environmental_Monitoring.View.Components.RoundedTextBox txtDonVi;
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
            this.txtTenThongSo = new Environmental_Monitoring.View.Components.RoundedTextBox();
            this.txtDonVi = new Environmental_Monitoring.View.Components.RoundedTextBox();
            this.numGioiHanMin = new NumericUpDown();
            this.numGioiHanMax = new NumericUpDown();
            this.cbbPhuTrach = new ComboBox();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.lblTenThongSo = new Label();
            this.lblDonVi = new Label();
            this.lblGioiHanMin = new Label();
            this.lblGioiHanMax = new Label();
            this.lblPhuTrach = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.numGioiHanMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGioiHanMax)).BeginInit();
            this.SuspendLayout();
            // 
            // Labels and controls
            // 
            this.lblTenThongSo.Text = "Tên thông s?";
            this.lblTenThongSo.Location = new System.Drawing.Point(20, 20);
            this.txtTenThongSo.Location = new System.Drawing.Point(150, 20);
            this.txtTenThongSo.Size = new System.Drawing.Size(200, 30);

            this.lblDonVi.Text = "??n v?";
            this.lblDonVi.Location = new System.Drawing.Point(20, 60);
            this.txtDonVi.Location = new System.Drawing.Point(150, 60);
            this.txtDonVi.Size = new System.Drawing.Size(200, 30);

            this.lblGioiHanMin.Text = "Gi?i h?n Min";
            this.lblGioiHanMin.Location = new System.Drawing.Point(20, 100);
            this.numGioiHanMin.Location = new System.Drawing.Point(150, 100);
            this.numGioiHanMin.Size = new System.Drawing.Size(200, 30);
            this.numGioiHanMin.DecimalPlaces = 2;
            this.numGioiHanMin.Maximum = 1000000;

            this.lblGioiHanMax.Text = "Gi?i h?n Max";
            this.lblGioiHanMax.Location = new System.Drawing.Point(20, 140);
            this.numGioiHanMax.Location = new System.Drawing.Point(150, 140);
            this.numGioiHanMax.Size = new System.Drawing.Size(200, 30);
            this.numGioiHanMax.DecimalPlaces = 2;
            this.numGioiHanMax.Maximum = 1000000;

            this.lblPhuTrach.Text = "Ph? trách";
            this.lblPhuTrach.Location = new System.Drawing.Point(20, 180);
            this.cbbPhuTrach.Location = new System.Drawing.Point(150, 180);
            this.cbbPhuTrach.Size = new System.Drawing.Size(200, 30);
            this.cbbPhuTrach.DropDownStyle = ComboBoxStyle.DropDownList;

            this.btnOK.Text = "OK";
            this.btnOK.Location = new System.Drawing.Point(80, 230);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnCancel.Text = "H?y";
            this.btnCancel.Location = new System.Drawing.Point(200, 230);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.ClientSize = new System.Drawing.Size(380, 280);
            this.Controls.Add(this.lblTenThongSo);
            this.Controls.Add(this.txtTenThongSo);
            this.Controls.Add(this.lblDonVi);
            this.Controls.Add(this.txtDonVi);
            this.Controls.Add(this.lblGioiHanMin);
            this.Controls.Add(this.numGioiHanMin);
            this.Controls.Add(this.lblGioiHanMax);
            this.Controls.Add(this.numGioiHanMax);
            this.Controls.Add(this.lblPhuTrach);
            this.Controls.Add(this.cbbPhuTrach);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm/Ch?nh s?a Thông S?";
            ((System.ComponentModel.ISupportInitialize)(this.numGioiHanMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGioiHanMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
