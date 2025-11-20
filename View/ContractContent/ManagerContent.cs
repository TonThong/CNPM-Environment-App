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
            if (dgvManager.Columns.Contains("TenNhanVien")) dgvManager.Columns["TenNhanVien"].HeaderText = rm.GetString("Business_Employee", culture) ?? "NV Thụ Lý";
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
            int contractId = Convert.ToInt32(dgvManager.Rows[rowIndex].Cells["ContractID"].Value);
            string maDon = dgvManager.Rows[rowIndex].Cells["MaDon"].Value.ToString();

            using (var popup = new ContractDetailPopup(contractId, maDon))
            {
                // Đăng ký sự kiện: Khi popup lưu xong -> Tải lại danh sách bên ngoài
                popup.OnDataSaved += () =>
                {
                    LoadContractsList();
                };

                popup.ShowDialog();
            }
        }

        private void DgvManager_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string[] mergeColumns = { "MaDon", "NgayKy", "NgayTraKetQua", "TenDoanhNghiep", "TenNguoiDaiDien", "TenNhanVien", "Detail" };
            bool isMergeColumn = mergeColumns.Contains(dgvManager.Columns[e.ColumnIndex].Name);

            if (!isMergeColumn)
            {
                e.Handled = false;
                return;
            }

            e.Handled = true;
            e.PaintBackground(e.CellBounds, true);

            int currentContractId = Convert.ToInt32(dgvManager.Rows[e.RowIndex].Cells["ContractID"].Value);

            using (Pen gridPen = new Pen(dgvManager.GridColor, 1))
            {
                e.Graphics.DrawLine(gridPen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);

                bool drawBottomLine = true;
                if (e.RowIndex < dgvManager.Rows.Count - 1)
                {
                    int nextContractId = Convert.ToInt32(dgvManager.Rows[e.RowIndex + 1].Cells["ContractID"].Value);
                    if (currentContractId == nextContractId)
                    {
                        drawBottomLine = false;
                    }
                }

                if (drawBottomLine)
                {
                    e.Graphics.DrawLine(gridPen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                }
            }

            bool isFirstRowOfGroup = false;
            if (e.RowIndex == 0)
            {
                isFirstRowOfGroup = true;
            }
            else
            {
                int prevContractId = Convert.ToInt32(dgvManager.Rows[e.RowIndex - 1].Cells["ContractID"].Value);
                if (currentContractId != prevContractId)
                {
                    isFirstRowOfGroup = true;
                }
            }

            if (isFirstRowOfGroup)
            {
                if (e.Value != null || dgvManager.Columns[e.ColumnIndex].Name == "Detail")
                {
                    string textToDraw = "";

                    if (e.Value is DateTime dtVal)
                        textToDraw = dtVal.ToString("dd/MM/yyyy");
                    else if (dgvManager.Columns[e.ColumnIndex].Name == "Detail")
                        textToDraw = "Xem";
                    else
                        textToDraw = e.Value?.ToString();

                    if (dgvManager.Columns[e.ColumnIndex].Name == "Detail")
                    {
                        Rectangle btnRect = new Rectangle(e.CellBounds.X + 10, e.CellBounds.Y + 4, e.CellBounds.Width - 20, e.CellBounds.Height - 8);
                        using (Brush btnBrush = new SolidBrush(Color.FromArgb(40, 167, 69)))
                        {
                            e.Graphics.FillRectangle(btnBrush, btnRect);
                        }
                        TextRenderer.DrawText(e.Graphics, textToDraw, e.CellStyle.Font, btnRect, Color.White,
                            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    }
                    else
                    {
                        using (Brush textBrush = new SolidBrush(Color.Black))
                        {
                            e.Graphics.DrawString(textToDraw, e.CellStyle.Font, textBrush, e.CellBounds, new StringFormat
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
    }
}