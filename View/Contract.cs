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
    // Thêm từ khóa "partial" ở đây
    internal partial class Contract : UserControl
    {
        #region Fields and Properties

        private Color tabDefaultColor = Color.Transparent;
        private Color tabSelectedColor = Color.FromArgb(192, 192, 192);
        private Components.RoundedButton currentActiveTab;

        private ResourceManager rm;
        private CultureInfo culture;

        #endregion

        #region Initialization

        public Contract()
        {
            InitializeComponent(); // Hàm này giờ nằm trong file .Designer.cs
            InitializeLocalization();
            this.Load += new System.EventHandler(this.Contract_Load);
        }

        private void Contract_Load(object sender, EventArgs e)
        {
            ApplyRolePermissions();
            UpdateUIText();
        }
        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Contract).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;
        }

        #endregion

        #region Core Logic (Permissions, UI Updates, Page Loading)
        private void ApplyRolePermissions()
        {
            string userRole = UserSession.CurrentUser?.Role?.RoleName ?? "";

            if (UserSession.IsAdmin())
            {
                btnBusiness.BackColor = btnBusiness.BaseColor = tabDefaultColor;
                btnPlan.BackColor = btnPlan.BaseColor = tabDefaultColor;
                btnReal.BackColor = btnReal.BaseColor = tabDefaultColor;
                roundedButton1.BackColor = roundedButton1.BaseColor = tabDefaultColor;
                btnResult.BackColor = btnResult.BaseColor = tabDefaultColor;

                LoadPage(new BusinessContent());
                HighlightTab(btnBusiness);
                return;
            }
            btnBusiness.Enabled = false;
            btnPlan.Enabled = false;
            btnReal.Enabled = false;
            roundedButton1.Enabled = false;
            btnResult.Enabled = false;

            switch (userRole.ToLowerInvariant())
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
                case "scene":
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
                    LoadPage(new BusinessContent());
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

            selectedButton.BackColor = tabSelectedColor;
            selectedButton.BaseColor = tabSelectedColor;

            currentActiveTab = selectedButton;
        }

        // SỬA HÀM NÀY
        public void UpdateUIText()
        {
            culture = Thread.CurrentThread.CurrentUICulture;

            // 1. Cập nhật các text của trang Contract (Tiêu đề, các tab)
            lbContract.Text = rm.GetString("Contract_Title", culture);
            roundedTextBox1.PlaceholderText = rm.GetString("Search_Placeholder", culture);

            btnBusiness.Text = rm.GetString("Contract_Tab_Business", culture);
            btnPlan.Text = rm.GetString("Contract_Tab_Plan", culture);
            btnReal.Text = rm.GetString("Contract_Tab_Scene", culture);
            roundedButton1.Text = rm.GetString("Contract_Tab_Lab", culture);
            btnResult.Text = rm.GetString("Contract_Tab_Result", culture);

            // 2. THÊM MỚI: Cập nhật luôn cho trang con đang được tải (ví dụ: PlanContent)
            if (pnContent.Controls.Count > 0)
            {
                Control currentChildPage = pnContent.Controls[0];
                try
                {
                    // Dùng reflection để gọi hàm "UpdateUIText" của trang con
                    var method = currentChildPage.GetType().GetMethod("UpdateUIText");
                    if (method != null)
                    {
                        method.Invoke(currentChildPage, null);
                    }
                }
                catch (Exception)
                {
                    // Bỏ qua nếu trang con không có hàm UpdateUIText
                }
            }
        }

        private void LoadPage(UserControl pageToLoad)
        {
            pnContent.Controls.Clear();
            pageToLoad.Dock = DockStyle.Fill;
            pnContent.Controls.Add(pageToLoad);

            // THÊM MỚI: Khi tải trang con mới, cũng cập nhật ngôn ngữ cho nó
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

        #endregion
    }
}
