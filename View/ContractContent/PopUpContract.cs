using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;
using System.Globalization;
using System.Threading;
using Environmental_Monitoring.Controller;
using Environmental_Monitoring.Controller.Data;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class PopUpContract : Form
    {
        public event Action<int> ContractSelected;
        private ResourceManager rm;
        private CultureInfo culture;
        private bool isAuthorizedToUnlock = false;

        public PopUpContract(DataTable dt)
        {
            InitializeComponent();
            InitializeLocalization();
            CheckPermission();

            // Xử lý dữ liệu đầu vào: Đảm bảo có cột IsUnlocked
            if (!dt.Columns.Contains("IsUnlocked"))
            {
                dt.Columns.Add("IsUnlocked", typeof(bool));
                // Nếu DB không trả về cột này, mặc định là False (Chưa mở)
                foreach (DataRow r in dt.Rows) r["IsUnlocked"] = false;
            }

            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

            // Ẩn cột IsUnlocked (Logic ngầm)
            if (dataGridView1.Columns.Contains("IsUnlocked"))
                dataGridView1.Columns["IsUnlocked"].Visible = false;

            // Ẩn cột Unlock (Vì ta tự vẽ nút giả lập đè lên, không cần cột nút thật)
            if (dataGridView1.Columns.Contains("Unlock"))
                dataGridView1.Columns["Unlock"].Visible = false;

            UpdateUIText();

            // ĐĂNG KÝ SỰ KIỆN
            dataGridView1.CellFormatting += DataGridView1_CellFormatting; // Tô màu đỏ
            dataGridView1.RowPostPaint += DataGridView1_RowPostPaint;     // Vẽ lớp phủ & Nút
            dataGridView1.MouseClick += DataGridView1_MouseClick;         // Xử lý click nút
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick; // Chọn hợp đồng
        }

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(PopUpContract).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;
        }

        private void CheckPermission()
        {
            if (UserSession.CurrentUser != null)
            {
                // Admin (5) hoặc Trưởng bộ phận (1) được quyền mở
                isAuthorizedToUnlock = (UserSession.CurrentUser.RoleID == 5) || (UserSession.CurrentUser.TruongBoPhan == 1);
            }
        }

        private void UpdateUIText()
        {
            this.Text = rm.GetString("Popup_SelectContractTitle", culture) ?? "Chọn Hợp Đồng";
            if (dataGridView1.Columns.Contains("ContractID")) dataGridView1.Columns["ContractID"].HeaderText = rm.GetString("Grid_ContractID", culture);
            if (dataGridView1.Columns.Contains("MaDon")) dataGridView1.Columns["MaDon"].HeaderText = rm.GetString("Grid_ContractCode", culture);
            if (dataGridView1.Columns.Contains("NgayKy")) dataGridView1.Columns["NgayKy"].HeaderText = rm.GetString("Grid_SignDate", culture);
            if (dataGridView1.Columns.Contains("NgayTraKetQua")) dataGridView1.Columns["NgayTraKetQua"].HeaderText = rm.GetString("Grid_DueDate", culture);
            if (dataGridView1.Columns.Contains("Status")) dataGridView1.Columns["Status"].HeaderText = rm.GetString("Grid_Status", culture);
        }

        // --- 1. LUÔN TÔ MÀU ĐỎ NẾU EXPIRED ---
        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            string status = row.Cells["Status"].Value?.ToString() ?? "";

            if (status.Equals("Expired", StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 200, 200); // Đỏ nhạt
                e.CellStyle.ForeColor = Color.DarkRed;
                e.CellStyle.SelectionBackColor = Color.FromArgb(255, 150, 150);
                e.CellStyle.SelectionForeColor = Color.White;
            }
        }

        // --- 2. VẼ LỚP PHỦ XÁM VÀ NÚT UNLOCK NHỎ (NỀN TRẮNG CHỮ ĐEN) ---
        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            string status = row.Cells["Status"].Value?.ToString() ?? "";

            bool isUnlocked = false;
            // Lấy giá trị IsUnlocked an toàn
            if (dataGridView1.Columns.Contains("IsUnlocked") && row.Cells["IsUnlocked"].Value != DBNull.Value)
            {
                var val = row.Cells["IsUnlocked"].Value;
                if (val is int iVal) isUnlocked = (iVal == 1);
                else if (val is bool bVal) isUnlocked = bVal;
                else if (val is ulong uVal) isUnlocked = (uVal == 1);
            }

            // CHỈ VẼ KHI: Hết hạn VÀ Chưa mở khóa
            if (status.Equals("Expired", StringComparison.OrdinalIgnoreCase) && !isUnlocked)
            {
                // A. Vẽ lớp xám bán trong suốt trùm lên hàng
                using (Brush grayBrush = new SolidBrush(Color.FromArgb(180, 160, 160, 160)))
                {
                    Rectangle rowBounds = e.RowBounds;
                    rowBounds.Height -= 1;
                    e.Graphics.FillRectangle(grayBrush, rowBounds);
                }

                // B. Vẽ nút Unlock
                string btnText = isAuthorizedToUnlock ? "UNLOCK 🔓" : "LOCKED 🔒";
                Font btnFont = new Font("Segoe UI", 7, FontStyle.Bold); // Font nhỏ size 7-8
                Size textSize = TextRenderer.MeasureText(btnText, btnFont);

                int btnWidth = textSize.Width + 10;
                int btnHeight = textSize.Height + 4;

                // Căn giữa dòng
                int btnX = e.RowBounds.Left + (e.RowBounds.Width - btnWidth) / 2;
                int btnY = e.RowBounds.Top + (e.RowBounds.Height - btnHeight) / 2;

                Rectangle btnRect = new Rectangle(btnX, btnY, btnWidth, btnHeight);

                // Bóng đổ nhẹ
                using (Brush shadowBrush = new SolidBrush(Color.FromArgb(50, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(shadowBrush, btnX + 1, btnY + 1, btnWidth, btnHeight);
                }

                // Nền nút: TRẮNG
                using (Brush btnBrush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillRectangle(btnBrush, btnRect);
                }

                // Viền nút: ĐEN
                using (Pen btnPen = new Pen(Color.Black))
                {
                    e.Graphics.DrawRectangle(btnPen, btnRect);
                }

                // Chữ: ĐEN
                TextRenderer.DrawText(e.Graphics, btnText, btnFont, btnRect, Color.Black,
                                      TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        // --- 3. XỬ LÝ CLICK CHUỘT VÀO NÚT UNLOCK ---
        private void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            var hitTest = dataGridView1.HitTest(e.X, e.Y);
            if (hitTest.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[hitTest.RowIndex];
                string status = row.Cells["Status"].Value?.ToString() ?? "";

                bool isUnlocked = false;
                if (dataGridView1.Columns.Contains("IsUnlocked") && row.Cells["IsUnlocked"].Value != DBNull.Value)
                {
                    var val = row.Cells["IsUnlocked"].Value;
                    if (val is int iVal) isUnlocked = (iVal == 1);
                    else if (val is bool bVal) isUnlocked = bVal;
                    else if (val is ulong uVal) isUnlocked = (uVal == 1);
                }

                // Chỉ xử lý nếu đang bị KHÓA
                if (status.Equals("Expired", StringComparison.OrdinalIgnoreCase) && !isUnlocked)
                {
                    // TÍNH LẠI VÙNG BẤM (Phải khớp logic vẽ bên trên)
                    Rectangle rowBounds = dataGridView1.GetRowDisplayRectangle(hitTest.RowIndex, false);
                    string btnText = isAuthorizedToUnlock ? "UNLOCK 🔓" : "LOCKED 🔒";
                    Font btnFont = new Font("Segoe UI", 7, FontStyle.Bold);
                    Size textSize = TextRenderer.MeasureText(btnText, btnFont);
                    int btnWidth = textSize.Width + 10;
                    int btnHeight = textSize.Height + 4;
                    int btnX = rowBounds.Left + (rowBounds.Width - btnWidth) / 2;
                    int btnY = rowBounds.Top + (rowBounds.Height - btnHeight) / 2;
                    Rectangle btnRect = new Rectangle(btnX, btnY, btnWidth, btnHeight);

                    // Kiểm tra chuột có click trúng nút không
                    if (btnRect.Contains(e.Location))
                    {
                        if (isAuthorizedToUnlock)
                        {
                            if (MessageBox.Show("Mở khóa hợp đồng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                UnlockContract(hitTest.RowIndex);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bạn không phải là Trưởng bộ phận hoặc Admin.\nKhông có quyền mở khóa.",
                                            "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        private void UnlockContract(int rowIndex)
        {
            try
            {
                int contractId = 0;
                var val = dataGridView1.Rows[rowIndex].Cells["ContractID"].Value;
                if (val != null) int.TryParse(val.ToString(), out contractId);

                if (contractId > 0)
                {
                    // Cập nhật Database: Đặt IsUnlocked = 1
                    string query = "UPDATE Contracts SET IsUnlocked = 1 WHERE ContractID = @id";
                    DataProvider.Instance.ExecuteNonQuery(query, new object[] { contractId });

                    // Cập nhật UI ngay lập tức (Xóa cờ khóa để lớp xám biến mất)
                    dataGridView1.Rows[rowIndex].Cells["IsUnlocked"].Value = true;
                    dataGridView1.InvalidateRow(rowIndex); // Vẽ lại dòng
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // --- 4. CHẶN DOUBLE CLICK NẾU KHÓA ---
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            string status = row.Cells["Status"].Value?.ToString() ?? "";

            bool isUnlocked = false;
            if (dataGridView1.Columns.Contains("IsUnlocked") && row.Cells["IsUnlocked"].Value != DBNull.Value)
            {
                var val = row.Cells["IsUnlocked"].Value;
                if (val is int iVal) isUnlocked = (iVal == 1);
                else if (val is bool bVal) isUnlocked = bVal;
                else if (val is ulong uVal) isUnlocked = (uVal == 1);
            }

            // Nếu Expired mà chưa Unlock -> Chặn
            if (status.Equals("Expired", StringComparison.OrdinalIgnoreCase) && !isUnlocked)
            {
                return; // Không làm gì
            }

            // Nếu OK -> Chọn
            try
            {
                int contractId = 0;
                if (dataGridView1.Columns.Contains("ContractID")) int.TryParse(row.Cells["ContractID"].Value?.ToString(), out contractId);
                else if (row.Cells.Count > 0) int.TryParse(row.Cells[0].Value?.ToString(), out contractId);

                if (contractId > 0)
                {
                    ContractSelected?.Invoke(contractId);
                    this.Close();
                }
            }
            catch { }
        }
    }
}