using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.Components
{
    [DefaultEvent("SelectedIndexChanged")]
    public partial class RoundedComboBox : UserControl
    {
        private System.Windows.Forms.ComboBox cbb;
        // Xóa bỏ 'arrowLabel' và các code 'OwnerDraw'

        private bool isHovered = false;
        private bool isFocused = false;

        private int _borderRadius = 12;
        private int _borderThickness = 2;
        private Color _normalBorderColor = Color.LightGray;
        private Color _hoverBorderColor = Color.FromArgb(52, 168, 83);
        private Color _focusBorderColor = Color.FromArgb(52, 168, 83);

        #region Properties
        // ... (Tất cả các thuộc tính [Category("...")] của bạn giữ nguyên) ...
        [Category("Custom Appearance")]
        [Description("Độ cong của 4 góc.")]
        public int BorderRadius
        {
            get { return _borderRadius; }
            set
            {
                _borderRadius = value < 0 ? 0 : value;
                UpdateControlLocations();
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
                UpdateControlLocations();
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

        // (Xóa thuộc tính SelectionColor, vì chúng ta dùng màu mặc định)

        [Category("Custom Data")]
        public object DataSource
        {
            get { return cbb.DataSource; }
            set { cbb.DataSource = value; }
        }

        [Category("Custom Data")]
        public string DisplayMember
        {
            get { return cbb.DisplayMember; }
            set { cbb.DisplayMember = value; }
        }

        [Category("Custom Data")]
        public string ValueMember
        {
            get { return cbb.ValueMember; }
            set { cbb.ValueMember = value; }
        }

        [Category("Custom Data")]
        public object SelectedValue
        {
            get { return cbb.SelectedValue; }
            set { cbb.SelectedValue = value; }
        }

        [Category("Custom Data")]
        public int SelectedIndex
        {
            get { return cbb.SelectedIndex; }
            set { cbb.SelectedIndex = value; }
        }

        [Category("Custom Data")]
        public object SelectedItem
        {
            get { return cbb.SelectedItem; }
            set { cbb.SelectedItem = value; }
        }

        [Category("Custom Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        public ComboBox.ObjectCollection Items
        {
            get { return cbb.Items; }
        }

        [Category("Custom Appearance")]
        public ComboBoxStyle DropDownStyle
        {
            get { return cbb.DropDownStyle; }
            set { cbb.DropDownStyle = value; } // Cho phép người dùng set
        }

        [Category("Custom Behavior")]
        public override string Text
        {
            get { return cbb.Text; }
            set { cbb.Text = value; }
        }

        [Category("Custom Action")]
        public event EventHandler SelectedIndexChanged
        {
            add { cbb.SelectedIndexChanged += value; }
            remove { cbb.SelectedIndexChanged -= value; }
        }
        #endregion

        // --- Constructor ---
        public RoundedComboBox()
        {
            InitializeComponent();

            cbb = new System.Windows.Forms.ComboBox();
            // === THAY ĐỔI 1: Trả về FlatStyle.Flat ===
            // Điều này sẽ hiển thị mũi tên mặc định nhưng xóa viền của ComboBox
            cbb.FlatStyle = FlatStyle.Flat;
            cbb.Font = this.Font;
            cbb.BackColor = this.BackColor;
            cbb.ForeColor = this.ForeColor;

            // === THAY ĐỔI 2: XÓA BỎ OwnerDraw ===
            // (Xóa các dòng cbb.DropDownStyle, cbb.DrawMode, cbb.ItemHeight)
            cbb.DropDownStyle = ComboBoxStyle.DropDownList; // Mặc định là DropDownList

            this.Controls.Add(cbb);

            // (Xóa toàn bộ code khởi tạo 'arrowLabel')

            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;

            UpdateControlLocations();

            // Gắn sự kiện
            this.MouseEnter += OnControlMouseEnter;
            this.MouseLeave += OnControlMouseLeave;
            this.Click += (s, e) => cbb.Focus(); // Click control = focus CBB
            this.Resize += new EventHandler(RoundedComboBox_Resize);
            cbb.MouseEnter += OnControlMouseEnter;
            cbb.MouseLeave += OnControlMouseLeave;
            cbb.Enter += OnControlEnter;
            cbb.Leave += OnControlLeave;
            cbb.DropDown += OnControlEnter;
            cbb.DropDownClosed += OnControlLeave;

            // (Xóa sự kiện cbb.DrawItem)
        }

        // (Xóa hàm Cbb_DrawItem)

        // === THAY ĐỔI 3: Cập nhật hàm căn chỉnh vị trí ===
        private void UpdateControlLocations()
        {
            if (cbb == null) return;

            // 1. Padding trái (dựa trên độ cong)
            int horizontalPadding = (int)Math.Ceiling((double)_borderRadius / 2) + _borderThickness;
            if (horizontalPadding < 8) horizontalPadding = 8;

            // 2. Căn giữa ComboBox theo chiều dọc
            int cbbHeight = cbb.GetItemHeight(0) + 6;
            cbb.Height = cbbHeight;
            cbb.Location = new Point(horizontalPadding, (this.Height - cbb.Height) / 2);

            // 3. Set chiều rộng cho ComboBox (trừ lề 2 bên)
            // Lề phải cũng bằng lề trái để cho cân đối
            cbb.Width = this.Width - (horizontalPadding * 2);
        }

        private void RoundedComboBox_Resize(object sender, EventArgs e)
        {
            UpdateControlLocations();
            this.Invalidate();
        }

        // --- Xử lý Sự kiện (Không đổi) ---
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
            if (cbb == null || !cbb.DroppedDown)
            {
                isFocused = false;
                Invalidate();
            }
        }

        // --- Ghi đè sự kiện thay đổi thuộc tính ---

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            if (cbb != null)
            {
                cbb.BackColor = this.BackColor;
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
            UpdateControlLocations();
        }

        // --- Logic Vẽ (OnPaint) ---
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color borderColor;
            if (isFocused || (cbb != null && cbb.DroppedDown))
                borderColor = _focusBorderColor;
            else if (isHovered)
                borderColor = _hoverBorderColor;
            else
                borderColor = _normalBorderColor;

            // 1. Vẽ Viền và Nền
            using (Pen borderPen = new Pen(borderColor, _borderThickness))
            using (SolidBrush backgroundBrush = new SolidBrush(this.BackColor))
            {
                RectangleF rect = new RectangleF(
                    _borderThickness / 2.0f,
                    _borderThickness / 2.0f,
                    Width - _borderThickness - 1,
                    Height - _borderThickness - 1
                );

                GraphicsPath path = GetRoundedRect(rect, _borderRadius);
                g.Clear(this.Parent.BackColor);
                g.FillPath(backgroundBrush, path);
                g.DrawPath(borderPen, path);
            }

            // === THAY ĐỔI 4: XÓA BỎ CODE VẼ MŨI TÊN ===
            // (Toàn bộ code "Color arrowColor..." đã bị xóa)
        }

        // --- Hàm trợ giúp GetRoundedRect (Không đổi) ---
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

        // Hàm InitializeComponent (Không đổi)
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