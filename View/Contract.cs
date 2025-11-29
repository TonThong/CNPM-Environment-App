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
using Vosk;
using NAudio.Wave;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Environmental_Monitoring.View
{
    internal partial class Contract : UserControl
    {
        #region Fields and Properties

        private Color tabDefaultColor = Color.Transparent;
        private Components.RoundedButton currentActiveTab;

        private ResourceManager rm;
        private CultureInfo culture;

        // --- CÁC BIẾN CHO VOSK (NHẬN DIỆN GIỌNG NÓI) ---
        private Vosk.Model voskModel;
        private VoskRecognizer voskRecognizer;
        private WaveInEvent waveIn;
        private bool isModelLoaded = false;
        // ------------------------------------------------

        private bool isListening = false;
        private Button btnMicOverlay;

        #endregion

        #region Initialization

        public Contract()
        {
            InitializeComponent();
            InitializeLocalization();

            // Khởi tạo Vosk (chạy ngầm để không đơ UI lúc mới mở)
            Task.Run(() => InitializeVosk());

            SetupSearchMicrophone();

            this.btnManager.Click += new System.EventHandler(this.btnManager_Click);
            this.Load += new System.EventHandler(this.Contract_Load);
            this.VisibleChanged += new System.EventHandler(this.Contract_VisibleChanged);
            this.Resize += (s, e) => AdjustMicPosition();
        }

        /// <summary>
        /// Sự kiện Load của UserControl: Cập nhật ngôn ngữ và vị trí nút Mic.
        /// </summary>
        private void Contract_Load(object sender, EventArgs e)
        {
            UpdateUIText();
            AdjustMicPosition();
        }

        /// <summary>
        /// Khi UserControl hiển thị lại, kích hoạt kiểm tra quyền để đảm bảo bảo mật.
        /// </summary>
        private void Contract_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && UserSession.CurrentUser != null)
            {
                string roleName = UserSession.CurrentUser.Role?.RoleName ?? "";
                SetTabAccess(roleName);
            }
        }

        /// <summary>
        /// Khởi tạo tài nguyên ngôn ngữ (ResourceManager).
        /// </summary>
        private void InitializeLocalization()
        {
            rm = new ResourceManager("Environmental_Monitoring.Strings", typeof(Contract).Assembly);
            culture = Thread.CurrentThread.CurrentUICulture;
        }

        /// <summary>
        /// Cấu hình và thêm nút Micro đè lên ô tìm kiếm.
        /// </summary>
        private void SetupSearchMicrophone()
        {
            btnMicOverlay = new Button();

            // 1. Load icon Micro từ Resources
            Image originalIcon = null;
            try
            {
                originalIcon = Properties.Resources.Micro;
            }
            catch { }

            // 2. Cấu hình nút để trông "tàng hình" trên thanh tìm kiếm
            btnMicOverlay.BackgroundImage = originalIcon;
            btnMicOverlay.BackgroundImageLayout = ImageLayout.Zoom;
            btnMicOverlay.Text = originalIcon == null ? "🎤" : ""; // Fallback text nếu thiếu ảnh

            btnMicOverlay.FlatStyle = FlatStyle.Flat;
            btnMicOverlay.FlatAppearance.BorderSize = 0;

            // Đặt màu nền trùng với màu textbox để hòa làm một
            btnMicOverlay.BackColor = ThemeManager.PanelColor;
            btnMicOverlay.FlatAppearance.MouseDownBackColor = ThemeManager.PanelColor;
            btnMicOverlay.FlatAppearance.MouseOverBackColor = ThemeManager.PanelColor;

            btnMicOverlay.Cursor = Cursors.Hand;
            btnMicOverlay.Size = new Size(24, 24); // Kích thước nhỏ gọn
            btnMicOverlay.Click += new EventHandler(this.btnVoiceSearch_Click);

            this.Controls.Add(btnMicOverlay);
            btnMicOverlay.BringToFront();

            AdjustMicPosition();
        }

        /// <summary>
        /// Tự động căn chỉnh vị trí nút Mic luôn nằm ở cuối ô tìm kiếm khi thay đổi kích thước.
        /// </summary>
        private void AdjustMicPosition()
        {
            if (roundedTextBox1 != null && btnMicOverlay != null)
            {
                int x = roundedTextBox1.Location.X + roundedTextBox1.Width - btnMicOverlay.Width - 5;
                int y = roundedTextBox1.Location.Y + (roundedTextBox1.Height - btnMicOverlay.Height) / 2;

                btnMicOverlay.Location = new Point(x, y);

                if (!isListening)
                {
                    btnMicOverlay.BackColor = roundedTextBox1.BackColor;
                    btnMicOverlay.FlatAppearance.MouseOverBackColor = roundedTextBox1.BackColor;
                    btnMicOverlay.FlatAppearance.MouseDownBackColor = roundedTextBox1.BackColor;
                }
            }
        }

        #endregion

        #region Vosk Implementation (Free Text Search)

        /// <summary>
        /// Khởi tạo Model Vosk và thiết lập Microphone (Chạy 1 lần duy nhất).
        /// </summary>
        private void InitializeVosk()
        {
            try
            {
                // Lấy đường dẫn đến thư mục chứa model
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string modelPath = Path.Combine(baseDir, "model");

                if (!Directory.Exists(modelPath))
                {
                    // Không tìm thấy model thì thôi, không crash app
                    return;
                }

                Vosk.Vosk.SetLogLevel(-1); // Tắt log rác

                // Khởi tạo Model và Recognizer
                voskModel = new Vosk.Model(modelPath);
                voskRecognizer = new VoskRecognizer(voskModel, 16000.0f); // 16kHz là chuẩn của Vosk

                // Cấu hình NAudio để bắt mic
                waveIn = new WaveInEvent();
                waveIn.WaveFormat = new WaveFormat(16000, 1);
                waveIn.DataAvailable += WaveIn_DataAvailable;

                isModelLoaded = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khởi tạo Vosk: " + ex.Message);
                isModelLoaded = false;
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi Mic thu được dữ liệu âm thanh -> Gửi vào Vosk.
        /// </summary>
        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (voskRecognizer == null) return;

            if (voskRecognizer.AcceptWaveform(e.Buffer, e.BytesRecorded))
            {
                string jsonResult = voskRecognizer.Result();
                ProcessVoskResult(jsonResult);
            }
        }

        /// <summary>
        /// Phân tích kết quả JSON từ Vosk -> Lấy ra văn bản thô.
        /// </summary>
        private void ProcessVoskResult(string json)
        {
            try
            {
                var parsed = JObject.Parse(json);
                string text = parsed["text"].ToString().Trim();

                if (!string.IsNullOrEmpty(text))
                {
                    this.Invoke(new Action(() => ExecuteVoiceSearch(text)));
                }
            }
            catch { }
        }

        /// <summary>
        /// Xử lý văn bản nhận được: Điền vào ô tìm kiếm và tự động nhấn Enter.
        /// </summary>
        private void ExecuteVoiceSearch(string spokenText)
        {
            if (string.IsNullOrWhiteSpace(spokenText)) return;

            // 1. Điền nội dung nghe được vào ô tìm kiếm
            roundedTextBox1.Text = spokenText;

            // 2. Giả lập hành động nhấn phím Enter để kích hoạt sự kiện tìm kiếm
            SendKeys.Send("{ENTER}");
        }

        /// <summary>
        /// Cập nhật giao diện nút Mic (Đỏ khi đang nghe, Mặc định khi dừng).
        /// </summary>
        private void UpdateMicUI(bool recording)
        {
            if (btnMicOverlay == null) return;

            try
            {
                Image originalIcon = Properties.Resources.Micro;

                if (recording)
                {
                    // Đổi icon sang màu đỏ đậm để báo hiệu đang nghe
                    btnMicOverlay.BackgroundImage = RecolorImage(originalIcon, Color.Red);

                    roundedTextBox1.PlaceholderText = "Đang nghe... (Nói tên, mã số...)";
                    roundedTextBox1.Focus();
                }
                else
                {
                    // Trả về icon màu gốc
                    btnMicOverlay.BackgroundImage = originalIcon;

                    roundedTextBox1.PlaceholderText = rm.GetString("Search_Placeholder", culture);
                }
            }
            catch { }
        }

        /// <summary>
        /// Hàm hỗ trợ: Đổi màu của một bức ảnh (dùng để đổi màu icon Mic).
        /// </summary>
        private Image RecolorImage(Image originalImage, Color newColor)
        {
            if (originalImage == null) return null;

            Bitmap bmp = new Bitmap(originalImage.Width, originalImage.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix(new float[][]
                {
                    new float[] {0, 0, 0, 0, 0},
                    new float[] {0, 0, 0, 0, 0},
                    new float[] {0, 0, 0, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {newColor.R / 255.0f, newColor.G / 255.0f, newColor.B / 255.0f, 0, 1}
                });

                System.Drawing.Imaging.ImageAttributes attributes = new System.Drawing.Imaging.ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);

                g.DrawImage(originalImage, new Rectangle(0, 0, bmp.Width, bmp.Height),
                    0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel, attributes);
            }
            return bmp;
        }

        /// <summary>
        /// Dừng thu âm và reset trạng thái UI.
        /// </summary>
        private void StopListening()
        {
            if (isListening && waveIn != null)
            {
                waveIn.StopRecording();
                isListening = false;
                this.Invoke(new Action(() => { UpdateMicUI(false); }));
            }
        }

        #endregion

        #region Core Logic

        /// <summary>
        /// Xử lý phím Enter trong ô tìm kiếm để gọi hàm tìm kiếm của trang con.
        /// </summary>
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

        /// <summary>
        /// Bật/Tắt chế độ tìm kiếm bằng giọng nói khi nhấn nút Mic.
        /// </summary>
        private void btnVoiceSearch_Click(object sender, EventArgs e)
        {
            if (!isModelLoaded)
            {
                MessageBox.Show("Mô hình giọng nói chưa sẵn sàng. Vui lòng kiểm tra thư mục 'model'.", "Lỗi");
                return;
            }

            if (!isListening)
            {
                try
                {
                    voskRecognizer.Reset();
                    waveIn.StartRecording();
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
                StopListening();
            }
        }

        /// <summary>
        /// Phân quyền hiển thị Tab dựa trên Role của người dùng.
        /// </summary>
        public void SetTabAccess(string roleName)
        {
            if (string.IsNullOrEmpty(roleName)) return;

            string cleanRole = roleName.ToLowerInvariant().Trim();
            LockAllTabs(); // Khóa tất cả trước

            // Admin/Manager thì mở hết
            if (cleanRole == "admin" || cleanRole == "manager")
            {
                UnlockAllTabs();
                if (currentActiveTab == null) btnBusiness.PerformClick();
                return;
            }

            // Nhân viên phòng nào mở phòng đó
            switch (cleanRole)
            {
                case "business": UnlockAndOpen(btnBusiness, new BusinessContent()); break;
                case "plan": UnlockAndOpen(btnPlan, new PlanContent()); break;
                case "field":
                case "real":
                case "scene": UnlockAndOpen(btnReal, new RealContent()); break;
                case "lab":
                case "experiment": UnlockAndOpen(roundedButton1, new ExperimentContent()); break;
                case "result": UnlockAndOpen(btnResult, new ResultContent()); break;
                default: break;
            }
        }

        // Helper: Khóa và làm mờ tất cả các Tab
        private void LockAllTabs()
        {
            btnBusiness.Enabled = false;
            btnPlan.Enabled = false;
            btnReal.Enabled = false;
            roundedButton1.Enabled = false;
            btnResult.Enabled = false;
            btnManager.Enabled = false;

            btnBusiness.BackColor = tabDefaultColor;
            btnPlan.BackColor = tabDefaultColor;
            btnReal.BackColor = tabDefaultColor;
            roundedButton1.BackColor = tabDefaultColor;
            btnResult.BackColor = tabDefaultColor;
            btnManager.BackColor = tabDefaultColor;

            Color defaultTxtColor = ThemeManager.TextColor;
            btnBusiness.ForeColor = defaultTxtColor;
            btnPlan.ForeColor = defaultTxtColor;
            btnReal.ForeColor = defaultTxtColor;
            roundedButton1.ForeColor = defaultTxtColor;
            btnResult.ForeColor = defaultTxtColor;
            btnManager.ForeColor = defaultTxtColor;
        }

        // Helper: Mở khóa tất cả các Tab
        private void UnlockAllTabs()
        {
            btnBusiness.Enabled = true;
            btnPlan.Enabled = true;
            btnReal.Enabled = true;
            roundedButton1.Enabled = true;
            btnResult.Enabled = true;
            btnManager.Enabled = true;
        }

        // Helper: Mở khóa và tự động click vào Tab
        private void UnlockAndOpen(Components.RoundedButton btn, UserControl pageContent)
        {
            btn.Enabled = true;
            btn.PerformClick();
        }

        // Helper: Đổi màu Tab được chọn (Xanh đậm #007000)
        private void HighlightTab(Components.RoundedButton selectedButton)
        {
            Color activeGreen = ColorTranslator.FromHtml("#007000");

            // Reset tab cũ
            if (currentActiveTab != null && currentActiveTab != selectedButton)
            {
                currentActiveTab.BackColor = tabDefaultColor;
                currentActiveTab.BaseColor = tabDefaultColor;
                currentActiveTab.ForeColor = ThemeManager.TextColor;
            }

            // Highlight tab mới
            selectedButton.BackColor = activeGreen;
            selectedButton.BaseColor = activeGreen;
            selectedButton.ForeColor = Color.White;
            currentActiveTab = selectedButton;
        }

        /// <summary>
        /// Cập nhật ngôn ngữ và Theme màu sắc cho giao diện.
        /// </summary>
        public void UpdateUIText()
        {
            culture = Thread.CurrentThread.CurrentUICulture;

            lbContract.Text = rm.GetString("Contract_Title", culture);

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
                try { var method = currentChildPage.GetType().GetMethod("UpdateUIText"); if (method != null) method.Invoke(currentChildPage, null); } catch { }
            }

            try
            {
                this.BackColor = ThemeManager.BackgroundColor_tab;
                lbContract.ForeColor = ThemeManager.TextColor;

                Color defaultTxtColor = ThemeManager.TextColor;
                btnBusiness.ForeColor = defaultTxtColor;
                btnPlan.ForeColor = defaultTxtColor;
                btnReal.ForeColor = defaultTxtColor;
                roundedButton1.ForeColor = defaultTxtColor;
                btnResult.ForeColor = defaultTxtColor;
                btnManager.ForeColor = defaultTxtColor;

                roundedTextBox1.BackColor = ThemeManager.PanelColor;
                roundedTextBox1.ForeColor = ThemeManager.TextColor;

                if (btnMicOverlay != null && !isListening)
                {
                    btnMicOverlay.BackColor = ThemeManager.PanelColor;
                    btnMicOverlay.FlatAppearance.MouseDownBackColor = ThemeManager.PanelColor;
                    btnMicOverlay.FlatAppearance.MouseOverBackColor = ThemeManager.PanelColor;
                    btnMicOverlay.BackgroundImage = Properties.Resources.Micro; // Reset icon về mặc định khi đổi theme
                }

                pnContent.BackColor = ThemeManager.PanelColor;

                if (currentActiveTab != null)
                {
                    Color activeGreen = ColorTranslator.FromHtml("#007000");
                    currentActiveTab.BackColor = activeGreen;
                    currentActiveTab.BaseColor = activeGreen;
                    currentActiveTab.ForeColor = Color.White;
                }
            }
            catch (Exception) { }
        }

        // Helper: Load trang con vào Panel chính
        private void LoadPage(UserControl pageToLoad)
        {
            pnContent.Controls.Clear();
            pageToLoad.Dock = DockStyle.Fill;
            pnContent.Controls.Add(pageToLoad);
            try { var method = pageToLoad.GetType().GetMethod("UpdateUIText"); if (method != null) method.Invoke(pageToLoad, null); } catch { }
        }

        #endregion

        #region Tab Click Events

        private void btnBusiness_Click(object sender, EventArgs e) { LoadPage(new BusinessContent()); HighlightTab(btnBusiness); }
        private void btnPlan_Click(object sender, EventArgs e) { LoadPage(new PlanContent()); HighlightTab(btnPlan); }
        private void btnReal_Click(object sender, EventArgs e) { LoadPage(new RealContent()); HighlightTab(btnReal); }
        private void roundedButton1_Click(object sender, EventArgs e) { LoadPage(new ExperimentContent()); HighlightTab(roundedButton1); }
        private void btnResult_Click(object sender, EventArgs e) { LoadPage(new ResultContent()); HighlightTab(btnResult); }
        private void btnManager_Click(object sender, EventArgs e) { if (UserSession.IsAdmin()) { LoadPage(new ManagerContent()); HighlightTab(btnManager); } }

        #endregion

        private void lbContract_Click(object sender, EventArgs e) { }
    }
}