using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.Components 
{
    public partial class CustomDateTimePicker : UserControl
    {
        private int borderRadius = 10;
        private int borderSize = 1;
        private Color borderColor = Color.FromArgb(171, 171, 171);
        private Color backColor = Color.White;
        private Color foreColor = Color.Black;

        private DateTime _value = DateTime.Now;
        private DateTimePickerFormat _format = DateTimePickerFormat.Short;
        private string _customFormat;

        private Form dropdownForm;
        private MonthCalendar calendar;
        private bool isDropdownOpen = false;

        [Category("Custom")]
        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = value; this.Invalidate(); }
        }

        [Category("Custom")]
        public int BorderSize
        {
            get => borderSize;
            set { borderSize = value; this.Invalidate(); }
        }

        [Category("Custom")]
        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; this.Invalidate(); }
        }

        [Category("Custom")]
        public override Color BackColor
        {
            get => backColor;
            set { backColor = value; this.Invalidate(); }
        }

        [Category("Custom")]
        public override Color ForeColor
        {
            get => foreColor;
            set { foreColor = value; this.Invalidate(); }
        }

        [Category("Custom")]
        [Description("Giá trị ngày/giờ được chọn.")]
        public DateTime Value
        {
            get => _value;
            set
            {
                _value = value;
                this.Invalidate(); 
                OnValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        [Category("Custom")]
        [Description("Định dạng hiển thị ngày/giờ.")]
        public DateTimePickerFormat Format
        {
            get => _format;
            set
            {
                _format = value;
                this.Invalidate();
            }
        }

        [Category("Custom")]
        [Description("Chuỗi định dạng tùy chỉnh.")]
        public string CustomFormat
        {
            get => _customFormat;
            set
            {
                _customFormat = value;
                this.Invalidate();
            }
        }

        [Category("Custom")]
        public event EventHandler OnValueChanged;

        public CustomDateTimePicker()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Font = new Font("Segoe UI", 9.5F);
            this.ForeColor = Color.FromArgb(64, 64, 64); 
            this.BackColor = Color.White;
            this.Size = new Size(200, 35);
        }

        private string GetFormattedDate()
        {
            switch (_format)
            {
                case DateTimePickerFormat.Long:
                    return _value.ToLongDateString();
                case DateTimePickerFormat.Short:
                    return _value.ToShortDateString();
                case DateTimePickerFormat.Time:
                    return _value.ToLongTimeString();
                case DateTimePickerFormat.Custom:
                    return _value.ToString(_customFormat);
                default:
                    return _value.ToShortDateString();
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float diameter = radius * 2;
            RectangleF arc = new RectangleF(rect.Location, new SizeF(diameter, diameter));

            path.StartFigure();
            arc.X = rect.Left;
            arc.Y = rect.Top;
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

        private void ShowDropdown()
        {
            if (isDropdownOpen)
            {
                CloseDropdown();
                return;
            }

            isDropdownOpen = true;
            dropdownForm = new Form();
            calendar = new MonthCalendar();

            dropdownForm.FormBorderStyle = FormBorderStyle.None;
            dropdownForm.ShowInTaskbar = false;
            dropdownForm.StartPosition = FormStartPosition.Manual;
            dropdownForm.TopMost = true;

            calendar.AutoSize = true;

            dropdownForm.Controls.Add(calendar);
            calendar.Location = new Point(0, 0);

            if (!calendar.IsHandleCreated)
                calendar.CreateControl();
            Size calSize = calendar.GetPreferredSize(Size.Empty);
            dropdownForm.ClientSize = new Size(calSize.Width + 84, calSize.Height + 2);

            calendar.DateSelected += Calendar_DateSelected;
            calendar.MinDate = new DateTime(1900, 1, 1);
            calendar.MaxDate = new DateTime(2100, 1, 1);
            calendar.SetDate(this.Value);

            dropdownForm.Deactivate += DropdownForm_Deactivate;

            Point screenPos = this.PointToScreen(new Point(0, this.Height));
            dropdownForm.Location = screenPos;
            dropdownForm.Show(this.FindForm());
        }



        private void CloseDropdown()
        {
            if (isDropdownOpen)
            {
                isDropdownOpen = false;
                dropdownForm.Deactivate -= DropdownForm_Deactivate;
                calendar.DateSelected -= Calendar_DateSelected;
                dropdownForm.Close();
                dropdownForm.Dispose();
                dropdownForm = null;
                calendar = null;
            }
        }

        private void Calendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.Value = e.Start;
            CloseDropdown();
        }

        private void DropdownForm_Deactivate(object sender, EventArgs e)
        {
            CloseDropdown();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            ShowDropdown();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(this.Parent.BackColor);
            using (SolidBrush backBrush = new SolidBrush(this.backColor))
            {
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                using (GraphicsPath path = GetRoundedPath(rect, borderRadius))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.FillPath(backBrush, path);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias; 

            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            Rectangle textRect = new Rectangle(rect.X + 10, rect.Y, rect.Width - 40, rect.Height);

            using (GraphicsPath path = GetRoundedPath(rect, borderRadius))
            using (Pen borderPen = new Pen(this.borderColor, borderSize))
            {
                borderPen.Alignment = PenAlignment.Inset;
                g.DrawPath(borderPen, path);
            }

            TextRenderer.DrawText(g,
                GetFormattedDate(),
                this.Font,
                textRect,
                this.ForeColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

            using (Font iconFont = new Font("Segoe UI Symbol", 12F))
            {
                Rectangle iconRect = new Rectangle(this.Width - 34, 0, 20, this.Height);
                TextRenderer.DrawText(g,
                    "\uE163",
                    iconFont,
                    iconRect,
                    this.ForeColor, 
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }
    }
}