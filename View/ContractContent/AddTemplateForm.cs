using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class AddTemplateForm : Form
    {
        public string TemplateName => txtTemplateName.Text.Trim();
        public List<int> SelectedParameterIds { get; private set; } = new List<int>();

        public AddTemplateForm(DataTable parameters)
        {
            InitializeComponent();

            // parameters DataTable expected to have ParameterID and TenThongSo
            lstParameters.DisplayMember = "TenThongSo";
            lstParameters.ValueMember = "ParameterID";

            if (parameters != null)
            {
                // Bind to DefaultView so DisplayMember is used instead of DataRowView.ToString()
                lstParameters.DataSource = parameters.DefaultView;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedParameterIds.Clear();
            foreach (var item in lstParameters.CheckedItems)
            {
                var drv = item as DataRowView;
                if (drv != null)
                {
                    int id = Convert.ToInt32(drv["ParameterID"]);
                    SelectedParameterIds.Add(id);
                }
            }

            if (string.IsNullOrWhiteSpace(TemplateName))
            {
                MessageBox.Show("Vui lòng nh?p tên m?u.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (SelectedParameterIds.Count == 0)
            {
                MessageBox.Show("Vui lòng ch?n ít nh?t m?t thông s?.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
