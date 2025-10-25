using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Environmental_Monitoring.View.Components
  
{
    [DefaultEvent("TextChanged")]
    public partial class RoundedTextBox : UserControl
    {
        private System.Windows.Forms.TextBox textBox;
        private bool isHovered = false;
        private bool isFocused = false;

        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern nint SendMessage(nint hWnd, int Msg, nint wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

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
                UpdatePadding();
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
                UpdatePadding(); 
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
        public bool UseSystemPasswordChar
        {
            get
            {
                return textBox.UseSystemPasswordChar;
            }
            set
            {
                textBox.UseSystemPasswordChar = value;
            }
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

        public new event EventHandler TextChanged
        {
            add { textBox.TextChanged += value; }
            remove { textBox.TextChanged -= value; }
        }

        public RoundedTextBox()
        {
            InitializeComponent();

            textBox = new System.Windows.Forms.TextBox();
            textBox.BorderStyle = BorderStyle.None;
            textBox.Dock = DockStyle.Fill;
            textBox.Font = Font; 
            textBox.BackColor = BackColor; 
            textBox.ForeColor = ForeColor; 

            Controls.Add(textBox);

            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;

            UpdatePadding();

            MouseEnter += OnControlMouseEnter;
            MouseLeave += OnControlMouseLeave;
            Click += OnControlClick;

            textBox.MouseEnter += OnControlMouseEnter; 
            textBox.MouseLeave += OnControlMouseLeave;
            textBox.Enter += OnTextBoxEnter;      
            textBox.Leave += OnTextBoxLeave;      

            textBox.HandleCreated += OnTextBoxHandleCreated;
        }

        private void OnTextBoxHandleCreated(object sender, EventArgs e)
        {
            SetCueBanner(_placeholderText);
        }

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

        private void OnTextBoxEnter(object sender, EventArgs e)
        {
            isFocused = true;
            Invalidate(); 
        }

        private void OnTextBoxLeave(object sender, EventArgs e)
        {
            isFocused = false;
            Invalidate(); 
        }

        private void OnControlClick(object sender, EventArgs e)
        {
          
            textBox.Focus();
        }


        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            if (textBox != null)
            {
                textBox.BackColor = BackColor; 
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

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
                RectangleF rect = new RectangleF(
                    _borderThickness / 2.0f,
                    _borderThickness / 2.0f,
                    Width - _borderThickness - 1,
                    Height - _borderThickness - 1
                );

                GraphicsPath path = GetRoundedRect(rect, _borderRadius);

                g.Clear(Parent.BackColor);

                g.FillPath(backgroundBrush, path);

                g.DrawPath(borderPen, path);
            }
        }

        private void UpdatePadding()
        {
            int padding = (int)Math.Ceiling((double)_borderRadius / 2) + _borderThickness;
            Padding = new Padding(padding, padding, padding, padding);
        }

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


        private void SetCueBanner(string text)
        {
            if (textBox != null && textBox.IsHandleCreated)
            {
                SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, text);
            }
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            Name = "RoundTextBox";
            Size = new Size(150, 30);
            ResumeLayout(false);
        }

    }
}