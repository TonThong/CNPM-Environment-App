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
    public partial class PopUpContract : Form
    {
        public event Action<int> ContractSelected;

        private ResourceManager rm;
        private CultureInfo culture;

        public PopUpContract(DataTable dt)
        {
            InitializeComponent();
            InitializeLocalization();
            UpdateUIText();

            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(PopUpContract).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;
        }

        private void UpdateUIText()
        {
            this.Text = rm.GetString("Popup_SelectContractTitle", culture);

            if (dataGridView1.Columns.Contains("MaDon"))
                dataGridView1.Columns["MaDon"].HeaderText = rm.GetString("Grid_ContractCode", culture);
            if (dataGridView1.Columns.Contains("NgayKy"))
                dataGridView1.Columns["NgayKy"].HeaderText = rm.GetString("Grid_SignDate", culture);
            if (dataGridView1.Columns.Contains("NgayTraKetQua"))
                dataGridView1.Columns["NgayTraKetQua"].HeaderText = rm.GetString("Grid_DueDate", culture);
            if (dataGridView1.Columns.Contains("Status"))
                dataGridView1.Columns["Status"].HeaderText = rm.GetString("Grid_Status", culture);
        }

        private void DataGridView1_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
        }

        private void DataGridView1_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var row = dataGridView1.Rows[e.RowIndex];
                if (row.Cells["ContractID"].Value != null && int.TryParse(row.Cells["ContractID"].Value.ToString(), out int contractId))
                {
                    ContractSelected?.Invoke(contractId);
                    this.Close();
                }
                else
                {
                    if (row.Cells.Count > 0 && int.TryParse(row.Cells[0].Value?.ToString(), out contractId))
                    {
                        ContractSelected?.Invoke(contractId);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(rm.GetString("Error_SelectContract", culture) + ": " + ex.Message,
                                rm.GetString("Alert_ErrorTitle", culture),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}