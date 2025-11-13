using System.Windows.Forms;
using System.Drawing;

namespace Environmental_Monitoring.View
{
    internal partial class Contract
    {
        #region Windows Forms Designer Generated Code

        private System.ComponentModel.IContainer components = null;

        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            roundedPanel1 = new Environmental_Monitoring.View.Components.RoundedPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            roundedButton1 = new Environmental_Monitoring.View.Components.RoundedButton();
            btnReal = new Environmental_Monitoring.View.Components.RoundedButton();
            btnPlan = new Environmental_Monitoring.View.Components.RoundedButton();
            btnBusiness = new Environmental_Monitoring.View.Components.RoundedButton();
            btnResult = new Environmental_Monitoring.View.Components.RoundedButton();
            roundedTextBox1 = new Environmental_Monitoring.View.Components.RoundedTextBox();
            lbContract = new Label();
            pnContent = new Environmental_Monitoring.View.Components.RoundedPanel();
            roundedPanel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // roundedPanel1
            // 
            roundedPanel1.BackColor = Color.FromArgb(217, 244, 227);
            roundedPanel1.BorderColor = Color.Transparent;
            roundedPanel1.BorderRadius = 20;
            roundedPanel1.BorderSize = 0;
            roundedPanel1.Controls.Add(tableLayoutPanel1);
            roundedPanel1.ForeColor = Color.Transparent;
            roundedPanel1.Location = new Point(0, 0);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(1227, 715);
            roundedPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(217, 244, 227);
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Controls.Add(lbContract, 1, 1);
            tableLayoutPanel1.Controls.Add(roundedTextBox1, 3, 1);
            tableLayoutPanel1.Controls.Add(pnContent, 0, 4);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 3);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 74F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1221, 709);
            tableLayoutPanel1.TabIndex = 32;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.FromArgb(217, 244, 227);
            tableLayoutPanel2.ColumnCount = 7;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 5);
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.5454545F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.181818F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.181818F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.181818F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.181818F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.181818F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.5454545F));
            tableLayoutPanel2.Controls.Add(roundedButton1, 4, 0);
            tableLayoutPanel2.Controls.Add(btnReal, 3, 0);
            tableLayoutPanel2.Controls.Add(btnPlan, 2, 0);
            tableLayoutPanel2.Controls.Add(btnBusiness, 1, 0);
            tableLayoutPanel2.Controls.Add(btnResult, 5, 0);
            tableLayoutPanel2.Location = new Point(3, 115);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1215, 64);
            tableLayoutPanel2.TabIndex = 21;
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.Transparent;
            roundedButton1.BaseColor = Color.Transparent;
            roundedButton1.BorderColor = Color.Transparent;
            roundedButton1.BorderRadius = 10;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.HoverColor = Color.Gray;
            roundedButton1.Location = new Point(718, 3);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Padding = new Padding(30, 15, 30, 15);
            roundedButton1.Size = new Size(214, 58);
            roundedButton1.TabIndex = 31;
            roundedButton1.Text = "Phòng Thí Nghiệm";
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // btnReal
            // 
            btnReal.BackColor = Color.Transparent;
            btnReal.BaseColor = Color.Transparent;
            btnReal.BorderColor = Color.Transparent;
            btnReal.BorderRadius = 10;
            btnReal.BorderSize = 0;
            btnReal.FlatAppearance.BorderSize = 0;
            btnReal.FlatStyle = FlatStyle.Flat;
            btnReal.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReal.ForeColor = Color.Black;
            btnReal.HoverColor = Color.DarkGray;
            btnReal.Location = new Point(498, 3);
            btnReal.Name = "btnReal";
            btnReal.Padding = new Padding(30, 15, 30, 15);
            btnReal.Size = new Size(214, 58);
            btnReal.TabIndex = 29;
            btnReal.Text = "Hiện Trường";
            btnReal.UseVisualStyleBackColor = false;
            btnReal.Click += btnReal_Click;
            // 
            // btnPlan
            // 
            btnPlan.BackColor = Color.Transparent;
            btnPlan.BaseColor = Color.Transparent;
            btnPlan.BorderColor = Color.Transparent;
            btnPlan.BorderRadius = 10;
            btnPlan.BorderSize = 0;
            btnPlan.FlatAppearance.BorderSize = 0;
            btnPlan.FlatStyle = FlatStyle.Flat;
            btnPlan.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPlan.ForeColor = Color.Black;
            btnPlan.HoverColor = Color.DarkGray;
            btnPlan.Location = new Point(278, 3);
            btnPlan.Name = "btnPlan";
            btnPlan.Padding = new Padding(30, 15, 30, 15);
            btnPlan.Size = new Size(214, 58);
            btnPlan.TabIndex = 28;
            btnPlan.Text = "Kế Hoạch";
            btnPlan.UseVisualStyleBackColor = false;
            btnPlan.Click += btnPlan_Click;
            // 
            // btnBusiness
            // 
            btnBusiness.BackColor = Color.Transparent;
            btnBusiness.BaseColor = Color.Transparent;
            btnBusiness.BorderColor = Color.Transparent;
            btnBusiness.BorderRadius = 10;
            btnBusiness.BorderSize = 0;
            btnBusiness.FlatAppearance.BorderSize = 0;
            btnBusiness.FlatStyle = FlatStyle.Flat;
            btnBusiness.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBusiness.ForeColor = Color.Black;
            btnBusiness.HoverColor = Color.DarkGray;
            btnBusiness.Location = new Point(58, 3);
            btnBusiness.Name = "btnBusiness";
            btnBusiness.Padding = new Padding(30, 15, 30, 15);
            btnBusiness.Size = new Size(214, 58);
            btnBusiness.TabIndex = 27;
            btnBusiness.Text = "Kinh Doanh";
            btnBusiness.UseVisualStyleBackColor = false;
            btnBusiness.Click += btnBusiness_Click;
            // 
            // btnResult
            // 
            btnResult.BackColor = Color.Transparent;
            btnResult.BaseColor = Color.Transparent;
            btnResult.BorderColor = Color.Transparent;
            btnResult.BorderRadius = 10;
            btnResult.BorderSize = 0;
            btnResult.FlatAppearance.BorderSize = 0;
            btnResult.FlatStyle = FlatStyle.Flat;
            btnResult.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnResult.ForeColor = Color.Black;
            btnResult.HoverColor = Color.DarkGray;
            btnResult.Location = new Point(938, 3);
            btnResult.Name = "btnResult";
            btnResult.Padding = new Padding(30, 15, 30, 15);
            btnResult.Size = new Size(214, 58);
            btnResult.TabIndex = 30;
            btnResult.Text = "Kết Quả";
            btnResult.UseVisualStyleBackColor = false;
            btnResult.Click += btnResult_Click;
            // 
            // roundedTextBox1
            // 
            roundedTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            roundedTextBox1.BackColor = Color.White;
            roundedTextBox1.BorderRadius = 15;
            roundedTextBox1.BorderThickness = 1;
            roundedTextBox1.FocusBorderColor = SystemColors.ControlDark;
            roundedTextBox1.HoverBorderColor = Color.DarkGray;
            roundedTextBox1.Location = new Point(625, 24);
            roundedTextBox1.Multiline = false;
            roundedTextBox1.Name = "roundedTextBox1";
            roundedTextBox1.NormalBorderColor = Color.DarkGray;
            roundedTextBox1.Padding = new Padding(9, 12, 9, 9);
            roundedTextBox1.PasswordChar = '\0';
            roundedTextBox1.PlaceholderText = "Tìm Kiếm...";
            roundedTextBox1.ReadOnly = false;
            roundedTextBox1.Size = new Size(531, 45);
            roundedTextBox1.TabIndex = 19;
            roundedTextBox1.UseSystemPasswordChar = false;
            // 
            // lbContract
            // 
            lbContract.AutoSize = true;
            lbContract.BackColor = Color.Transparent;
            lbContract.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lbContract.ForeColor = Color.Black;
            lbContract.Location = new Point(64, 21);
            lbContract.Name = "lbContract";
            lbContract.Size = new Size(383, 50);
            lbContract.TabIndex = 26;
            lbContract.Text = "QUẢN LÍ HỢP ĐỒNG";
            lbContract.Click += lbContract_Click;
            // 
            // pnContent
            // 
            pnContent.BackColor = Color.White;
            pnContent.BorderColor = Color.Transparent;
            pnContent.BorderRadius = 20;
            pnContent.BorderSize = 0;
            tableLayoutPanel1.SetColumnSpan(pnContent, 5);
            pnContent.Location = new Point(3, 185);
            pnContent.Name = "pnContent";
            pnContent.Size = new Size(1215, 521);
            pnContent.TabIndex = 23;
            // 
            // Contract
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(roundedPanel1);
            Name = "Contract";
            Size = new Size(1227, 715);
            roundedPanel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Components.RoundedPanel roundedPanel1;
        private Components.RoundedButton btnResult;
        private Components.RoundedButton btnReal;
        private Components.RoundedButton btnPlan;
        private Components.RoundedButton btnBusiness;
        private TableLayoutPanel tableLayoutPanel1;
        private Components.RoundedTextBox roundedTextBox1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Components.RoundedButton roundedButton1;
        private Components.RoundedPanel pnContent;
        private Label lbContract;

        #endregion
    }
}