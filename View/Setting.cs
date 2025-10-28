using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Environmental_Monitoring
{
    public partial class Setting : UserControl
    {
        public Setting()
        {
            InitializeComponent();
        }
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            cmbNgonNgu.Items.Clear();
            cmbNgonNgu.Items.Add("Tiếng Việt");
            cmbNgonNgu.Items.Add("English");

            cmbGiaoDien.Items.Clear();
            cmbGiaoDien.Items.Add("Sáng");
            cmbGiaoDien.Items.Add("Tối");

            cmbNgonNgu.SelectedItem = Properties.Settings.Default.Language;
            cmbGiaoDien.SelectedItem = Properties.Settings.Default.Theme;
            cbEmail.Checked = Properties.Settings.Default.NotifyByEmail;
            cbUngDung.Checked = Properties.Settings.Default.NotifyInApp;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.Language = cmbNgonNgu.SelectedItem.ToString();
                Properties.Settings.Default.Theme = cmbGiaoDien.SelectedItem.ToString();
                Properties.Settings.Default.NotifyByEmail = cbEmail.Checked;
                Properties.Settings.Default.NotifyInApp = cbUngDung.Checked;

                Properties.Settings.Default.Save();

                MessageBox.Show("Đã lưu cài đặt thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ApplySettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu cài đặt: " + ex.Message);
            }
        }
        private void ApplySettings()
        {

            string selectedTheme = Properties.Settings.Default.Theme;
            if (selectedTheme == "Tối")
            {

            }
            else 
            {

            }
        }










        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void label3_Click(object sender, EventArgs e)
        {
        }
        private void label10_Click(object sender, EventArgs e)
        {
        }
    }
}
