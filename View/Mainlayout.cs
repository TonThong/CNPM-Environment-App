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

namespace Environmental_Monitoring.View
{
    public partial class Mainlayout : Form
    {
        // === KHAI BÁO TRẠNG THÁI ===
        private MenuButton currentActiveButton;

        // Đảm bảo không còn khai báo AnimationTimer, pnlIndicator, indicatorTargetY, AnimationStep ở đây
        // (Nếu chúng còn tồn tại trong Designer.cs thì vẫn ổn)

        public Mainlayout()
        {
            InitializeComponent();

            // Gán CÙNG MỘT sự kiện Click cho tất cả các nút menu
            btnHome.Click += new EventHandler(MenuButton_Click);
            btnUser.Click += new EventHandler(MenuButton_Click);
            btnContracts.Click += new EventHandler(MenuButton_Click);
            btnNotification.Click += new EventHandler(MenuButton_Click);
            btnStats.Click += new EventHandler(MenuButton_Click);
            btnSetting.Click += new EventHandler(MenuButton_Click);
            btnAI.Click += new EventHandler(MenuButton_Click);
            btnIntroduce.Click += new EventHandler(MenuButton_Click);

            // Tải trang mặc định khi mở ứng dụng
            HighlightButton(btnNotification); // Sử dụng HighlightButton (chuyển đổi tức thời)
            LoadPage(new Notification()); // Tải trang thông báo
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            // 1. Reset TẤT CẢ các nút (Tắt vạch xanh đậm của nút cũ)
            MenuButton oldActiveButton = currentActiveButton;
            ResetAllButtons();

            // Buộc nút cũ vẽ lại để hiển thị nền Transparent/InactiveColor
            if (oldActiveButton != null)
            {
                oldActiveButton.Invalidate();
            }

            MenuButton clickedButton = (MenuButton)sender;

            // 2. Tô sáng nút vừa nhấn (Chuyển đổi tức thời)
            HighlightButton(clickedButton);

            // 3. Tải Trang (UserControl) tương ứng
            if (clickedButton == btnNotification)
            {
                LoadPage(new Notification());
            }
            else if (clickedButton == btnStats)
            {
                LoadPage(new Stats());
            }
            else if (clickedButton == btnSetting)
            {
                LoadPage(new Setting());
            }
            else if (clickedButton == btnAI)
            {
                LoadPage(new AI());
            }
            else if (clickedButton == btnContracts)
            {
                LoadPage(new Contract());
            }
            // v.v...
        }

        // === Phương thức 1: Tô sáng nút được chọn (Chuyển đổi tức thời) ===
        private void HighlightButton(MenuButton selectedButton)
        {
            currentActiveButton = selectedButton;
            selectedButton.IsSelected = true;

            // Nếu bạn có Panel Indicator (pnlIndicator) và KHÔNG muốn thấy nó, hãy ẩn nó đi.
            // if (pnlIndicator != null) pnlIndicator.Visible = false; 

            // Giữ lại dòng này nếu bạn muốn Icon đổi màu khi được chọn:
            // selectedButton.ForeColor = Color.White; 
        }

        // === Phương thức 2: Reset tất cả các nút ===
        private void ResetAllButtons()
        {
            // Giả định panelMenu tồn tại và chứa các MenuButton
            foreach (Control ctrl in panelMenu.Controls)
            {
                if (ctrl is MenuButton btn)
                {
                    btn.IsSelected = false; // Tắt cờ IsSelected
                }
            }
        }

        // === Phương thức 3: Tải Trang (UserControl) vào Panel ===
        private void LoadPage(UserControl pageToLoad)
        {
            panelContent.Controls.Clear();
            pageToLoad.Dock = DockStyle.Fill;
            panelContent.Controls.Add(pageToLoad);
        }

        // === Các phương thức không dùng (Giữ nguyên hoặc xóa) ===
        private void button1_Click(object sender, EventArgs e) { }
        private void btnSetting_Click(object sender, EventArgs e) { }
        private void button1_Click_1(object sender, EventArgs e) { }
        private void btnContracts_Click(object sender, EventArgs e) { }
        private void pnlIndicator_Paint(object sender, PaintEventArgs e) { }
    }
}
