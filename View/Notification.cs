using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Resources;
using Environmental_Monitoring.View.Components;
using Environmental_Monitoring.View;
using MySql.Data.MySqlClient; 
using Environmental_Monitoring.Controller; 
using Environmental_Monitoring.Controller.Data; 

namespace Environmental_Monitoring
{
    public partial class Notification : UserControl
    {

        public Notification()
        {
            InitializeComponent();
            this.Load += Notification_Load;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData == Keys.Enter) && (this.ActiveControl == txtSearch))
            {
                LoadNotifications(dgvQuaHan, "QuaHan");
                LoadNotifications(dgvSapQuaHan, "SapHetHan");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Notification_Load(object sender, EventArgs e)
        {
           
            dgvSapQuaHan.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvSapQuaHan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvQuaHan.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvQuaHan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvSapQuaHan.DefaultCellStyle.ForeColor = Color.Black;
            dgvSapQuaHan.DefaultCellStyle.BackColor = Color.White;
            dgvSapQuaHan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSapQuaHan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvSapQuaHan.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSapQuaHan.GridColor = Color.LightGray;
            dgvSapQuaHan.RowTemplate.Height = 40;
            dgvSapQuaHan.DefaultCellStyle.Padding = new Padding(0, 5, 0, 5);
            dgvQuaHan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvQuaHan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvQuaHan.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvQuaHan.GridColor = Color.LightGray;
            dgvQuaHan.RowTemplate.Height = 40;
            dgvQuaHan.DefaultCellStyle.Padding = new Padding(0, 5, 0, 5);

            UpdateUIText(); 
        }
        public void UpdateUIText()
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Notification).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            lblTitle.Text = rm.GetString("Notification_Title", culture);
            txtSearch.PlaceholderText = rm.GetString("Search_Placeholder", culture);
            lblPanelSoon.Text = rm.GetString("Notification_Panel_Soon", culture);
            lblPanelOverdue.Text = rm.GetString("Notification_Panel_Overdue", culture);

            LoadNotifications(dgvQuaHan, "QuaHan");
            LoadNotifications(dgvSapQuaHan, "SapHetHan");

            try
            {
                this.BackColor = ThemeManager.BackgroundColor;
                lblTitle.ForeColor = ThemeManager.TextColor;

                txtSearch.BackColor = ThemeManager.PanelColor;
                txtSearch.ForeColor = ThemeManager.TextColor;

                lblPanelSoon.ForeColor = ThemeManager.TextColor;
                dgvSapQuaHan.BackgroundColor = ThemeManager.PanelColor;
                dgvSapQuaHan.GridColor = ThemeManager.BorderColor;
                dgvSapQuaHan.DefaultCellStyle.BackColor = ThemeManager.PanelColor;
                dgvSapQuaHan.DefaultCellStyle.ForeColor = ThemeManager.TextColor;
                dgvSapQuaHan.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.SecondaryPanelColor;
                dgvSapQuaHan.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.TextColor;

                lblPanelOverdue.ForeColor = ThemeManager.TextColor;
                dgvQuaHan.BackgroundColor = ThemeManager.PanelColor;
                dgvQuaHan.GridColor = ThemeManager.BorderColor;
                dgvQuaHan.DefaultCellStyle.BackColor = ThemeManager.PanelColor;
                dgvQuaHan.DefaultCellStyle.ForeColor = ThemeManager.TextColor;
                dgvQuaHan.ColumnHeadersDefaultCellStyle.BackColor = ThemeManager.SecondaryPanelColor;
                dgvQuaHan.ColumnHeadersDefaultCellStyle.ForeColor = ThemeManager.TextColor;
            }
            catch (Exception) {}
        }

        private void LoadNotifications(DataGridView dgv, string loaiThongBao)
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Notification).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            DataTable dataTable = new DataTable();
            string query = "";

            string keySearch = txtSearch.Text.Trim();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            if (loaiThongBao == "SapHetHan")
            {
                query = @"
                    SELECT 
                        c.MaDon, 
                        cust.TenNguoiDaiDien,
                        DATEDIFF(c.NgayTraKetQua, CURDATE()) AS SoNgay
                    FROM 
                        Contracts c
                    JOIN 
                        Customers cust ON c.CustomerID = cust.CustomerID
                    WHERE 
                        DATEDIFF(c.NgayTraKetQua, CURDATE()) > 0
                        AND c.Status != 'Completed'";
            }
            else
            {
                query = @"
                    SELECT 
                        c.MaDon, 
                        cust.TenNguoiDaiDien,
                        ABS(DATEDIFF(c.NgayTraKetQua, CURDATE())) AS SoNgay
                    FROM 
                        Contracts c
                    JOIN 
                        Customers cust ON c.CustomerID = cust.CustomerID
                    WHERE 
                        DATEDIFF(c.NgayTraKetQua, CURDATE()) <= 0
                        AND c.Status != 'Completed'";
            }

            if (!string.IsNullOrWhiteSpace(keySearch))
            {
                query += " AND (UPPER(c.MaDon) LIKE UPPER(@keySearch) OR UPPER(cust.TenNguoiDaiDien) LIKE UPPER(@keySearch))";
                parameters.Add(new MySqlParameter("@keySearch", "%" + keySearch + "%"));
            }

            try
            {
                using (MySqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        if (parameters.Count > 0)
                        {
                            command.Parameters.AddRange(parameters.ToArray());
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }

                dgv.DataSource = dataTable;

                if (dgv.Columns.Count > 0)
                {
                    dgv.Columns["MaDon"].HeaderText = rm.GetString("Grid_ContractCode", culture);
                    dgv.Columns["TenNguoiDaiDien"].HeaderText = rm.GetString("Grid_CustomerName", culture);

                    if (loaiThongBao == "SapHetHan")
                    {
                        dgv.Columns["SoNgay"].HeaderText = rm.GetString("Grid_DaysRemaining", culture);
                    }
                    else
                    {
                        dgv.Columns["SoNgay"].HeaderText = rm.GetString("Grid_DaysOverdue", culture);
                    }
                }
            }
            catch (Exception ex)
            {
                Mainlayout mainForm = this.ParentForm as Mainlayout;
                string errorMsg = rm.GetString("Alert_LoadDataError", culture);
                mainForm?.ShowGlobalAlert(errorMsg + ex.Message, AlertPanel.AlertType.Error);
            }
        }
        private void btnSapQuaHan_Click_1(object sender, EventArgs e)
        {
            LoadNotifications(dgvSapQuaHan, "SapHetHan");
        }

        private void btnQuaHan_Click(object sender, EventArgs e)
        {
            LoadNotifications(dgvQuaHan, "QuaHan");
        }
    }
}