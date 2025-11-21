using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Environmental_Monitoring.Controller;
using Environmental_Monitoring.View.Components;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Linq;
using System.Reflection;

namespace Environmental_Monitoring.View.ContractContent
{
    public partial class ManagerContent : UserControl
    {
        private ResourceManager rm;
        private CultureInfo culture;

        public ManagerContent()
        {
            InitializeComponent();
            InitializeLocalization();

            SetupDataGridView();

            this.Load += ManagerContent_Load;
        }

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(ManagerContent).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;
        }

        private void SetupDataGridView()
        {
            dgvManager.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvManager.BackgroundColor = Color.White;
            dgvManager.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvManager.ReadOnly = true;
            dgvManager.AllowUserToAddRows = false;
            dgvManager.RowHeadersVisible = false;

            dgvManager.DefaultCellStyle.ForeColor = Color.Black;
            dgvManager.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvManager.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            dgvManager.CellBorderStyle = DataGridViewCellBorderStyle.None;

            try
            {
                Type dgvType = dgvManager.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                if (pi != null) pi.SetValue(dgvManager, true, null);
            }
            catch { }

            dgvManager.CellPainting += DgvManager_CellPainting;
            dgvManager.CellContentClick += DgvManager_CellContentClick;
        }

        private void ManagerContent_Load(object sender, EventArgs e)
        {
            LoadContractsList();
            UpdateUIText();
        }

        public void UpdateUIText()
        {
            culture = Thread.CurrentThread.CurrentUICulture;

            if (dgvManager.Columns.Contains("ContractID")) dgvManager.Columns["ContractID"].HeaderText = "ID";
            if (dgvManager.Columns.Contains("MaDon")) dgvManager.Columns["MaDon"].HeaderText = rm.GetString("Grid_ContractCode", culture) ?? "Mã HĐ";
            if (dgvManager.Columns.Contains("NgayKy")) dgvManager.Columns["NgayKy"].HeaderText = rm.GetString("Grid_SignDate", culture) ?? "Ngày Ký";
            if (dgvManager.Columns.Contains("NgayTraKetQua")) dgvManager.Columns["NgayTraKetQua"].HeaderText = rm.GetString("Grid_DueDate", culture) ?? "Ngày Trả";
            if (dgvManager.Columns.Contains("TenDoanhNghiep")) dgvManager.Columns["TenDoanhNghiep"].HeaderText = rm.GetString("PDF_Company", culture) ?? "Công Ty";
            if (dgvManager.Columns.Contains("TenNguoiDaiDien")) dgvManager.Columns["TenNguoiDaiDien"].HeaderText = rm.GetString("PDF_Representative", culture) ?? "Đại Diện";
            if (dgvManager.Columns.Contains("TenNhanVien")) dgvManager.Columns["TenNhanVien"].HeaderText = rm.GetString("Business_Employee", culture) ?? "Nhân Viên Thụ Lý";
            if (dgvManager.Columns.Contains("MaMau")) dgvManager.Columns["MaMau"].HeaderText = rm.GetString("Grid_Sample", culture) ?? "Mẫu";
            if (dgvManager.Columns.Contains("Detail")) dgvManager.Columns["Detail"].HeaderText = rm.GetString("Grid_Detail", culture) ?? "Chi Tiết";
        }

        private void LoadContractsList()
        {
            try
            {
                string query = @"
                    SELECT 
                        c.ContractID,
                        c.MaDon,
                        c.NgayKy,
                        c.NgayTraKetQua,
                        cus.TenDoanhNghiep,
                        cus.TenNguoiDaiDien,
                        c.EmployeeID, 
                        emp.HoTen as TenNhanVien,
                        samp.MaMau
                    FROM Contracts c
                    LEFT JOIN Customers cus ON c.CustomerID = cus.CustomerID
                    LEFT JOIN Employees emp ON c.EmployeeID = emp.EmployeeID
                    LEFT JOIN EnvironmentalSamples samp ON samp.ContractID = c.ContractID
                    ORDER BY c.ContractID DESC, samp.SampleID ASC";

                DataTable dt = DataProvider.Instance.ExecuteQuery(query);
                dgvManager.DataSource = dt;

                if (dgvManager.Columns["ContractID"] != null) dgvManager.Columns["ContractID"].Visible = false;
                if (dgvManager.Columns.Contains("EmployeeID")) dgvManager.Columns["EmployeeID"].Visible = false;

                if (!dgvManager.Columns.Contains("Detail"))
                {
                    DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
                    btnCol.Name = "Detail";
                    btnCol.HeaderText = "Chi Tiết";
                    btnCol.Text = "Xem";
                    btnCol.UseColumnTextForButtonValue = true;
                    dgvManager.Columns.Add(btnCol);
                }

                dgvManager.Columns["Detail"].DisplayIndex = dgvManager.Columns.Count - 1;

                UpdateUIText();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách hợp đồng: " + ex.Message);
            }
        }

        private void DgvManager_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvManager.Columns[e.ColumnIndex].Name == "Detail")
            {
                OpenDetailPopup(e.RowIndex);
            }
        }

        private void OpenDetailPopup(int rowIndex)
        {
            try
            {
                int contractId = Convert.ToInt32(dgvManager.Rows[rowIndex].Cells["ContractID"].Value);
                string maDon = dgvManager.Rows[rowIndex].Cells["MaDon"].Value.ToString();

                int employeeId = 0;
                if (dgvManager.Columns.Contains("EmployeeID"))
                {
                    var val = dgvManager.Rows[rowIndex].Cells["EmployeeID"].Value;
                    if (val != null && val != DBNull.Value)
                    {
                        int.TryParse(val.ToString(), out employeeId);
                    }
                }

                using (var popup = new ContractDetailPopup(contractId, maDon, employeeId))
                {
                    popup.OnDataSaved += () =>
                    {
                        LoadContractsList();
                    };

                    popup.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở chi tiết: " + ex.Message);
            }
        }

        private void DgvManager_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            e.Handled = true;

            bool isSelected = (e.State & DataGridViewElementStates.Selected) != 0;
            bool isSampleColumn = dgvManager.Columns[e.ColumnIndex].Name == "MaMau";

            Color backColor = Color.White;
            Color textColor = Color.Black; 

            if (isSelected && isSampleColumn)
            {
                backColor = SystemColors.Highlight;
                textColor = SystemColors.HighlightText;
            }

            using (Brush backBrush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(backBrush, e.CellBounds);
            }

            TextFormatFlags flags = TextFormatFlags.HorizontalCenter |
                                    TextFormatFlags.VerticalCenter |
                                    TextFormatFlags.SingleLine |
                                    TextFormatFlags.EndEllipsis |
                                    TextFormatFlags.NoPrefix |
                                    TextFormatFlags.PreserveGraphicsClipping;

            string[] mergeColumns = { "MaDon", "NgayKy", "NgayTraKetQua", "TenDoanhNghiep", "TenNguoiDaiDien", "TenNhanVien", "Detail" };
            bool isMergeColumn = mergeColumns.Contains(dgvManager.Columns[e.ColumnIndex].Name);

            using (Pen gridPen = new Pen(Color.LightGray, 1))
            {
                if (!isMergeColumn)
                {
                    string val = e.Value?.ToString();
                    if (e.Value is DateTime dtVal) val = dtVal.ToString("dd/MM/yyyy");

                    TextRenderer.DrawText(e.Graphics, val, e.CellStyle.Font, e.CellBounds, textColor, flags);

                    e.Graphics.DrawLine(gridPen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                    e.Graphics.DrawLine(gridPen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                    return;
                }

                int contractId = Convert.ToInt32(dgvManager.Rows[e.RowIndex].Cells["ContractID"].Value);

                int startIndex = e.RowIndex;
                while (startIndex > 0 && Convert.ToInt32(dgvManager.Rows[startIndex - 1].Cells["ContractID"].Value) == contractId)
                {
                    startIndex--;
                }

                int endIndex = e.RowIndex;
                while (endIndex < dgvManager.Rows.Count - 1 && Convert.ToInt32(dgvManager.Rows[endIndex + 1].Cells["ContractID"].Value) == contractId)
                {
                    endIndex++;
                }

                int totalHeight = 0;
                for (int i = startIndex; i <= endIndex; i++) totalHeight += dgvManager.Rows[i].Height;

                int offsetY = 0;
                for (int i = startIndex; i < e.RowIndex; i++) offsetY -= dgvManager.Rows[i].Height;

                Rectangle groupRect = new Rectangle(e.CellBounds.X, e.CellBounds.Y + offsetY, e.CellBounds.Width, totalHeight);

                if (dgvManager.Columns[e.ColumnIndex].Name == "Detail")
                {
                    int btnW = 60;
                    int btnH = 25;
                    Rectangle btnRect = new Rectangle(
                        groupRect.X + (groupRect.Width - btnW) / 2,
                        groupRect.Y + (groupRect.Height - btnH) / 2,
                        btnW, btnH);

                    using (Brush btnBrush = new SolidBrush(Color.FromArgb(40, 167, 69)))
                    {
                        e.Graphics.FillRectangle(btnBrush, btnRect);
                    }
                    TextRenderer.DrawText(e.Graphics, "Xem", new Font(e.CellStyle.Font, FontStyle.Bold), btnRect, Color.White, flags);
                }
                else
                {
                    string val = e.Value?.ToString();
                    if (e.Value is DateTime dtVal) val = dtVal.ToString("dd/MM/yyyy");

                    TextRenderer.DrawText(e.Graphics, val, e.CellStyle.Font, groupRect, textColor, flags);
                }

                e.Graphics.DrawLine(gridPen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom); 
                if (e.RowIndex == endIndex)
                {
                    e.Graphics.DrawLine(gridPen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1); // Kẻ ngang đáy
                }
            }
        }
    }
}