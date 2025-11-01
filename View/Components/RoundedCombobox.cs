using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

// Đảm bảo namespace này khớp với RoundedTextBox của bạn
namespace Environmental_Monitoring.View.Components
{
    [DefaultEvent("SelectedIndexChanged")]
    public partial class RoundedComboBox : UserControl
    {
        // === THAY ĐỔI 1: THÊM LABEL ĐỂ VẼ MŨI TÊN ===
        private System.Windows.Forms.ComboBox cbb;
        private Label arrowLabel; // Label này sẽ vẽ mũi tên và che nút mặc định

        // Biến trạng thái
        private bool isHovered = false;
        private bool isFocused = false;

        // --- SAO CHÉP TỪ ROUNDEDTEXTBOX: Thuộc tính Giao diện ---
        private int _borderRadius = 15;
        private int _borderThickness = 2;
        private Color _normalBorderColor = Color.Gray;
        private Color _hoverBorderColor = Color.DodgerBlue;
        private Color _focusBorderColor = Color.HotPink;

        [Category("Custom Appearance")]
        [Description("Độ cong của 4 góc.")]
        public int BorderRadius
        {
            get { return _borderRadius; }
            set
            {
                _borderRadius = value < 0 ? 0 : value;
                UpdatePadding(); // Cập nhật padding cho ComboBox con
                Invalidate();
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
                UpdatePadding(); // Cập nhật padding cho ComboBox con
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

        // --- THUỘC TÍNH RIÊNG CỦA COMBOBOX ---

        [Category("Custom Data")]
        [Description("Nguồn dữ liệu cho ComboBox.")]
        public object DataSource
        {
            get { return cbb.DataSource; }
            set { cbb.DataSource = value; }
        }

        [Category("Custom Data")]
        [Description("Thuộc tính để hiển thị trong ComboBox.")]
        public string DisplayMember
        {
            get { return cbb.DisplayMember; }
            set { cbb.DisplayMember = value; }
        }

        [Category("Custom Data")]
        [Description("Thuộc tính để làm giá trị cho ComboBox.")]
        public string ValueMember
        {
            get { return cbb.ValueMember; }
            set { cbb.ValueMember = value; }
        }

        [Category("Custom Data")]
        [Description("Giá trị được chọn, dựa trên ValueMember.")]
        public object SelectedValue
        {
            get { return cbb.SelectedValue; }
            set { cbb.SelectedValue = value; }
        }

        [Category("Custom Data")]
        [Description("Chỉ mục (index) của mục được chọn.")]
        public int SelectedIndex
        {
            get { return cbb.SelectedIndex; }
            set { cbb.SelectedIndex = value; }
        }

        [Category("Custom Data")]
        [Description("Đối tượng được chọn.")]
        public object SelectedItem
        {
            get { return cbb.SelectedItem; }
            set { cbb.SelectedItem = value; }
        }

        [Category("Custom Appearance")]
        [Description("Kiểu hiển thị của ComboBox.")]
        public ComboBoxStyle DropDownStyle
        {
            get { return cbb.DropDownStyle; }
            set { cbb.DropDownStyle = value; }
        }

        [Category("Custom Behavior")]
        public override string Text
        {
            get { return cbb.Text; }
            set { cbb.Text = value; }
        }

        // --- SỰ KIỆN RIÊNG CỦA COMBOBOX ---

        [Category("Custom Action")]
        public event EventHandler SelectedIndexChanged
        {
            add { cbb.SelectedIndexChanged += value; }
            remove { cbb.SelectedIndexChanged -= value; }
        }

        // --- Constructor ---
        public RoundedComboBox()
        {
            InitializeComponent();

            // Khởi tạo ComboBox con
            cbb = new System.Windows.Forms.ComboBox();
            cbb.FlatStyle = FlatStyle.Flat; // Quan trọng: bỏ viền mặc định

            cbb.Dock = DockStyle.Fill;      // Lấp đầy khu vực Padding
            cbb.Font = this.Font;
            cbb.BackColor = this.BackColor;
            cbb.ForeColor = this.ForeColor;
            cbb.DropDownStyle = ComboBoxStyle.DropDownList; // Mặc định là không cho phép gõ

            // === THAY ĐỔI 2: XÓA BỎ DÒNG GÂY LỖI 'APPEARANCE' ===
            // (Không cần thêm gì, chỉ cần đảm bảo dòng 'cbb.Appearance' không tồn tại)

            this.Controls.Add(cbb);

            // === THAY ĐỔI 3: KHỞI TẠO VÀ THÊM LABEL MŨI TÊN ===
            arrowLabel = new Label();
            arrowLabel.Width = 25; // Chiều rộng cố định cho vùng mũi tên
            arrowLabel.Dock = DockStyle.Right; // Dock vào bên phải
            arrowLabel.BackColor = this.BackColor; // Cùng màu nền để che nút
            arrowLabel.Cursor = Cursors.Hand; // Biểu tượng bàn tay
            arrowLabel.Paint += ArrowLabel_Paint; // Gắn sự kiện vẽ
            arrowLabel.Click += (s, ev) => cbb.Focus(); // Click vào label = focus cbb

            this.Controls.Add(arrowLabel);
            arrowLabel.BringToFront(); // Đặt label NẰM TRÊN combobox


            // --- SAO CHÉP TỪ ROUNDEDTEXTBOX: Cài đặt Style & Events ---
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;

            UpdatePadding();

            // Gắn sự kiện của UserControl
            this.MouseEnter += OnControlMouseEnter;
            this.MouseLeave += OnControlMouseLeave;
            this.Click += OnControlClick;

            // Gắn sự kiện của ComboBox con
            cbb.MouseEnter += OnControlMouseEnter;
            cbb.MouseLeave += OnControlMouseLeave;
            cbb.Enter += OnControlEnter;
            cbb.Leave += OnControlLeave;

            // === THAY ĐỔI 4: XỬ LÝ FOCUS KHI MỞ/ĐÓNG DROPDOWN ===
            cbb.DropDown += OnControlEnter; // Coi việc mở dropdown là "focus"
            cbb.DropDownClosed += (s, ev) => OnControlLeave(s, ev); // Coi việc đóng dropdown là "leave"
        }

        // --- SAO CHÉP TỪ ROUNDEDTEXTBOX: Xử lý Sự kiện ---

        private void OnControlMouseEnter(object sender, EventArgs e)
        {
            isHovered = true;
            Invalidate();
        }

        private void OnControlMouseLeave(object sender, EventArgs e)
        {
            isHovered = false;
            Invalidate();
        }

        private void OnControlEnter(object sender, EventArgs e)
        {
            isFocused = true;
            Invalidate();
        }

        private void OnControlLeave(object sender, EventArgs e)
        {
            // === THAY ĐỔI 5: GIỮ FOCUS KHI DROPDOWN MỞ ===
            // Chỉ tắt focus nếu dropdown đang đóng
            if (cbb == null || !cbb.DroppedDown)
            {
                isFocused = false;
                Invalidate();
            }
        }

        private void OnControlClick(object sender, EventArgs e)
        {
            // Click vào UserControl cũng là focus vào ComboBox
            cbb.Focus();
        }

        // --- SAO CHÉP TỪ ROUNDEDTEXTBOX: Ghi đè sự kiện thay đổi thuộc tính ---

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            if (cbb != null)
            {
                cbb.BackColor = this.BackColor;
            }
            // === THAY ĐỔI 6: CẬP NHẬT MÀU NỀN CHO LABEL MŨI TÊN ===
            if (arrowLabel != null)
            {
                arrowLabel.BackColor = this.BackColor;
            }
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            if (cbb != null)
            {
                cbb.ForeColor = this.ForeColor;
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (cbb != null)
            {
                cbb.Font = this.Font;
            }
        }

        // --- SAO CHÉP TỪ ROUNDEDTEXTBOX: Logic Vẽ (OnPaint) ---

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color borderColor;
            // === THAY ĐỔI 7: HIỂN THỊ VIỀN FOCUS KHI DROPDOWN MỞ ===
            if (isFocused || (cbb != null && cbb.DroppedDown))
                borderColor = _focusBorderColor;
            else if (isHovered)
                borderColor = _hoverBorderColor;
            else
                borderColor = _normalBorderColor;

            using (Pen borderPen = new Pen(borderColor, _borderThickness))
            using (SolidBrush backgroundBrush = new SolidBrush(this.BackColor)) // Dùng BackColor của UserControl
            {
                RectangleF rect = new RectangleF(
                    _borderThickness / 2.0f,
                    _borderThickness / 2.0f,
                    Width - _borderThickness - 1,
                    Height - _borderThickness - 1
                );

                GraphicsPath path = GetRoundedRect(rect, _borderRadius);

                // Xóa nền của UserControl (để làm trong suốt)
                g.Clear(this.Parent.BackColor);

                // Vẽ nền bo góc
                g.FillPath(backgroundBrush, path);

                // Vẽ viền bo góc
                g.DrawPath(borderPen, path);
            }

            // === THAY ĐỔI 8: XÓA BỎ CODE VẼ MŨI TÊN TRONG HÀM NÀY ===
            // (Logic vẽ mũi tên đã được chuyển vào sự kiện Paint của arrowLabel)
        }

        // --- SAO CHÉP TỪ ROUNDEDTEXTBOX: Logic Padding ---

        private void UpdatePadding()
        {
            // Logic padding này được thiết kế để phần text bên trong
            // không bị đè lên góc bo tròn, rất quan trọng.
            int padding = (int)Math.Ceiling((double)_borderRadius / 2) + _borderThickness;

            // Điều chỉnh padding cho ComboBox
            // Chiều dọc có thể cần ít hơn một chút
            int verticalPadding = (this.Height - cbb.ItemHeight) / 2;
            verticalPadding = Math.Max(verticalPadding, _borderThickness + 2); // Đảm bảo không quá nhỏ

            // === THAY ĐỔI 9: BỎ PADDING PHẢI CHO CBB ===
            // (Vì arrowLabel đã chiếm không gian đó)
            Padding = new Padding(padding, verticalPadding, padding, verticalPadding);
        }

        // --- SAO CHÉP TỪ ROUNDEDTEXTBOX: Hàm trợ giúp GetRoundedRect ---

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

            path.AddArc(arc, 180, 90);

            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        // === THAY ĐỔI 10: THÊM HÀM VẼ CHO LABEL MŨI TÊN ===
        private void ArrowLabel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 1. Xác định màu mũi tên (dùng ForeColor)
            using (SolidBrush arrowBrush = new SolidBrush(this.ForeColor))
            {
                // 2. Định nghĩa kích thước mũi tên
                int arrowSize = 6;

                // 3. Tính toán tọa độ tâm mũi tên (căn giữa trong Label)
                PointF center = new PointF(
                    (arrowLabel.Width - arrowSize) / 2f,
                    (arrowLabel.Height - (arrowSize / 2f)) / 2
                );

                // 4. Vẽ tam giác (mũi tên)
                PointF[] arrowPoints = new PointF[]
                {
                    new PointF(center.X, center.Y),
                    new PointF(center.X + arrowSize, center.Y),
                    new PointF(center.X + (arrowSize / 2f), center.Y + arrowSize - 2) // -2 để nó nhọn hơn
                };

                g.FillPolygon(arrowBrush, arrowPoints);
            }
        }


        // Hàm này bắt buộc phải có để Designer hoạt động
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RoundedComboBox
            // 
            this.Name = "RoundedComboBox";
            this.Size = new System.Drawing.Size(150, 30);
            this.ResumeLayout(false);
        }
    }
}