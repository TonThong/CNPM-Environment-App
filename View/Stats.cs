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
using System.Threading;
using System.Globalization;
using System.Resources;
using Environmental_Monitoring.View.Components;
using Environmental_Monitoring.View;
using Environmental_Monitoring.Controller; 
using Environmental_Monitoring.Controller.Data; 

namespace Environmental_Monitoring.View
{
    public partial class Stats : UserControl
    {

        private class ChartDataPoint
        {
            public int Month { get; set; }
            public int Count { get; set; }
            public bool IsOnTime { get; set; }
        }

        public Stats()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.Stats_Load);
        }

        private void Stats_Load(object sender, EventArgs e)
        {
            int currentYear = DateTime.Now.Year;
            cmbNam.Items.Clear();
            for (int i = 0; i < 5; i++)
            {
                cmbNam.Items.Add(currentYear - i);
            }
            cmbNam.SelectedIndex = 0;

            UpdateUIText();
        }

        public void UpdateUIText()
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Stats).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            lblTitle.Text = rm.GetString("Stats_Title", culture);
            txtSearch.PlaceholderText = rm.GetString("Search_Placeholder", culture);
            lblOrderCountTitle.Text = rm.GetString("Stats_OrderCount", culture);
            lblOnTimeRateTitle.Text = rm.GetString("Stats_OnTimeRate", culture);
            btnApply.Text = rm.GetString("Stats_ApplyButton", culture);

            int selectedIndex = cmbQuy.SelectedIndex;
            cmbQuy.Items.Clear();
            cmbQuy.Items.Add(rm.GetString("Stats_Quarter1", culture));
            cmbQuy.Items.Add(rm.GetString("Stats_Quarter2", culture));
            cmbQuy.Items.Add(rm.GetString("Stats_Quarter3", culture));
            cmbQuy.Items.Add(rm.GetString("Stats_Quarter4", culture));
            cmbQuy.SelectedIndex = (selectedIndex < 0) ? 0 : selectedIndex; 

            try
            {
                this.BackColor = ThemeManager.BackgroundColor;
                lblTitle.ForeColor = ThemeManager.TextColor;

                txtSearch.BackColor = ThemeManager.PanelColor;
                txtSearch.ForeColor = ThemeManager.TextColor;

                cmbNam.BackColor = ThemeManager.PanelColor;
                cmbNam.ForeColor = ThemeManager.TextColor;
                cmbQuy.BackColor = ThemeManager.PanelColor;
                cmbQuy.ForeColor = ThemeManager.TextColor;

                btnApply.BackColor = ThemeManager.AccentColor;
                btnApply.ForeColor = Color.White;

                lblOrderCountTitle.ForeColor = ThemeManager.TextColor;
                lblOnTimeRateTitle.ForeColor = ThemeManager.TextColor;

                ApplyChartTheme(chartOrderQuantity);
                ApplyChartTheme(chartOnTimeRate);
            }
            catch (Exception) {}
           
        }

        private void ApplyChartTheme(Chart chart)
        {
            if (chart == null || chart.ChartAreas.Count == 0) return;

            chart.BackColor = ThemeManager.PanelColor;
            chart.ChartAreas[0].BackColor = ThemeManager.PanelColor;

            chart.ChartAreas[0].AxisX.LineColor = ThemeManager.BorderColor;
            chart.ChartAreas[0].AxisY.LineColor = ThemeManager.BorderColor;
            chart.ChartAreas[0].AxisX.MajorGrid.LineColor = ThemeManager.BorderColor;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = ThemeManager.BorderColor;

            chart.ChartAreas[0].AxisX.LabelStyle.ForeColor = ThemeManager.TextColor;
            chart.ChartAreas[0].AxisY.LabelStyle.ForeColor = ThemeManager.TextColor;
            chart.ChartAreas[0].AxisX.TitleForeColor = ThemeManager.TextColor;
            chart.ChartAreas[0].AxisY.TitleForeColor = ThemeManager.TextColor;

            if (chart.Legends.Count > 0)
            {
                chart.Legends[0].BackColor = ThemeManager.PanelColor;
                chart.Legends[0].ForeColor = ThemeManager.TextColor;
            }

            foreach (var series in chart.Series)
            {
                series.LabelForeColor = ThemeManager.TextColor;
            }
        }

        private async void btnApply_Click(object sender, EventArgs e)
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Stats).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
            Mainlayout mainForm = this.ParentForm as Mainlayout;

            if (cmbNam.SelectedItem == null || cmbQuy.SelectedItem == null)
            {
                string errorMsg = rm.GetString("Validation_SelectYearAndQuarter", culture);
                mainForm?.ShowGlobalAlert(errorMsg, AlertPanel.AlertType.Error);
                return;
            }

            int selectedYear = (int)cmbNam.SelectedItem;
            int selectedQuarter = cmbQuy.SelectedIndex + 1;

            try
            {
                btnApply.Enabled = false;
                btnApply.Text = rm.GetString("Stats_Loading", culture);

                var data = await LoadChartDataAsync(selectedYear, selectedQuarter);
                int[] monthsInQuarter = GetMonthsForQuarter(selectedQuarter);

                UpdateOrderQuantityChart(data, monthsInQuarter, rm, culture);
                UpdateOnTimeRateChart(data, rm, culture);

                ApplyChartTheme(chartOrderQuantity);
                ApplyChartTheme(chartOnTimeRate);
               
            }
            catch (Exception ex)
            {
                string errorMsg = rm.GetString("Alert_LoadDataError", culture);
                string errorTitle = rm.GetString("Alert_ErrorDetails", culture);
                mainForm?.ShowGlobalAlert($"{errorTitle}: {errorMsg} {ex.Message}", AlertPanel.AlertType.Error);
            }
            finally
            {
                btnApply.Enabled = true;
                btnApply.Text = rm.GetString("Stats_ApplyButton", culture);
            }
        }

        private async Task<List<ChartDataPoint>> LoadChartDataAsync(int year, int quarter)
        {
            var results = new List<ChartDataPoint>();
            int[] months = GetMonthsForQuarter(quarter);

            List<string> monthParams = new List<string>();
            for (int i = 0; i < months.Length; i++)
            {
                monthParams.Add($"@month{i}");
            }
            string monthList = string.Join(",", monthParams);

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

            using (var connection = DatabaseHelper.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Year", year);

                    for (int i = 0; i < months.Length; i++)
                    {
                        command.Parameters.AddWithValue($"@month{i}", months[i]);
                    }

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

        private void UpdateOrderQuantityChart(List<ChartDataPoint> data, int[] months, ResourceManager rm, CultureInfo culture)
        {
            var chart = chartOrderQuantity;
            chart.Series.Clear();
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chart.Legends[0].Docking = Docking.Bottom;

            chart.ChartAreas[0].AxisX.CustomLabels.Clear();
            chart.ChartAreas[0].AxisX.Interval = 1;

            var seriesOnTime = new Series(rm.GetString("Stats_OnTime", culture))
            {
                ChartType = SeriesChartType.Column,
                Color = ThemeManager.AccentColor 
            };

            var seriesLate = new Series(rm.GetString("Stats_Late", culture))
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(240, 130, 150)
            };

            int pointIndex = 1;
            bool hasData = false;

            string monthFormat = rm.GetString("Stats_Month", culture);

            foreach (int month in months)
            {
                int onTimeCount = data.FirstOrDefault(d => d.Month == month && d.IsOnTime)?.Count ?? 0;
                int lateCount = data.FirstOrDefault(d => d.Month == month && !d.IsOnTime)?.Count ?? 0;
                if (onTimeCount + lateCount > 0) hasData = true;

                string monthName = string.Format(monthFormat, month);
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
                var maxMonthlyTotal = months.Max(month =>
                    data.Where(d => d.Month == month).Sum(d => d.Count)
                );
                chart.ChartAreas[0].AxisY.Maximum = maxMonthlyTotal + 1;
                chart.ChartAreas[0].AxisY.Interval = Math.Max(1, Math.Ceiling((maxMonthlyTotal + 1) / 5.0));
            }
            else
            {
                chart.ChartAreas[0].AxisY.Maximum = 2;
                chart.Refresh();
                return;
            }
        }

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

            var seriesPie = new Series(rm.GetString("Stats_Rate", culture))
            {
                ChartType = SeriesChartType.Pie
            };

            DataPoint dpOnTime = new DataPoint(0, totalOnTime);
            dpOnTime.LegendText = rm.GetString("Stats_OnTime", culture);
            dpOnTime.Label = $"{(totalOnTime / total):P2}";
            dpOnTime.Color = ThemeManager.AccentColor; 
            seriesPie.Points.Add(dpOnTime);

            DataPoint dpLate = new DataPoint(0, totalLate);
            dpLate.LegendText = rm.GetString("Stats_Late", culture);
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