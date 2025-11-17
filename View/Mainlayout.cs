using Environmental_Monitoring;
using Environmental_Monitoring.View;
using Environmental_Monitoring.View.Components;
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
using Environmental_Monitoring.Controller;
using Environmental_Monitoring.Properties; 
using Environmental_Monitoring.Controller.Data; 
using System.IO; 

namespace Environmental_Monitoring.View
{
    public partial class Mainlayout : Form
    {
        #region Fields & Properties

        private MenuButton currentActiveButton;
        private bool isDragging = false;
        private Point lastLocation;

        private bool isMenuCollapsed = true;
        private int menuCollapsedWidth = 90;
        private int menuExpandedWidth = 190;

        private Notification notificationPage;
        private Contract contractPage;
        private Employee employeePage;
        private Stats statsPage;
        private Setting settingPage;
        private AI aiPage;
        private Introduce introducePage;

        private int unreadCount = 0;
        private NotificationBell notificationBell;

        #endregion

        #region Initialization

        public Mainlayout()
        {
            InitializeComponent();
            InitializeFormProperties();
            RegisterEventHandlers();

            panelMenu.Width = menuCollapsedWidth;
            SetMenuState(isMenuCollapsed);
            ApplyTheme();
        }


        private void InitializeFormProperties()
        {
            this.Load += new System.EventHandler(this.Mainlayout_Load);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.DoubleBuffered = true;
        }

        private void RegisterEventHandlers()
        {
            btnToggleMenu.Click += new EventHandler(btnToggleMenu_Click);
            btnHome.Click += new EventHandler(MenuButton_Click);
            btnUser.Click += new EventHandler(MenuButton_Click);
            btnContracts.Click += new EventHandler(MenuButton_Click);
            btnNotification.Click += new EventHandler(MenuButton_Click);
            btnStats.Click += new EventHandler(MenuButton_Click);
            btnSetting.Click += new EventHandler(MenuButton_Click);
            btnAI.Click += new EventHandler(MenuButton_Click);
            btnIntroduce.Click += new EventHandler(MenuButton_Click);

            iconBell.Click -= iconBell_Click;
            iconBell.Click += new EventHandler(iconBell_Click);

            panel.MouseDown += new MouseEventHandler(panelHeadder_MouseDown);
            panel.MouseMove += new MouseEventHandler(panelHeadder_MouseMove);
            panel.MouseUp += new MouseEventHandler(panelHeadder_MouseUp);

            if (this.pbLogout != null)
            {
                this.pbLogout.Click += new System.EventHandler(this.pbLogout_Click);
            }
        }

        private async void Mainlayout_Load(object sender, EventArgs e)
        {
            ApplyPermissions();
            LoadHomePageForRole();
            UpdateUIText();

            if (UserSession.CurrentUser != null)
            {
                lblUserName.Text = UserSession.CurrentUser.HoTen;
            }
            else
            {
                lblUserName.Text = "Guest";
            }

            await RunDailyNotificationChecks();

            CheckForUnreadNotifications();

            if (this.timerNotifications != null)
            {
                timerNotifications.Interval = 60000;
                timerNotifications.Tick += TimerNotifications_Tick;
                timerNotifications.Start();
            }

            NotificationService.OnNotificationCreated += RefreshNotificationCount;

        }

        #endregion


        #region Permissions and Role Logic

        private void ApplyPermissions()
        {
            if (UserSession.IsAdmin())
            {
                return;
            }
            btnUser.Enabled = false;
        }

        private void LoadDefaultPageForRole()
        {
            UserControl defaultPage = GetOrCreatePage(ref notificationPage);
            MenuButton defaultButton = btnNotification;

            LoadPage(defaultPage);
            ResetAllButtons();
            HighlightButton(defaultButton);
        }

        private void LoadHomePageForRole()
        {
            UserControl homePage = null;
            MenuButton homeButton;

            string roleName = UserSession.CurrentUser?.Role?.RoleName ?? "";
            string cleanRoleName = roleName.ToLowerInvariant().Trim();

            switch (cleanRoleName)
            {
                case "admin":
                    homePage = GetOrCreatePage(ref notificationPage);
                    homeButton = btnNotification;
                    break;

                case "plan":
                case "business":
                case "field":
                case "lab":
                case "result":
                    homePage = GetOrCreatePage(ref contractPage);
                    homeButton = btnContracts;
                    break;

                default:
                    homePage = GetOrCreatePage(ref notificationPage);
                    homeButton = btnNotification;
                    break;
            }

            LoadPage(homePage);
            ResetAllButtons();
            HighlightButton(homeButton);
        }

        public void LoadContractPageForEmployee()
        {
            LoadPage(GetOrCreatePage(ref contractPage));
            ResetAllButtons();
            HighlightButton(btnContracts);
        }

        #endregion

        #region Page Navigation & Loading

        private T GetOrCreatePage<T>(ref T pageInstance) where T : UserControl, new()
        {
            if (pageInstance == null || pageInstance.IsDisposed)
            {
                pageInstance = new T();
                pageInstance.Dock = DockStyle.Fill;
                panelContent.Controls.Add(pageInstance);
            }
            return pageInstance;
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            MenuButton clickedButton = (MenuButton)sender;

            if (clickedButton == btnHome)
            {
                LoadHomePageForRole();
                return;
            }

            MenuButton oldActiveButton = currentActiveButton;
            ResetAllButtons();
            if (oldActiveButton != null)
            {
                oldActiveButton.Invalidate();
            }

            HighlightButton(clickedButton);

            if (clickedButton == btnUser)
            {
                LoadPage(GetOrCreatePage(ref employeePage));
            }
            else if (clickedButton == btnContracts)
            {
                LoadPage(GetOrCreatePage(ref contractPage));
            }
            else if (clickedButton == btnNotification)
            {
                LoadPage(GetOrCreatePage(ref notificationPage));
            }
            else if (clickedButton == btnStats)
            {
                LoadPage(GetOrCreatePage(ref statsPage));
            }
            else if (clickedButton == btnSetting)
            {
                LoadPage(GetOrCreatePage(ref settingPage));
            }
            else if (clickedButton == btnAI)
            {
                LoadPage(GetOrCreatePage(ref aiPage));
            }
            else if (clickedButton == btnIntroduce)
            {
                LoadPage(GetOrCreatePage(ref introducePage));
            }
        }

        private void LoadPage(UserControl pageToLoad)
        {
            if (pageToLoad == null) return;
            pageToLoad.BringToFront();
        }

        private void HighlightButton(MenuButton selectedButton)
        {
            currentActiveButton = selectedButton;
            selectedButton.IsSelected = true;
        }

        private void ResetAllButtons()
        {
            foreach (Control ctrl in panelMenu.Controls)
            {
                if (ctrl is MenuButton btn)
                {
                    btn.IsSelected = false;
                }
            }
        }

        #endregion

        #region Menu Collapse/Expand Logic

        private void btnToggleMenu_Click(object sender, EventArgs e)
        {
            isMenuCollapsed = !isMenuCollapsed;
            int newMenuWidth = isMenuCollapsed ? menuCollapsedWidth : menuExpandedWidth;

            this.SuspendLayout();

            panelMenu.Width = newMenuWidth;

            panel.Left = newMenuWidth;
            panel.Width = this.ClientSize.Width - newMenuWidth;
            panelContent.Left = newMenuWidth;
            panelContent.Top = panel.Height;
            panelContent.Width = this.ClientSize.Width - newMenuWidth;
            panelContent.Height = this.ClientSize.Height - panel.Height;

            SetMenuState(isMenuCollapsed);

            this.ResumeLayout(true);
        }

        private void SetMenuState(bool collapsed)
        {
            try
            {
                ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Mainlayout).Assembly);
                CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

                btnHome.Text = collapsed ? "" : rm.GetString("Menu_Home", culture);
                btnUser.Text = collapsed ? "" : rm.GetString("Menu_User", culture);
                btnContracts.Text = collapsed ? "" : rm.GetString("Menu_Contracts", culture);
                btnNotification.Text = collapsed ? "" : rm.GetString("Menu_Notification", culture);
                btnStats.Text = collapsed ? "" : rm.GetString("Menu_Stats", culture);
                btnSetting.Text = collapsed ? "" : rm.GetString("Menu_Settings", culture);
                btnAI.Text = collapsed ? "" : rm.GetString("Menu_AI", culture);
                btnIntroduce.Text = collapsed ? "" : rm.GetString("Menu_Introduce", culture);
            }
            catch (Exception ex)
            {
                ShowGlobalAlert("Lỗi tải tài nguyên ngôn ngữ: " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        #endregion

        #region Borderless Form Dragging

        private void panelHeadder_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastLocation = e.Location;
            }
        }

        private void panelHeadder_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void panelHeadder_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        #endregion

        #region Theme & Language

        private void ApplyTheme()
        {
            this.BackColor = ThemeManager.BackgroundColor;
            this.BackgroundImage = ThemeManager.BackgroundImage;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            panelMenu.BackColor = ThemeManager.PanelColor;

            foreach (Control ctrl in panelMenu.Controls)
            {
                if (ctrl is MenuButton btn)
                {
                    btn.ForeColor = ThemeManager.SecondaryTextColor;
                    btn.BackColor = ThemeManager.PanelColor;
                }
            }

            panelContent.BackColor = ThemeManager.BackgroundColor;

            if (lblUserName != null)
            {
                lblUserName.ForeColor = ThemeManager.TextColor;
            }
        }

        public void UpdateUIText()
        {
            SetMenuState(isMenuCollapsed);
            ApplyTheme();
        }

        public void UpdateAllChildLanguages()
        {
            this.UpdateUIText();

            if (panelContent.Controls.Count > 0)
            {
                foreach (Control ctl in panelContent.Controls)
                {
                    try
                    {
                        var method = ctl.GetType().GetMethod("UpdateUIText");
                        if (method != null)
                        {
                            method.Invoke(ctl, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowGlobalAlert($"Không thể cập nhật ngôn ngữ cho trang {ctl.Name}: {ex.Message}", AlertPanel.AlertType.Error);
                    }
                }
            }
        }

        #endregion

        #region Utilities & Form Events

        public void ShowGlobalAlert(string message, AlertPanel.AlertType type)
        {
            if (globalAlertPanel != null)
            {
                globalAlertPanel.ShowAlert(message, type);
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbLogout_Click(object sender, EventArgs e)
        {
            ResourceManager rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Mainlayout).Assembly);
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

            string title = rm.GetString("Logout_Confirm_Title", culture);
            string message = rm.GetString("Logout_Confirm_Message", culture);

            DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                UserSession.EndSession();
                Login loginForm = new Login();
                loginForm.Show();
                this.Close();
            }
        }

        #endregion

        #region Notification Logic

        /// <summary>
        /// Sự kiện chạy mỗi phút để kiểm tra thông báo mới.
        /// </summary>
        private void TimerNotifications_Tick(object sender, EventArgs e)
        {
            CheckForUnreadNotifications();
        }

        /// <summary>
        /// Kiểm tra CSDL xem có thông báo chưa đọc CỦA NGƯỜI DÙNG NÀY không.
        /// </summary>
        public void CheckForUnreadNotifications()
        {
            if (UserSession.CurrentUser == null || UserSession.CurrentUser.RoleID == null)
            {
                iconBell.Visible = false;
                return;
            }

            int currentUserRoleID = UserSession.CurrentUser.RoleID.Value;
            iconBell.Visible = true;

            try
            {
                string query = "SELECT COUNT(*) FROM Notifications WHERE DaDoc = 0 AND RecipientRoleID = @roleId";
                int newCount = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query, new object[] { currentUserRoleID }));

                if (newCount != unreadCount)
                {
                    unreadCount = newCount;
                    UpdateBellIcon();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi kiểm tra thông báo: " + ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật hình ảnh chuông (và/hoặc Badge)
        /// </summary>
        private void UpdateBellIcon()
        {
            if (unreadCount > 0)
            {
                lblBadge.Text = unreadCount.ToString();
                lblBadge.Visible = true;
                lblBadge.BringToFront();
                iconBell.BackColor = Color.Transparent;
            }
            else
            {
                lblBadge.Visible = false;
                iconBell.BackColor = Color.Transparent;
            }
        }

        /// <summary>
        /// Chạy kiểm tra các thông báo tự động (hết hạn, trễ hạn) - Chỉ dành cho Admin.
        /// </summary>
        private async Task RunDailyNotificationChecks()
        {
            if (!UserSession.IsAdmin() || UserSession.CurrentUser?.RoleID.Value != 5)
            {
                return;
            }

            if (DateTime.Now.Date <= Settings.Default.LastNotificationCheck.Date)
            {
                return;
            }

            int adminRoleID = UserSession.CurrentUser.RoleID.Value;

            try
            {
                string overdueQuery = @"SELECT ContractID, MaDon, NgayTraKetQua FROM Contracts 
                                        WHERE (Status IS NULL OR Status != 'Completed') 
                                        AND NgayTraKetQua < CURDATE()";

                DataTable overdueContracts = await Task.Run(() => DataProvider.Instance.ExecuteQuery(overdueQuery));
                foreach (DataRow row in overdueContracts.Rows)
                {
                    int contractId = Convert.ToInt32(row["ContractID"]);
                    string maDon = row["MaDon"].ToString();
                    DateTime ngayTra = Convert.ToDateTime(row["NgayTraKetQua"]);
                    int daysOverdue = (DateTime.Now.Date - ngayTra.Date).Days;

                    string noiDung = $"Hợp đồng '{maDon}' đã trễ hạn {daysOverdue} ngày.";
                    await CreateDailyNotificationOnce(contractId, "QuaHan", noiDung, adminRoleID);
                }

                string expiringQuery = @"SELECT ContractID, MaDon, NgayTraKetQua FROM Contracts 
                                         WHERE (Status IS NULL OR Status != 'Completed') 
                                         AND NgayTraKetQua BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 7 DAY)";

                DataTable expiringContracts = await Task.Run(() => DataProvider.Instance.ExecuteQuery(expiringQuery));
                foreach (DataRow row in expiringContracts.Rows)
                {
                    int contractId = Convert.ToInt32(row["ContractID"]);
                    string maDon = row["MaDon"].ToString();
                    DateTime ngayTra = Convert.ToDateTime(row["NgayTraKetQua"]);
                    int daysLeft = (ngayTra.Date - DateTime.Now.Date).Days;

                    string noiDung = $"Hợp đồng '{maDon}' sắp hết hạn, còn {daysLeft} ngày.";
                    await CreateDailyNotificationOnce(contractId, "SapHetHan", noiDung, adminRoleID);
                }

                Settings.Default.LastNotificationCheck = DateTime.Now;
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                ShowGlobalAlert("Lỗi khi chạy kiểm tra thông báo: " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        /// <summary>
        /// Hàm phụ: Đảm bảo chỉ tạo 1 thông báo/ngày cho mỗi loại VÀ MỖI ROLE.
        /// </summary>
        private async Task CreateDailyNotificationOnce(int contractId, string loai, string noiDung, int recipientRoleID)
        {
            string checkQuery = @"SELECT COUNT(*) FROM Notifications 
                                  WHERE ContractID = @contractId 
                                  AND LoaiThongBao = @loai 
                                  AND RecipientRoleID = @roleId
                                  AND DATE(ThoiGianGui) = CURDATE()";

            int count = Convert.ToInt32(await Task.Run(() =>
                DataProvider.Instance.ExecuteScalar(checkQuery, new object[] { contractId, loai, recipientRoleID })
            ));

            if (count == 0)
            {
                NotificationService.CreateNotification(loai, noiDung, recipientRoleID, contractId, null);
            }
        }

        #endregion

        private void iconBell_Click(object sender, EventArgs e)
        {
            if (UserSession.CurrentUser == null || UserSession.CurrentUser.RoleID == null) return;
            int currentUserRoleID = UserSession.CurrentUser.RoleID.Value;

            if (notificationBell == null || notificationBell.IsDisposed)
            {
                notificationBell = new NotificationBell();

                Point bellScreenLocation = iconBell.PointToScreen(Point.Empty);

                int formX = bellScreenLocation.X + iconBell.Width - notificationBell.Width;

                int formY = bellScreenLocation.Y + iconBell.Height + 5;
                notificationBell.Location = new Point(formX, formY);

                notificationBell.Show();
                notificationBell.LoadNotifications(currentUserRoleID);
            }
            else
            {
                notificationBell.Close();
                return;
            }

            try
            {
                string query = "UPDATE Notifications SET DaDoc = 1 WHERE DaDoc = 0 AND RecipientRoleID = @roleId";
                DataProvider.Instance.ExecuteNonQuery(query, new object[] { currentUserRoleID });

                unreadCount = 0;
                UpdateBellIcon();
            }
            catch (Exception ex)
            {
                ShowGlobalAlert("Lỗi đánh dấu đã đọc: " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        /// <summary>
        /// Hàm này được gọi bằng Event mỗi khi có thông báo mới được tạo
        /// từ BẤT CỨ ĐÂU trong ứng dụng.
        /// </summary>
        private void RefreshNotificationCount()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(CheckForUnreadNotifications));
            }
            else
            {
                CheckForUnreadNotifications();
            }
        }

        private void btnTestNotify_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserSession.CurrentUser == null || UserSession.CurrentUser.RoleID == null)
                {
                    MessageBox.Show("Bạn chưa đăng nhập để lấy RoleID!");
                    return;
                }

                int currentUserRoleID = UserSession.CurrentUser.RoleID.Value;
                string noiDung = "Đây là thông báo test lúc " + DateTime.Now.ToLongTimeString();

                // SỬA ĐỔI TẠI ĐÂY:
                // Dùng một giá trị ENUM hợp lệ từ database của bạn
                NotificationService.CreateNotification(
                    "HopDongMoi",       // <-- THAY "Test" BẰNG "HopDongMoi"
                    noiDung,            // noiDung
                    currentUserRoleID,  // recipientRoleID
                    null,               // contractId 
                    null                // employeeId (file NotificationService của bạn có tên này)
                );

                CheckForUnreadNotifications();
                ShowGlobalAlert("Đã gửi thông báo test thành công!", AlertPanel.AlertType.Success);
            }
            catch (Exception ex)
            {
                ShowGlobalAlert("Lỗi gửi thông báo test: " + ex.Message, AlertPanel.AlertType.Error);
            }
        }
    }
}