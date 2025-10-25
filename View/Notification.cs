using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace Environmental_Monitoring
{
    public partial class Notification : UserControl
    {

        string connectionString = "Server=sql12.freesqldatabase.com;Database=sql12800882;Uid=sql12800882;Pwd=TMlsWFrPxZ;";

        public Notification()
        {
            InitializeComponent();
            this.Load += Notification_Load;

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

            LoadNotifications(dgvQuaHan, "QuaHan");
            LoadNotifications(dgvSapQuaHan, "SapHetHan");

        }

        private void LoadNotifications(DataGridView dgv, string loaiThongBao)
        {
            DataTable dataTable = new DataTable();

            string query = @"
        SELECT 
            c.MaDon, 
            cust.TenNguoiDaiDien,
            
            -- Cột Số Ngày Sắp Hết Hạn (chỉ hiển thị nếu > 0)
            CASE 
                WHEN DATEDIFF(c.NgayTraKetQua, CURDATE()) > 0 
                THEN DATEDIFF(c.NgayTraKetQua, CURDATE()) 
                ELSE NULL 
            END AS SoNgaySapHetHan,
            
            -- Cột Số Ngày Đã Hết Hạn (chỉ hiển thị nếu <= 0, và lấy giá trị dương)
            CASE 
                WHEN DATEDIFF(c.NgayTraKetQua, CURDATE()) <= 0 
                THEN ABS(DATEDIFF(c.NgayTraKetQua, CURDATE())) 
                ELSE NULL 
            END AS SoNgayDaHetHan
        FROM 
            Notifications n
        JOIN 
            Contracts c ON n.ContractID = c.ContractID
        JOIN 
            Customers cust ON c.CustomerID = cust.CustomerID
        "; 

            if (loaiThongBao == "SapHetHan")
            {
                query += " WHERE DATEDIFF(c.NgayTraKetQua, CURDATE()) > 0";
            }
            else 
            {
                query += " WHERE DATEDIFF(c.NgayTraKetQua, CURDATE()) <= 0";
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }

                dgv.DataSource = dataTable;

                if (dgv.Columns.Count > 0)
                {
                    dgv.Columns["MaDon"].HeaderText = "Mã Đơn";
                    dgv.Columns["TenNguoiDaiDien"].HeaderText = "Tên Khách Hàng";

                    if (loaiThongBao == "SapHetHan")
                    {
                        dgv.Columns["SoNgaySapHetHan"].HeaderText = "Số Ngày Còn Lại";
                        dgv.Columns["SoNgaySapHetHan"].Visible = true;
                        dgv.Columns["SoNgayDaHetHan"].Visible = false;
                    }
                    else
                    {
                        dgv.Columns["SoNgaySapHetHan"].Visible = false;
                        dgv.Columns["SoNgayDaHetHan"].HeaderText = "Số Ngày Quá Hạn";
                        dgv.Columns["SoNgayDaHetHan"].Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
