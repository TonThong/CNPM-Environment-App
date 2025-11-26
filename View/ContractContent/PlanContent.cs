using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Environmental_Monitoring.Controller.Data;
using Environmental_Monitoring.Model;
using System.Resources;
using System.Globalization;
using System.Threading;
using Environmental_Monitoring.Controller;
using Environmental_Monitoring.View.Components;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class PlanContent : UserControl
    {
        private int currentContractId = 0;
        private List<SampleDTO> _listSamples = new List<SampleDTO>();
        private ResourceManager rm;
        private CultureInfo culture;

        public PlanContent()
        {
            InitializeComponent();
            InitializeLocalization();
        }

        // Khởi tạo ResourceManager để hỗ trợ đa ngôn ngữ
        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(PlanContent).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;
        }

        // Sự kiện chạy khi UserControl được load lên giao diện
        private void PlanContent_Load(object sender, EventArgs e)
        {
            // Lấy control panel chứa nút mẫu
            flpTemplates = this.Controls.Find("flpTemplates", true).FirstOrDefault() as FlowLayoutPanel;

            // Thiết lập cột và giao diện cho bảng (Grid)
            SetupMainGrid();

            // Đăng ký sự kiện nút Lưu (tránh đăng ký kép)
            if (roundedButton2 != null)
            {
                roundedButton2.Click -= btnSaveContract_Click;
                roundedButton2.Click += btnSaveContract_Click;
            }

            // Đăng ký sự kiện nút Hủy
            var btnCancel = this.Controls.Find("btnCancel", true).FirstOrDefault();
            if (btnCancel != null) btnCancel.Click += btnCancel_Click;

            // Tải danh sách các nút chọn mẫu môi trường
            LoadSampleTemplates();

            // Cập nhật text và trạng thái nút theo ngữ cảnh
            UpdateUIText();
            UpdateSaveButtonState();
        }

        // Cấu hình các cột, header và style cho DataGridView hiển thị danh sách mẫu
        private void SetupMainGrid()
        {
            roundedDataGridView1.AutoGenerateColumns = false;
            roundedDataGridView1.AllowUserToAddRows = false;
            roundedDataGridView1.Columns.Clear();
            roundedDataGridView1.RowHeadersVisible = false;
            roundedDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Style đường kẻ lưới
            roundedDataGridView1.GridColor = Color.Black;
            roundedDataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            // Cột 1: Nền mẫu (Dùng để gộp ô)
            var colNenMau = new DataGridViewTextBoxColumn
            {
                HeaderText = "Nền mẫu",
                DataPropertyName = "TenNenMau",
                Width = 150
            };
            colNenMau.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            roundedDataGridView1.Columns.Add(colNenMau);

            // Các cột thông tin khác
            roundedDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ký hiệu", DataPropertyName = "KyHieuMau", Width = 80 });
            roundedDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Vị trí lấy mẫu", DataPropertyName = "ViTri", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            roundedDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "SL Thông số", Name = "colCount", Width = 100 });

            // Cột nút Xóa
            DataGridViewButtonColumn btnDel = new DataGridViewButtonColumn();
            btnDel.Name = "colDelete";
            btnDel.HeaderText = "Xóa";
            btnDel.Text = "X";
            btnDel.UseColumnTextForButtonValue = true;
            btnDel.Width = 50;
            btnDel.FlatStyle = FlatStyle.Flat;
            btnDel.DefaultCellStyle.ForeColor = Color.Red;
            roundedDataGridView1.Columns.Add(btnDel);

            // Style font chữ
            roundedDataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            roundedDataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Đăng ký các sự kiện thao tác trên Grid
            roundedDataGridView1.CellContentClick -= roundedDataGridView1_CellContentClick;
            roundedDataGridView1.CellContentClick += roundedDataGridView1_CellContentClick;

            roundedDataGridView1.CellFormatting -= roundedDataGridView1_CellFormatting;
            roundedDataGridView1.CellFormatting += roundedDataGridView1_CellFormatting;

            roundedDataGridView1.CellDoubleClick -= roundedDataGridView1_CellDoubleClick;
            roundedDataGridView1.CellDoubleClick += roundedDataGridView1_CellDoubleClick;

            // Sự kiện vẽ lại ô để xử lý Merge (Gộp ô) cột Nền mẫu
            roundedDataGridView1.CellPainting -= roundedDataGridView1_CellPainting;
            roundedDataGridView1.CellPainting += roundedDataGridView1_CellPainting;
        }

        // Xử lý vẽ giao diện Gộp ô (Merge Cells) cho cột "Nền mẫu" nếu các dòng liên tiếp có cùng giá trị
        private void roundedDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            e.Handled = true; // Chặn vẽ mặc định

            using (Brush backBrush = new SolidBrush(Color.White))
            {
                e.Graphics.FillRectangle(backBrush, e.CellBounds);
            }

            string currentValue = e.Value?.ToString();

            // Tìm dòng bắt đầu và kết thúc của nhóm cần gộp
            int startIndex = e.RowIndex;
            while (startIndex > 0)
            {
                var prevValue = roundedDataGridView1.Rows[startIndex - 1].Cells[e.ColumnIndex].Value?.ToString();
                if (prevValue != currentValue) break;
                startIndex--;
            }

            int endIndex = e.RowIndex;
            while (endIndex < roundedDataGridView1.Rows.Count - 1)
            {
                var nextValue = roundedDataGridView1.Rows[endIndex + 1].Cells[e.ColumnIndex].Value?.ToString();
                if (nextValue != currentValue) break;
                endIndex++;
            }

            // Tính toán kích thước ô gộp
            int totalHeight = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                totalHeight += roundedDataGridView1.Rows[i].Height;
            }

            int offsetY = 0;
            for (int i = startIndex; i < e.RowIndex; i++)
            {
                offsetY -= roundedDataGridView1.Rows[i].Height;
            }

            Rectangle groupRect = new Rectangle(
                e.CellBounds.X,
                e.CellBounds.Y + offsetY,
                e.CellBounds.Width,
                totalHeight);

            // Vẽ text vào giữa ô gộp
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter |
                                    TextFormatFlags.VerticalCenter |
                                    TextFormatFlags.WordBreak |
                                    TextFormatFlags.PreserveGraphicsClipping;

            TextRenderer.DrawText(e.Graphics,
                                  e.FormattedValue?.ToString(),
                                  e.CellStyle.Font,
                                  groupRect,
                                  e.CellStyle.ForeColor,
                                  flags);

            // Vẽ đường kẻ khung
            using (Pen gridPen = new Pen(Color.Black, 1))
            {
                e.Graphics.DrawLine(gridPen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                if (e.RowIndex == endIndex)
                {
                    e.Graphics.DrawLine(gridPen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                }
            }
        }

        // Tải danh sách 3 nút chọn mẫu cơ bản (Không khí, Nước, Đất) từ Database lên giao diện
        private void LoadSampleTemplates()
        {
            if (flpTemplates == null) return;

            try
            {
                string q = "SELECT TemplateID, TenMau FROM SampleTemplates WHERE TenMau NOT LIKE 'Template cho %'";
                DataTable dt = DataProvider.Instance.ExecuteQuery(q);

                flpTemplates.Controls.Clear();

                if (dt == null || dt.Rows.Count == 0) return;

                var targetEnvs = new string[] { "Air Environment", "Water Environment", "Soil Environment" };
                var foundTemplates = new Dictionary<string, (int id, string originalName)>();

                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["TemplateID"]);
                    string nameGoc = row["TenMau"].ToString();
                    string nameChuan = GetLocalizedTemplateName(nameGoc);

                    if (targetEnvs.Contains(nameChuan) && !foundTemplates.ContainsKey(nameChuan))
                    {
                        foundTemplates[nameChuan] = (id, nameGoc);
                    }
                }

                foreach (string envName in targetEnvs)
                {
                    if (foundTemplates.ContainsKey(envName))
                    {
                        var data = foundTemplates[envName];
                        Button btn = CreateTemplateButton(envName, data.id, data.originalName);
                        flpTemplates.Controls.Add(btn);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAlert("Lỗi tải template: " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        // Tạo nút bấm cho từng loại mẫu
        private Button CreateTemplateButton(string displayText, int id, string originalName)
        {
            Button btn = new Button();
            btn.Text = displayText;
            btn.Tag = new SampleTemplateDisplayItem
            {
                TemplateID = id,
                TenMauHienThi = displayText,
                TenMauGoc = originalName
            };
            btn.Size = new Size(200, 40);
            btn.BackColor = Color.White;
            btn.ForeColor = Color.DarkGreen;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.LightGray;
            btn.FlatAppearance.BorderSize = 1;
            btn.Cursor = Cursors.Hand;
            btn.Margin = new Padding(8);
            btn.MouseEnter += (s, e) => { btn.BackColor = Color.FromArgb(220, 248, 220); };
            btn.MouseLeave += (s, e) => { btn.BackColor = Color.White; };
            btn.Click += TemplateButton_Click;
            return btn;
        }

        // Sự kiện Click nút chọn Mẫu: Mở form nhập thông tin mẫu
        private void TemplateButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var item = btn.Tag as SampleTemplateDisplayItem;
            if (item == null) return;

            if (currentContractId == 0)
            {
                ShowAlert(rm.GetString("Plan_SelectContract", culture) ?? "Vui lòng chọn Hợp đồng trước!", AlertPanel.AlertType.Error);
                return;
            }

            using (var frm = new SampleInformation(item.TemplateID, item.TenMauHienThi))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _listSamples.Add(frm.ResultSample);
                    BindMainGrid();
                }
            }
        }

        // Sự kiện Double Click vào dòng trong Grid để Sửa thông tin mẫu
        private void roundedDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var sampleToEdit = _listSamples[e.RowIndex];

            using (var frm = new SampleInformation(sampleToEdit.BaseTemplateID, sampleToEdit.TenNenMau, sampleToEdit))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _listSamples[e.RowIndex] = frm.ResultSample;
                    BindMainGrid();
                }
            }
        }

        // Sự kiện Click vào nút Xóa trong Grid
        private void roundedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && roundedDataGridView1.Columns[e.ColumnIndex].Name == "colDelete")
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa mẫu này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _listSamples.RemoveAt(e.RowIndex);
                    BindMainGrid();
                }
            }
        }

        // Định dạng hiển thị cột số lượng thông số
        private void roundedDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && roundedDataGridView1.Columns[e.ColumnIndex].Name == "colCount")
            {
                var sample = _listSamples[e.RowIndex];
                e.Value = sample.Parameters.Count;
            }
        }

        // Refresh lại dữ liệu trên Grid từ danh sách _listSamples
        private void BindMainGrid()
        {
            // Sắp xếp để hỗ trợ gộp ô
            _listSamples = _listSamples.OrderBy(x => x.TenNenMau)
                                       .ThenBy(x => x.KyHieuMau)
                                       .ToList();

            roundedDataGridView1.DataSource = null;
            roundedDataGridView1.DataSource = _listSamples;
        }

        // [QUAN TRỌNG] Lưu toàn bộ dữ liệu Hợp đồng và Mẫu vào Database
        private void btnSaveContract_Click(object sender, EventArgs e)
        {
            if (currentContractId == 0) return;
            if (_listSamples.Count == 0)
            {
                ShowAlert("Danh sách mẫu trống! Vui lòng thêm ít nhất 1 mẫu.", AlertPanel.AlertType.Error);
                return;
            }

            try
            {
                string maDon = DataProvider.Instance.ExecuteScalar("SELECT MaDon FROM Contracts WHERE ContractID = @id", new object[] { currentContractId }).ToString();

                foreach (var sample in _listSamples)
                {
                    // 1. Tạo Template riêng cho mẫu cụ thể này
                    string specificTemplateName = $"Template cho {maDon} - {sample.KyHieuMau}";
                    string insertTempQ = "INSERT INTO SampleTemplates (TenMau) VALUES (@name)";
                    DataProvider.Instance.ExecuteNonQuery(insertTempQ, new object[] { specificTemplateName });
                    int newTemplateID = Convert.ToInt32(DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null));

                    // 2. Xử lý lưu các thông số
                    foreach (var p in sample.Parameters)
                    {
                        int finalParamID = p.ParameterID;

                        // Nếu ID = 0 (tức là thông số mới thêm thủ công), cần Insert vào DB để tạo ID mới
                        if (finalParamID == 0)
                        {
                            string insertParam = @"INSERT INTO Parameters (TenThongSo, DonVi, GioiHanMin, GioiHanMax, PhuongPhap, QuyChuan, PhuTrach, ONhiem)
                                                   VALUES (@ten, @dv, @min, @max, @pp, @qc, @pt, 0)";
                            DataProvider.Instance.ExecuteNonQuery(insertParam, new object[] {
                                p.TenThongSo, p.DonVi, p.Min, p.Max, p.PhuongPhap, p.QuyChuan, p.PhuTrach
                            });
                            // Lấy ID vừa tạo
                            finalParamID = Convert.ToInt32(DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null));
                        }
                        // Nếu ID > 0 (chọn từ list có sẵn), giữ nguyên ID đó để tái sử dụng, không Insert mới

                        // 3. Liên kết thông số (dù cũ hay mới) vào Template
                        string linkQ = "INSERT INTO TemplateParameters (TemplateID, ParameterID) VALUES (@tid, @pid)";
                        DataProvider.Instance.ExecuteNonQuery(linkQ, new object[] { newTemplateID, finalParamID });
                    }

                    // 4. Lưu thông tin Mẫu môi trường
                    string maMauFull = $"{maDon} - {sample.KyHieuMau}";
                    string insertSampleQ = @"INSERT INTO EnvironmentalSamples 
                                           (MaMau, ContractID, TemplateID, KyHieuMau, ViTriLayMau, ToaDoX, ToaDoY, TenNenMau)
                                           VALUES (@ma, @cid, @tid, @kyhieu, @vitri, @x, @y, @tennen)";

                    DataProvider.Instance.ExecuteNonQuery(insertSampleQ, new object[] {
                        maMauFull,
                        currentContractId,
                        newTemplateID,
                        sample.KyHieuMau,
                        sample.ViTri,
                        sample.ToaDoX,
                        sample.ToaDoY,
                        sample.TenNenMau
                    });
                }

                // Cập nhật trạng thái Hợp đồng
                string updateContract = "UPDATE Contracts SET TienTrinh = 2 WHERE ContractID = @id";
                DataProvider.Instance.ExecuteNonQuery(updateContract, new object[] { currentContractId });

                // Gửi thông báo hệ thống
                int hienTruongRoleID = 10;
                string noiDung = $"Hợp đồng '{maDon}' đã lập kế hoạch lấy mẫu xong.";
                NotificationService.CreateNotification("ChinhSua", noiDung, hienTruongRoleID, this.currentContractId, null);

                string successMsg = string.Format(rm.GetString("Plan_SaveSuccess", culture) ?? "Lưu thành công cho HĐ {0}", maDon);
                ShowAlert(successMsg, AlertPanel.AlertType.Success);

                // Reset giao diện
                _listSamples.Clear();
                BindMainGrid();
                currentContractId = 0;
                lbContractID.Text = "Khách Hàng:";
                UpdateSaveButtonState();
            }
            catch (Exception ex)
            {
                ShowAlert("Lỗi lưu dữ liệu: " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        // Cập nhật ngôn ngữ hiển thị
        public void UpdateUIText()
        {
            culture = Thread.CurrentThread.CurrentUICulture;

            if (currentContractId == 0) lbContractID.Text = "Khách Hàng:";
            if (btnContracts != null) btnContracts.Text = rm.GetString("Plan_ContractListButton", culture);
            if (roundedButton2 != null) roundedButton2.Text = rm.GetString("Button_Save", culture);
            LoadSampleTemplates();
        }

        // Sự kiện mở Popup chọn Hợp đồng
        private void btnContracts_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT ContractID, MaDon, NgayKy, NgayTraKetQua, Status, IsUnlocked FROM Contracts WHERE TienTrinh = 1";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);
                using (PopUpContract popup = new PopUpContract(dt))
                {
                    popup.ContractSelected += (contractId) =>
                    {
                        this.currentContractId = contractId;
                        // Lấy tên khách hàng hiển thị
                        string cusQuery = @"SELECT cus.TenDoanhNghiep 
                                            FROM Contracts c
                                            JOIN Customers cus ON c.CustomerID = cus.CustomerID
                                            WHERE c.ContractID = @cid";
                        object result = DataProvider.Instance.ExecuteScalar(cusQuery, new object[] { contractId });
                        string tenCongTy = result != null ? result.ToString() : "Không xác định";

                        lbContractID.Text = "Khách Hàng: " + tenCongTy;
                        UpdateSaveButtonState();
                    };
                    popup.ShowDialog();
                }
            }
            catch (Exception ex) { ShowAlert("Lỗi tải HĐ: " + ex.Message, AlertPanel.AlertType.Error); }
        }

        // Nút Hủy bỏ
        private void btnCancel_Click(object sender, EventArgs e)
        {
            _listSamples.Clear();
            BindMainGrid();
            currentContractId = 0;
            lbContractID.Text = "Khách Hàng:";
            UpdateSaveButtonState();
        }

        // Cập nhật trạng thái Enable/Disable cho nút Lưu
        private void UpdateSaveButtonState()
        {
            bool isContractSelected = (currentContractId != 0);
            if (roundedButton2 != null) roundedButton2.Enabled = isContractSelected;
        }

        // Hàm tiện ích hiển thị thông báo
        private void ShowAlert(string message, AlertPanel.AlertType type)
        {
            var mainLayout = this.FindForm() as Mainlayout;
            if (mainLayout != null) mainLayout.ShowGlobalAlert(message, type);
            else MessageBox.Show(message, type.ToString(), MessageBoxButtons.OK, type == AlertPanel.AlertType.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        // Hàm chuyển đổi tên Template sang ngôn ngữ chuẩn (Air/Water/Soil)
        private string GetLocalizedTemplateName(string dbName)
        {
            if (string.IsNullOrEmpty(dbName)) return "";
            if (culture.Name == "vi-VN") return dbName;
            string lower = dbName.ToLower();
            if (lower.Contains("không khí") || lower.Contains("air")) return "Air Environment";
            if (lower.Contains("nước") || lower.Contains("water")) return "Water Environment";
            if (lower.Contains("đất") || lower.Contains("soil")) return "Soil Environment";
            return dbName;
        }

        // Class DTO phụ trợ lưu thông tin nút Template
        public class SampleTemplateDisplayItem
        {
            public int TemplateID { get; set; }
            public string TenMauHienThi { get; set; }
            public string TenMauGoc { get; set; }
        }
    }
}