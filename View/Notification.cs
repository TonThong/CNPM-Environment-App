using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Environmental_Monitoring
{
    public partial class Notification : UserControl

    {
        public Notification()
        {
            InitializeComponent();
            this.Load += Notification_Load;

        }


        private void Notification_Load(object sender, EventArgs e)
        {
            // Đặt font hỗ trợ Unicode (Segoe UI là chuẩn nhất)
            dgvSapQuaHan.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvSapQuaHan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvQuaHan.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvQuaHan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Nếu bạn muốn tất cả đều dùng cùng kiểu hiển thị
            dgvSapQuaHan.DefaultCellStyle.ForeColor = Color.Black;
            dgvSapQuaHan.DefaultCellStyle.BackColor = Color.White;

            // Sau đó thêm cột
            dgvSapQuaHan.Columns.Add("MaDon", "Mã đơn");
            dgvSapQuaHan.Columns.Add("TenKH", "Tên khách hàng");
            dgvSapQuaHan.Columns.Add("SoNgayConLai", "Số ngày còn lại");
            dgvSapQuaHan.Rows.Add("SoNgayTre", "Số ngày trễ");
            dgvSapQuaHan.Rows.Add("SoNgayTre", "Số ngày trễ");
            dgvSapQuaHan.Rows.Add("SoNgayTre", "Số ngày trễ");
            dgvSapQuaHan.Rows.Add("SoNgayTre", "Số ngày trễ");
            dgvSapQuaHan.Rows.Add("SoNgayTre", "Số ngày trễ");
            dgvSapQuaHan.Rows.Add("SoNgayTre", "Số ngày trễ");
            dgvSapQuaHan.Rows.Add("SoNgayTre", "Số ngày trễ");

            dgvQuaHan.Columns.Add("MaDon", "Mã đơn");
            dgvQuaHan.Columns.Add("TenKH", "Tên khách hàng");
            dgvQuaHan.Columns.Add("SoNgayTre", "Số ngày trễ");
            dgvQuaHan.Rows.Add("SoNgayTre", "Số ngày trễ");
            dgvQuaHan.Rows.Add("SoNgayTre", "Số ngày trễ");
            dgvQuaHan.Rows.Add("SoNgayTre", "Số ngày trễ");
            dgvQuaHan.Rows.Add("SoNgayTre", "Số ngày trễ");
            dgvQuaHan.Rows.Add("SoNgayTre", "Số ngày trễ");
            dgvQuaHan.Rows.Add("SoNgayTre", "Số ngày trễ");
            dgvQuaHan.Rows.Add("SoNgayTre", "Số ngày trễ");


            dgvSapQuaHan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSapQuaHan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvSapQuaHan.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // chỉ viền ngang
            dgvSapQuaHan.GridColor = Color.LightGray; // màu đường ngăn giữa hàng

            dgvSapQuaHan.RowTemplate.Height = 40; // chiều cao hàng (tăng một chút)
            dgvSapQuaHan.DefaultCellStyle.Padding = new Padding(0, 5, 0, 5); // thêm đệm trên/dưới

            dgvQuaHan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvQuaHan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvQuaHan.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // chỉ viền ngang
            dgvQuaHan.GridColor = Color.LightGray; // màu đường ngăn giữa hàng

            dgvQuaHan.RowTemplate.Height = 40; // chiều cao hàng (tăng một chút)
            dgvQuaHan.DefaultCellStyle.Padding = new Padding(0, 5, 0, 5); // thêm đệm trên/dưới





        }

        private void AdjustLabelFontInTable(Label label)
        {
            if (label == null || label.Parent == null) return;

            // Lấy kích thước ô chứa label (cell trong TableLayoutPanel)
            int cellWidth = label.Parent.Width;
            int cellHeight = label.Parent.Height;

            // Tính cỡ chữ mới theo tỉ lệ cell
            float newSize = Math.Max(10, Math.Min(cellWidth / 45f, cellHeight / 20f));

            // Gán font mới
            label.Font = new Font("Segoe UI", newSize, FontStyle.Bold);
        }


        private void btnSapQuaHan_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnQuaHan_Click(object sender, EventArgs e)
        {

        }

        private void roundedTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvQuaHan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblThongBao_Click(object sender, EventArgs e)
        {

        }

        private void dgvSapQuaHan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvQuaHan_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void roundedTextBox2_Load(object sender, EventArgs e)
        {

        }

        private void roundedTextBoxPro1_Load(object sender, EventArgs e)
        {

        }
    }
}
