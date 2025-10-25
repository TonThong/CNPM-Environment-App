using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CNPM.View.Components
{
    public class RoundedComboBox : UserControl
    {
        private ComboBox comboBox = new ComboBox();
        private int borderRadius = 15;
        private int borderSize = 1;
        private Color borderColor = Color.LightGray;
        private Color focusBorderColor = Color.SeaGreen;
        private bool isFocused = false;

        public RoundedComboBox()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            Size = new Size(200, 40);

            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Font = new Font("Segoe UI", 10f);
            comboBox.ForeColor = Color.Black;
            comboBox.BackColor = Color.White;

            comboBox.Margin = new Padding(0);
            comboBox.Location = new Point(Width / 4, (Height - comboBox.PreferredHeight) / 2);
            comboBox.Width = Width - Width / 4 - 10;
            comboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            comboBox.GotFocus += (s, e) => { isFocused = true; Invalidate(); };
            comboBox.LostFocus += (s, e) => { isFocused = false; Invalidate(); };

            Controls.Add(comboBox);
        }

        [Category("Rounded")]
        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = Math.Max(1, value); Invalidate(); }
        }

        [Category("Rounded")]
        public int BorderSize
        {
            get => borderSize;
            set { borderSize = Math.Max(0, value); Invalidate(); }
        }

        [Category("Rounded")]
        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; Invalidate(); }
        }

        [Category("Rounded")]
        public Color FocusBorderColor
        {
            get => focusBorderColor;
            set { focusBorderColor = value; Invalidate(); }
        }

        [Category("Rounded")]
        public ComboBoxStyle DropDownStyle
        {
            get => comboBox.DropDownStyle;
            set => comboBox.DropDownStyle = value;
        }

        [Category("Rounded")]
        public ComboBox.ObjectCollection Items => comboBox.Items;

        [Category("Rounded")]
        public object SelectedItem
        {
            get => comboBox.SelectedItem;
            set => comboBox.SelectedItem = value;
        }

        [Category("Rounded")]
        public int SelectedIndex
        {
            get => comboBox.SelectedIndex;
            set => comboBox.SelectedIndex = value;
        }

        [Category("Rounded")]
        public string Texts
        {
            get => comboBox.Text;
            set => comboBox.Text = value;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rectBorder = new Rectangle(0, 0, Width - 1, Height - 1);
            GraphicsPath path = GetRoundPath(rectBorder, borderRadius);

            Color drawColor = isFocused ? focusBorderColor : borderColor;
            using (Pen pen = new Pen(drawColor, borderSize))
            {
                Region = new Region(path);
                e.Graphics.DrawPath(pen, path);
            }

            comboBox.Location = new Point(Width / 4, (Height - comboBox.PreferredHeight) / 2);
            comboBox.Width = Width - Width / 4 - 10;
        }

        private GraphicsPath GetRoundPath(Rectangle rect, int radius)
        {
            float r = radius * 2F;
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, r, r, 180, 90);
            path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
            path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
            path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
