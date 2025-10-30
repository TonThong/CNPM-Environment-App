using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

// --- THÊM MỚI ---
using System.Threading;
using System.Globalization;
using System.Resources;
using Environmental_Monitoring.View.Components;
using Environmental_Monitoring.View; // Để tham chiếu đến Mainlayout
// -----------------

namespace Environmental_Monitoring.View
{
    public partial class Stats : UserControl
    {
        string connectionString = "Server=sql12.freesqldatabase.com;Database=sql12800882;Uid=sql12800882;Pwd=TMlsWFrPxZ;";

        private class ChartDataPoint
        {
            public int Month { get; set; }
            public int Count { get; set; }
            public bool IsOnTime { get; set; }
        }

        public Stats()
        {
            InitializeComponent();
            // --- THÊM MỚI: Gắn sự kiện Load ---
            this.Load += new System.EventHandler(this.Stats_Load);
        }

        // --- ĐÃ CẬP NHẬT ---
        private void Stats_Load(object sender, EventArgs e)
        {
            // 1. Set Culture (giống như Setting.cs)
            string savedLanguage = Properties.Settings.Default.Language;
            string cultureName = "vi";
            if (savedLanguage == "English")
            {
                cultureName = "en";
            }
            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // 2. Tải ComboBox Năm (Không cần dịch)
            int currentYear = DateTime.Now.Year;
            cmbNam.Items.Clear();
            for (int i = 0; i < 5; i++)
            {
                cmbNam.Items.Add(currentYear - i);
            }
            cmbNam.SelectedIndex = 0;

            // 3. Tải ComboBox Quý và dịch các control tĩnh
            UpdateUIText();
        }

        // --- HÀM MỚI: Cập nhật UI theo ngôn ngữ ---
        public void UpdateUIText()
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Stats).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            // Dịch các control tĩnh (sửa tên control cho đúng)
            lblTitle.Text = rm.GetString("Stats_Title", culture);
            txtSearch.PlaceholderText = rm.GetString("Search_Placeholder", culture);
            lblOrderCountTitle.Text = rm.GetString("Stats_OrderCount", culture);
            lblOnTimeRateTitle.Text = rm.GetString("Stats_OnTimeRate", culture);
            btnApply.Text = rm.GetString("Stats_ApplyButton", culture);

            // Dịch ComboBox Quý
            cmbQuy.Items.Clear();
            cmbQuy.Items.Add(rm.GetString("Stats_Quarter1", culture));
            cmbQuy.Items.Add(rm.GetString("Stats_Quarter2", culture));
            cmbQuy.Items.Add(rm.GetString("Stats_Quarter3", culture));
            cmbQuy.Items.Add(rm.GetString("Stats_Quarter4", culture));
            cmbQuy.SelectedIndex = 0;

            // Tải lại biểu đồ với ngôn ngữ mới (nếu cần)
            // (Chỉ tải khi người dùng bấm "Apply")
            // Hoặc bạn có thể gọi lại btnApply_Click(null, null); nếu muốn tự động tải
        }


        // --- ĐÃ CẬP NHẬT: Dịch thông báo và nút ---
        private async void btnApply_Click(object sender, EventArgs e)
        {
            // Lấy ngôn ngữ
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Stats).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
            Mainlayout mainForm = this.ParentForm as Mainlayout;

            if (cmbNam.SelectedItem == null || cmbQuy.SelectedItem == null)
            {
                // THAY THẾ MessageBox bằng AlertPanel
                string errorMsg = rm.GetString("Validation_SelectYearAndQuarter", culture);
                mainForm?.ShowGlobalAlert(errorMsg, AlertPanel.AlertType.Error);
                return;
            }

            int selectedYear = (int)cmbNam.SelectedItem;
            int selectedQuarter = cmbQuy.SelectedIndex + 1;

            try
            {
                btnApply.Enabled = false;
                btnApply.Text = rm.GetString("Stats_Loading", culture); // Dịch "Đang tải..."

                var data = await LoadChartDataAsync(selectedYear, selectedQuarter);

                int[] monthsInQuarter = GetMonthsForQuarter(selectedQuarter);

                // Truyền rm và culture vào hàm vẽ biểu đồ
                UpdateOrderQuantityChart(data, monthsInQuarter, rm, culture);
                UpdateOnTimeRateChart(data, rm, culture);

            }
            catch (Exception ex)
            {
                // THAY THẾ MessageBox bằng AlertPanel
                string errorMsg = rm.GetString("Alert_LoadDataError", culture);
                string errorTitle = rm.GetString("Alert_ErrorDetails", culture); // "Lỗi Chi Tiết"

                // (AlertPanel của bạn không có tiêu đề, nên chúng ta gộp lại)
                mainForm?.ShowGlobalAlert($"{errorTitle}: {errorMsg} {ex.Message}", AlertPanel.AlertType.Error);
            }
            finally
            {
                btnApply.Enabled = true;
                btnApply.Text = rm.GetString("Stats_ApplyButton", culture); // Dịch "Áp Dụng"
            }
        }

        private async Task<List<ChartDataPoint>> LoadChartDataAsync(int year, int quarter)
        {
            // (Code này không thay đổi - Logic CSDL)
            var results = new List<ChartDataPoint>();
            int[] months = GetMonthsForQuarter(quarter);
            string monthList = string.Join(",", months);
            string sql = $@"
                SELECT 
                    MONTH(NgayTraKetQua) AS Thang,
                    (CASE WHEN TRIM(Status) = 'Completed' THEN 1 ELSE 0 END) AS OnTimeFlag,
                    COUNT(ContractID) AS SoLuong
                FROM Contracts
                WHERE 
                    YEAR(NgayTraKetQua) = @Year
                    AND MONTH(NgayTraKetQua) IN ({monthList})
                    AND TRIM(Status) IN ('Completed', 'Expired')
                GROUP BY Thang, OnTimeFlag;
            ";
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Year", year);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            results.Add(new ChartDataPoint
                            {
                                Month = reader.IsDBNull("Thang") ? 0 : reader.GetInt32("Thang"),
                                Count = reader.IsDBNull("SoLuong") ? 0 : reader.GetInt32("SoLuong"),
                                IsOnTime = !reader.IsDBNull("OnTimeFlag") && reader.GetInt32("OnTimeFlag") == 1
                            });
                        }
                    }
                }
            }
            return results;
        }

        // --- ĐÃ CẬP NHẬT: Dịch Legend (chú thích) và Axis (trục) ---
        private void UpdateOrderQuantityChart(List<ChartDataPoint> data, int[] months, ResourceManager rm, CultureInfo culture)
        {
            var chart = chartOrderQuantity;
            chart.Series.Clear();
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chart.Legends[0].Docking = Docking.Bottom;

            chart.ChartAreas[0].AxisX.CustomLabels.Clear();
            chart.ChartAreas[0].AxisX.Interval = 1;

            var seriesOnTime = new Series(rm.GetString("Stats_OnTime", culture)) // Dịch "Đúng hẹn"
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(135, 120, 250)
            };

            var seriesLate = new Series(rm.GetString("Stats_Late", culture)) // Dịch "Trễ hẹn"
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(240, 130, 150)
            };

            int pointIndex = 1;
            bool hasData = false;

            // Lấy chuỗi "Tháng {0}" hoặc "Month {0}"
            string monthFormat = rm.GetString("Stats_Month", culture);

            foreach (int month in months)
            {
                int onTimeCount = data.FirstOrDefault(d => d.Month == month && d.IsOnTime)?.Count ?? 0;
                int lateCount = data.FirstOrDefault(d => d.Month == month && !d.IsOnTime)?.Count ?? 0;
                if (onTimeCount + lateCount > 0) hasData = true;

                string monthName = string.Format(monthFormat, month); // Dịch "Tháng {month}"
                seriesOnTime.Points.AddXY(pointIndex, onTimeCount);
                seriesLate.Points.AddXY(pointIndex, lateCount);
                chart.ChartAreas[0].AxisX.CustomLabels.Add(pointIndex - 0.5, pointIndex + 0.5, monthName);
                pointIndex++;
            }

            chart.Series.Add(seriesOnTime);
            chart.Series.Add(seriesLate);

            chart.ChartAreas[0].AxisX.Minimum = 0.5;
            chart.ChartAreas[0].AxisX.Maximum = months.Length + 0.5;

            chart.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
            chart.ChartAreas[0].AxisY.Interval = 1;
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisY.IsStartedFromZero = true;

            if (data.Any())
            {
                // Sửa lỗi logic: Cần tính tổng theo tháng
                var maxMonthlyTotal = months.Max(month =>
                    data.Where(d => d.Month == month).Sum(d => d.Count)
                );
                chart.ChartAreas[0].AxisY.Maximum = maxMonthlyTotal + 1;
                chart.ChartAreas[0].AxisY.Interval = Math.Max(1, Math.Ceiling((maxMonthlyTotal + 1) / 5.0)); // Tự động chia
            }
            else
            {
                chart.ChartAreas[0].AxisY.Maximum = 2; // Giá trị mặc định nếu không có data
                chart.Refresh();
                return;
            }
        }

        // --- ĐÃ CẬP NHẬT: Dịch Legend (chú thích) ---
        private void UpdateOnTimeRateChart(List<ChartDataPoint> data, ResourceManager rm, CultureInfo culture)
        {
            var chart = chartOnTimeRate;
            chart.Series.Clear();
            chart.Legends[0].Docking = Docking.Right;

            int totalOnTime = data.Where(d => d.IsOnTime == true).Sum(d => d.Count);
            int totalLate = data.Where(d => d.IsOnTime == false).Sum(d => d.Count);
            double total = totalOnTime + totalLate;

            if (total == 0)
            {
                // Thêm code để xóa data cũ nếu không có data mới
                var seriesEmpty = new Series(rm.GetString("Stats_Rate", culture))
                {
                    ChartType = SeriesChartType.Pie
                };
                seriesEmpty.Points.Add(1);
                seriesEmpty.Points[0].Color = Color.LightGray;
                seriesEmpty.Points[0].Label = "0.00%";
                seriesEmpty.Points[0].LegendText = "N/A";
                chart.Series.Add(seriesEmpty);
                return;
            }

            var seriesPie = new Series(rm.GetString("Stats_Rate", culture)) // Dịch "Tỷ lệ"
            {
                ChartType = SeriesChartType.Pie
            };

            DataPoint dpOnTime = new DataPoint(0, totalOnTime);
            dpOnTime.LegendText = rm.GetString("Stats_OnTime", culture); // Dịch "Đúng hẹn"
            dpOnTime.Label = $"{(totalOnTime / total):P2}";
            dpOnTime.Color = Color.FromArgb(135, 120, 250);
            seriesPie.Points.Add(dpOnTime);

            DataPoint dpLate = new DataPoint(0, totalLate);
            dpLate.LegendText = rm.GetString("Stats_Late", culture); // Dịch "Trễ hẹn"
            dpLate.Label = $"{(totalLate / total):P2}";
            dpLate.Color = Color.FromArgb(240, 130, 150);
            seriesPie.Points.Add(dpLate);

            seriesPie["PieLabelStyle"] = "Outside";
            seriesPie.LabelForeColor = Color.Black;

            chart.Series.Add(seriesPie);
        }

        private int[] GetMonthsForQuarter(int quarter)
        {
            // (Code này không thay đổi - Logic)
            switch (quarter)
            {
                case 1: return new int[] { 1, 2, 3 };
                case 2: return new int[] { 4, 5, 6 };
                case 3: return new int[] { 7, 8, 9 };
                case 4: return new int[] { 10, 11, 12 };
                default: throw new ArgumentException("Quý không hợp lệ.");
            }
        }
    }
}