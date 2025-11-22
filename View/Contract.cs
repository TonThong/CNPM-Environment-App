using Environmental_Monitoring.Controller;
using Environmental_Monitoring.View.Components;
using Environmental_Monitoring.View.ContractContent;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Environmental_Monitoring.View
{
    internal partial class Contract : UserControl
    {
        #region Fields and Properties

        private Color tabDefaultColor = Color.Transparent;
        private Components.RoundedButton currentActiveTab;

        private ResourceManager rm;
        private CultureInfo culture;

        #endregion

        #region Initialization

        public Contract()
        {
            InitializeComponent();
            InitializeLocalization();

            this.btnManager.Click += new System.EventHandler(this.btnManager_Click);

            this.Load += new System.EventHandler(this.Contract_Load);
            this.VisibleChanged += new System.EventHandler(this.Contract_VisibleChanged);
        }

        private void Contract_Load(object sender, EventArgs e)
        {
            UpdateUIText();
        }

        private void Contract_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                ApplyRolePermissions();
            }
        }

        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Contract).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;
        }

        #endregion

        #region Core Logic (Permissions, UI Updates, Page Loading)

        /// <summary>
        /// Xử lý phím Enter trên thanh tìm kiếm (giống logic bên Employee)
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Kiểm tra nếu phím nhấn là Enter và con trỏ đang ở ô tìm kiếm (roundedTextBox1)
            if (keyData == Keys.Enter && this.ActiveControl == roundedTextBox1)
            {
                string searchText = roundedTextBox1.Text.Trim();

                // Kiểm tra xem có tab nào đang hiển thị trong pnContent không
                if (pnContent.Controls.Count > 0)
                {
                    Control currentChildPage = pnContent.Controls[0];
                    try
                    {
                        // Dùng Reflection để gọi hàm PerformSearch(string) nếu tab con có hỗ trợ
                        var method = currentChildPage.GetType().GetMethod("PerformSearch");
                        if (method != null)
                        {
                            method.Invoke(currentChildPage, new object[] { searchText });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Search Error: " + ex.Message);
                    }
                }
                return true; // Đã xử lý phím Enter, không kêu 'ding'
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ResetTabsForEmployee()
        {
            btnBusiness.Enabled = false;
            btnPlan.Enabled = false;
            btnReal.Enabled = false;
            roundedButton1.Enabled = false;
            btnResult.Enabled = false;

            // Vô hiệu hóa nút Manager với nhân viên
            btnManager.Enabled = false;

            btnBusiness.BackColor = btnBusiness.BaseColor = tabDefaultColor;
            btnPlan.BackColor = btnPlan.BaseColor = tabDefaultColor;
            btnReal.BackColor = btnReal.BaseColor = tabDefaultColor;
            roundedButton1.BackColor = roundedButton1.BaseColor = tabDefaultColor;
            btnResult.BackColor = btnResult.BaseColor = tabDefaultColor;
            btnManager.BackColor = btnManager.BaseColor = tabDefaultColor;

            currentActiveTab = null;
        }

        private void ResetTabsForAdmin()
        {
            btnBusiness.Enabled = true;
            btnPlan.Enabled = true;
            btnReal.Enabled = true;
            roundedButton1.Enabled = true;
            btnResult.Enabled = true;

            // Cho phép Admin bấm nút Manager
            btnManager.Enabled = true;

            btnBusiness.BackColor = btnBusiness.BaseColor = tabDefaultColor;
            btnPlan.BackColor = btnPlan.BaseColor = tabDefaultColor;
            btnReal.BackColor = btnReal.BaseColor = tabDefaultColor;
            roundedButton1.BackColor = roundedButton1.BaseColor = tabDefaultColor;
            btnResult.BackColor = btnResult.BaseColor = tabDefaultColor;
            btnManager.BackColor = btnManager.BaseColor = tabDefaultColor;

            currentActiveTab = null;
        }

        private void ApplyRolePermissions()
        {
            string userRole = UserSession.CurrentUser?.Role?.RoleName ?? "";

            string cleanRoleName = userRole.ToLowerInvariant().Trim();

            if (UserSession.IsAdmin())
            {
                ResetTabsForAdmin();
                LoadPage(new BusinessContent());
                HighlightTab(btnBusiness);
                return;
            }

            ResetTabsForEmployee();

            switch (cleanRoleName)
            {
                case "business":
                    btnBusiness.Enabled = true;
                    LoadPage(new BusinessContent());
                    HighlightTab(btnBusiness);
                    break;
                case "plan":
                    btnPlan.Enabled = true;
                    LoadPage(new PlanContent());
                    HighlightTab(btnPlan);
                    break;
                case "field":
                    btnReal.Enabled = true;
                    LoadPage(new RealContent());
                    HighlightTab(btnReal);
                    break;
                case "lab":
                    roundedButton1.Enabled = true;
                    LoadPage(new ExperimentContent());
                    HighlightTab(roundedButton1);
                    break;
                case "result":
                    btnResult.Enabled = true;
                    LoadPage(new ResultContent());
                    HighlightTab(btnResult);
                    break;
                default:
                    pnContent.Controls.Clear();
                    break;
            }
        }

        private void HighlightTab(Components.RoundedButton selectedButton)
        {
            if (UserSession.IsAdmin() && currentActiveTab != null && currentActiveTab != selectedButton)
            {
                currentActiveTab.BackColor = tabDefaultColor;
                currentActiveTab.BaseColor = tabDefaultColor;
            }

            selectedButton.BackColor = ThemeManager.SecondaryPanelColor;
            selectedButton.BaseColor = ThemeManager.SecondaryPanelColor;

            currentActiveTab = selectedButton;
        }

        public void UpdateUIText()
        {
            culture = Thread.CurrentThread.CurrentUICulture;

            lbContract.Text = rm.GetString("Contract_Title", culture);
            roundedTextBox1.PlaceholderText = rm.GetString("Search_Placeholder", culture);

            btnBusiness.Text = rm.GetString("Contract_Tab_Business", culture);
            btnPlan.Text = rm.GetString("Contract_Tab_Plan", culture);
            btnReal.Text = rm.GetString("Contract_Tab_Scene", culture);
            roundedButton1.Text = rm.GetString("Contract_Tab_Lab", culture);
            btnResult.Text = rm.GetString("Contract_Tab_Result", culture);
            btnManager.Text = rm.GetString("Contract_Tab_Manager", culture) ?? "Quản Lý";

            if (pnContent.Controls.Count > 0)
            {
                Control currentChildPage = pnContent.Controls[0];
                try
                {
                    var method = currentChildPage.GetType().GetMethod("UpdateUIText");
                    if (method != null)
                    {
                        method.Invoke(currentChildPage, null);
                    }
                }
                catch (Exception) { }
            }

            try
            {
                this.BackColor = ThemeManager.BackgroundColor_tab;
                lbContract.ForeColor = ThemeManager.TextColor;

                btnBusiness.ForeColor = ThemeManager.TextColor;
                btnPlan.ForeColor = ThemeManager.TextColor;
                btnReal.ForeColor = ThemeManager.TextColor;
                roundedButton1.ForeColor = ThemeManager.TextColor;
                btnResult.ForeColor = ThemeManager.TextColor;
                btnManager.ForeColor = ThemeManager.TextColor;

                if (currentActiveTab != null)
                {
                    currentActiveTab.BackColor = ThemeManager.SecondaryPanelColor;
                    currentActiveTab.BaseColor = ThemeManager.SecondaryPanelColor;
                }

                roundedTextBox1.BackColor = ThemeManager.PanelColor;
                roundedTextBox1.ForeColor = ThemeManager.TextColor;

                pnContent.BackColor = ThemeManager.PanelColor;
            }
            catch (Exception) { }
        }

        private void LoadPage(UserControl pageToLoad)
        {
            pnContent.Controls.Clear();
            pageToLoad.Dock = DockStyle.Fill;
            pnContent.Controls.Add(pageToLoad);

            try
            {
                var method = pageToLoad.GetType().GetMethod("UpdateUIText");
                if (method != null)
                {
                    method.Invoke(pageToLoad, null);
                }
            }
            catch (Exception) { }
        }

        #endregion

        #region Tab Click Events

        private void btnBusiness_Click(object sender, EventArgs e)
        {
            LoadPage(new BusinessContent());
            HighlightTab(btnBusiness);
        }

        private void btnPlan_Click(object sender, EventArgs e)
        {
            LoadPage(new PlanContent());
            HighlightTab(btnPlan);
        }

        private void btnReal_Click(object sender, EventArgs e)
        {
            LoadPage(new RealContent());
            HighlightTab(btnReal);
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            LoadPage(new ExperimentContent());
            HighlightTab(roundedButton1);
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            LoadPage(new ResultContent());
            HighlightTab(btnResult);
        }

        private void btnManager_Click(object sender, EventArgs e)
        {
            if (UserSession.IsAdmin())
            {
                LoadPage(new ManagerContent());
                HighlightTab(btnManager);
            }
        }

        #endregion

        private void lbContract_Click(object sender, EventArgs e)
        {

        }
    }
}