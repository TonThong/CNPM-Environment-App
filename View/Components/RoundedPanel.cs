using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.Components
{
    public class RoundedPanel : Panel
    {
        private int _borderRadius = 20;
        private Color _borderColor = Color.Transparent;
        private int _borderSize = 0;

        [Category("Rounded")]
        public int BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = Math.Max(0, value); Invalidate(); }
        }

        [Category("Rounded")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        [Category("Rounded")]
        public int BorderSize
        {
            get => _borderSize;
            set { _borderSize = Math.Max(0, value); Invalidate(); }
        }

        public RoundedPanel()
        {
            DoubleBuffered = true;
            BackColor = Color.Honeydew; 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            g.Clear(Parent.BackColor);

            Rectangle rectSurface = ClientRectangle;
            rectSurface.Inflate(-1, -1);
            int radius = _borderRadius;
            using (GraphicsPath path = GetRoundPath(rectSurface, radius))
            using (Pen borderPen = new Pen(_borderColor, _borderSize))
            {
                Region = new Region(path);
                e.Graphics.FillPath(new SolidBrush(BackColor), path);

                if (_borderSize > 0)
                    e.Graphics.DrawPath(borderPen, path);
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
