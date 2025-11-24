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
// BẮT BUỘC CÓ THƯ VIỆN NÀY
using System.Speech.Recognition;

namespace Environmental_Monitoring.View
{
    internal partial class Contract : UserControl
    {
        #region Fields and Properties

        private Color tabDefaultColor = Color.Transparent;
        private Components.RoundedButton currentActiveTab;

        private ResourceManager rm;
        private CultureInfo culture;

        // --- BIẾN NHẬN DIỆN GIỌNG NÓI ---
        private SpeechRecognitionEngine recognitionEngine;
        private bool isListening = false;

        // Nút Micro đè lên thanh tìm kiếm
        private Button btnMicOverlay;

        #endregion

        #region Initialization

        public Contract()
        {
            InitializeComponent();
            InitializeLocalization();

            // 1. Khởi tạo bộ nhận diện giọng nói
            InitializeVoiceRecognition();

            // 2. Thiết lập nút Micro nằm TRÊN thanh tìm kiếm
            SetupSearchMicrophone();

            this.btnManager.Click += new System.EventHandler(this.btnManager_Click);

            this.Load += new System.EventHandler(this.Contract_Load);
            this.VisibleChanged += new System.EventHandler(this.Contract_VisibleChanged);

            // Cập nhật vị trí nút Mic khi thay đổi kích thước
            this.Resize += (s, e) => AdjustMicPosition();
        }

        private void Contract_Load(object sender, EventArgs e)
        {
            UpdateUIText();
            AdjustMicPosition(); // Đảm bảo vị trí đúng khi load xong
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

        // --- TẠO NÚT MICRO ĐÈ LÊN TEXTBOX ---
        private void SetupSearchMicrophone()
        {
            btnMicOverlay = new Button();

            // --- Tìm đến đoạn gán ảnh trong hàm SetupSearchMicrophone ---

            try
            {
                // BƯỚC 1: Gán ảnh vào BackgroundImage thay vì Image
                btnMicOverlay.BackgroundImage = Properties.Resources.Micro;

                // BƯỚC 2: Chọn chế độ hiển thị
                // ImageLayout.Zoom: Co giãn ảnh cho vừa khung nhưng vẫn GIỮ TỶ LỆ (ảnh không bị méo)
                // ImageLayout.Stretch: Kéo dãn ảnh để LẤP ĐẦY toàn bộ nút (ảnh có thể bị méo) -> Bạn chọn 1 trong 2 nhé
                btnMicOverlay.BackgroundImageLayout = ImageLayout.Zoom;

                // Đảm bảo xóa thuộc tính Image và Text cũ để không bị đè
                btnMicOverlay.Image = null;
                btnMicOverlay.Text = "";
            }
            catch
            {
                // Fallback nếu không tìm thấy ảnh
                btnMicOverlay.Text = "🎤";
                btnMicOverlay.Font = new Font("Segoe UI", 12f, FontStyle.Regular);
                btnMicOverlay.ForeColor = Color.Gray;
            }

            btnMicOverlay.FlatStyle = FlatStyle.Flat;
            btnMicOverlay.FlatAppearance.BorderSize = 0;
            btnMicOverlay.BackColor = ThemeManager.PanelColor;
            btnMicOverlay.FlatAppearance.MouseDownBackColor = ThemeManager.PanelColor;
            btnMicOverlay.FlatAppearance.MouseOverBackColor = ThemeManager.PanelColor;

            btnMicOverlay.Cursor = Cursors.Hand;
            btnMicOverlay.Size = new Size(30, 25);

            // Gán sự kiện Click
            btnMicOverlay.Click += new EventHandler(this.btnVoiceSearch_Click);

            this.Controls.Add(btnMicOverlay);
            btnMicOverlay.BringToFront();

            AdjustMicPosition();
        }

        private void AdjustMicPosition()
        {
            if (roundedTextBox1 != null && btnMicOverlay != null)
            {
                int x = roundedTextBox1.Location.X + roundedTextBox1.Width - btnMicOverlay.Width - 5;
                int y = roundedTextBox1.Location.Y + (roundedTextBox1.Height - btnMicOverlay.Height) / 2;

                btnMicOverlay.Location = new Point(x, y);

                // Đồng bộ màu nền cơ bản (chưa tính trạng thái Recording)
                if (!isListening)
                {
                    btnMicOverlay.BackColor = roundedTextBox1.BackColor;
                    btnMicOverlay.FlatAppearance.MouseOverBackColor = roundedTextBox1.BackColor;
                    btnMicOverlay.FlatAppearance.MouseDownBackColor = roundedTextBox1.BackColor;
                }
            }
        }

        // --- [CẬP NHẬT 1] CẤU HÌNH NHẬN DIỆN GIỌNG NÓI CHÍNH XÁC ---
        private void InitializeVoiceRecognition()
        {
            try
            {
                // Sử dụng Tiếng Anh (Mỹ)
                recognitionEngine = new SpeechRecognitionEngine(new CultureInfo("en-US"));

                // --- TẠO DANH SÁCH TỪ KHÓA ---
                Choices commandList = new Choices();
                commandList.Add(new string[] {
                    "Business", "Plan", "Scene", "Real", "Field", // Các tab
                    "Lab", "Experiment", "Result", "Manager",     // Các tab
                    "Search", "Find", "Clear", "Close",           // Lệnh
                    "Contract", "Home"                            // Khác
                });

                // Xây dựng Grammar
                GrammarBuilder grammarBuilder = new GrammarBuilder();
                grammarBuilder.Append(commandList);
                Grammar customGrammar = new Grammar(grammarBuilder);

                // Load Grammar thay vì DictationGrammar
                recognitionEngine.LoadGrammar(customGrammar);

                recognitionEngine.SpeechRecognized += RecognitionEngine_SpeechRecognized;
                recognitionEngine.SetInputToDefaultAudioDevice();
            }
            catch (Exception ex)
            {
                // Nếu máy không có mic hoặc lỗi driver
                Console.WriteLine("Lỗi khởi tạo giọng nói: " + ex.Message);
            }
        }

        // --- [CẬP NHẬT 2] XỬ LÝ KẾT QUẢ THÔNG MINH ---
        private void RecognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // Tăng độ tin cậy lên 0.6 vì đã dùng Grammar List
            if (e.Result.Confidence < 0.6) return;

            string spokenText = e.Result.Text;

            this.Invoke(new Action(() =>
            {
                // Logic thông minh: Điều hướng hoặc Tìm kiếm
                switch (spokenText)
                {
                    case "Business":
                        if (btnBusiness.Enabled) btnBusiness.PerformClick();
                        break;
                    case "Plan":
                        if (btnPlan.Enabled) btnPlan.PerformClick();
                        break;
                    case "Scene":
                    case "Real":
                    case "Field":
                        if (btnReal.Enabled) btnReal.PerformClick();
                        break;
                    case "Lab":
                    case "Experiment":
                        if (roundedButton1.Enabled) roundedButton1.PerformClick();
                        break;
                    case "Result":
                        if (btnResult.Enabled) btnResult.PerformClick();
                        break;
                    case "Manager":
                        if (btnManager.Enabled) btnManager.PerformClick();
                        break;
                    case "Clear":
                        roundedTextBox1.Text = "";
                        break;
                    default:
                        // Nếu là lệnh tìm kiếm bình thường
                        roundedTextBox1.Text = spokenText;
                        SendKeys.Send("{ENTER}"); // Tự động Enter
                        break;
                }
            }));
        }

        // --- [CẬP NHẬT 3] QUẢN LÝ GIAO DIỆN MIC (ĐỔI MÀU) ---
        private void UpdateMicUI(bool recording)
        {
            if (btnMicOverlay == null) return;

            if (recording)
            {
                // ĐANG NGHE: Màu đỏ, Placeholder hướng dẫn
                btnMicOverlay.BackColor = Color.FromArgb(255, 192, 192); // Màu đỏ nhạt
                btnMicOverlay.FlatAppearance.MouseOverBackColor = Color.Red;
                roundedTextBox1.PlaceholderText = "Listening... (Say: Business, Plan...)";
                roundedTextBox1.Focus();
            }
            else
            {
                // ĐÃ TẮT: Trả về màu giao diện gốc
                btnMicOverlay.BackColor = ThemeManager.PanelColor;
                btnMicOverlay.FlatAppearance.MouseOverBackColor = ThemeManager.PanelColor;
                roundedTextBox1.PlaceholderText = rm.GetString("Search_Placeholder", culture);
            }
        }

        private void StopListening()
        {
            if (isListening && recognitionEngine != null)
            {
                recognitionEngine.RecognizeAsyncStop();
                isListening = false;

                this.Invoke(new Action(() => {
                    UpdateMicUI(false);
                }));
            }
        }

        #endregion

        #region Core Logic (Permissions, UI Updates, Page Loading)

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && this.ActiveControl == roundedTextBox1)
            {
                string searchText = roundedTextBox1.Text.Trim();
                if (pnContent.Controls.Count > 0)
                {
                    Control currentChildPage = pnContent.Controls[0];
                    try
                    {
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
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // --- [CẬP NHẬT 4] SỰ KIỆN CLICK MIC ---
        private void btnVoiceSearch_Click(object sender, EventArgs e)
        {
            if (recognitionEngine == null)
            {
                MessageBox.Show("Chức năng giọng nói chưa sẵn sàng (Kiểm tra Micro).", "Thông báo");
                return;
            }

            if (!isListening)
            {
                try
                {
                    // Bắt đầu nghe liên tục
                    recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
                    isListening = true;
                    UpdateMicUI(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể kích hoạt Mic: " + ex.Message);
                }
            }
            else
            {
                // Dừng nghe chủ động
                StopListening();
            }
        }

        private void ResetTabsForEmployee()
        {
            btnBusiness.Enabled = false;
            btnPlan.Enabled = false;
            btnReal.Enabled = false;
            roundedButton1.Enabled = false;
            btnResult.Enabled = false;
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

            // Chỉ reset placeholder nếu KHÔNG đang nghe
            if (!isListening)
            {
                roundedTextBox1.PlaceholderText = rm.GetString("Search_Placeholder", culture);
            }

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

                // Cập nhật màu nút Mic (nếu đang KHÔNG nghe thì đồng bộ màu)
                if (btnMicOverlay != null && !isListening)
                {
                    btnMicOverlay.BackColor = ThemeManager.PanelColor;
                    btnMicOverlay.FlatAppearance.MouseDownBackColor = ThemeManager.PanelColor;
                    btnMicOverlay.FlatAppearance.MouseOverBackColor = ThemeManager.PanelColor;
                }

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