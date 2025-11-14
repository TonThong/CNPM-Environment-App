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

            if (parameters != null)
            {
                foreach (DataRow row in parameters.Rows)
                {
                    lstParameters.Items.Add(
                        new ParameterItem
                        {
                            ParameterID = Convert.ToInt32(row["ParameterID"]),
                            TenThongSo = row["TenThongSo"].ToString()
                        },
                        false // chưa được chọn
                    );
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedParameterIds.Clear();
            foreach (var item in lstParameters.CheckedItems)
            {
                if (item is ParameterItem p)
                {
                    SelectedParameterIds.Add(p.ParameterID);
                }
            }

            if (string.IsNullOrWhiteSpace(TemplateName))
            {
                MessageBox.Show("Vui lòng nhập tên mẫu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (SelectedParameterIds.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một thông số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

public class ParameterItem
{
    public int ParameterID { get; set; }
    public string TenThongSo { get; set; }

    public override string ToString()
    {
        return TenThongSo; // hiển thị tên thay vì kiểu đối tượng
    }
}
