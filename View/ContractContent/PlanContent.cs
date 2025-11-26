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

        // Khởi tạo đa ngôn ngữ
        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(PlanContent).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;
        }

        // Sự kiện khi UserControl được tải
        private void PlanContent_Load(object sender, EventArgs e)
        {
            // Tìm FlowLayoutPanel chứa các nút mẫu
            flpTemplates = this.Controls.Find("flpTemplates", true).FirstOrDefault() as FlowLayoutPanel;

            // Cấu hình bảng hiển thị (Grid)
            SetupMainGrid();

            // Đăng ký sự kiện cho nút Lưu để tránh gán trùng lặp
            if (roundedButton2 != null)
            {
                roundedButton2.Click -= btnSaveContract_Click;
                roundedButton2.Click += btnSaveContract_Click;
            }

            // Đăng ký sự kiện nút Hủy
            var btnCancel = this.Controls.Find("btnCancel", true).FirstOrDefault();
            if (btnCancel != null) btnCancel.Click += btnCancel_Click;

            // Tải danh sách các mẫu môi trường lên giao diện
            LoadSampleTemplates();

            // Cập nhật ngôn ngữ và trạng thái nút
            UpdateUIText();
            UpdateSaveButtonState();
        }

        // Cấu hình hiển thị cho bảng dữ liệu chính (DataGridView)
        private void SetupMainGrid()
        {
            roundedDataGridView1.AutoGenerateColumns = false;
            roundedDataGridView1.AllowUserToAddRows = false;
            roundedDataGridView1.Columns.Clear();
            roundedDataGridView1.RowHeadersVisible = false;
            roundedDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            roundedDataGridView1.GridColor = Color.Black; // Đặt màu lưới là Đen
            roundedDataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            // --- 1. CỘT NỀN MẪU (Đưa lên đầu tiên, căn giữa để gộp ô đẹp hơn) ---
            var colNenMau = new DataGridViewTextBoxColumn
            {
                HeaderText = "Nền mẫu",
                DataPropertyName = "TenNenMau",
                Width = 150
            };
            colNenMau.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa
            roundedDataGridView1.Columns.Add(colNenMau);

            // --- 2. CÁC CỘT CÒN LẠI ---
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

            // Style chung: Chữ đen, Header in đậm
            roundedDataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            roundedDataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Đăng ký lại các sự kiện click, format
            roundedDataGridView1.CellContentClick -= roundedDataGridView1_CellContentClick;
            roundedDataGridView1.CellContentClick += roundedDataGridView1_CellContentClick;

            roundedDataGridView1.CellFormatting -= roundedDataGridView1_CellFormatting;
            roundedDataGridView1.CellFormatting += roundedDataGridView1_CellFormatting;

            roundedDataGridView1.CellDoubleClick -= roundedDataGridView1_CellDoubleClick;
            roundedDataGridView1.CellDoubleClick += roundedDataGridView1_CellDoubleClick;

            // --- QUAN TRỌNG: Đăng ký sự kiện vẽ lại ô để thực hiện Gộp ô (Merge) ---
            roundedDataGridView1.CellPainting -= roundedDataGridView1_CellPainting;
            roundedDataGridView1.CellPainting += roundedDataGridView1_CellPainting;
        }

        // Hàm vẽ lại ô để Gộp các dòng giống nhau ở cột "Nền mẫu"
        // Thay thế hàm roundedDataGridView1_CellPainting cũ bằng hàm này
        private void roundedDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 1. Chỉ xử lý cột "Nền mẫu" (Index 0)
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            e.Handled = true; // Ngăn Grid vẽ mặc định để ta tự vẽ

            // 2. Vẽ nền trắng
            using (Brush backBrush = new SolidBrush(Color.White))
            {
                e.Graphics.FillRectangle(backBrush, e.CellBounds);
            }

            // 3. Logic tìm nhóm gộp (Giữ nguyên logic cũ)
            string currentValue = e.Value?.ToString();

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

            // 4. Tính toán vị trí vẽ chữ (Giữ nguyên logic cũ)
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

            // 5. Vẽ chữ
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

            // 6. VẼ ĐƯỜNG KẺ MÀU ĐEN (Sửa đoạn này)
            // Dùng Color.Black thay vì Color.LightGray
            using (Pen gridPen = new Pen(Color.Black, 1))
            {
                // Vẽ đường dọc bên phải (ngăn cách với cột Ký hiệu)
                e.Graphics.DrawLine(gridPen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);

                // Vẽ đường ngang bên dưới (CHỈ vẽ nếu là dòng cuối cùng của nhóm)
                // Đây chính là logic "trừ các dòng cùng cột ô của cột nền mẫu"
                if (e.RowIndex == endIndex)
                {
                    e.Graphics.DrawLine(gridPen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                }
            }
        }

        // Tải danh sách nút bấm chọn mẫu môi trường
        // Thay thế hàm LoadSampleTemplates cũ bằng hàm này
        private void LoadSampleTemplates()
        {
            if (flpTemplates == null) return;

            try
            {
                // 1. Lấy tất cả template từ DB
                string q = "SELECT TemplateID, TenMau FROM SampleTemplates WHERE TenMau NOT LIKE 'Template cho %'";
                DataTable dt = DataProvider.Instance.ExecuteQuery(q);

                flpTemplates.Controls.Clear();

                if (dt == null || dt.Rows.Count == 0) return;

                // 2. Định nghĩa danh sách 3 môi trường cố định cần hiển thị
                // Key: Tên hiển thị chuẩn, Value: Dữ liệu tìm được từ DB
                var targetEnvs = new string[] { "Air Environment", "Water Environment", "Soil Environment" };
                var foundTemplates = new Dictionary<string, (int id, string originalName)>();

                // 3. Quét DB để tìm ID tương ứng cho từng môi trường
                foreach (DataRow row in dt.Rows)
                {
                    int id = Convert.ToInt32(row["TemplateID"]);
                    string nameGoc = row["TenMau"].ToString();
                    string nameChuan = GetLocalizedTemplateName(nameGoc); // Hàm này sẽ quy về Air/Water/Soil

                    // Nếu tên thuộc 1 trong 3 loại kia VÀ chưa tìm thấy trước đó (tránh trùng lặp)
                    if (targetEnvs.Contains(nameChuan) && !foundTemplates.ContainsKey(nameChuan))
                    {
                        foundTemplates[nameChuan] = (id, nameGoc);
                    }
                }

                // 4. Tạo nút bấm theo đúng thứ tự cố định (Air -> Water -> Soil)
                foreach (string envName in targetEnvs)
                {
                    // Chỉ tạo nút nếu trong DB có template tương ứng
                    if (foundTemplates.ContainsKey(envName))
                    {
                        var data = foundTemplates[envName];

                        Button btn = new Button();
                        btn.Text = envName; // Luôn hiển thị tên chuẩn (Air Environment...)

                        // Tag lưu ID thật lấy từ DB
                        btn.Tag = new SampleTemplateDisplayItem
                        {
                            TemplateID = data.id,
                            TenMauHienThi = envName,
                            TenMauGoc = data.originalName
                        };

                        // --- Style nút bấm (Giữ nguyên style cũ) ---
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

                        flpTemplates.Controls.Add(btn);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowAlert("Lỗi tải template: " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        // Xử lý khi bấm nút chọn mẫu (Mở form SampleInformation)
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

        // Xử lý double click để sửa mẫu
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

        // Xử lý nút xóa mẫu
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

        // Hiển thị số lượng thông số trong cột Grid
        private void roundedDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && roundedDataGridView1.Columns[e.ColumnIndex].Name == "colCount")
            {
                var sample = _listSamples[e.RowIndex];
                e.Value = sample.Parameters.Count;
            }
        }

        // Hàm cập nhật dữ liệu cho Grid
        private void BindMainGrid()
        {

            _listSamples = _listSamples.OrderBy(x => x.TenNenMau)
                               .ThenBy(x => x.KyHieuMau)
                               .ToList();

            roundedDataGridView1.DataSource = null;
            roundedDataGridView1.DataSource = _listSamples;
        }

        // Logic Lưu Hợp Đồng vào Database
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
                    // 1. Tạo Template riêng cho mẫu này
                    // Logic mới: Ưu tiên dùng tên người dùng nhập, nếu trống thì tự sinh tên
                    string specificTemplateName = !string.IsNullOrEmpty(sample.TenNenMau)
                                                  ? sample.TenNenMau
                                                  : $"Template cho {maDon} - {sample.KyHieuMau}";

                    string insertTempQ = "INSERT INTO SampleTemplates (TenMau) VALUES (@name)";
                    DataProvider.Instance.ExecuteNonQuery(insertTempQ, new object[] { specificTemplateName });
                    int newTemplateID = Convert.ToInt32(DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null));

                    // 2. Lưu các thông số của mẫu
                    foreach (var p in sample.Parameters)
                    {
                        string insertParam = @"INSERT INTO Parameters (TenThongSo, DonVi, GioiHanMin, GioiHanMax, PhuongPhap, QuyChuan, PhuTrach, ONhiem)
                                               VALUES (@ten, @dv, @min, @max, @pp, @qc, @pt, 0)";
                        DataProvider.Instance.ExecuteNonQuery(insertParam, new object[] {
                            p.TenThongSo, p.DonVi, p.Min, p.Max, p.PhuongPhap, p.QuyChuan, p.PhuTrach
                        });
                        int newParamID = Convert.ToInt32(DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null));

                        // Link thông số vào Template
                        string linkQ = "INSERT INTO TemplateParameters (TemplateID, ParameterID) VALUES (@tid, @pid)";
                        DataProvider.Instance.ExecuteNonQuery(linkQ, new object[] { newTemplateID, newParamID });
                    }

                    // 3. Tạo Mẫu môi trường (Lưu vào bảng EnvironmentalSamples)
                    string maMauFull = $"{maDon} - {sample.KyHieuMau}";

                    // Logic mới: Lưu thêm cột TenNenMau để phục vụ xuất PDF
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
                        sample.TenNenMau // Lưu giá trị thực tế của textbox vào cột này
                    });
                }

                // Cập nhật tiến trình Hợp đồng
                string updateContract = "UPDATE Contracts SET TienTrinh = 2 WHERE ContractID = @id";
                DataProvider.Instance.ExecuteNonQuery(updateContract, new object[] { currentContractId });

                // Gửi thông báo
                int hienTruongRoleID = 10;
                string noiDung = $"Hợp đồng '{maDon}' đã lập kế hoạch lấy mẫu xong.";
                NotificationService.CreateNotification("ChinhSua", noiDung, hienTruongRoleID, this.currentContractId, null);

                string successMsg = string.Format(rm.GetString("Plan_SaveSuccess", culture) ?? "Lưu thành công cho HĐ {0}", maDon);
                ShowAlert(successMsg, AlertPanel.AlertType.Success);

                // Reset giao diện sau khi lưu
                _listSamples.Clear();
                BindMainGrid();
                currentContractId = 0;
                lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
                UpdateSaveButtonState();
            }
            catch (Exception ex)
            {
                ShowAlert("Lỗi lưu dữ liệu: " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        // Cập nhật text trên giao diện (đa ngôn ngữ)
        public void UpdateUIText()
        {
            culture = Thread.CurrentThread.CurrentUICulture;
            //if (lbContractID != null) lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture) + (currentContractId != 0 ? " " + currentContractId : "");

            if (currentContractId == 0)
            {
                lbContractID.Text = "Khách Hàng:"; // Hoặc lấy từ Resource nếu muốn đa ngôn ngữ
            }

            if (btnContracts != null) btnContracts.Text = rm.GetString("Plan_ContractListButton", culture);
            if (roundedButton2 != null) roundedButton2.Text = rm.GetString("Button_Save", culture);
            LoadSampleTemplates();
        }

        // Xử lý mở popup chọn hợp đồng
        private void btnContracts_Click(object sender, EventArgs e)
        {
            try
            {
                // Query lấy danh sách hợp đồng cho Popup (Giữ nguyên)
                string query = "SELECT ContractID, MaDon, NgayKy, NgayTraKetQua, Status, IsUnlocked FROM Contracts WHERE TienTrinh = 1";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);

                using (PopUpContract popup = new PopUpContract(dt))
                {
                    popup.ContractSelected += (contractId) =>
                    {
                        this.currentContractId = contractId;

                        // --- THÊM ĐOẠN NÀY ĐỂ LẤY TÊN KHÁCH HÀNG ---
                        string cusQuery = @"
                    SELECT cus.TenDoanhNghiep 
                    FROM Contracts c
                    JOIN Customers cus ON c.CustomerID = cus.CustomerID
                    WHERE c.ContractID = @cid";

                        object result = DataProvider.Instance.ExecuteScalar(cusQuery, new object[] { contractId });
                        string tenCongTy = result != null ? result.ToString() : "Không xác định";

                        // Hiển thị lên Label
                        lbContractID.Text = "Khách Hàng: " + tenCongTy;
                        // -------------------------------------------

                        UpdateSaveButtonState();
                    };
                    popup.ShowDialog();
                }
            }
            catch (Exception ex) { ShowAlert("Lỗi tải HĐ: " + ex.Message, AlertPanel.AlertType.Error); }
        }

        // Xử lý nút Hủy bỏ toàn bộ thao tác
        private void btnCancel_Click(object sender, EventArgs e)
        {
            _listSamples.Clear();
            BindMainGrid();
            currentContractId = 0;
            lbContractID.Text = rm.GetString("Plan_ContractIDLabel", culture);
            UpdateSaveButtonState();
        }

        // Cập nhật trạng thái nút Lưu (chỉ sáng khi đã chọn HĐ)
        private void UpdateSaveButtonState()
        {
            bool isContractSelected = (currentContractId != 0);
            if (roundedButton2 != null) roundedButton2.Enabled = isContractSelected;
        }

        // Hàm hiển thị thông báo chung
        private void ShowAlert(string message, AlertPanel.AlertType type)
        {
            var mainLayout = this.FindForm() as Mainlayout;
            if (mainLayout != null) mainLayout.ShowGlobalAlert(message, type);
            else MessageBox.Show(message, type.ToString(), MessageBoxButtons.OK, type == AlertPanel.AlertType.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        // Hàm dịch tên Template (nếu cần)
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

        // Class phụ trợ để chứa thông tin hiển thị nút
        public class SampleTemplateDisplayItem
        {
            public int TemplateID { get; set; }
            public string TenMauHienThi { get; set; }
            public string TenMauGoc { get; set; }
        }
    }
}