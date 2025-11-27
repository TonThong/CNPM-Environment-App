using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class Requestforcorrection : Form
    {
        public int SelectedTienTrinh { get; private set; } = 0;
        public string SelectedPhongBan { get; private set; } = "";

        private ResourceManager rm;
        private CultureInfo culture;

        public Requestforcorrection()
        {
            InitializeComponent();
            InitializeLocalization(); 
            UpdateUIText();
        }

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Requestforcorrection).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;
        }

        private void UpdateUIText()
        {
            label1.Text = rm.GetString("Request_Title", culture);
            if (btnSave != null)
                btnSave.Text = rm.GetString("Button_Save", culture);
            if (btnCancel != null)
                btnCancel.Text = rm.GetString("Button_Cancel", culture);
            if (radHienTruong != null)
                radHienTruong.Text = rm.GetString("Request_FieldDept", culture);
            if (radThiNghiem != null)
                radThiNghiem.Text = rm.GetString("Request_LabDept", culture);
       }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (radHienTruong.Checked)
            {
                SelectedTienTrinh = 2;
                SelectedPhongBan = rm.GetString("Request_FieldDept", culture); 
            }
            else if (radThiNghiem.Checked)
            {
                SelectedTienTrinh = 3;
                SelectedPhongBan = rm.GetString("Request_LabDept", culture); 
            }
            else
            {
                MessageBox.Show(rm.GetString("Request_SelectDeptWarning", culture),
                                rm.GetString("Alert_WarningTitle", culture),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}