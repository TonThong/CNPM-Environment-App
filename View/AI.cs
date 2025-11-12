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
using System.Windows.Forms.DataVisualization.Charting;
using Environmental_Monitoring;

namespace Environmental_Monitoring.View
{
    public partial class AI : UserControl
    {
        public AI()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.AI_Load);


            cmbLocation.Items.Add("HaNoi");
            cmbLocation.Items.Add("HCM");
            cmbLocation.SelectedIndex = 0;
        }

        private void AI_Load(object sender, EventArgs e)
        {

            UpdateUIText();
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
                txtCustomerName.PlaceholderText = rm.GetString("AI_Placeholder_Customer", culture);
                btnPredictResign.Text = rm.GetString("AI_Button_Predict", culture);

                lblPanelPollution.Text = rm.GetString("AI_Panel_Pollution", culture);
                lblPollutionLevel.Text = rm.GetString("AI_Label_PollutionLevel", culture);
                btnPredictPollution.Text = rm.GetString("AI_Button_Predict", culture);

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
                string errorMsg = rm.GetString("Alert_LoadLanguageError", culture);
                mainForm?.ShowGlobalAlert(errorMsg + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        private void btnPredictResign_Click(object sender, EventArgs e)
        {
            var inputData = new ResignModel.ModelInput()
            {
                ContractType = "Quy",
                WasOnTime = "Completed",
                PollutionCount = 0,
                WarningCount = 1,
                TotalContracts = 3
            };

            ResignModel.ModelOutput prediction = ResignModel.Predict(inputData);


            bool predictedLabel = prediction.PredictedLabel; 
            float probability = prediction.Score; 

            int percentTrue, percentFalse;

            if (predictedLabel == true)
            {
                percentTrue = (int)(probability * 100);
                percentFalse = 100 - percentTrue;
            }
            else
            {
                percentFalse = (int)(probability * 100);
                percentTrue = 100 - percentFalse;
            }

            if (this.Controls.Find("chartResign", true).FirstOrDefault() is Chart chartResign)
            {
                chartResign.Series.Clear();
                Series series = chartResign.Series.Add("ResignRate");
                series.ChartType = SeriesChartType.Pie;

                series.Points.Add(percentTrue);
                series.Points.Add(percentFalse);

                DataPoint dpTrue = series.Points[0];
                dpTrue.LegendText = "Khả năng tái ký";
                dpTrue.Label = $"{percentTrue}%";
                dpTrue.Color = Color.FromArgb(106, 90, 205); 

                DataPoint dpFalse = series.Points[1];
                dpFalse.LegendText = "Khả năng không tái ký";
                dpFalse.Label = $"{percentFalse}%";
                dpFalse.Color = Color.FromArgb(250, 128, 114); 

                if (chartResign.Legends.Count == 0) chartResign.Legends.Add("Default");
                chartResign.Legends[0].Docking = Docking.Right;
            }
        }

        private void btnPredictPollution_Click(object sender, EventArgs e)
        {
            PollutionModel.ModelOutput prediction = PollutionModel.Predict(horizon: 6);

            float[] forecasts = prediction.Value;

            if (this.Controls.Find("chartPollution", true).FirstOrDefault() is Chart chartPollution)
            {
                chartPollution.Series.Clear();
                Series series = chartPollution.Series.Add("PollutionLevel");
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 3;
                series.Color = Color.FromArgb(106, 90, 205); 
                series.MarkerStyle = MarkerStyle.Circle;
                series.MarkerSize = 8;

                DateTime currentDate = DateTime.Now;

                for (int i = 0; i < forecasts.Length; i++)
                {
                    string monthLabel = $"Th {currentDate.AddMonths(i + 1).Month}";
                    series.Points.AddXY(monthLabel, forecasts[i]);
                }

                chartPollution.ChartAreas[0].AxisY.Minimum = 0;
                if (chartPollution.Legends.Count > 0)
                {
                    chartPollution.Legends[0].Enabled = false;
                }
            }
        }
    }
}