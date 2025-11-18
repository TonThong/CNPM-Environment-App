using Environmental_Monitoring.Controller.Data;
using Environmental_Monitoring.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.Components
{
    public partial class CreateUpdateContract : Form
    {
        private int contractId = 0;
        private DataTable dtResults;
        private ResourceManager rm;
        private CultureInfo culture;

        public CreateUpdateContract(int contractId = 0)
        {
            InitializeComponent();
            this.contractId = contractId;

            // Khởi tạo ResourceManager để lấy tên cột đa ngôn ngữ
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(CreateUpdateContract).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;

            this.Load += CreateUpdateContract_Load;

            this.Text = "Chỉnh Sửa Chi Tiết Hợp Đồng";
            this.StartPosition = FormStartPosition.CenterParent;
            

            this.StartPosition = FormStartPosition.CenterScreen; // Hiển thị giữa màn hình
            this.Size = new Size(940, 570); // Đặt kích thước cố định mong muốn

            // Các thuộc tính khóa kích thước:
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Viền cố định, không cho kéo thay đổi kích thước
            this.MaximizeBox = false; // Vô hiệu hóa nút Phóng to (Maximize)
            this.MinimizeBox = false; // (Tùy chọn) Vô hiệu hóa nút Thu nhỏ
        }

        private void CreateUpdateContract_Load(object sender, EventArgs e)
        {
            // Tạo nút Save nếu chưa có
            if (this.Controls.Find("btnSave", true).Length == 0)
            {
                Button btnSave = new Button();
                btnSave.Name = "btnSave";
                btnSave.Text = "Lưu Thay Đổi";
                btnSave.Size = new Size(120, 40);
                // Đặt nút ở góc dưới bên phải, neo theo form
                btnSave.Location = new Point(this.ClientSize.Width - 140, this.ClientSize.Height - 60);
                btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                btnSave.BackColor = Color.ForestGreen;
                btnSave.ForeColor = Color.White;
                btnSave.Click += btnSave_Click;
                this.Controls.Add(btnSave);
            }
            else
            {
                Control btn = this.Controls.Find("btnSave", true)[0];
                btn.Click += btnSave_Click;
            }

            if (contractId > 0)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            try
            {
                // Câu truy vấn lấy ĐẦY ĐỦ thông tin như bên ResultContent
                // Lấy thêm ResultID, SampleID, ParameterID để phục vụ việc Update
                string query = @"
                    SELECT 
                        c.MaDon, c.NgayKy, c.NgayTraKetQua, c.Status,
                        cu.TenDoanhNghiep, cu.TenNguoiDaiDien,
                        e.HoTen AS TenNhanVien,
                        CONCAT(s.MaMau, ' - ', t.TenMau) AS MauKiemNghiem,
                        p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax,
                        r.GiaTri,
                        
                        -- Các cột ẩn dùng để xử lý logic
                        r.ResultID, s.SampleID, p.ParameterID,
                        p.ONhiem, r.TrangThaiPheDuyet
                    FROM Contracts c
                    JOIN Customers cu ON c.CustomerID = cu.CustomerID
                    JOIN Employees e ON c.EmployeeID = e.EmployeeID
                    JOIN EnvironmentalSamples s ON s.ContractID = c.ContractID
                    JOIN SampleTemplates t ON s.TemplateID = t.TemplateID
                    JOIN TemplateParameters tp ON t.TemplateID = tp.TemplateID
                    JOIN Parameters p ON tp.ParameterID = p.ParameterID
                    LEFT JOIN Results r ON r.SampleID = s.SampleID AND r.ParameterID = p.ParameterID
                    WHERE c.ContractID = @contractId
                    ORDER BY c.MaDon, s.MaMau, p.TenThongSo";

                dtResults = DataProvider.Instance.ExecuteQuery(query, new object[] { this.contractId });

                if (dtResults == null) return;

                // Thêm các cột tính toán (chưa có trong DB)
                if (!dtResults.Columns.Contains("KetQua"))
                    dtResults.Columns.Add("KetQua", typeof(string));
                if (!dtResults.Columns.Contains("TrangThaiHienThi"))
                    dtResults.Columns.Add("TrangThaiHienThi", typeof(string));
                if (!dtResults.Columns.Contains("TrangThaiHopDong"))
                    dtResults.Columns.Add("TrangThaiHopDong", typeof(string));

                // Lấy chuỗi ngôn ngữ
                string polluted = rm.GetString("Grid_Polluted", culture);
                string soonPolluted = rm.GetString("Grid_SoonPolluted", culture);
                string notPolluted = rm.GetString("Grid_NotPolluted", culture);
                string approved = rm.GetString("Grid_Approved", culture);
                string notApproved = rm.GetString("Grid_NotApproved", culture);
                string status_Completed = rm.GetString("Status_Completed", culture);
                string status_Expired = rm.GetString("Status_Expired", culture);
                string status_Overdue = rm.GetString("Status_Overdue", culture);
                string status_Active = rm.GetString("Status_Active", culture);

                // Tính toán giá trị cho các cột mới
                foreach (DataRow row in dtResults.Rows)
                {
                    // 1. Tính Kết Quả (Ô nhiễm)
                    int onhiemStatus = 0;
                    if (row["ONhiem"] != DBNull.Value)
                        onhiemStatus = Convert.ToInt32(row["ONhiem"]);

                    switch (onhiemStatus)
                    {
                        case 1: row["KetQua"] = polluted; break;
                        case 2: row["KetQua"] = soonPolluted; break;
                        default: row["KetQua"] = notPolluted; break;
                    }

                    // 2. Tính Trạng Thái Duyệt
                    int pheDuyetStatus = 0;
                    if (row["TrangThaiPheDuyet"] != DBNull.Value)
                        int.TryParse(row["TrangThaiPheDuyet"].ToString(), out pheDuyetStatus);

                    row["TrangThaiHienThi"] = (pheDuyetStatus == 1) ? approved : notApproved;

                    // 3. Tính Trạng Thái Hợp Đồng
                    string statusHopDongDB = row["Status"].ToString();
                    DateTime ngayTra = Convert.ToDateTime(row["NgayTraKetQua"]);
                    if (statusHopDongDB == "Completed")
                        row["TrangThaiHopDong"] = status_Completed;
                    else if (statusHopDongDB == "Expired")
                        row["TrangThaiHopDong"] = status_Expired;
                    else if (DateTime.Now > ngayTra)
                        row["TrangThaiHopDong"] = status_Overdue;
                    else
                        row["TrangThaiHopDong"] = status_Active;
                }

                dataGridView1.DataSource = dtResults;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGrid()
        {
            if (dataGridView1.Columns.Count == 0) return;

            // Ẩn các cột ID kỹ thuật
            string[] hiddenCols = { "ResultID", "SampleID", "ParameterID", "ONhiem", "TrangThaiPheDuyet", "Status" };
            foreach (string col in hiddenCols)
            {
                if (dataGridView1.Columns.Contains(col)) dataGridView1.Columns[col].Visible = false;
            }

            // Đặt tên cột hiển thị (Header Text) từ Resource
            SetHeader("MaDon", "Grid_ContractCode");
            SetHeader("NgayKy", "Grid_SignDate");
            SetHeader("NgayTraKetQua", "Grid_DueDate");
            SetHeader("TenDoanhNghiep", "PDF_Company");
            SetHeader("TenNguoiDaiDien", "PDF_Representative");
            SetHeader("TenNhanVien", "Business_Employee");
            SetHeader("MauKiemNghiem", "Grid_Sample");
            SetHeader("TenThongSo", "Grid_ParamName");
            SetHeader("GioiHanMin", "Grid_Min");
            SetHeader("GioiHanMax", "Grid_Max");
            SetHeader("DonVi", "Grid_Unit");
            SetHeader("GiaTri", "Grid_Value");
            SetHeader("KetQua", "Grid_Result");
            SetHeader("TrangThaiHienThi", "Grid_ApprovalStatus");
            SetHeader("TrangThaiHopDong", "Grid_ContractStatus");

            // Cấu hình ReadOnly (Chỉ cho phép sửa Min, Max, Giá trị)
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Name == "GioiHanMin" || col.Name == "GioiHanMax" || col.Name == "GiaTri" || col.Name == "TenNhanVien")
                {
                    col.ReadOnly = false; // Cho phép sửa
                    col.DefaultCellStyle.BackColor = Color.LightYellow; // Đánh dấu màu nhạt để biết là sửa được
                }
                else
                {
                    col.ReadOnly = true; 
                }
            }

            // Cấu hình hiển thị Grid
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Cho phép thanh cuộn ngang
            dataGridView1.ScrollBars = ScrollBars.Both; // Hiển thị cả 2 thanh cuộn
            dataGridView1.AllowUserToAddRows = false;

            // Tự động chỉnh độ rộng cột cho đẹp
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Visible) col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
        }

        private void SetHeader(string colName, string resourceKey)
        {
            if (dataGridView1.Columns.Contains(colName))
            {
                dataGridView1.Columns[colName].HeaderText = rm.GetString(resourceKey, culture) ?? colName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtResults == null) return;

            try
            {
                int updateCount = 0;

                foreach (DataRow row in dtResults.Rows)
                {
                    // Chỉ xử lý các dòng có thay đổi
                    if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added)
                    {
                        int sampleId = Convert.ToInt32(row["SampleID"]);
                        int parameterId = Convert.ToInt32(row["ParameterID"]);

                        // 1. Lưu Giá Trị Đo (Results Table)
                        object giaTriObj = row["GiaTri"];
                        decimal? giaTri = null;
                        if (giaTriObj != DBNull.Value && !string.IsNullOrWhiteSpace(giaTriObj.ToString()))
                        {
                            if (decimal.TryParse(giaTriObj.ToString(), out decimal val)) giaTri = val;
                        }

                        // Kiểm tra tồn tại
                        object existId = DataProvider.Instance.ExecuteScalar(
                            "SELECT ResultID FROM Results WHERE SampleID = @sid AND ParameterID = @pid",
                            new object[] { sampleId, parameterId });

                        if (existId != null) // Đã có -> Update
                        {
                            if (giaTri.HasValue)
                            {
                                DataProvider.Instance.ExecuteNonQuery(
                                    "UPDATE Results SET GiaTri = @val, NgayPhanTich = CURRENT_TIMESTAMP WHERE ResultID = @rid",
                                    new object[] { giaTri.Value, existId });
                            }
                            else
                            {
                                DataProvider.Instance.ExecuteNonQuery(
                                    "UPDATE Results SET GiaTri = NULL WHERE ResultID = @rid",
                                    new object[] { existId });
                            }
                        }
                        else if (giaTri.HasValue) // Chưa có -> Insert
                        {
                            DataProvider.Instance.ExecuteNonQuery(
                                "INSERT INTO Results (SampleID, ParameterID, GiaTri, NgayPhanTich) VALUES (@sid, @pid, @val, CURRENT_TIMESTAMP)",
                                new object[] { sampleId, parameterId, giaTri.Value });
                        }

                        // 2. Lưu Min/Max (Parameters Table)
                        object minObj = row["GioiHanMin"];
                        object maxObj = row["GioiHanMax"];

                        DataProvider.Instance.ExecuteNonQuery(
                            "UPDATE Parameters SET GioiHanMin = @min, GioiHanMax = @max WHERE ParameterID = @pid",
                            new object[] {
                                minObj == DBNull.Value ? (object)DBNull.Value : minObj,
                                maxObj == DBNull.Value ? (object)DBNull.Value : maxObj,
                                parameterId
                            });

                        updateCount++;
                    }
                }

                if (updateCount > 0)
                {
                    MessageBox.Show($"Đã cập nhật thành công {updateCount} dòng dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không có thay đổi nào để lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}