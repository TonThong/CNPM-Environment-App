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

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(PlanContent).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;
        }

        private void PlanContent_Load(object sender, EventArgs e)
        {
            SetupMainGrid();

            if (roundedButton2 != null)
            {
                roundedButton2.Click -= btnSaveContract_Click;
                roundedButton2.Click += btnSaveContract_Click;
            }

            var btnCancel = this.Controls.Find("btnCancel", true).FirstOrDefault();
            if (btnCancel != null) btnCancel.Click += btnCancel_Click;

            if (btnThemMau != null)
            {
                btnThemMau.Click -= btnThemMau_Click;
                btnThemMau.Click += btnThemMau_Click;
            }

            UpdateUIText();
            UpdateSaveButtonState();
        }

        private void btnThemMau_Click(object sender, EventArgs e)
        {
            if (currentContractId == 0)
            {
                ShowAlert(rm.GetString("Plan_SelectContract", culture) ?? "Vui lòng chọn Hợp đồng trước!", AlertPanel.AlertType.Error);
                return;
            }

            using (var frm = new SampleInformation(editSample: null))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _listSamples.Add(frm.ResultSample);
                    BindMainGrid();
                }
            }
        }

        // --- [CẬP NHẬT] Cấu hình Grid để hiện vạch kẻ ---
        private void SetupMainGrid()
        {
            roundedDataGridView1.AutoGenerateColumns = false;
            roundedDataGridView1.AllowUserToAddRows = false;
            roundedDataGridView1.Columns.Clear();
            roundedDataGridView1.RowHeadersVisible = false;
            roundedDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 1. Bật vạch kẻ lưới (Single)
            roundedDataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            // 2. Chọn màu vạch kẻ (Màu xám nhạt hoặc Đen tùy bạn, ở đây chọn Silver cho thanh thoát)
            roundedDataGridView1.GridColor = Color.Silver;

            // Cấu hình màu chữ
            roundedDataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            roundedDataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            roundedDataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Cột Nền Mẫu
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

            DataGridViewButtonColumn btnDel = new DataGridViewButtonColumn();
            btnDel.Name = "colDelete";
            btnDel.HeaderText = "Xóa";
            btnDel.Text = "X";
            btnDel.UseColumnTextForButtonValue = true;
            btnDel.DefaultCellStyle.ForeColor = Color.Red;
            roundedDataGridView1.Columns.Add(btnDel);

            roundedDataGridView1.CellContentClick += roundedDataGridView1_CellContentClick;
            roundedDataGridView1.CellPainting += roundedDataGridView1_CellPainting;
            roundedDataGridView1.CellDoubleClick += roundedDataGridView1_CellDoubleClick;
            roundedDataGridView1.CellFormatting += roundedDataGridView1_CellFormatting;
        }

        // --- [CẬP NHẬT] Vẽ lại ô Nền mẫu để khớp với vạch kẻ ---
        private void roundedDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Chỉ xử lý cột 0 (Nền mẫu)
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            e.Handled = true; // Ngăn Grid vẽ mặc định cho ô này

            // Vẽ nền trắng
            using (Brush backBrush = new SolidBrush(Color.White))
            {
                e.Graphics.FillRectangle(backBrush, e.CellBounds);
            }

            string currentValue = e.Value?.ToString();

            // Logic tìm nhóm gộp (Start - End)
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

            // Tính toán vị trí Text
            int totalHeight = 0;
            for (int i = startIndex; i <= endIndex; i++) totalHeight += roundedDataGridView1.Rows[i].Height;

            int offsetY = 0;
            for (int i = startIndex; i < e.RowIndex; i++) offsetY -= roundedDataGridView1.Rows[i].Height;

            Rectangle groupRect = new Rectangle(e.CellBounds.X, e.CellBounds.Y + offsetY, e.CellBounds.Width, totalHeight);

            // Vẽ Text
            TextRenderer.DrawText(e.Graphics, e.FormattedValue?.ToString(), e.CellStyle.Font, groupRect, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            // --- VẼ VẠCH KẺ THỦ CÔNG CHO Ô GỘP ---
            // Sử dụng màu GridColor đã cài đặt ở SetupMainGrid
            using (Pen gridPen = new Pen(roundedDataGridView1.GridColor, 1))
            {
                // 1. Luôn vẽ đường dọc bên phải (Ngăn cách cột Nền mẫu và Ký hiệu)
                e.Graphics.DrawLine(gridPen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);

                // 2. Chỉ vẽ đường ngang bên dưới nếu là DÒNG CUỐI CÙNG của nhóm
                if (e.RowIndex == endIndex)
                {
                    e.Graphics.DrawLine(gridPen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                }

                // (Các dòng ở giữa nhóm sẽ không được vẽ đường ngang -> tạo hiệu ứng gộp ô)
            }
        }

        private void roundedDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var sampleToEdit = _listSamples[e.RowIndex];

            using (var frm = new SampleInformation(editSample: sampleToEdit))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _listSamples[e.RowIndex] = frm.ResultSample;
                    BindMainGrid();
                }
            }
        }

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

        private void roundedDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && roundedDataGridView1.Columns[e.ColumnIndex].Name == "colCount")
            {
                e.Value = _listSamples[e.RowIndex].Parameters.Count;
            }
        }

        private void BindMainGrid()
        {
            _listSamples = _listSamples.OrderBy(x => x.TenNenMau).ThenBy(x => x.KyHieuMau).ToList();
            roundedDataGridView1.DataSource = null;
            roundedDataGridView1.DataSource = _listSamples;
        }

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
                    string specificTemplateName = $"Template cho {maDon} - {sample.KyHieuMau}";
                    string insertTempQ = "INSERT INTO SampleTemplates (TenMau) VALUES (@name)";
                    DataProvider.Instance.ExecuteNonQuery(insertTempQ, new object[] { specificTemplateName });
                    int newTemplateID = Convert.ToInt32(DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null));

                    foreach (var p in sample.Parameters)
                    {
                        int finalParamID = p.ParameterID;
                        if (finalParamID == 0)
                        {
                            string insertParam = @"INSERT INTO Parameters (TenThongSo, DonVi, GioiHanMin, GioiHanMax, PhuongPhap, QuyChuan, PhuTrach, ONhiem)
                                                   VALUES (@ten, @dv, @min, @max, @pp, @qc, @pt, 0)";
                            DataProvider.Instance.ExecuteNonQuery(insertParam, new object[] {
                                p.TenThongSo, p.DonVi, p.Min, p.Max, p.PhuongPhap, p.QuyChuan, p.PhuTrach
                            });
                            finalParamID = Convert.ToInt32(DataProvider.Instance.ExecuteScalar("SELECT LAST_INSERT_ID()", null));
                        }
                        string linkQ = "INSERT INTO TemplateParameters (TemplateID, ParameterID) VALUES (@tid, @pid)";
                        DataProvider.Instance.ExecuteNonQuery(linkQ, new object[] { newTemplateID, finalParamID });
                    }

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

                string updateContract = "UPDATE Contracts SET TienTrinh = 2 WHERE ContractID = @id";
                DataProvider.Instance.ExecuteNonQuery(updateContract, new object[] { currentContractId });

                ShowAlert("Lưu kế hoạch thành công!", AlertPanel.AlertType.Success);

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

        public void UpdateUIText()
        {
            culture = Thread.CurrentThread.CurrentUICulture;
            if (currentContractId == 0) lbContractID.Text = "Khách Hàng:";
            if (btnContracts != null) btnContracts.Text = rm.GetString("Plan_ContractListButton", culture);
            if (roundedButton2 != null) roundedButton2.Text = rm.GetString("Button_Save", culture);
        }

        private void btnContracts_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT c.ContractID, c.MaDon, c.NgayKy, c.NgayTraKetQua, c.Status, c.IsUnlocked, cus.TenDoanhNghiep 
                 FROM Contracts c 
                 JOIN Customers cus ON c.CustomerID = cus.CustomerID 
                 WHERE c.TienTrinh = 1";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);
                using (PopUpContract popup = new PopUpContract(dt))
                {
                    popup.ContractSelected += (contractId) =>
                    {
                        this.currentContractId = contractId;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _listSamples.Clear();
            BindMainGrid();
            currentContractId = 0;
            lbContractID.Text = "Khách Hàng:";
            UpdateSaveButtonState();
        }

        private void UpdateSaveButtonState()
        {
            bool isContractSelected = (currentContractId != 0);
            if (roundedButton2 != null) roundedButton2.Enabled = isContractSelected;
        }

        private void ShowAlert(string message, AlertPanel.AlertType type)
        {
            var mainLayout = this.FindForm() as Mainlayout;
            if (mainLayout != null) mainLayout.ShowGlobalAlert(message, type);
            else MessageBox.Show(message, type.ToString(), MessageBoxButtons.OK, type == AlertPanel.AlertType.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }
    }
}