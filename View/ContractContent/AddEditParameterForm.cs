using System;
using System.Windows.Forms;
using Environmental_Monitoring.Model;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class AddEditParameterForm : Form
    {
        public Parameter Parameter { get; private set; }

        private ResourceManager rm;
        private CultureInfo culture;

        public AddEditParameterForm(Parameter parameter = null)
        {
            InitializeComponent();
            InitializeLocalization(); 
            UpdateUIText();

            if (parameter != null)
            {
                txtTenThongSo.Text = parameter.TenThongSo;
                txtDonVi.Text = parameter.DonVi;
                numGioiHanMin.Value = parameter.GioiHanMin ?? 0;
                numGioiHanMax.Value = parameter.GioiHanMax ?? 0;
                cbbPhuTrach.SelectedItem = parameter.PhuTrach;
                Parameter = parameter;
                this.Text = rm.GetString("AddEditParam_EditTitle", culture); 
            }
            else
            {
                Parameter = new Parameter();
                this.Text = rm.GetString("AddEditParam_AddTitle", culture);
            }
        }

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(AddEditParameterForm).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;
        }

        private void UpdateUIText()
        {
            if (btnOK != null)
                btnOK.Text = rm.GetString("Button_OK", culture);
            if (btnCancel != null)
                btnCancel.Text = rm.GetString("Button_Cancel", culture);

            if (lblTenThongSo != null) 
                lblTenThongSo.Text = rm.GetString("Grid_ParamName", culture);
            if (lblDonVi != null)
                lblDonVi.Text = rm.GetString("Grid_Unit", culture);
            if (lblGioiHanMin != null)
                lblGioiHanMin.Text = rm.GetString("Grid_Min", culture);
            if (lblGioiHanMax != null)
                lblGioiHanMax.Text = rm.GetString("Grid_Max", culture);
            if (lblPhuTrach != null)
                lblPhuTrach.Text = rm.GetString("Grid_Department", culture);
        }

        public void SetPhuTrachOptions(string[] options)
        {
            cbbPhuTrach.Items.Clear();
            cbbPhuTrach.Items.AddRange(options);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenThongSo.Text) || cbbPhuTrach.SelectedItem == null)
            {
                MessageBox.Show(rm.GetString("AddEditParam_ValidationWarning", culture),
                                rm.GetString("Alert_WarningTitle", culture),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            Parameter.TenThongSo = txtTenThongSo.Text;
            Parameter.DonVi = txtDonVi.Text;
            Parameter.GioiHanMin = numGioiHanMin.Value;
            Parameter.GioiHanMax = numGioiHanMax.Value;
            Parameter.PhuTrach = cbbPhuTrach.SelectedItem?.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}