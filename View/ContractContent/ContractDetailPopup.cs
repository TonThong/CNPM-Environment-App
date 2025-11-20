using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Environmental_Monitoring.Controller;
using Environmental_Monitoring.View.Components;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class ContractDetailPopup : Form
    {
        private int _contractId;
        private int _initialEmployeeId;
        private RoundedDataGridView dgvDetails;
        private ResourceManager rm;
        private CultureInfo culture;

        private Label lblTitle;
        private Label lblClose;
        private RoundedButton btnSave;
        private RoundedButton btnCancel;

        private ComboBox cboEmployeeSelect;
        private Label lblEmployeeTitle;

        public event Action OnDataSaved;

        // Constructor nhận tham số employeeId
        public ContractDetailPopup(int contractId, string maDon, int employeeId)
        {
            _contractId = contractId;
            _initialEmployeeId = employeeId; // Lưu ID nhận từ Form Manager

            InitializeComponentCustom(maDon);
            InitializeLocalization();

            // QUAN TRỌNG: Dùng sự kiện Load để đảm bảo UI đã khởi tạo xong trước khi nạp dữ liệu
            this.Load += ContractDetailPopup_Load;
        }

        private void ContractDetailPopup_Load(object sender, EventArgs e)
        {
            // 1. Nạp danh sách nhân viên vào ComboBox trước
            LoadEmployeesToComboBox();

            // 2. Chọn nhân viên theo ID đã truyền vào (Logic gán cứng)
            SetSelectedEmployee(_initialEmployeeId);

            // 3. Cuối cùng mới tải chi tiết hợp đồng
            LoadContractDetails();
        }

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(ManagerContent).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;

            if (btnSave != null) btnSave.Text = rm.GetString("Button_Save", culture) ?? "Lưu";
            if (btnCancel != null) btnCancel.Text = rm.GetString("Button_Cancel", culture) ?? "Hủy";
        }

        private void InitializeComponentCustom(string maDon)
        {
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.MediumSeaGreen;
            this.Padding = new Padding(2);

            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = Color.White;
            this.Controls.Add(mainPanel);

            // --- HEADER ---
            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 140;
            headerPanel.BackColor = Color.White;
            mainPanel.Controls.Add(headerPanel);

            lblClose = new Label();
            lblClose.Text = "X";
            lblClose.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblClose.ForeColor = Color.Gray;
            lblClose.TextAlign = ContentAlignment.MiddleCenter;
            lblClose.Size = new Size(40, 40);
            lblClose.Location = new Point(headerPanel.Width - 45, 5);
            lblClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblClose.Cursor = Cursors.Hand;
            lblClose.Click += (s, e) => this.Close();
            lblClose.MouseEnter += (s, e) => { lblClose.ForeColor = Color.Red; lblClose.BackColor = Color.WhiteSmoke; };
            lblClose.MouseLeave += (s, e) => { lblClose.ForeColor = Color.Gray; lblClose.BackColor = Color.White; };
            headerPanel.Controls.Add(lblClose);

            lblTitle = new Label();
            lblTitle.Text = $"Chi tiết Hợp đồng: {maDon}";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 128, 0);
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 60;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            headerPanel.Controls.Add(lblTitle);

            // --- CHỌN NHÂN VIÊN ---
            lblEmployeeTitle = new Label();
            lblEmployeeTitle.Text = "Nhân viên thụ lý:";
            lblEmployeeTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblEmployeeTitle.AutoSize = true;
            lblEmployeeTitle.Location = new Point(120, 90);
            headerPanel.Controls.Add(lblEmployeeTitle);
            lblEmployeeTitle.BringToFront();

            Panel borderPanel = new Panel();
            borderPanel.BackColor = Color.White;
            borderPanel.Size = new Size(320, 35);
            borderPanel.Location = new Point(300, 85);

            borderPanel.Paint += (s, e) =>
            {
                using (Pen p = new Pen(Color.Black, 2))
                {
                    e.Graphics.DrawLine(p, 0, borderPanel.Height - 1, borderPanel.Width, borderPanel.Height - 1);
                }
            };

            cboEmployeeSelect = new ComboBox();
            cboEmployeeSelect.Font = new Font("Segoe UI", 11);
            cboEmployeeSelect.Width = 318;
            cboEmployeeSelect.Location = new Point(1, 1);
            cboEmployeeSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEmployeeSelect.FlatStyle = FlatStyle.Flat;
            cboEmployeeSelect.BackColor = Color.White;

            borderPanel.Controls.Add(cboEmployeeSelect);
            headerPanel.Controls.Add(borderPanel);
            borderPanel.BringToFront();

            // --- FOOTER ---
            Panel footerPanel = new Panel();
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Height = 70;
            footerPanel.BackColor = Color.WhiteSmoke;
            mainPanel.Controls.Add(footerPanel);

            btnSave = new RoundedButton();
            btnSave.Text = "Lưu";
            btnSave.Size = new Size(150, 45);
            btnSave.BorderRadius = 5;
            btnSave.Location = new Point(mainPanel.Width / 2 - 160, 12);
            btnSave.Anchor = AnchorStyles.None;
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.ForeColor = Color.White;
            btnSave.Click += BtnSave_Click;
            footerPanel.Controls.Add(btnSave);

            btnCancel = new RoundedButton();
            btnCancel.Text = "Hủy";
            btnCancel.Size = new Size(150, 45);
            btnCancel.BorderRadius = 5;
            btnCancel.Location = new Point(mainPanel.Width / 2 + 10, 12);
            btnCancel.Anchor = AnchorStyles.None;
            btnCancel.BackColor = Color.Gray;
            btnCancel.ForeColor = Color.White;
            btnCancel.Click += (s, e) => this.Close();
            footerPanel.Controls.Add(btnCancel);

            // --- GRIDVIEW ---
            dgvDetails = new RoundedDataGridView();
            dgvDetails.Dock = DockStyle.Fill;

            Panel gridContainer = new Panel();
            gridContainer.Dock = DockStyle.Fill;
            gridContainer.Padding = new Padding(20, 10, 20, 10);
            gridContainer.Controls.Add(dgvDetails);
            mainPanel.Controls.Add(gridContainer);
            gridContainer.BringToFront();

            dgvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvDetails.RowHeadersVisible = false;
            dgvDetails.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvDetails.BackgroundColor = Color.White;
            dgvDetails.GridColor = Color.Black;
            dgvDetails.AllowUserToAddRows = false;
            dgvDetails.ReadOnly = false;
            dgvDetails.EditMode = DataGridViewEditMode.EditOnEnter;

            try
            {
                Type dgvType = dgvDetails.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                if (pi != null) pi.SetValue(dgvDetails, true, null);
            }
            catch { }

            dgvDetails.CellPainting += DgvDetails_CellPainting;
            dgvDetails.DataError += (s, e) => { e.ThrowException = false; };
        }

        private void LoadEmployeesToComboBox()
        {
            try
            {
                string query = "SELECT EmployeeID, HoTen FROM Employees";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);

                cboEmployeeSelect.DataSource = dt;
                cboEmployeeSelect.DisplayMember = "HoTen";
                cboEmployeeSelect.ValueMember = "EmployeeID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhân viên: " + ex.Message);
            }
        }

        private void SetSelectedEmployee(int targetEmployeeId)
        {
            if (targetEmployeeId <= 0) return;

            // Cách 1: Gán trực tiếp (thường dùng nếu kiểu dữ liệu khớp hoàn toàn)
            cboEmployeeSelect.SelectedValue = targetEmployeeId;

            // Cách 2: Duyệt qua từng item để tìm ID khớp (An toàn tuyệt đối nếu kiểu dữ liệu int/long bị lệch)
            if (cboEmployeeSelect.SelectedValue == null || Convert.ToInt32(cboEmployeeSelect.SelectedValue) != targetEmployeeId)
            {
                for (int i = 0; i < cboEmployeeSelect.Items.Count; i++)
                {
                    DataRowView drv = cboEmployeeSelect.Items[i] as DataRowView;
                    if (drv != null)
                    {
                        // Ép kiểu về int để so sánh chính xác
                        int idInList = Convert.ToInt32(drv["EmployeeID"]);
                        if (idInList == targetEmployeeId)
                        {
                            cboEmployeeSelect.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        private void LoadContractDetails()
        {
            try
            {
                string q = QueryRepository.LoadContractResults;
                DataTable dt = DataProvider.Instance.ExecuteQuery(q, new object[] { _contractId });

                if (dt == null) dt = new DataTable();

                if (!dt.Columns.Contains("KetQua")) dt.Columns.Add("KetQua", typeof(string));
                if (!dt.Columns.Contains("TrangThaiHienThi")) dt.Columns.Add("TrangThaiHienThi", typeof(string));

                string polluted = rm.GetString("Grid_Polluted", culture) ?? "Ô nhiễm";
                string soonPolluted = rm.GetString("Grid_SoonPolluted", culture) ?? "Sắp ô nhiễm";
                string notPolluted = rm.GetString("Grid_NotPolluted", culture) ?? "Không ô nhiễm";
                string approved = rm.GetString("Grid_Approved", culture) ?? "Đã duyệt";
                string notApproved = rm.GetString("Grid_NotApproved", culture) ?? "Chưa duyệt";

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        int onhiemStatus = (row["ONhiem"] != DBNull.Value) ? Convert.ToInt32(row["ONhiem"]) : 0;
                        switch (onhiemStatus)
                        {
                            case 1: row["KetQua"] = polluted; break;
                            case 2: row["KetQua"] = soonPolluted; break;
                            default: row["KetQua"] = notPolluted; break;
                        }

                        int pheDuyetStatus = 0;
                        if (row["TrangThaiPheDuyet"] != DBNull.Value) int.TryParse(row["TrangThaiPheDuyet"].ToString(), out pheDuyetStatus);
                        row["TrangThaiHienThi"] = (pheDuyetStatus == 1) ? approved : notApproved;
                    }
                }

                if (dt.Rows.Count > 0 && dt.Columns.Contains("MauKiemNghiem") && dt.Columns.Contains("TenThongSo"))
                {
                    dt.DefaultView.Sort = "MauKiemNghiem ASC, TenThongSo ASC";
                }

                dgvDetails.DataSource = dt.DefaultView.ToTable();

                string[] hiddenCols = { "SampleID", "ParameterID", "ONhiem", "TrangThaiPheDuyet", "Status", "ContractID", "MaDon", "NgayKy", "NgayTraKetQua", "TenDoanhNghiep", "TenNguoiDaiDien", "TenNhanVien", "TrangThaiHopDong", "EmployeeID" };
                foreach (string col in hiddenCols)
                {
                    if (dgvDetails.Columns.Contains(col)) dgvDetails.Columns[col].Visible = false;
                }

                string[] readOnlyCols = { "MauKiemNghiem", "TenThongSo", "DonVi", "KetQua", "TrangThaiHienThi" };
                foreach (string col in readOnlyCols)
                {
                    if (dgvDetails.Columns.Contains(col))
                    {
                        dgvDetails.Columns[col].ReadOnly = true;
                        dgvDetails.Columns[col].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    }
                }

                string[] editableCols = { "GiaTri", "GioiHanMin", "GioiHanMax" };
                foreach (string col in editableCols)
                {
                    if (dgvDetails.Columns.Contains(col))
                    {
                        dgvDetails.Columns[col].ReadOnly = false;
                        dgvDetails.Columns[col].DefaultCellStyle.BackColor = Color.White;
                        dgvDetails.Columns[col].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    }
                }

                if (dgvDetails.Columns.Contains("MauKiemNghiem")) dgvDetails.Columns["MauKiemNghiem"].HeaderText = rm.GetString("Grid_Sample", culture) ?? "Mẫu";
                if (dgvDetails.Columns.Contains("TenThongSo")) dgvDetails.Columns["TenThongSo"].HeaderText = rm.GetString("Grid_ParamName", culture) ?? "Thông số";
                if (dgvDetails.Columns.Contains("DonVi")) dgvDetails.Columns["DonVi"].HeaderText = rm.GetString("Grid_Unit", culture) ?? "Đơn vị";
                if (dgvDetails.Columns.Contains("GioiHanMin")) dgvDetails.Columns["GioiHanMin"].HeaderText = "Min";
                if (dgvDetails.Columns.Contains("GioiHanMax")) dgvDetails.Columns["GioiHanMax"].HeaderText = "Max";
                if (dgvDetails.Columns.Contains("GiaTri")) dgvDetails.Columns["GiaTri"].HeaderText = rm.GetString("Grid_Value", culture) ?? "Giá trị";
                if (dgvDetails.Columns.Contains("KetQua")) dgvDetails.Columns["KetQua"].HeaderText = rm.GetString("Grid_Result", culture) ?? "Kết quả";
                if (dgvDetails.Columns.Contains("TrangThaiHienThi")) dgvDetails.Columns["TrangThaiHienThi"].HeaderText = rm.GetString("Grid_ApprovalStatus", culture) ?? "Trạng thái";

                if (dgvDetails.Columns.Contains("cboEmployee")) dgvDetails.Columns.Remove("cboEmployee");

                UpdateResultColors();

                if (dgvDetails.Columns.Contains("TenThongSo")) dgvDetails.Columns["TenThongSo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (dgvDetails.Columns.Contains("MauKiemNghiem")) dgvDetails.Columns["MauKiemNghiem"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết: " + ex.Message);
            }
        }

        private void UpdateResultColors()
        {
            string polluted = rm.GetString("Grid_Polluted", culture) ?? "Ô nhiễm";
            string soonPolluted = rm.GetString("Grid_SoonPolluted", culture) ?? "Sắp ô nhiễm";

            foreach (DataGridViewRow dgRow in dgvDetails.Rows)
            {
                if (dgRow.Cells["KetQua"] == null || dgRow.Cells["KetQua"].Value == null) continue;

                var cell = dgRow.Cells["KetQua"];
                string v = cell.Value?.ToString() ?? string.Empty;
                if (v == polluted) { cell.Style.BackColor = Color.Red; cell.Style.ForeColor = Color.White; }
                else if (v == soonPolluted) { cell.Style.BackColor = Color.Orange; cell.Style.ForeColor = Color.White; }
                else if (!string.IsNullOrEmpty(v)) { cell.Style.BackColor = Color.Green; cell.Style.ForeColor = Color.White; }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (cboEmployeeSelect.SelectedValue != null)
                {
                    int newEmpID = Convert.ToInt32(cboEmployeeSelect.SelectedValue);
                    string updateContractQuery = "UPDATE Contracts SET EmployeeID = @EmpID WHERE ContractID = @ContractID";
                    DataProvider.Instance.ExecuteNonQuery(updateContractQuery, new object[] { newEmpID, _contractId });
                }

                foreach (DataGridViewRow row in dgvDetails.Rows)
                {
                    if (row.IsNewRow) continue;

                    int sampleId = Convert.ToInt32(row.Cells["SampleID"].Value);
                    int paramId = Convert.ToInt32(row.Cells["ParameterID"].Value);

                    object valObj = row.Cells["GiaTri"].Value;
                    double? giaTri = (valObj != null && valObj != DBNull.Value && double.TryParse(valObj.ToString(), out double v)) ? v : (double?)null;

                    object minObj = row.Cells["GioiHanMin"].Value;
                    double? minVal = (minObj != null && minObj != DBNull.Value && double.TryParse(minObj.ToString(), out double min)) ? min : (double?)null;

                    object maxObj = row.Cells["GioiHanMax"].Value;
                    double? maxVal = (maxObj != null && maxObj != DBNull.Value && double.TryParse(maxObj.ToString(), out double max)) ? max : (double?)null;

                    string updateResultQuery = @"
                        UPDATE Results 
                        SET GiaTri = @GiaTri 
                        WHERE SampleID = @SampleID AND ParameterID = @ParameterID";

                    DataProvider.Instance.ExecuteNonQuery(updateResultQuery, new object[] {
                        giaTri.HasValue ? (object)giaTri.Value : DBNull.Value,
                        sampleId,
                        paramId
                    });

                    string updateParamQuery = @"
                        UPDATE Parameters 
                        SET GioiHanMin = @Min, GioiHanMax = @Max 
                        WHERE ParameterID = @ParameterID";

                    DataProvider.Instance.ExecuteNonQuery(updateParamQuery, new object[] {
                        minVal.HasValue ? (object)minVal.Value : DBNull.Value,
                        maxVal.HasValue ? (object)maxVal.Value : DBNull.Value,
                        paramId
                    });
                }

                this.Cursor = Cursors.Default;
                MessageBox.Show($"Đã lưu thành công dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                OnDataSaved?.Invoke();
                LoadContractDetails();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvDetails_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);

            bool isMergeColumn = (dgvDetails.Columns[e.ColumnIndex].Name == "MauKiemNghiem");

            using (Pen gridPen = new Pen(dgvDetails.GridColor, 1))
            {
                e.Graphics.DrawLine(gridPen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);

                if (e.ColumnIndex == 0)
                {
                    e.Graphics.DrawLine(gridPen, e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Left, e.CellBounds.Bottom);
                }

                bool drawBottomLine = true;
                if (isMergeColumn)
                {
                    if (e.RowIndex < dgvDetails.Rows.Count - 1)
                    {
                        string currVal = e.Value?.ToString();
                        string nextVal = dgvDetails.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value?.ToString();
                        if (currVal == nextVal) drawBottomLine = false;
                    }
                }

                if (drawBottomLine)
                {
                    e.Graphics.DrawLine(gridPen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                }

                if (e.RowIndex == 0)
                {
                    e.Graphics.DrawLine(gridPen, e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Top);
                }
            }

            bool isPrevSame = false;
            if (isMergeColumn && e.RowIndex > 0)
            {
                string currVal = e.Value?.ToString();
                string prevVal = dgvDetails.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value?.ToString();
                if (currVal == prevVal) isPrevSame = true;
            }

            if (e.Value != null)
            {
                if (isMergeColumn && isPrevSame)
                {
                    return;
                }

                string textToDraw = e.Value.ToString();

                using (Brush textBrush = new SolidBrush(Color.Black))
                {
                    Rectangle textRect = e.CellBounds;
                    e.Graphics.DrawString(textToDraw, e.CellStyle.Font, textBrush, textRect, new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                        FormatFlags = StringFormatFlags.NoWrap,
                        Trimming = StringTrimming.EllipsisCharacter
                    });
                }
            }
        }
    }
}