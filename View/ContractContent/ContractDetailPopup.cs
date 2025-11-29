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
using Environmental_Monitoring.Controller.Data;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class ContractDetailPopup : Form
    {
        private int _contractId;
        private int _initialEmployeeId;
        private string _maDon;
        private RoundedDataGridView dgvDetails;
        private ResourceManager rm;
        private CultureInfo culture;

        private Label lblTitle;
        private Label lblClose;
        private RoundedButton btnSave;
        private RoundedButton btnCancel;

        private ComboBox cboEmployeeSelect;
        private Label lblEmployeeTitle;

        private Label lblProgressTitle;
        private TextBox txtProgress;

        public event Action OnDataSaved;

        public ContractDetailPopup(int contractId, string maDon, int employeeId)
        {
            _contractId = contractId;
            _maDon = maDon;
            _initialEmployeeId = employeeId;

            InitializeComponentCustom(maDon);
            InitializeLocalization();

            this.Load += ContractDetailPopup_Load;
        }

        private void ContractDetailPopup_Load(object sender, EventArgs e)
        {
            LoadEmployeesToComboBox();
            SetSelectedEmployee(_initialEmployeeId);
            LoadContractDetails();
        }

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(ContractDetailPopup).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;

            if (btnSave != null) btnSave.Text = rm.GetString("Button_Save", culture) ?? "Lưu";
            if (btnCancel != null) btnCancel.Text = rm.GetString("Button_Cancel", culture) ?? "Hủy";

            if (lblTitle != null)
            {
                string titleFormat = rm.GetString("ContractDetail_Title", culture) ?? "Chi tiết Hợp đồng: {0}";
                try { lblTitle.Text = string.Format(titleFormat, _maDon); }
                catch { lblTitle.Text = titleFormat + " " + _maDon; }
            }

            if (lblEmployeeTitle != null)
                lblEmployeeTitle.Text = rm.GetString("Business_Employee", culture) ?? "Nhân viên thụ lý:";

            if (lblProgressTitle != null)
                lblProgressTitle.Text = rm.GetString("Progress_Title", culture) ?? "Tiến độ hiện tại:";
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

            // --- NHÂN VIÊN ---
            lblEmployeeTitle = new Label();
            lblEmployeeTitle.Text = "Nhân viên thụ lý:";
            lblEmployeeTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblEmployeeTitle.AutoSize = true;
            lblEmployeeTitle.Location = new Point(50, 90);
            headerPanel.Controls.Add(lblEmployeeTitle);

            Panel borderPanel = new Panel();
            borderPanel.BackColor = Color.White;
            borderPanel.Size = new Size(300, 35);
            borderPanel.Location = new Point(220, 85);

            borderPanel.Paint += (s, e) =>
            {
                using (Pen p = new Pen(Color.Black, 2))
                {
                    e.Graphics.DrawLine(p, 0, borderPanel.Height - 1, borderPanel.Width, borderPanel.Height - 1);
                }
            };

            cboEmployeeSelect = new ComboBox();
            cboEmployeeSelect.Font = new Font("Segoe UI", 11);
            cboEmployeeSelect.Width = 298;
            cboEmployeeSelect.Location = new Point(3, 1);
            cboEmployeeSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEmployeeSelect.FlatStyle = FlatStyle.Flat;
            cboEmployeeSelect.BackColor = Color.White;

            borderPanel.Controls.Add(cboEmployeeSelect);
            headerPanel.Controls.Add(borderPanel);
            borderPanel.BringToFront();

            // --- TIẾN TRÌNH ---
            lblProgressTitle = new Label();
            lblProgressTitle.Text = "Tiến độ hiện tại:";
            lblProgressTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblProgressTitle.AutoSize = true;
            lblProgressTitle.Location = new Point(650, 90);
            headerPanel.Controls.Add(lblProgressTitle);

            txtProgress = new TextBox();
            txtProgress.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            txtProgress.Location = new Point(800, 87);
            txtProgress.Size = new Size(300, 30);
            txtProgress.ReadOnly = true;
            txtProgress.BackColor = Color.WhiteSmoke;
            txtProgress.TextAlign = HorizontalAlignment.Center;
            txtProgress.ForeColor = Color.Black;
            headerPanel.Controls.Add(txtProgress);

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
            dgvDetails.GridColor = Color.Black;
            dgvDetails.BackgroundColor = Color.White;
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
            dgvDetails.EditingControlShowing += DgvDetails_EditingControlShowing;
        }

        private void DgvDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox tb)
            {
                tb.BorderStyle = BorderStyle.None;
                tb.BackColor = Color.White;
            }
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
            catch (Exception ex) { MessageBox.Show("Lỗi tải nhân viên: " + ex.Message); }
        }

        private void SetSelectedEmployee(int targetEmployeeId)
        {
            if (targetEmployeeId <= 0) return;
            cboEmployeeSelect.SelectedValue = targetEmployeeId;
            if (cboEmployeeSelect.SelectedValue == null || Convert.ToInt32(cboEmployeeSelect.SelectedValue) != targetEmployeeId)
            {
                for (int i = 0; i < cboEmployeeSelect.Items.Count; i++)
                {
                    DataRowView drv = cboEmployeeSelect.Items[i] as DataRowView;
                    if (drv != null && Convert.ToInt32(drv["EmployeeID"]) == targetEmployeeId)
                    {
                        cboEmployeeSelect.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void LoadContractDetails()
        {
            try
            {
                // Lấy tiến độ hợp đồng
                string tienTrinhQuery = "SELECT TienTrinh FROM Contracts WHERE ContractID = @id";
                object ttObj = DataProvider.Instance.ExecuteScalar(tienTrinhQuery, new object[] { _contractId });
                int tienTrinh = (ttObj != null && ttObj != DBNull.Value) ? Convert.ToInt32(ttObj) : 0;
                txtProgress.Text = GetProgressName(tienTrinh);

                // Query lấy dữ liệu
                string q = @"SELECT r.ResultID, r.SampleID, r.ParameterID, r.GiaTri, r.TrangThaiPheDuyet, 
                                    p.TenThongSo, p.DonVi, p.GioiHanMin, p.GioiHanMax, 
                                    s.TenNenMau, s.MaMau 
                             FROM Results r
                             JOIN Parameters p ON r.ParameterID = p.ParameterID
                             JOIN EnvironmentalSamples s ON r.SampleID = s.SampleID
                             WHERE s.ContractID = @cid";

                DataTable dt = DataProvider.Instance.ExecuteQuery(q, new object[] { _contractId });
                if (dt == null) dt = new DataTable();

                if (!dt.Columns.Contains("TrangThaiHienThi")) dt.Columns.Add("TrangThaiHienThi", typeof(string));

                string approved = rm.GetString("Grid_Approved", culture) ?? "Đã duyệt";
                string notApproved = rm.GetString("Grid_NotApproved", culture) ?? "Chưa duyệt";

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        int pheDuyetStatus = 0;
                        if (row["TrangThaiPheDuyet"] != DBNull.Value) int.TryParse(row["TrangThaiPheDuyet"].ToString(), out pheDuyetStatus);
                        row["TrangThaiHienThi"] = (pheDuyetStatus == 1) ? approved : notApproved;
                    }
                }

                // Sắp xếp theo Tên nền mẫu để gộp ô
                if (dt.Rows.Count > 0 && dt.Columns.Contains("TenNenMau") && dt.Columns.Contains("TenThongSo"))
                    dt.DefaultView.Sort = "TenNenMau ASC, TenThongSo ASC";

                dgvDetails.DataSource = dt.DefaultView.ToTable();

                // Ẩn các cột không cần thiết
                string[] hiddenCols = { "ResultID", "SampleID", "ParameterID", "TrangThaiPheDuyet", "MaMau" };
                foreach (string col in hiddenCols) if (dgvDetails.Columns.Contains(col)) dgvDetails.Columns[col].Visible = false;

                // --- SẮP XẾP LẠI THỨ TỰ CỘT (QUAN TRỌNG) ---
                if (dgvDetails.Columns.Contains("TenNenMau")) dgvDetails.Columns["TenNenMau"].DisplayIndex = 0;
                if (dgvDetails.Columns.Contains("TenThongSo")) dgvDetails.Columns["TenThongSo"].DisplayIndex = 1;
                if (dgvDetails.Columns.Contains("DonVi")) dgvDetails.Columns["DonVi"].DisplayIndex = 2;
                if (dgvDetails.Columns.Contains("GioiHanMin")) dgvDetails.Columns["GioiHanMin"].DisplayIndex = 3;
                if (dgvDetails.Columns.Contains("GioiHanMax")) dgvDetails.Columns["GioiHanMax"].DisplayIndex = 4;
                if (dgvDetails.Columns.Contains("GiaTri")) dgvDetails.Columns["GiaTri"].DisplayIndex = 5;
                if (dgvDetails.Columns.Contains("TrangThaiHienThi")) dgvDetails.Columns["TrangThaiHienThi"].DisplayIndex = 6;

                // Cấu hình ReadOnly
                string[] readOnlyCols = { "TenNenMau", "TenThongSo", "DonVi", "TrangThaiHienThi", "GioiHanMin", "GioiHanMax" };
                foreach (string col in readOnlyCols)
                {
                    if (dgvDetails.Columns.Contains(col))
                    {
                        dgvDetails.Columns[col].ReadOnly = true;
                        dgvDetails.Columns[col].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    }
                }

                // Cột GiaTri cho phép sửa
                string[] editableCols = { "GiaTri" };
                foreach (string col in editableCols)
                {
                    if (dgvDetails.Columns.Contains(col))
                    {
                        dgvDetails.Columns[col].ReadOnly = false;
                        dgvDetails.Columns[col].DefaultCellStyle.BackColor = Color.White;
                        dgvDetails.Columns[col].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        dgvDetails.Columns[col].DefaultCellStyle.Padding = new Padding(2);
                    }
                }

                // Đặt Header
                if (dgvDetails.Columns.Contains("TenNenMau")) dgvDetails.Columns["TenNenMau"].HeaderText = "Tên nền mẫu";
                if (dgvDetails.Columns.Contains("TenThongSo")) dgvDetails.Columns["TenThongSo"].HeaderText = rm.GetString("Grid_ParamName", culture) ?? "Thông số";
                if (dgvDetails.Columns.Contains("DonVi")) dgvDetails.Columns["DonVi"].HeaderText = rm.GetString("Grid_Unit", culture) ?? "Đơn vị";
                if (dgvDetails.Columns.Contains("GioiHanMin")) dgvDetails.Columns["GioiHanMin"].HeaderText = "Min";
                if (dgvDetails.Columns.Contains("GioiHanMax")) dgvDetails.Columns["GioiHanMax"].HeaderText = "Max";
                if (dgvDetails.Columns.Contains("GiaTri")) dgvDetails.Columns["GiaTri"].HeaderText = rm.GetString("Grid_Value", culture) ?? "Giá trị";
                if (dgvDetails.Columns.Contains("TrangThaiHienThi")) dgvDetails.Columns["TrangThaiHienThi"].HeaderText = rm.GetString("Grid_ApprovalStatus", culture) ?? "Trạng thái";

                if (dgvDetails.Columns.Contains("TenThongSo")) dgvDetails.Columns["TenThongSo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (dgvDetails.Columns.Contains("TenNenMau")) dgvDetails.Columns["TenNenMau"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải chi tiết: " + ex.Message); }
        }

        private string GetProgressName(int tienTrinh)
        {
            switch (tienTrinh)
            {
                case 1: return "Phòng Kế Hoạch (Planning)";
                case 2: return "Phòng Hiện Trường (Scene)";
                case 3: return "Phòng Thí Nghiệm (Laboratory)";
                case 4: return "Phòng Kết Quả (Result)";
                case 5: return "Hoàn Thành (Completed)";
                default: return "Chưa xác định (Unknown)";
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

                    string updateResultQuery = @"UPDATE Results SET GiaTri = @GiaTri WHERE SampleID = @SampleID AND ParameterID = @ParameterID";
                    DataProvider.Instance.ExecuteNonQuery(updateResultQuery, new object[] { giaTri.HasValue ? (object)giaTri.Value : DBNull.Value, sampleId, paramId });

                    string updateParamQuery = @"UPDATE Parameters SET GioiHanMin = @Min, GioiHanMax = @Max WHERE ParameterID = @ParameterID";
                    DataProvider.Instance.ExecuteNonQuery(updateParamQuery, new object[] { minVal.HasValue ? (object)minVal.Value : DBNull.Value, maxVal.HasValue ? (object)maxVal.Value : DBNull.Value, paramId });
                }
                this.Cursor = Cursors.Default;
                MessageBox.Show($"Đã lưu thành công dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnDataSaved?.Invoke();
                LoadContractDetails();
            }
            catch (Exception ex) { this.Cursor = Cursors.Default; MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void DgvDetails_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);

            // Gộp ô cho cột TenNenMau (Cột đầu tiên)
            bool isMergeColumn = (dgvDetails.Columns[e.ColumnIndex].Name == "TenNenMau");

            using (Pen gridPen = new Pen(dgvDetails.GridColor, 1))
            {
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

                // Vẽ đường kẻ
                e.Graphics.DrawLine(gridPen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                if (e.ColumnIndex == 0) e.Graphics.DrawLine(gridPen, e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Left, e.CellBounds.Bottom);
                if (drawBottomLine) e.Graphics.DrawLine(gridPen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                if (e.RowIndex == 0) e.Graphics.DrawLine(gridPen, e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Top);
            }

            if (e.Value != null)
            {
                string textToDraw = e.Value.ToString();
                using (Brush textBrush = new SolidBrush(Color.Black))
                {
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter };
                    if (isMergeColumn)
                    {
                        int startRow = e.RowIndex;
                        while (startRow > 0 && dgvDetails.Rows[startRow - 1].Cells[e.ColumnIndex].Value?.ToString() == textToDraw) startRow--;
                        int endRow = e.RowIndex;
                        while (endRow < dgvDetails.Rows.Count - 1 && dgvDetails.Rows[endRow + 1].Cells[e.ColumnIndex].Value?.ToString() == textToDraw) endRow++;
                        int totalHeight = 0;
                        int yOffset = 0;
                        for (int i = startRow; i <= endRow; i++)
                        {
                            int h = dgvDetails.Rows[i].Height;
                            if (i < e.RowIndex) yOffset += h;
                            totalHeight += h;
                        }
                        Rectangle groupRect = new Rectangle(e.CellBounds.X, e.CellBounds.Y - yOffset, e.CellBounds.Width, totalHeight);
                        e.Graphics.DrawString(textToDraw, e.CellStyle.Font, textBrush, groupRect, sf);
                    }
                    else e.Graphics.DrawString(textToDraw, e.CellStyle.Font, textBrush, e.CellBounds, sf);
                }
            }
        }
    }
}