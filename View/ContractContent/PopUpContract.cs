using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;
using System.Globalization;
using System.Threading;
using Environmental_Monitoring.Controller;
using Environmental_Monitoring.Controller.Data;
using System.Collections.Generic; // Cần thêm cái này để dùng List

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
                foreach (DataRow r in dt.Rows) r["IsUnlocked"] = false;
            }

            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

            // Ẩn cột logic
            if (dataGridView1.Columns.Contains("IsUnlocked")) dataGridView1.Columns["IsUnlocked"].Visible = false;
            if (dataGridView1.Columns.Contains("Unlock")) dataGridView1.Columns["Unlock"].Visible = false;

            UpdateUIText();

            // ĐĂNG KÝ CÁC SỰ KIỆN GRID
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
            dataGridView1.RowPostPaint += DataGridView1_RowPostPaint;
            dataGridView1.MouseClick += DataGridView1_MouseClick;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

            // --- [MỚI] ĐĂNG KÝ SỰ KIỆN TÌM KIẾM ---
            // Giả sử tên TextBox trong Design là txtSearch
            if (txtSearch != null)
            {
                txtSearch.TextChanged += TxtSearch_TextChanged;
                // Thêm placeholder text nếu cần
                SetPlaceholder(txtSearch, "Tìm kiếm...");
            }
        }

        // --- [MỚI] HÀM XỬ LÝ TÌM KIẾM ---
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;

            // Nếu là placeholder text thì coi như rỗng
            if (keyword == "Tìm kiếm...") keyword = "";

            PerformSearch(keyword);
        }

        private void PerformSearch(string keyword)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            if (dt == null) return;

            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    dt.DefaultView.RowFilter = string.Empty; // Xóa lọc
                }
                else
                {
                    // Xử lý ký tự đặc biệt để tránh lỗi SQL
                    string safeKeyword = keyword.Replace("'", "''")
                                                .Replace("[", "[[]")
                                                .Replace("]", "[]]")
                                                .Replace("%", "[%]")
                                                .Replace("*", "[*]")
                                                .Trim();

                    // Tạo danh sách các điều kiện lọc
                    List<string> filterParts = new List<string>();

                    // Duyệt qua tất cả các cột có trong DataTable để tìm kiếm
                    foreach (DataColumn col in dt.Columns)
                    {
                        // Bỏ qua cột IsUnlocked hoặc các cột bool không cần thiết nếu muốn
                        if (col.DataType == typeof(bool)) continue;

                        // Cấu trúc: Convert(TenCot, 'System.String') LIKE '%keyword%'
                        filterParts.Add($"Convert([{col.ColumnName}], 'System.String') LIKE '%{safeKeyword}%'");
                    }

                    // Nối các điều kiện bằng OR
                    dt.DefaultView.RowFilter = string.Join(" OR ", filterParts);
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Search Error: " + ex.Message);
            }
        }

        // Helper tạo Placeholder cho đẹp (Tùy chọn)
        private void SetPlaceholder(Environmental_Monitoring.View.Components.RoundedTextBox txt, string placeholder)
        {
            txt.Text = placeholder;
            txt.ForeColor = Color.Gray;

            txt.Enter += (s, e) =>
            {
                if (txt.Text == placeholder)
                {
                    txt.Text = "";
                    txt.ForeColor = Color.Black;
                }
            };

            txt.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.Text = placeholder;
                    txt.ForeColor = Color.Gray;
                }
            };
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

        // --- CÁC HÀM XỬ LÝ GRID (GIỮ NGUYÊN) ---
        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            string status = row.Cells["Status"].Value?.ToString() ?? "";

            if (status.Equals("Expired", StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 200, 200);
                e.CellStyle.ForeColor = Color.DarkRed;
                e.CellStyle.SelectionBackColor = Color.FromArgb(255, 150, 150);
                e.CellStyle.SelectionForeColor = Color.White;
            }
        }

        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
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

            if (status.Equals("Expired", StringComparison.OrdinalIgnoreCase) && !isUnlocked)
            {
                using (Brush grayBrush = new SolidBrush(Color.FromArgb(180, 160, 160, 160)))
                {
                    Rectangle rowBounds = e.RowBounds;
                    rowBounds.Height -= 1;
                    e.Graphics.FillRectangle(grayBrush, rowBounds);
                }

                string btnText = isAuthorizedToUnlock ? "UNLOCK 🔓" : "LOCKED 🔒";
                Font btnFont = new Font("Segoe UI", 7, FontStyle.Bold);
                Size textSize = TextRenderer.MeasureText(btnText, btnFont);

                int btnWidth = textSize.Width + 10;
                int btnHeight = textSize.Height + 4;
                int btnX = e.RowBounds.Left + (e.RowBounds.Width - btnWidth) / 2;
                int btnY = e.RowBounds.Top + (e.RowBounds.Height - btnHeight) / 2;
                Rectangle btnRect = new Rectangle(btnX, btnY, btnWidth, btnHeight);

                using (Brush shadowBrush = new SolidBrush(Color.FromArgb(50, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(shadowBrush, btnX + 1, btnY + 1, btnWidth, btnHeight);
                }
                using (Brush btnBrush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillRectangle(btnBrush, btnRect);
                }
                using (Pen btnPen = new Pen(Color.Black))
                {
                    e.Graphics.DrawRectangle(btnPen, btnRect);
                }
                TextRenderer.DrawText(e.Graphics, btnText, btnFont, btnRect, Color.Black,
                                      TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

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

                if (status.Equals("Expired", StringComparison.OrdinalIgnoreCase) && !isUnlocked)
                {
                    Rectangle rowBounds = dataGridView1.GetRowDisplayRectangle(hitTest.RowIndex, false);
                    string btnText = isAuthorizedToUnlock ? "UNLOCK 🔓" : "LOCKED 🔒";
                    Font btnFont = new Font("Segoe UI", 7, FontStyle.Bold);
                    Size textSize = TextRenderer.MeasureText(btnText, btnFont);
                    int btnWidth = textSize.Width + 10;
                    int btnHeight = textSize.Height + 4;
                    int btnX = rowBounds.Left + (rowBounds.Width - btnWidth) / 2;
                    int btnY = rowBounds.Top + (rowBounds.Height - btnHeight) / 2;
                    Rectangle btnRect = new Rectangle(btnX, btnY, btnWidth, btnHeight);

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
                    string query = "UPDATE Contracts SET IsUnlocked = 1 WHERE ContractID = @id";
                    DataProvider.Instance.ExecuteNonQuery(query, new object[] { contractId });
                    dataGridView1.Rows[rowIndex].Cells["IsUnlocked"].Value = true;
                    dataGridView1.InvalidateRow(rowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

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

            if (status.Equals("Expired", StringComparison.OrdinalIgnoreCase) && !isUnlocked)
            {
                return;
            }

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