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

namespace Environmental_Monitoring.View
{
    public partial class Stats : UserControl
    {
        string connectionString = "Server=sql12.freesqldatabase.com;Database=sql12800882;Uid=sql12800882;Pwd=TMlsWFrPxZ;";

        // === SỬA 1: Đơn giản hóa class, bỏ 'Status' đi ===
        private class ChartDataPoint
        {
            public int Month { get; set; }
            public int Count { get; set; }
            public bool IsOnTime { get; set; }
        }

        public Stats()
        {
            InitializeComponent();
        }

        private void Stats_Load(object sender, EventArgs e)
        {
            int currentYear = DateTime.Now.Year;
            for (int i = 0; i < 5; i++)
            {
                cmbNam.Items.Add(currentYear - i);
            }
            cmbNam.SelectedIndex = 0;

            cmbQuy.Items.Add("Quý 1");
            cmbQuy.Items.Add("Quý 2");
            cmbQuy.Items.Add("Quý 3");
            cmbQuy.Items.Add("Quý 4");
            cmbQuy.SelectedIndex = 0;
        }

        private async void btnApply_Click(object sender, EventArgs e)
        {
            if (cmbNam.SelectedItem == null || cmbQuy.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn Năm và Quý.");
                return;
            }

            int selectedYear = (int)cmbNam.SelectedItem;
            int selectedQuarter = cmbQuy.SelectedIndex + 1;

            try
            {
                btnApply.Enabled = false;
                btnApply.Text = "Đang tải...";

                var data = await LoadChartDataAsync(selectedYear, selectedQuarter);

                int[] monthsInQuarter = GetMonthsForQuarter(selectedQuarter);

                UpdateOrderQuantityChart(data, monthsInQuarter);
                UpdateOnTimeRateChart(data);

            }
            catch (Exception ex)
            {
                string errorMessage = $"Lỗi: {ex.Message}\n\nNguồn: {ex.Source}\n\nStack Trace:\n{ex.StackTrace}";
                if (ex.InnerException != null)
                {
                    errorMessage += $"\n\nLỗi bên trong: {ex.InnerException.Message}";
                }

                MessageBox.Show(errorMessage, "Lỗi Chi Tiết", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnApply.Enabled = true;
                btnApply.Text = "Áp Dụng";
            }
        }

        // === SỬA 2: Sửa SQL để trả về OnTimeFlag (1 hoặc 0) ===
        private async Task<List<ChartDataPoint>> LoadChartDataAsync(int year, int quarter)
        {
            var results = new List<ChartDataPoint>();
            int[] months = GetMonthsForQuarter(quarter);

            // ⚙️ Dùng chuỗi động cho IN(...)
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

        // === SỬA 3: Sửa lỗi "chỉ hiện 1 tháng" bằng CustomLabels ===
        private void UpdateOrderQuantityChart(List<ChartDataPoint> data, int[] months)
        {
            var chart = chartOrderQuantity;
            chart.Series.Clear();
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chart.Legends[0].Docking = Docking.Bottom;

            chart.ChartAreas[0].AxisX.CustomLabels.Clear();
            chart.ChartAreas[0].AxisX.Interval = 1;

            var seriesOnTime = new Series("Đúng hẹn")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(135, 120, 250)
            };

            var seriesLate = new Series("Trễ hẹn")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(240, 130, 150)
            };

            int pointIndex = 1;
            bool hasData = false;

            foreach (int month in months)
            {
                int onTimeCount = data.FirstOrDefault(d => d.Month == month && d.IsOnTime)?.Count ?? 0;
                int lateCount = data.FirstOrDefault(d => d.Month == month && !d.IsOnTime)?.Count ?? 0;
                if (onTimeCount + lateCount > 0) hasData = true;

                string monthName = $"Tháng {month}";
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
                int maxValue = data.Max(d => d.Count);
                chart.ChartAreas[0].AxisY.Maximum = maxValue + 1;
                chart.ChartAreas[0].AxisY.Interval = 1;
            }
            else
            {
                chart.Refresh();
                return;
            }
        }


        // === SỬA 4: Đã dọn dẹp, logic này giờ đã đúng ===
        private void UpdateOnTimeRateChart(List<ChartDataPoint> data)
        {
            var chart = chartOnTimeRate;
            chart.Series.Clear(); // Luôn xóa series cũ
            chart.Legends[0].Docking = Docking.Right;

            int totalOnTime = data.Where(d => d.IsOnTime == true).Sum(d => d.Count);
            int totalLate = data.Where(d => d.IsOnTime == false).Sum(d => d.Count);
            double total = totalOnTime + totalLate;

            if (total == 0)
            {
                return; // Thoát nếu không có dữ liệu
            }

            var seriesPie = new Series("Tỷ lệ")
            {
                ChartType = SeriesChartType.Pie
            };

            DataPoint dpOnTime = new DataPoint(0, totalOnTime);
            dpOnTime.LegendText = "Đúng hẹn";
            dpOnTime.Label = $"{(totalOnTime / total):P2}";
            dpOnTime.Color = Color.FromArgb(135, 120, 250);
            seriesPie.Points.Add(dpOnTime);

            DataPoint dpLate = new DataPoint(0, totalLate);
            dpLate.LegendText = "Trễ hẹn";
            dpLate.Label = $"{(totalLate / total):P2}";
            dpLate.Color = Color.FromArgb(240, 130, 150);
            seriesPie.Points.Add(dpLate);

            seriesPie["PieLabelStyle"] = "Outside";
            seriesPie.LabelForeColor = Color.Black;

            chart.Series.Add(seriesPie);
        }

        private int[] GetMonthsForQuarter(int quarter)
        {
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