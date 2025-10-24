using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.Components
{
    public class MenuButton : Button
    {
        // === Các thuộc tính tùy chỉnh (Fields) ===
        private bool _isSelected = false;
        private bool isHovering = false;
        private bool isPressed = false; // 🌟 TRẠNG THÁI MỚI: Nhấn chuột

        private int _borderLeftSize = 10;
        private Color _inactiveBackColor = Color.Transparent;
        private Color _activeBackColor = Color.FromArgb(220, 240, 220); // Xanh nhạt
        private Color _activeBorderColor = Color.FromArgb(0, 100, 0); // Xanh đậm
        private Color _hoverBackColor = Color.FromArgb(230, 230, 230); // Màu hover
        private Color _pressedBackColor = Color.FromArgb(200, 200, 200); // 🌟 MÀU MỚI: Khi nhấn

        // Các thuộc tính công khai (Giữ nguyên)
        // ... (InactiveBackColor, ActiveBackColor, ActiveBorderColor, BorderLeftSize, IsSelected, HoverBackColor)

        [Category("Custom Appearance")]
        [Description("Màu nền khi di chuột qua")]
        public Color HoverBackColor
        {
            get { return _hoverBackColor; }
            set { _hoverBackColor = value; Invalidate(); }
        }

        [Category("Custom Appearance")]
        [Description("Màu nền khi nhấn chuột")]
        public Color PressedBackColor
        {
            get { return _pressedBackColor; }
            set { _pressedBackColor = value; Invalidate(); }
        }

        // --- Các thuộc tính khác (InactiveBackColor, ActiveBackColor, ActiveBorderColor, BorderLeftSize, IsSelected)
        // đã có sẵn trong code của bạn, không thay đổi phần đó ---

        [Category("Custom Appearance")]
        [Description("Màu nền khi không được chọn")]
        public Color InactiveBackColor
        {
            get { return _inactiveBackColor; }
            set { _inactiveBackColor = value; Invalidate(); }
        }

        [Category("Custom Appearance")]
        [Description("Màu nền khi được chọn")]
        public Color ActiveBackColor
        {
            get { return _activeBackColor; }
            set { _activeBackColor = value; Invalidate(); }
        }

        [Category("Custom Appearance")]
        [Description("Màu vạch bên trái khi được chọn")]
        public Color ActiveBorderColor
        {
            get { return _activeBorderColor; }
            set { _activeBorderColor = value; Invalidate(); }
        }

        [Category("Custom Appearance")]
        [Description("Độ dày vạch bên trái")]
        public int BorderLeftSize
        {
            get { return _borderLeftSize; }
            set { _borderLeftSize = value; Invalidate(); }
        }

        [Category("Custom Behavior")]
        [Description("Trạng thái: Nút có đang được chọn hay không")]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                Invalidate(); // Yêu cầu nút vẽ lại
            }
        }


        // === Hàm dựng (Constructor) ===
        public MenuButton()
        {
            // Thiết lập mặc định cho giao diện phẳng
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;

            // Bật các cờ style
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.SupportsTransparentBackColor |
                          ControlStyles.UserPaint |
                          ControlStyles.Selectable, true);

            BackColor = _inactiveBackColor;
        }

        // === Ghi đè sự kiện chuột ===
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovering = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovering = false;
            Invalidate();
        }

        // 🌟 THÊM: Xử lý trạng thái Pressed
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            isPressed = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            isPressed = false;
            Invalidate();
        }

        // 🌟 CẬP NHẬT LOGIC: OnPaintBackground (Chọn màu nền)
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            Color currentBackColor = _inactiveBackColor;

            if (_isSelected)
                currentBackColor = _activeBackColor;
            // 🌟 Ưu tiên Pressed (Nhấn) cao hơn Hover
            else if (isPressed)
                currentBackColor = _pressedBackColor;
            else if (isHovering)
                currentBackColor = _hoverBackColor;

            // Gán màu nền tạm thời cho control
            BackColor = currentBackColor;

            if (BackColor == Color.Transparent && Parent != null)
            {
                // LOGIC VẼ NỀN TRONG SUỐT THỰC SỰ
                Graphics g = pevent.Graphics;
                g.TranslateTransform(-Left, -Top);

                try
                {
                    PaintEventArgs newPe = new PaintEventArgs(g, Parent.ClientRectangle);
                    InvokePaintBackground(Parent, newPe);
                    InvokePaint(Parent, newPe);
                }
                finally
                {
                    g.TranslateTransform(Left, Top);
                }
            }
            else
            {
                // LOGIC VẼ NỀN MÀU ĐẶC (Solid Color)
                using (SolidBrush brush = new SolidBrush(BackColor))
                {
                    pevent.Graphics.FillRectangle(brush, pevent.ClipRectangle);
                }
            }
        }

        // === OnPaint (Không cần thay đổi logic chọn màu vì nó đã chuyển sang OnPaintBackground) ===
        protected override void OnPaint(PaintEventArgs pe)
        {
            // KHÔNG gọi base.OnPaint(pe)

            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // 1. Vẽ Nền
            OnPaintBackground(pe);

            // 2. Vẽ vạch Active (Nếu đang được chọn)
            if (_isSelected)
            {
                using (SolidBrush borderBrush = new SolidBrush(_activeBorderColor))
                {
                    g.FillRectangle(borderBrush, 0, 0, _borderLeftSize, Height);
                }
            }

            // 3. Vẽ Icon (từ BackgroundImage) - Logic giữ nguyên
            if (BackgroundImage != null)
            {
                Rectangle destRect;
                Image img = BackgroundImage;

                float imgAspect = (float)img.Width / img.Height;
                float btnAspect = (float)Width / Height;

                int newWidth;
                int newHeight;
                int newX;
                int newY;

                int padding = 5;
                RectangleF buttonRect = new RectangleF(
                    padding,
                    padding,
                    Width - padding * 2,
                    Height - padding * 2
                );

                if (imgAspect > btnAspect)
                {
                    newWidth = (int)buttonRect.Width;
                    newHeight = (int)(newWidth / imgAspect);
                }
                else
                {
                    newHeight = (int)buttonRect.Height;
                    newWidth = (int)(newHeight * imgAspect);
                }

                newX = padding + (int)(buttonRect.Width - newWidth) / 2;
                newY = padding + (int)(buttonRect.Height - newHeight) / 2;

                destRect = new Rectangle(newX, newY, newWidth, newHeight);

                g.DrawImage(img, destRect);
            }
        }
    }
}