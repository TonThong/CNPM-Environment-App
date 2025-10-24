using System.ComponentModel;
using System.Drawing.Drawing2D;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.Components
{
    public class MenuButton : Button
    {
        private bool _isSelected = false;
        private bool isHovering = false;
        private bool isPressed = false;

        private int _borderLeftSize = 15;
        private Color _inactiveBackColor = Color.Transparent;
        private Color _activeBackColor = Color.FromArgb(220, 240, 220); 
        private Color _activeBorderColor = Color.FromArgb(0, 100, 0);
        private Color _hoverBackColor = Color.FromArgb(230, 230, 230); 
        private Color _pressedBackColor = Color.FromArgb(200, 200, 200); 

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
                Invalidate(); 
            }
        }

        public MenuButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.SupportsTransparentBackColor |
                          ControlStyles.UserPaint |
                          ControlStyles.Selectable, true);

            BackColor = _inactiveBackColor;

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                  ControlStyles.AllPaintingInWmPaint |
                  ControlStyles.ResizeRedraw |
                  ControlStyles.SupportsTransparentBackColor |
                  ControlStyles.UserPaint |
                  ControlStyles.Selectable |
                  ControlStyles.Opaque, true);
        }

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

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            Color currentBackColor = _inactiveBackColor;

            if (_isSelected)
                currentBackColor = _activeBackColor;
          
            else if (isPressed)
                currentBackColor = _pressedBackColor;
            else if (isHovering)
                currentBackColor = _hoverBackColor;

            BackColor = currentBackColor;

            if (BackColor == Color.Transparent && Parent != null)
            {
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
       
                using (SolidBrush brush = new SolidBrush(BackColor))
                {
                    pevent.Graphics.FillRectangle(brush, pevent.ClipRectangle);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
           
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            OnPaintBackground(pe);

            if (_isSelected)
            {
                using (SolidBrush borderBrush = new SolidBrush(_activeBorderColor))
                {
                    g.FillRectangle(borderBrush, 0, 0, _borderLeftSize, Height);
                }
            }

            const int ICON_SIZE = 90;     
            const int ICON_MARGIN_LEFT = 0; 
            const int SPACING_AFTER_ICON = 10;

            if (BackgroundImage != null)
            {
                
                int iconX = ICON_MARGIN_LEFT;
                int iconY = (Height - ICON_SIZE) / 2;

                Rectangle destRect = new Rectangle(iconX, iconY, ICON_SIZE, ICON_SIZE);

               
                g.DrawImage(BackgroundImage, destRect);
            }

            if (!string.IsNullOrEmpty(Text) && Parent != null && Parent.Width > 100)
            {
                int textStartX = ICON_MARGIN_LEFT + ICON_SIZE + SPACING_AFTER_ICON;

                Rectangle textRect = new Rectangle(
                    textStartX,
                    0,
                    Width - textStartX, 
                    Height
                );

                TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis;

                TextRenderer.DrawText(
                    g,
                    Text,
                    Font,
                    textRect,
                    ForeColor,
                    flags
                );
            }

            if (Image != null)
            {
                Image = null;
            }
        }
    }
}