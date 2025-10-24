using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Environmental_Monitoring.View.Components
    // <-- Thay bằng namespace dự án của bạn
{
    // Đặt sự kiện mặc định là TextChanged, giống như TextBox thông thường
    [DefaultEvent("TextChanged")]
    public partial class RoundedTextBox : UserControl
    {
        // === Các biến nội bộ ===
        private TextBox textBox; // TextBox thực sự bên trong
        private bool isHovered = false;
        private bool isFocused = false;

        // === P/Invoke để tạo Placeholder (Cue Banner) ===

        // Hằng số message của Windows
        private const int EM_SETCUEBANNER = 0x1501;

        // Khai báo hàm SendMessage từ user32.dll
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern nint SendMessage(nint hWnd, int Msg, nint wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        // === Các thuộc tính tùy chỉnh (Properties) ===
        private int _borderRadius = 15;
        private int _borderThickness = 2;
        private Color _normalBorderColor = Color.Gray;
        private Color _hoverBorderColor = Color.DodgerBlue;
        private Color _focusBorderColor = Color.HotPink;
        private string _placeholderText = "";

        [Category("Custom Appearance")]
        [Description("Độ cong của 4 góc.")]
        public int BorderRadius
        {
            get { return _borderRadius; }
            set
            {
                _borderRadius = value < 0 ? 0 : value;
                UpdatePadding(); // Cập nhật padding khi bo góc thay đổi
                Invalidate(); // Vẽ lại control
            }
        }

        [Category("Custom Appearance")]
        [Description("Độ dày của viền.")]
        public int BorderThickness
        {
            get { return _borderThickness; }
            set
            {
                _borderThickness = value < 1 ? 1 : value;
                UpdatePadding(); // Cập nhật padding khi độ dày thay đổi
                Invalidate();
            }
        }

        [Category("Custom Appearance")]
        [Description("Màu viền ở trạng thái bình thường.")]
        public Color NormalBorderColor
        {
            get { return _normalBorderColor; }
            set { _normalBorderColor = value; Invalidate(); }
        }

        [Category("Custom Appearance")]
        [Description("Màu viền khi di chuột vào (hover).")]
        public Color HoverBorderColor
        {
            get { return _hoverBorderColor; }
            set { _hoverBorderColor = value; Invalidate(); }
        }

        [Category("Custom Appearance")]
        [Description("Màu viền khi được focus (click vào).")]
        public Color FocusBorderColor
        {
            get { return _focusBorderColor; }
            set { _focusBorderColor = value; Invalidate(); }
        }

        // === Các thuộc tính của TextBox gốc cần "show" ra ngoài ===

        [Category("Custom Behavior")]
        public override string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        [Category("Custom Behavior")]
        [Description("Văn bản mờ (placeholder) hiển thị khi TextBox rỗng.")]
        public string PlaceholderText
        {
            get { return _placeholderText; }
            set
            {
                _placeholderText = value;
                // Gọi hàm để cập nhật placeholder cho TextBox bên trong
                SetCueBanner(_placeholderText);
            }
        }

        [Category("Custom Behavior")]
        public char PasswordChar
        {
            get { return textBox.PasswordChar; }
            set { textBox.PasswordChar = value; }
        }

        [Category("Custom Behavior")]
        public bool Multiline
        {
            get { return textBox.Multiline; }
            set { textBox.Multiline = value; }
        }

        [Category("Custom Behavior")]
        public bool ReadOnly
        {
            get { return textBox.ReadOnly; }
            set { textBox.ReadOnly = value; }
        }

        // === Sự kiện (Event) ===
        // Chuyển tiếp (forward) sự kiện TextChanged từ TextBox bên trong
        public new event EventHandler TextChanged
        {
            add { textBox.TextChanged += value; }
            remove { textBox.TextChanged -= value; }
        }

        // === Hàm dựng (Constructor) ===
        public RoundedTextBox()
        {
            InitializeComponent(); // Hàm này được tạo tự động nếu bạn dùng Designer

            // Khởi tạo TextBox nội bộ
            textBox = new TextBox();
            textBox.BorderStyle = BorderStyle.None;
            textBox.Dock = DockStyle.Fill;
            textBox.Font = Font; // Lấy Font từ UserControl
            textBox.BackColor = BackColor; // Lấy BackColor từ UserControl
            textBox.ForeColor = ForeColor; // Lấy ForeColor từ UserControl

            // Thêm TextBox vào UserControl
            Controls.Add(textBox);

            // Cài đặt Double Buffering để vẽ mượt hơn
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;

            // Cập nhật Padding ban đầu
            UpdatePadding();

            // === Gán các sự kiện để xử lý hover, focus ===

            // Sự kiện của UserControl (vùng viền)
            MouseEnter += OnControlMouseEnter;
            MouseLeave += OnControlMouseLeave;
            Click += OnControlClick;

            // Sự kiện của TextBox bên trong
            textBox.MouseEnter += OnControlMouseEnter; // Khi chuột vào TextBox cũng là vào Control
            textBox.MouseLeave += OnControlMouseLeave; // Khi chuột rời TextBox cũng là rời Control
            textBox.Enter += OnTextBoxEnter;       // Khi TextBox được focus
            textBox.Leave += OnTextBoxLeave;       // Khi TextBox mất focus

            textBox.HandleCreated += OnTextBoxHandleCreated;
        }

        // === Các hàm xử lý sự kiện ===

        private void OnTextBoxHandleCreated(object sender, EventArgs e)
        {
            // Chỉ gọi SetCueBanner khi chúng ta BIẾT CHẮC handle của textBox đã tồn tại
            SetCueBanner(_placeholderText);
        }

        private void OnControlMouseEnter(object sender, EventArgs e)
        {
            isHovered = true;
            Invalidate(); // Yêu cầu vẽ lại
        }

        private void OnControlMouseLeave(object sender, EventArgs e)
        {
            isHovered = false;
            Invalidate(); // Yêu cầu vẽ lại
        }

        private void OnTextBoxEnter(object sender, EventArgs e)
        {
            isFocused = true;
            Invalidate(); // Yêu cầu vẽ lại
        }

        private void OnTextBoxLeave(object sender, EventArgs e)
        {
            isFocused = false;
            Invalidate(); // Yêu cầu vẽ lại
        }

        private void OnControlClick(object sender, EventArgs e)
        {
            // Khi click vào vùng viền, focus vào TextBox bên trong
            textBox.Focus();
        }

        // === Đồng bộ hóa thuộc tính màu sắc, font chữ ===

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            if (textBox != null)
            {
                textBox.BackColor = BackColor; // Nền của TextBox = nền của UserControl
            }
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            if (textBox != null)
            {
                textBox.ForeColor = ForeColor;
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (textBox != null)
            {
                textBox.Font = Font;
            }
        }

        // === Hàm quan trọng: Tự vẽ Control (OnPaint) ===

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias; // Bật khử răng cưa

            // Chọn màu viền dựa trên trạng thái
            Color borderColor;
            if (isFocused)
                borderColor = _focusBorderColor;
            else if (isHovered)
                borderColor = _hoverBorderColor;
            else
                borderColor = _normalBorderColor;

            using (Pen borderPen = new Pen(borderColor, _borderThickness))
            using (SolidBrush backgroundBrush = new SolidBrush(BackColor))
            {
                // Tính toán hình chữ nhật để vẽ (co vào 1 chút để Pen nằm gọn bên trong)
                RectangleF rect = new RectangleF(
                    _borderThickness / 2.0f,
                    _borderThickness / 2.0f,
                    Width - _borderThickness - 1,
                    Height - _borderThickness - 1
                );

                // Tạo đường dẫn (path) bo tròn
                GraphicsPath path = GetRoundedRect(rect, _borderRadius);

                // 1. Xóa nền của UserControl (để làm "trong suốt" với control cha)
                g.Clear(Parent.BackColor);

                // 2. Vẽ nền bo tròn (chính là nền của TextBox)
                g.FillPath(backgroundBrush, path);

                // 3. Vẽ viền bo tròn
                g.DrawPath(borderPen, path);
            }
        }

        // === Hàm tiện ích ===

        // Cập nhật Padding để TextBox không đè lên viền
        private void UpdatePadding()
        {
            // Padding đơn giản: lấy bán kính + độ dày viền
            int padding = (int)Math.Ceiling((double)_borderRadius / 2) + _borderThickness;
            Padding = new Padding(padding, padding, padding, padding);
        }

        // Hàm helper để tạo GraphicsPath bo tròn
        private GraphicsPath GetRoundedRect(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            float diameter = radius * 2;
            RectangleF arc = new RectangleF(rect.Location, new SizeF(diameter, diameter));

            // Góc trên bên trái
            path.AddArc(arc, 180, 90);

            // Góc trên bên phải
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Góc dưới bên phải
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Góc dưới bên trái
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure(); // Đóng đường path
            return path;
        }


        // Hàm helper để gửi message EM_SETCUEBANNER
        private void SetCueBanner(string text)
        {
            // Kiểm tra xem handle của TextBox đã được tạo chưa
            if (textBox != null && textBox.IsHandleCreated)
            {
                // Gửi message cho TextBox nội bộ
                // wParam = 0 (false) nghĩa là placeholder sẽ BIẾN MẤT khi focus
                SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, text);
            }
        }

        // Hàm này được tạo tự động nếu bạn dùng Designer
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // RoundTextBox
            // 
            Name = "RoundTextBox";
            Size = new Size(150, 30);
            ResumeLayout(false);
        }
    }
}