using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Collections;

namespace Environmental_Monitoring.View.Components
{
    [DefaultEvent("SelectedIndexChanged")]
    public partial class CustomComboBox : UserControl
    {

        #region Members & Properties

        private ListBox _internalListBox;
        private Form _dropDownForm;
        private ListBox _dropDownListBox;

        private bool isHovered = false;
        private bool isFocused = false;
        private bool isDroppedDown = false;
        private string _displayText = "";

        private int _borderRadius = 15;
        private int _borderThickness = 2;
        private Color _normalBorderColor = Color.Gray;
        private Color _hoverBorderColor = Color.DodgerBlue;
        private Color _focusBorderColor = Color.HotPink;
        private Color _arrowColor = Color.DimGray;
        private int _arrowWidth = 10;
        private Color _dropDownBackColor = Color.White;
        private Color _dropDownHoverColor = Color.DodgerBlue;

        #region Custom Appearance Properties
        [Category("Custom Appearance")]
        [Description("Độ cong của 4 góc.")]
        public int BorderRadius
        {
            get { return _borderRadius; }
            set { _borderRadius = value < 0 ? 0 : value; Invalidate(); }
        }

        [Category("Custom Appearance")]
        [Description("Độ dày của viền.")]
        public int BorderThickness
        {
            get { return _borderThickness; }
            set { _borderThickness = value < 1 ? 1 : value; Invalidate(); }
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

        [Category("Custom Appearance")]
        [Description("Màu của mũi tên thả xuống.")]
        public Color ArrowColor
        {
            get { return _arrowColor; }
            set { _arrowColor = value; Invalidate(); }
        }

        [Category("Custom Appearance")]
        [Description("Màu nền của danh sách thả xuống.")]
        public Color DropDownBackColor
        {
            get { return _dropDownBackColor; }
            set { _dropDownBackColor = value; }
        }

        [Category("Custom Appearance")]
        [Description("Màu khi di chuột vào item trong danh sách.")]
        public Color DropDownHoverColor
        {
            get { return _dropDownHoverColor; }
            set { _dropDownHoverColor = value; }
        }

        #endregion

        #region Data Properties
        [Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [MergableProperty(false)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]

        public ListBox.ObjectCollection Items
        {
            get { return _internalListBox.Items; }
        }

        [Category("Data")]
        [DefaultValue(null)]
        public object DataSource
        {
            get { return _internalListBox.DataSource; }
            set { _internalListBox.DataSource = value; }
        }

        [Category("Data")]
        [DefaultValue("")]
        public string DisplayMember
        {
            get { return _internalListBox.DisplayMember; }
            set { _internalListBox.DisplayMember = value; }
        }

        [Category("Data")]
        [DefaultValue("")]
        public string ValueMember
        {
            get { return _internalListBox.ValueMember; }
            set { _internalListBox.ValueMember = value; }
        }

        [Category("Data")]
        [Browsable(false)]
        public int SelectedIndex
        {
            get { return _internalListBox.SelectedIndex; }
            set { _internalListBox.SelectedIndex = value; }
        }

        [Category("Data")]
        [Browsable(false)]
        public object SelectedItem
        {
            get { return _internalListBox.SelectedItem; }
            set { _internalListBox.SelectedItem = value; }
        }

        [Category("Data")]
        [Browsable(false)]
        public object SelectedValue
        {
            get { return _internalListBox.SelectedValue; }
            set { _internalListBox.SelectedValue = value; }
        }

        [Category("Appearance")]
        public override string Text
        {
            get { return _displayText; }
            set { _displayText = value; Invalidate(); }
        }

        [Category("Behavior")]
        public int DropDownHeight { get; set; } = 150;
        #endregion
        #endregion

        [Category("Action")]
        public event EventHandler SelectedIndexChanged;

        public CustomComboBox()
        {
            InitializeComponent();

            _internalListBox = new ListBox();
            _dropDownForm = new Form();
            _dropDownListBox = new ListBox();

            _internalListBox.DrawMode = DrawMode.OwnerDrawFixed;
            _internalListBox.SelectedIndexChanged += _internalListBox_SelectedIndexChanged;

            _dropDownForm.FormBorderStyle = FormBorderStyle.None;
            _dropDownForm.StartPosition = FormStartPosition.Manual;
            _dropDownForm.ShowInTaskbar = false;
            _dropDownForm.TopMost = true;
            _dropDownForm.Deactivate += _dropDownForm_Deactivate;
            _dropDownForm.VisibleChanged += _dropDownForm_VisibleChanged;


            _dropDownListBox.Dock = DockStyle.Fill;
            _dropDownListBox.BorderStyle = BorderStyle.None;
            _dropDownListBox.DrawMode = DrawMode.OwnerDrawFixed;
            _dropDownListBox.MouseClick += _dropDownListBox_MouseClick;
            _dropDownListBox.DrawItem += _dropDownListBox_DrawItem;

            _dropDownForm.Controls.Add(_dropDownListBox);

            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;

            this.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
        }

        private void _internalListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_internalListBox.SelectedItem != null)
            {
                _displayText = _internalListBox.GetItemText(_internalListBox.SelectedItem);
            }
            else
            {
                _displayText = "";
            }

            SelectedIndexChanged?.Invoke(this, e);
            Invalidate();
        }

        private void _dropDownListBox_MouseClick(object sender, MouseEventArgs e)
        {
            int index = _dropDownListBox.IndexFromPoint(e.Location);
            if (index >= 0)
            {
                // Dòng này bây giờ sẽ an toàn, vì _internalListBox vẫn
                // giữ DataSource của nó và có Items.Count > 0
                _internalListBox.SelectedIndex = _dropDownListBox.SelectedIndex;

                CloseDropDown();
                isFocused = true;
                Invalidate();
            }
        }

        private void _dropDownListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            // Sửa lỗi: Phải dùng GetItemText trên chính _dropDownListBox
            string itemText = _dropDownListBox.GetItemText(_dropDownListBox.Items[e.Index]);

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected ||
                (e.State & DrawItemState.Focus) == DrawItemState.Focus)
            {
                e.Graphics.FillRectangle(new SolidBrush(_dropDownHoverColor), e.Bounds);
                e.Graphics.DrawString(itemText, e.Font, Brushes.White, e.Bounds, StringFormat.GenericDefault);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(_dropDownBackColor), e.Bounds);
                e.Graphics.DrawString(itemText, e.Font, new SolidBrush(this.ForeColor), e.Bounds, StringFormat.GenericDefault);
            }
            e.DrawFocusRectangle();
        }

        private void _dropDownForm_Deactivate(object sender, EventArgs e)
        {
            CloseDropDown();
            isFocused = false;
            Invalidate();
        }

        private void _dropDownForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!_dropDownForm.Visible)
            {
                isDroppedDown = false;
                Invalidate();
            }
        }

        #region UserControl Events
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            ToggleDropDown();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovered = false;
            Invalidate();
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            isFocused = true;
            Invalidate();
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            if (!isDroppedDown)
            {
                isFocused = false;
                Invalidate();
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            _internalListBox.Font = this.Font;
            _dropDownListBox.Font = this.Font;
            _dropDownListBox.ItemHeight = this.Font.Height + 2;
        }
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            _dropDownListBox.BackColor = _dropDownBackColor;
        }
        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            _internalListBox.ForeColor = this.ForeColor;
            _dropDownListBox.ForeColor = this.ForeColor;
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color borderColor;
            if (isFocused || isDroppedDown)
                borderColor = _focusBorderColor;
            else if (isHovered)
                borderColor = _hoverBorderColor;
            else
                borderColor = _normalBorderColor;

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

                g.Clear(Parent.BackColor);
                g.FillPath(backgroundBrush, path);
                g.DrawPath(borderPen, path);
            }

            DrawArrow(g, _arrowColor);

            int textPaddingLeft = (int)Math.Ceiling((double)_borderRadius / 2) + _borderThickness + 2;

            int arrowSpaceRight = _arrowWidth + textPaddingLeft + _borderThickness;

            Rectangle textBounds = new Rectangle(
                this.ClientRectangle.X + textPaddingLeft,
                this.ClientRectangle.Y,
                this.ClientRectangle.Width - textPaddingLeft - arrowSpaceRight,
                this.ClientRectangle.Height
            );

            TextRenderer.DrawText(g, _displayText, this.Font,
                textBounds,
                this.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);
        }

        private void ToggleDropDown()
        {
            if (isDroppedDown)
            {
                CloseDropDown();
            }
            else
            {
                ShowDropDown();
            }
        }

        private void ShowDropDown()
        {
            if (isDroppedDown) return;

            isDroppedDown = true;
            isFocused = true;
            Invalidate();

            _dropDownListBox.DataSource = null;
            _dropDownListBox.Items.Clear();
            _dropDownListBox.DisplayMember = _internalListBox.DisplayMember;
            _dropDownListBox.ValueMember = _internalListBox.ValueMember;

            // <--- BẮT ĐẦU SỬA LỖI ---
            // Luôn sao chép các Items từ _internalListBox
            // Collection này vẫn hoạt động ngay cả khi _internalListBox có DataSource
            foreach (var item in _internalListBox.Items)
            {
                _dropDownListBox.Items.Add(item);
            }
            // <--- KẾT THÚC SỬA LỖI ---


            if (this.FindForm() != null)
            {
                _dropDownForm.Owner = this.FindForm();
            }

            Point screenLocation = this.PointToScreen(Point.Empty);
            _dropDownForm.Location = new Point(screenLocation.X, screenLocation.Y + this.Height);
            _dropDownForm.Width = this.Width;

            // Chúng ta luôn dùng _internalListBox.Items.Count
            int itemCount = _internalListBox.Items.Count;

            if (itemCount > 0)
            {
                int totalHeight = itemCount * _dropDownListBox.ItemHeight + 2;
                _dropDownForm.Height = Math.Min(DropDownHeight, totalHeight);
                _dropDownListBox.SelectedIndex = _internalListBox.SelectedIndex;
            }
            else
            {
                _dropDownForm.Height = 1;
            }

            _dropDownForm.Show();
        }

        private void CloseDropDown()
        {
            if (!isDroppedDown) return;
            _dropDownForm.Hide();
        }

        #region Helper Methods
        private void DrawArrow(Graphics g, Color color)
        {
            using (Pen arrowPen = new Pen(color, 2))
            {
                arrowPen.StartCap = LineCap.Round;
                arrowPen.EndCap = LineCap.Round;

                int arrowHeight = _arrowWidth / 2;
                int arrowX = this.ClientRectangle.Right - _arrowWidth - (_borderRadius / 2) - _borderThickness;
                int arrowY = (this.ClientRectangle.Height - arrowHeight) / 2;

                Point point1 = new Point(arrowX, arrowY);
                Point point2 = new Point(arrowX + _arrowWidth / 2, arrowY + arrowHeight);
                Point point3 = new Point(arrowX + _arrowWidth, arrowY);

                g.DrawLine(arrowPen, point1, point2);
                g.DrawLine(arrowPen, point2, point3);
            }
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
        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "CustomComboBox";
            this.Size = new System.Drawing.Size(150, 30);
            this.ResumeLayout(false);
        }
    }
}