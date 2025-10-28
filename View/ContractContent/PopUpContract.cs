using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class PopUpContract : Form
    {
        // Event raised when a contract is selected (returns ContractID)
        public event Action<int> ContractSelected;

        public PopUpContract(DataTable dt)
        {
            InitializeComponent();
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Handle double click or cell click to select a contract
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void DataGridView1_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            // optionally highlight row on single click; do not close
        }

        private void DataGridView1_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var row = dataGridView1.Rows[e.RowIndex];
                // Try to get ContractID column value
                if (row.Cells["ContractID"].Value != null && int.TryParse(row.Cells["ContractID"].Value.ToString(), out int contractId))
                {
                    ContractSelected?.Invoke(contractId);
                    this.Close();
                }
                else
                {
                    // Fallback: try first cell
                    if (row.Cells.Count > 0 && int.TryParse(row.Cells[0].Value?.ToString(), out contractId))
                    {
                        ContractSelected?.Invoke(contractId);
                        this.Close();
                    }
                }
            }
            catch (Exception)
            {
                // ignore selection errors
            }
        }
    }
}
