using Environmental_Monitoring.Controller;
using Environmental_Monitoring.Controller.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.Components
{
    public partial class NotificationBell : Form
    {
        public NotificationBell()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Tải tất cả thông báo cho vai trò (role) được chỉ định.
        /// Đã được cập nhật để dùng FlowLayoutPanel.
        /// </summary>
        public void LoadNotifications(int recipientRoleID)
        {

            try
            {
                string query = @"SELECT NoiDung, ThoiGianGui 
                                 FROM Notifications 
                                 WHERE RecipientRoleID = @roleId 
                                 ORDER BY ThoiGianGui DESC 
                                 LIMIT 20"; 

                DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { recipientRoleID });

                int scrollBarWidth = SystemInformation.VerticalScrollBarWidth;
                int itemMargins = 6;
                int itemWidth = flowPanelNotifications.ClientSize.Width - scrollBarWidth - itemMargins;

                foreach (DataRow row in dt.Rows)
                {
                    string content = row["NoiDung"].ToString();
                    DateTime timeFromDb = Convert.ToDateTime(row["ThoiGianGui"]);

                    DateTime localTime = DateTime.SpecifyKind(timeFromDb, DateTimeKind.Utc).ToLocalTime();

                    string timeAgo = ConvertToTimeAgo(localTime);

                    NotificationItem item = new NotificationItem();

                    item.Width = itemWidth;            
                    item.SetContentWidth(itemWidth); 
                    item.SetData(content, timeAgo);   

                    flowPanelNotifications.Controls.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông báo: " + ex.Message);
            }
        }

        /// <summary>
        /// Hàm mới: Chuyển đổi DateTime thành "X phút trước", "Y giờ trước"...
        /// </summary>
        private string ConvertToTimeAgo(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                return $"{years} năm trước";
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                return $"{months} tháng trước";
            }
            if (span.Days > 0)
                return $"{span.Days} ngày trước";
            if (span.Hours > 0)
                return $"{span.Hours} giờ trước";
            if (span.Minutes > 0)
                return $"{span.Minutes} phút trước";
            if (span.Seconds > 5)
                return $"{span.Seconds} giây trước";
            return "vừa xong";
        }


        /// <summary>
        /// Tự động đóng Form khi người dùng bấm ra ngoài.
        /// </summary>
        private void NotificationForm_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Gán sự kiện Deactivate khi Form được tải.
        /// </summary>
        private void NotificationForm_Load(object sender, EventArgs e)
        {
            this.Deactivate += NotificationForm_Deactivate;
        }
    }
}