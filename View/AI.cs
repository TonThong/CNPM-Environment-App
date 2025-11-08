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

namespace Environmental_Monitoring.View
{
    public partial class AI : UserControl
    {
        public AI()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.AI_Load);
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
    }
}