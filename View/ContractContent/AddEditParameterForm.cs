using System;
using System.Windows.Forms;
using Environmental_Monitoring.Model;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class AddEditParameterForm : Form
    {
        public Parameter Parameter { get; private set; }
        public AddEditParameterForm(Parameter parameter = null)
        {
            InitializeComponent();
            if (parameter != null)
            {
                txtTenThongSo.Text = parameter.TenThongSo;
                txtDonVi.Text = parameter.DonVi;
                numGioiHanMin.Value = parameter.GioiHanMin ?? 0;
                numGioiHanMax.Value = parameter.GioiHanMax ?? 0;
                cbbPhuTrach.SelectedItem = parameter.PhuTrach;
                Parameter = parameter;
            }
            else
            {
                Parameter = new Parameter();
            }
        }

        public void SetPhuTrachOptions(string[] options)
        {
            cbbPhuTrach.Items.Clear();
            cbbPhuTrach.Items.AddRange(options);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
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