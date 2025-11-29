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

        // Các trang con
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

        // Thiết lập thuộc tính cơ bản của Form
        private void InitializeFormProperties()
        {
            this.Load += new System.EventHandler(this.Mainlayout_Load);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.DoubleBuffered = true;
        }

        // Đăng ký các sự kiện click và kéo thả
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

        // Xử lý logic khi Form vừa khởi chạy
        private async void Mainlayout_Load(object sender, EventArgs e)
        {
            await UpdateContractStatuses();

            ApplyPermissions();
            LoadHomePageForRole(); // Load trang chủ và áp dụng quyền ngay lập tức
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
            await RunDailyTasksInternal();
        }

        #endregion

        #region Permissions and Role Logic

        // Ẩn nút quản lý nhân viên nếu không phải Admin
        private void ApplyPermissions()
        {
            if (UserSession.IsAdmin())
            {
                return;
            }
            btnUser.Enabled = false;
        }

        // Gọi hàm phân quyền bên trong trang Contract
        private void ConfigureContractPermissions()
        {
            GetOrCreatePage(ref contractPage);

            if (contractPage != null && UserSession.CurrentUser != null)
            {
                string roleName = UserSession.CurrentUser.Role?.RoleName ?? "";
                contractPage.SetTabAccess(roleName);
            }
        }

        // Tải trang chủ mặc định dựa trên vai trò người dùng
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
                    // Kích hoạt phân quyền Tab ngay khi load trang
                    ConfigureContractPermissions();
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

        // Hàm hỗ trợ để các form khác gọi về trang hợp đồng
        public void LoadContractPageForEmployee()
        {
            LoadPage(GetOrCreatePage(ref contractPage));
            ConfigureContractPermissions(); // Đảm bảo phân quyền lại khi gọi hàm này
            ResetAllButtons();
            HighlightButton(btnContracts);
        }

        #endregion

        #region Page Navigation & Loading

        // Tạo mới trang nếu chưa có hoặc trả về trang đã cache
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

        // Xử lý sự kiện click menu sidebar
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

            if (clickedButton == btnUser) LoadPage(GetOrCreatePage(ref employeePage));
            else if (clickedButton == btnContracts)
            {
                LoadPage(GetOrCreatePage(ref contractPage));
                ConfigureContractPermissions(); // Phân quyền Tab ngay khi bấm vào
            }
            else if (clickedButton == btnNotification) LoadPage(GetOrCreatePage(ref notificationPage));
            else if (clickedButton == btnStats) LoadPage(GetOrCreatePage(ref statsPage));
            else if (clickedButton == btnSetting) LoadPage(GetOrCreatePage(ref settingPage));
            else if (clickedButton == btnAI) LoadPage(GetOrCreatePage(ref aiPage));
            else if (clickedButton == btnIntroduce) LoadPage(GetOrCreatePage(ref introducePage));
        }

        // Hiển thị UserControl lên panel chính
        private void LoadPage(UserControl pageToLoad)
        {
            if (pageToLoad == null) return;
            pageToLoad.BringToFront();
        }

        // Đổi trạng thái visual của nút được chọn
        private void HighlightButton(MenuButton selectedButton)
        {
            currentActiveButton = selectedButton;
            selectedButton.IsSelected = true;
        }

        // Reset trạng thái visual của tất cả nút
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

        // Xử lý mở/đóng menu sidebar
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

        // Cập nhật text hiển thị trên nút menu
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

        // Áp dụng theme màu sắc cho form
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

        // Cập nhật ngôn ngữ cho toàn bộ giao diện
        public void UpdateUIText()
        {
            SetMenuState(isMenuCollapsed);
            ApplyTheme();
        }

        // Cập nhật ngôn ngữ cho tất cả các control con
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

        // Hiển thị thông báo dạng popup
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

        // Đóng ứng dụng
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Đăng xuất người dùng
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

        // Timer kiểm tra thông báo và hợp đồng định kỳ
        private async void TimerNotifications_Tick(object sender, EventArgs e)
        {
            CheckForUnreadNotifications();
            await UpdateContractStatuses();

            DateTime now = DateTime.Now;
            if (now.Hour == 0 && now.Minute == 1)
            {
                if (Settings.Default.LastNotificationCheck.Date < now.Date)
                {
                    try
                    {
                        await RunDailyTasksInternal();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi chạy tác vụ hàng ngày tự động: " + ex.Message);
                    }
                }
            }
        }

        // Cập nhật trạng thái hợp đồng quá hạn trong DB
        private async Task UpdateContractStatuses()
        {
            try
            {
                string updateExpiredQuery = @"
                    UPDATE Contracts
                    SET Status = 'Expired'
                    WHERE NgayTraKetQua < CURDATE()
                      AND Status = 'Active';";

                await Task.Run(() => DataProvider.Instance.ExecuteNonQuery(updateExpiredQuery));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi cập nhật trạng thái hợp đồng: " + ex.Message);
            }
        }

        // Kiểm tra số lượng thông báo chưa đọc
        public void CheckForUnreadNotifications()
        {
            if (UserSession.CurrentUser == null || UserSession.CurrentUser.RoleID == null)
            {
                if (iconBell != null) iconBell.Visible = false;
                if (lblBadge != null) lblBadge.Visible = false;
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

        // Cập nhật icon chuông thông báo
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

        // Chạy kiểm tra thông báo hàng ngày (chỉ Admin)
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

            try
            {
                await RunDailyTasksInternal();
            }
            catch (Exception ex)
            {
                ShowGlobalAlert("Lỗi khi chạy kiểm tra thông báo: " + ex.Message, AlertPanel.AlertType.Error);
            }
        }

        // Logic chính: Gửi mail và tạo thông báo hợp đồng
        private async Task RunDailyTasksInternal()
        {
            await UpdateContractStatuses();

            int adminRoleID = 5;
            string adminEmail = await GetEmailByRoleID(adminRoleID);

            var deptHeadOverdueMails = new Dictionary<int, StringBuilder>();
            var deptHeadExpiringMails = new Dictionary<int, StringBuilder>();

            StringBuilder adminOverdueBody = new StringBuilder();
            StringBuilder adminExpiringBody = new StringBuilder();

            int adminOverdueCount = 0;
            int adminExpiringCount = 0;

            string baseQuery = @"SELECT c.ContractID, c.MaDon, c.NgayTraKetQua, c.TienTrinh, cus.TenDoanhNghiep 
                                 FROM Contracts c 
                                 JOIN Customers cus ON c.CustomerID = cus.CustomerID";

            // XỬ LÝ HỢP ĐỒNG QUÁ HẠN
            string overdueQuery = baseQuery + " WHERE c.Status = 'Expired' AND c.NgayTraKetQua < CURDATE()";
            DataTable overdueContracts = await Task.Run(() => DataProvider.Instance.ExecuteQuery(overdueQuery));

            foreach (DataRow row in overdueContracts.Rows)
            {
                int contractId = Convert.ToInt32(row["ContractID"]);
                string maDon = row["MaDon"].ToString();
                string tenCongTy = row["TenDoanhNghiep"].ToString();
                int tienTrinh = Convert.ToInt32(row["TienTrinh"]);
                DateTime ngayTra = Convert.ToDateTime(row["NgayTraKetQua"]);
                int daysOverdue = (DateTime.Now.Date - ngayTra.Date).Days;

                string noiDung = $"Hợp đồng '{maDon}' ({tenCongTy}) đã trễ hạn {daysOverdue} ngày.";
                string mailLine = $"- <b>{maDon} - {tenCongTy}</b>: Trễ {daysOverdue} ngày (Hạn chót: {ngayTra:dd/MM/yyyy})<br/>";

                bool isNew = await CreateDailyNotificationOnce(contractId, "QuaHan", noiDung, adminRoleID);
                int headRoleID = GetDepartmentHeadRoleID(tienTrinh);
                if (headRoleID > 0)
                {
                    await CreateDailyNotificationOnce(contractId, "QuaHan", noiDung, headRoleID);
                }

                if (isNew)
                {
                    adminOverdueBody.AppendLine(mailLine);
                    adminOverdueCount++;

                    if (headRoleID > 0)
                    {
                        if (!deptHeadOverdueMails.ContainsKey(headRoleID)) deptHeadOverdueMails[headRoleID] = new StringBuilder();
                        deptHeadOverdueMails[headRoleID].AppendLine(mailLine);
                    }
                }
            }

            // XỬ LÝ HỢP ĐỒNG SẮP HẾT HẠN
            string expiringQuery = baseQuery + " WHERE c.Status = 'Active' AND c.NgayTraKetQua BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 5 DAY)";
            DataTable expiringContracts = await Task.Run(() => DataProvider.Instance.ExecuteQuery(expiringQuery));

            foreach (DataRow row in expiringContracts.Rows)
            {
                int contractId = Convert.ToInt32(row["ContractID"]);
                string maDon = row["MaDon"].ToString();
                string tenCongTy = row["TenDoanhNghiep"].ToString();
                int tienTrinh = Convert.ToInt32(row["TienTrinh"]);
                DateTime ngayTra = Convert.ToDateTime(row["NgayTraKetQua"]);
                int daysLeft = (ngayTra.Date - DateTime.Now.Date).Days;

                string noiDung = $"Hợp đồng '{maDon}' ({tenCongTy}) sắp hết hạn, còn {daysLeft} ngày.";
                string mailLine = $"- <b>{maDon} - {tenCongTy}</b>: Còn {daysLeft} ngày (Hạn chót: {ngayTra:dd/MM/yyyy})<br/>";

                bool isNew = await CreateDailyNotificationOnce(contractId, "SapHetHan", noiDung, adminRoleID);
                int headRoleID = GetDepartmentHeadRoleID(tienTrinh);
                if (headRoleID > 0)
                {
                    await CreateDailyNotificationOnce(contractId, "SapHetHan", noiDung, headRoleID);
                }

                if (isNew)
                {
                    adminExpiringBody.AppendLine(mailLine);
                    adminExpiringCount++;

                    if (headRoleID > 0)
                    {
                        if (!deptHeadExpiringMails.ContainsKey(headRoleID)) deptHeadExpiringMails[headRoleID] = new StringBuilder();
                        deptHeadExpiringMails[headRoleID].AppendLine(mailLine);
                    }
                }
            }

            // GỬI MAIL
            if (adminOverdueCount > 0 && !string.IsNullOrEmpty(adminEmail))
            {
                string subject = $"[CẢNH BÁO] Tổng hợp {adminOverdueCount} hợp đồng QUÁ HẠN (Toàn hệ thống)";
                string body = $"<h3>Danh sách hợp đồng quá hạn trên toàn hệ thống:</h3>{adminOverdueBody}<p>Vui lòng kiểm tra và đôn đốc các bộ phận.</p>";
                await EmailService.SendEmailAsync(adminEmail, subject, body);
            }
            if (adminExpiringCount > 0 && !string.IsNullOrEmpty(adminEmail))
            {
                string subject = $"[NHẮC NHỞ] Tổng hợp {adminExpiringCount} hợp đồng SẮP HẾT HẠN (Toàn hệ thống)";
                string body = $"<h3>Danh sách hợp đồng sắp hết hạn trên toàn hệ thống:</h3>{adminExpiringBody}<p>Vui lòng kiểm tra.</p>";
                await EmailService.SendEmailAsync(adminEmail, subject, body);
            }

            foreach (var kvp in deptHeadOverdueMails)
            {
                int roleID = kvp.Key;
                string content = kvp.Value.ToString();
                string email = await GetEmailByRoleID(roleID);

                if (!string.IsNullOrEmpty(email))
                {
                    string subject = $"[CẢNH BÁO] Phòng bạn có hợp đồng QUÁ HẠN xử lý";
                    string body = $"<h3>Các hợp đồng đang nằm tại phòng bạn đã quá hạn:</h3>{content}<p>Vui lòng xử lý gấp.</p>";
                    await EmailService.SendEmailAsync(email, subject, body);
                }
            }

            foreach (var kvp in deptHeadExpiringMails)
            {
                int roleID = kvp.Key;
                string content = kvp.Value.ToString();
                string email = await GetEmailByRoleID(roleID);

                if (!string.IsNullOrEmpty(email))
                {
                    string subject = $"[NHẮC NHỞ] Phòng bạn có hợp đồng SẮP HẾT HẠN";
                    string body = $"<h3>Các hợp đồng đang nằm tại phòng bạn sắp hết hạn:</h3>{content}<p>Vui lòng ưu tiên xử lý.</p>";
                    await EmailService.SendEmailAsync(email, subject, body);
                }
            }

            Settings.Default.LastNotificationCheck = DateTime.Now;
            Settings.Default.Save();
        }

        // Lấy Email theo RoleID
        private async Task<string> GetEmailByRoleID(int roleID)
        {
            try
            {
                string query = "SELECT Email FROM Employees WHERE RoleID = @roleId LIMIT 1";
                object result = await Task.Run(() => DataProvider.Instance.ExecuteScalar(query, new object[] { roleID }));
                if (result != null && result != DBNull.Value) return result.ToString();
            }
            catch { }
            return "";
        }

        // Tạo thông báo DB (chỉ 1 lần/ngày)
        private async Task<bool> CreateDailyNotificationOnce(int contractId, string loai, string noiDung, int recipientRoleID)
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
                return true;
            }

            return false;
        }

        #endregion

        // Mở popup thông báo
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

        // Cập nhật số thông báo (gọi từ luồng khác)
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

        // Mapping RoleID theo tiến trình
        private int GetDepartmentHeadRoleID(int tienTrinh)
        {
            switch (tienTrinh)
            {
                case 1: return 6;  // Kế hoạch -> RoleID 6
                case 2: return 10; // Hiện trường -> RoleID 10
                case 3: return 7;  // Thí nghiệm -> RoleID 7
                case 4: return 8;  // Kết quả -> RoleID 8

                default: return 0;
            }
        }
    }
}