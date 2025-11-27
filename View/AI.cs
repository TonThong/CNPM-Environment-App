using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Resources;
using Environmental_Monitoring.View.Components;
using Environmental_Monitoring.View;
using Environmental_Monitoring.Controller;
using Environmental_Monitoring.Controller.Data;
using System.Windows.Forms.DataVisualization.Charting;
using Environmental_Monitoring;

namespace Environmental_Monitoring.View
{
    public partial class AI : UserControl
    {
        // Cache data to redraw chart without re-fetching API
        private List<PollutionData> _cachedData;

        public AI()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.AI_Load);

            // Default items, will be cleared and re-populated in LoadProvinces
            cmbLocation.Items.Add("HaNoi");
            cmbLocation.Items.Add("HCM");
            if (cmbLocation.Items.Count > 0) cmbLocation.SelectedIndex = 0;
        }

        private void AI_Load(object sender, EventArgs e)
        {
            UpdateUIText();
            LoadProvinces();
        }

        private void LoadProvinces()
        {
            cmbLocation.Items.Clear();
            // Get province list from Service
            foreach (var province in AirQualityService.Provinces.Keys)
            {
                cmbLocation.Items.Add(province);
            }
            if (cmbLocation.Items.Count > 0) cmbLocation.SelectedIndex = 0;
        }

        public void UpdateUIText()
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(AI).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
            Mainlayout mainForm = this.ParentForm as Mainlayout;

            try
            {
                lblTitle.Text = rm.GetString("AI_Title", culture);
                txtSearch.PlaceholderText = rm.GetString("Search_Placeholder", culture);

                lblPanelResign.Text = rm.GetString("AI_Panel_Resign", culture);
                txtCustomerName.PlaceholderText = "Nhập tên khách hàng...";
                btnPredictResign.Text = rm.GetString("AI_Button_Predict", culture);

                lblPanelPollution.Text = rm.GetString("AI_Panel_Pollution", culture);
                lblPollutionLevel.Text = rm.GetString("AI_Label_PollutionLevel", culture);
                btnPredictPollution.Text = rm.GetString("AI_Button_Predict", culture);

                // Apply Theme Colors
                this.BackColor = ThemeManager.BackgroundColor;
                lblTitle.ForeColor = ThemeManager.TextColor;
                txtSearch.BackColor = ThemeManager.PanelColor;
                txtSearch.ForeColor = ThemeManager.TextColor;
                panelResign.BackColor = ThemeManager.PanelColor;
                lblPanelResign.ForeColor = ThemeManager.TextColor;
                txtCustomerName.BackColor = ThemeManager.BackgroundColor;
                txtCustomerName.ForeColor = ThemeManager.TextColor;
                btnPredictResign.BackColor = ThemeManager.AccentColor;
                btnPredictResign.ForeColor = Color.White;
                panelPollution.BackColor = ThemeManager.PanelColor;
                lblPanelPollution.ForeColor = ThemeManager.TextColor;
                lblPollutionLevel.ForeColor = ThemeManager.TextColor;
                btnPredictPollution.BackColor = ThemeManager.AccentColor;
                btnPredictPollution.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                
            }
        }
        // SỰ KIỆN NÚT DỰ ĐOÁN (Đã sửa logic: Tìm theo Tên)
        private async void btnPredictResign_Click(object sender, EventArgs e)
        {
            string inputName = txtCustomerName.Text.Trim(); // Đổi biến inputID -> inputName

            // Validate đầu vào
            if (string.IsNullOrEmpty(inputName))
            {
                MessageBox.Show("Vui lòng nhập Tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnPredictResign.Enabled = false;
            string originalText = btnPredictResign.Text;
            btnPredictResign.Text = "Đang Tải...";

            try
            {
                await Task.Run(() =>
                {
                    // 1. Tìm ID dựa trên Tên nhập vào (Tìm gần đúng)
                    int customerId = GetCustomerIDByName(inputName);

                    this.Invoke((MethodInvoker)delegate
                    {
                        // Nếu không tìm thấy ID nào
                        if (customerId == -1)
                        {
                            MessageBox.Show($"Không tìm thấy khách hàng nào có tên chứa: '{inputName}'", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Nếu tìm thấy, tiếp tục logic cũ
                        // (Lấy thống kê và dự đoán)
                        CustomerStats stats = GetCustomerStats(customerId);

                        if (stats == null)
                        {
                            MessageBox.Show($"Khách hàng '{inputName}' (ID: {customerId}) chưa có dữ liệu hợp đồng để phân tích.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Chuẩn bị dữ liệu cho AI
                        var inputData = new ResignModel.ModelInput()
                        {
                            ContractType = "Quarterly",
                            WasOnTime = stats.WasOnTime,
                            TotalContracts = stats.TotalContracts,
                            CorrectionCount = stats.CorrectionCount,
                            DaysOverdue = stats.DaysOverdue,
                            Label = false
                        };

                        // Gọi AI dự đoán
                        ResignModel.ModelOutput prediction = ResignModel.Predict(inputData);

                        // Cập nhật biểu đồ
                        UpdateResignChart(prediction, stats);
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi phân tích AI: " + ex.Message);
            }
            finally
            {
                btnPredictResign.Enabled = true;
                btnPredictResign.Text = originalText;
            }
        }

        // HÀM MỚI: TÌM ID KHÁCH HÀNG THEO TÊN (Tìm gần đúng với LIKE)
        private int GetCustomerIDByName(string name)
        {
            try
            {
                // Tìm kiếm không phân biệt hoa thường, lấy khách hàng đầu tiên tìm thấy
                string query = "SELECT CustomerID FROM Customers WHERE TenDoanhNghiep LIKE @name LIMIT 1";
                object result = DataProvider.Instance.ExecuteScalar(query, new object[] { "%" + name + "%" });

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
            }
            return -1; // Trả về -1 nếu không tìm thấy
        }

        private void UpdateResignChart(ResignModel.ModelOutput prediction, CustomerStats stats)
        {
            float currentScore = prediction.Score;

            // --- HYBRID SCORING LOGIC ---
            float contractBonus = Math.Min(stats.TotalContracts * 0.4f, 3.0f);
            float overduePenalty = stats.DaysOverdue * 0.1f;
            float correctionPenalty = stats.CorrectionCount * 0.4f;

            float reputationBonus = 0;
            if (stats.WasOnTime == "OnTime" && stats.DaysOverdue == 0)
                reputationBonus = 2.0f;

            float finalScore = currentScore + contractBonus + reputationBonus - overduePenalty - correctionPenalty;

            // Sigmoid function
            float finalProbability = 1f / (1f + (float)Math.Exp(-finalScore));
            if (finalProbability > 0.98f) finalProbability = 0.98f;
            if (finalProbability < 0.02f) finalProbability = 0.02f;

            int percentTaiky = (int)(finalProbability * 100);
            int percentRoibo = 100 - percentTaiky;

            if (chartResign != null)
            {
                chartResign.Series.Clear();
                Series series = chartResign.Series.Add("ResignRate");
                series.ChartType = SeriesChartType.Pie;

                DataPoint dpTaiky = new DataPoint(0, percentTaiky);
                dpTaiky.LegendText = "Tái ký";
                dpTaiky.Label = $"{percentTaiky}%";
                dpTaiky.Color = Color.FromArgb(113, 110, 226);
                dpTaiky.LabelForeColor = Color.White;

                DataPoint dpRoibo = new DataPoint(0, percentRoibo);
                dpRoibo.LegendText = "Rời bỏ";
                dpRoibo.Label = $"{percentRoibo}%";
                dpRoibo.Color = Color.FromArgb(255, 127, 127);
                dpRoibo.LabelForeColor = Color.White;

                series.Points.Add(dpTaiky);
                series.Points.Add(dpRoibo);

                if (chartResign.Legends.Count == 0) chartResign.Legends.Add("Default");
                chartResign.Legends[0].Docking = Docking.Right;
                chartResign.Legends[0].Alignment = StringAlignment.Center;
            }
        }

        private CustomerStats GetCustomerStats(int customerId)
        {
            string queryCheck = "SELECT COUNT(*) FROM Customers WHERE CustomerID = @cid";
            object checkObj = DataProvider.Instance.ExecuteScalar(queryCheck, new object[] { customerId });
            if (checkObj == null || Convert.ToInt32(checkObj) == 0) return null;

            var stats = new CustomerStats();
            string queryTotal = "SELECT COUNT(*) FROM Contracts WHERE CustomerID = @cid";
            stats.TotalContracts = Convert.ToSingle(DataProvider.Instance.ExecuteScalar(queryTotal, new object[] { customerId }));

            string queryCorrection = @"SELECT COUNT(*) FROM Notifications n 
                                       JOIN Contracts c ON n.ContractID = c.ContractID 
                                       WHERE c.CustomerID = @cid AND n.LoaiThongBao = 'ChinhSua'";
            stats.CorrectionCount = Convert.ToSingle(DataProvider.Instance.ExecuteScalar(queryCorrection, new object[] { customerId }));

            string queryLatestStatus = @"SELECT Status FROM Contracts WHERE CustomerID = @cid ORDER BY ContractID DESC LIMIT 1";
            object latestStatusObj = DataProvider.Instance.ExecuteScalar(queryLatestStatus, new object[] { customerId });
            string latestStatus = latestStatusObj?.ToString() ?? "";

            if (latestStatus == "Completed" || latestStatus == "Active")
            {
                stats.WasOnTime = "OnTime";
                stats.DaysOverdue = 0;
            }
            else
            {
                string queryMaxDays = @"SELECT MAX(DATEDIFF(CURRENT_DATE, NgayTraKetQua)) FROM Contracts 
                                        WHERE CustomerID = @cid AND Status = 'Expired'";
                object maxDaysObj = DataProvider.Instance.ExecuteScalar(queryMaxDays, new object[] { customerId });

                if (maxDaysObj != null && maxDaysObj != DBNull.Value)
                {
                    stats.WasOnTime = "Late";
                    stats.DaysOverdue = Convert.ToSingle(maxDaysObj);
                    if (stats.DaysOverdue < 0) stats.DaysOverdue = 0;
                }
                else
                {
                    string queryHistory = "SELECT COUNT(*) FROM Contracts WHERE CustomerID = @cid AND Status = 'Late Completion'";
                    int hist = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(queryHistory, new object[] { customerId }));
                    if (hist > 0)
                    {
                        stats.WasOnTime = "Late";
                        stats.DaysOverdue = 5;
                    }
                    else
                    {
                        stats.WasOnTime = "OnTime";
                        stats.DaysOverdue = 0;
                    }
                }
            }
            return stats;
        }


        private async void btnPredictPollution_Click(object sender, EventArgs e)
        {
            string selectedProvince = cmbLocation.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedProvince)) return;

            var coords = AirQualityService.Provinces[selectedProvince];

            btnPredictPollution.Enabled = false;
            btnPredictPollution.Text = "Đang tải...";

            try
            {
                var data = await AirQualityService.GetPollutionForecastAsync(coords.Lat, coords.Lon);

                if (data != null && data.list != null)
                {
                    _cachedData = data.list;
                    UpdatePollutionChart();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi API: " + ex.Message);
            }
            finally
            {
                btnPredictPollution.Enabled = true;
                btnPredictPollution.Text = "Dự đoán";
            }
        }

        private void UpdatePollutionChart()
        {
            if (_cachedData == null || chartPollution == null) return;

            chartPollution.Series.Clear();
            chartPollution.ChartAreas.Clear();

            // 1. Cấu hình giao diện Chart
            ChartArea area = new ChartArea("MainArea");
            area.BorderWidth = 0;
            area.BackColor = Color.White;

            area.AxisX.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);

            area.AxisX.LineColor = Color.Transparent;
            area.AxisY.LineColor = Color.Transparent;

            area.AxisX.LabelStyle.ForeColor = Color.Gray;
            area.AxisY.LabelStyle.ForeColor = Color.Gray;

            area.AxisX.LabelStyle.Format = "dd/MM";
            area.AxisX.Interval = 1;
            area.AxisX.IntervalType = DateTimeIntervalType.Days;

            area.AxisY.Minimum = 0;
            area.AxisY.Maximum = 6;
            area.AxisY.Interval = 1;

            chartPollution.ChartAreas.Add(area);

            Series series = new Series("AQI");
            series.ChartType = SeriesChartType.Spline; 
            series.BorderWidth = 3;
            series.Color = Color.FromArgb(255, 100, 0); 
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 10;
            series.MarkerColor = Color.White;
            series.MarkerBorderColor = series.Color;
            series.MarkerBorderWidth = 2;

            series.XValueType = ChartValueType.DateTime;

            var dailyData = new Dictionary<DateTime, int>(); 
            DateTime now = DateTime.Now;

            foreach (var item in _cachedData)
            {
                DateTime t = DateTimeOffset.FromUnixTimeSeconds(item.dt).LocalDateTime;

                if (t.Date >= now.Date)
                {
                    DateTime dayKey = t.Date; 

                    if (!dailyData.ContainsKey(dayKey) || t.Hour == 12)
                    {
                        dailyData[dayKey] = item.main.aqi;
                    }
                }
            }

            int count = 0;
            foreach (var kvp in dailyData.OrderBy(k => k.Key)) 
            {
                int pointIndex = series.Points.AddXY(kvp.Key, kvp.Value);

                string status = "";
                switch (kvp.Value)
                {
                    case 1: status = "Tốt"; break;
                    case 2: status = "Khá"; break;
                    case 3: status = "TB"; break;
                    case 4: status = "Kém"; break;
                    case 5: status = "Nguy hại"; break;
                }
                series.Points[pointIndex].ToolTip = $"Ngày {kvp.Key:dd/MM}: AQI {kvp.Value} ({status})";

                count++;
                if (count >= 5) break;
            }

            chartPollution.Series.Add(series);
        }

        private void chartPollution_Click(object sender, EventArgs e)
        {

        }
    }

    public class CustomerStats
    {
        public float TotalContracts { get; set; }
        public float CorrectionCount { get; set; }
        public string WasOnTime { get; set; }
        public float DaysOverdue { get; set; }
    }
}