using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.Components
{
    public class RoundedButton : Button
    {
        private int _borderRadius = 25;
        private int _borderSize = 0;
        private Color _borderColor = Color.Transparent;
        private Color _hoverColor = Color.FromArgb(34, 139, 34);
        private Color _baseColor = Color.SeaGreen;

        [Category("Rounded")]
        public int BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = Math.Max(1, value); Invalidate(); }
        }

        [Category("Rounded")]
        public int BorderSize
        {
            get => _borderSize;
            set { _borderSize = Math.Max(0, value); Invalidate(); }
        }

        [Category("Rounded")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        [Category("Rounded")]
        public Color HoverColor
        {
            get => _hoverColor;
            set { _hoverColor = value; Invalidate(); }
        }

        [Category("Rounded")]
        public Color BaseColor
        {
            get => _baseColor;
            set { _baseColor = value; BackColor = value; Invalidate(); }
        }

        private bool isHovering = false;

        public RoundedButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = _baseColor;
            ForeColor = Color.White;
            DoubleBuffered = true;
            Resize += (s, e) => { Invalidate(); };
            MouseEnter += (s, e) => { isHovering = true; Invalidate(); };
            MouseLeave += (s, e) => { isHovering = false; Invalidate(); };
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rectSurface = ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -_borderSize, -_borderSize);
            int radius = Math.Min(_borderRadius, Height);

            using (GraphicsPath pathSurface = GetRoundPath(rectSurface, radius))
            using (GraphicsPath pathBorder = GetRoundPath(rectBorder, Math.Max(0, radius - _borderSize)))
            using (Pen penBorder = new Pen(_borderColor, _borderSize))
            {
                pevent.Graphics.Clear(Parent?.BackColor ?? SystemColors.Control);
                pevent.Graphics.FillPath(new SolidBrush(isHovering ? _hoverColor : BackColor), pathSurface);

                if (_borderSize > 0)
                    pevent.Graphics.DrawPath(penBorder, pathBorder);
            }

            // --- VẼ ẢNH (ICON) + CHỮ ---
            Rectangle textRect = rectSurface;
            int padding = 10;
            int textOffset = 0;

            if (Image != null)
            {
                Size imgSize = Image.Size;
                Point imgLocation = Point.Empty;

                switch (ImageAlign)
                {
                    case ContentAlignment.MiddleLeft:
                        imgLocation = new Point(padding, (Height - imgSize.Height) / 2);
                        textOffset = imgSize.Width + 8;
                        textRect = new Rectangle(imgLocation.X + textOffset, 0, Width - textOffset - padding, Height);
                        break;

                    case ContentAlignment.MiddleRight:
                        imgLocation = new Point(Width - imgSize.Width - padding, (Height - imgSize.Height) / 2);
                        textRect = new Rectangle(padding, 0, Width - imgSize.Width - textOffset - padding, Height);
                        break;

                    case ContentAlignment.MiddleCenter:
                        imgLocation = new Point((Width - imgSize.Width) / 2, (Height - imgSize.Height) / 2);
                        break;
                }

                pevent.Graphics.DrawImage(Image, imgLocation);
            }

            // --- VẼ CHỮ ---
            StringFormat sf = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = TextAlign == ContentAlignment.MiddleLeft ? StringAlignment.Near :
                            TextAlign == ContentAlignment.MiddleRight ? StringAlignment.Far :
                            StringAlignment.Center
            };

            using (Brush brush = new SolidBrush(ForeColor))
            {
                pevent.Graphics.DrawString(Text, Font, brush, textRect, sf);
            }
        }

        private GraphicsPath GetRoundPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            if (radius <= 0)
            {
                path.AddRectangle(rect);
                path.CloseFigure();
                return path;
            }

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
