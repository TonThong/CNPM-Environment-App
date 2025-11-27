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

        // Sự kiện khi UserControl được load
        private void PlanContent_Load(object sender, EventArgs e)
        {
            SetupMainGrid(); // Cấu hình bảng

            // Đăng ký sự kiện nút Lưu Hợp Đồng
            if (roundedButton2 != null)
            {
                roundedButton2.Click -= btnSaveContract_Click;
                roundedButton2.Click += btnSaveContract_Click;
            }

            // Đăng ký sự kiện nút Hủy
            var btnCancel = this.Controls.Find("btnCancel", true).FirstOrDefault();
            if (btnCancel != null) btnCancel.Click += btnCancel_Click;

            // --- [MỚI] ĐĂNG KÝ SỰ KIỆN CHO NÚT "THÊM MẪU" ---
            if (btnThemMau != null)
            {
                btnThemMau.Click -= btnThemMau_Click;
                btnThemMau.Click += btnThemMau_Click;
            }

            UpdateUIText();
            UpdateSaveButtonState();
        }

        // --- SỰ KIỆN MỚI: BẤM NÚT THÊM MẪU ---
        private void btnThemMau_Click(object sender, EventArgs e)
        {
            // Kiểm tra đã chọn hợp đồng chưa
            if (currentContractId == 0)
            {
                ShowAlert(rm.GetString("Plan_SelectContract", culture) ?? "Vui lòng chọn Hợp đồng trước!", AlertPanel.AlertType.Error);
                return;
            }

            // Gọi Popup ở chế độ THÊM MỚI (tham số editSample = null)
            using (var frm = new SampleInformation(editSample: null))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Thêm mẫu mới vào danh sách và cập nhật Grid
                    _listSamples.Add(frm.ResultSample);
                    BindMainGrid();
                }
            }
        }

        // Cấu hình GridView hiển thị danh sách mẫu đã thêm
        private void SetupMainGrid()
        {
            roundedDataGridView1.AutoGenerateColumns = false;
            roundedDataGridView1.AllowUserToAddRows = false;
            roundedDataGridView1.Columns.Clear();
            roundedDataGridView1.RowHeadersVisible = false;
            roundedDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // --- CẤU HÌNH MÀU CHỮ ĐEN ---
            roundedDataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            roundedDataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            roundedDataGridView1.RowsDefaultCellStyle.ForeColor = Color.Black;

            // Style chung: Header in đậm
            roundedDataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            // -----------------------------

            // Cột Nền Mẫu (sẽ được gộp ô nếu giống nhau)
            var colNenMau = new DataGridViewTextBoxColumn
            {
                HeaderText = "Nền mẫu",
                DataPropertyName = "TenNenMau",
                Width = 150
            };
            colNenMau.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            roundedDataGridView1.Columns.Add(colNenMau);

            roundedDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ký hiệu", DataPropertyName = "KyHieuMau", Width = 80 });
            roundedDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Vị trí lấy mẫu", DataPropertyName = "ViTri", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            roundedDataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "SL Thông số", Name = "colCount", Width = 100 });

            // Cột Xóa
            DataGridViewButtonColumn btnDel = new DataGridViewButtonColumn();
            btnDel.Name = "colDelete";
            btnDel.HeaderText = "Xóa";
            btnDel.Text = "X";
            btnDel.UseColumnTextForButtonValue = true;
            btnDel.DefaultCellStyle.ForeColor = Color.Red; // Nút xóa màu đỏ
            roundedDataGridView1.Columns.Add(btnDel);

            // Đăng ký sự kiện
            roundedDataGridView1.CellContentClick += roundedDataGridView1_CellContentClick;
            roundedDataGridView1.CellPainting += roundedDataGridView1_CellPainting; // Sự kiện vẽ gộp ô
            roundedDataGridView1.CellDoubleClick += roundedDataGridView1_CellDoubleClick; // Sự kiện sửa
            roundedDataGridView1.CellFormatting += roundedDataGridView1_CellFormatting;
        }

        // Vẽ lại Grid để gộp các ô có cùng "Nền mẫu"
        private void roundedDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;
            e.Handled = true;

            using (Brush backBrush = new SolidBrush(Color.White))
            {
                e.Graphics.FillRectangle(backBrush, e.CellBounds);
            }

            string currentValue = e.Value?.ToString();

            // Tìm vị trí bắt đầu và kết thúc của nhóm cần gộp
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
            for (int i = startIndex; i <= endIndex; i++) totalHeight += roundedDataGridView1.Rows[i].Height;

            int offsetY = 0;
            for (int i = startIndex; i < e.RowIndex; i++) offsetY -= roundedDataGridView1.Rows[i].Height;

            Rectangle groupRect = new Rectangle(e.CellBounds.X, e.CellBounds.Y + offsetY, e.CellBounds.Width, totalHeight);

            // Vẽ Text - Ép buộc màu chữ là Color.Black
            TextRenderer.DrawText(e.Graphics, e.FormattedValue?.ToString(), e.CellStyle.Font, groupRect, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            // Vẽ đường kẻ
            using (Pen gridPen = new Pen(Color.Black, 1))
            {
                e.Graphics.DrawLine(gridPen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                if (e.RowIndex == endIndex) e.Graphics.DrawLine(gridPen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
            }
        }

        // Sự kiện: Double Click vào dòng để Sửa Mẫu
        private void roundedDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var sampleToEdit = _listSamples[e.RowIndex];

            // Mở Popup ở chế độ SỬA (truyền object sampleToEdit vào)
            using (var frm = new SampleInformation(editSample: sampleToEdit))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _listSamples[e.RowIndex] = frm.ResultSample;
                    BindMainGrid();
                }
            }
        }

        // Sự kiện: Click nút Xóa
        private void roundedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && roundedDataGridView1.Columns[e.ColumnIndex].Name == "colDelete")
            {
                if (MessageBox.Show("Xóa mẫu này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _listSamples.RemoveAt(e.RowIndex);
                    BindMainGrid();
                }
            }
        }

        // Hiển thị số lượng thông số
        private void roundedDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && roundedDataGridView1.Columns[e.ColumnIndex].Name == "colCount")
            {
                e.Value = _listSamples[e.RowIndex].Parameters.Count;
            }
        }

        // Refresh lại dữ liệu trên Grid
        private void BindMainGrid()
        {
            // Sắp xếp theo Tên nền mẫu để hàm gộp ô hoạt động đúng
            _listSamples = _listSamples.OrderBy(x => x.TenNenMau).ThenBy(x => x.KyHieuMau).ToList();
            roundedDataGridView1.DataSource = null;
            roundedDataGridView1.DataSource = _listSamples;
        }

        // Logic Lưu toàn bộ Hợp đồng và Mẫu vào Database
        private void btnSaveContract_Click(object sender, EventArgs e)
        {
            if (currentContractId == 0) return;
            if (_listSamples.Count == 0)
            {
                ShowAlert("Danh sách mẫu trống!", AlertPanel.AlertType.Error);
                return;
            }

            try
            {
                string maDon = DataProvider.Instance.ExecuteScalar("SELECT MaDon FROM Contracts WHERE ContractID = @id", new object[] { currentContractId }).ToString();

                foreach (var sample in _listSamples)
                {
                    // 1. Tạo Template riêng cho mẫu cụ thể này (SampleTemplates)
                    string specificTemplateName = $"Template cho {maDon} - {sample.KyHieuMau}";
                    string insertTempQ = "INSERT INTO SampleTemplates (TenMau) VALUES (@name)";
                    DataProvider.Instance.ExecuteNonQuery(insertTempQ, new object[] { specificTemplateName });
                    int newTemplateID = Convert.ToInt32(DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null));

                    // 2. Lưu các thông số
                    foreach (var p in sample.Parameters)
                    {
                        int finalParamID = p.ParameterID;

                        // Nếu là thông số mới thêm thủ công (ID = 0) -> Insert mới vào bảng Parameters
                        if (finalParamID == 0)
                        {
                            string insertParam = @"INSERT INTO Parameters (TenThongSo, DonVi, GioiHanMin, GioiHanMax, PhuongPhap, QuyChuan, PhuTrach, ONhiem)
                                                   VALUES (@ten, @dv, @min, @max, @pp, @qc, @pt, 0)";
                            DataProvider.Instance.ExecuteNonQuery(insertParam, new object[] {
                                p.TenThongSo, p.DonVi, p.Min, p.Max, p.PhuongPhap, p.QuyChuan, p.PhuTrach
                            });
                            finalParamID = Convert.ToInt32(DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null));
                        }

                        // 3. Liên kết thông số với Template
                        string linkQ = "INSERT INTO TemplateParameters (TemplateID, ParameterID) VALUES (@tid, @pid)";
                        DataProvider.Instance.ExecuteNonQuery(linkQ, new object[] { newTemplateID, finalParamID });
                    }

                    // 4. Lưu Mẫu môi trường (EnvironmentalSamples)
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
                        sample.TenNenMau // Lưu tên người dùng nhập tay
                    });
                }

                // Cập nhật trạng thái Hợp đồng -> Đã lập kế hoạch
                string updateContract = "UPDATE Contracts SET TienTrinh = 2 WHERE ContractID = @id";
                DataProvider.Instance.ExecuteNonQuery(updateContract, new object[] { currentContractId });

                // Thông báo thành công
                ShowAlert("Lưu kế hoạch thành công!", AlertPanel.AlertType.Success);

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
        }

        // Sự kiện: Mở Popup chọn Hợp đồng
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
                        // Lấy tên khách hàng
                        string cusQuery = @"SELECT cus.TenDoanhNghiep FROM Contracts c JOIN Customers cus ON c.CustomerID = cus.CustomerID WHERE c.ContractID = @cid";
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

        // Sự kiện: Nút Hủy
        private void btnCancel_Click(object sender, EventArgs e)
        {
            _listSamples.Clear();
            BindMainGrid();
            currentContractId = 0;
            lbContractID.Text = "Khách Hàng:";
            UpdateSaveButtonState();
        }

        // Bật/Tắt nút Lưu
        private void UpdateSaveButtonState()
        {
            bool isContractSelected = (currentContractId != 0);
            if (roundedButton2 != null) roundedButton2.Enabled = isContractSelected;
        }

        // Hàm hiển thị thông báo
        private void ShowAlert(string message, AlertPanel.AlertType type)
        {
            var mainLayout = this.FindForm() as Mainlayout;
            if (mainLayout != null) mainLayout.ShowGlobalAlert(message, type);
            else MessageBox.Show(message, type.ToString(), MessageBoxButtons.OK, type == AlertPanel.AlertType.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }
    }
}