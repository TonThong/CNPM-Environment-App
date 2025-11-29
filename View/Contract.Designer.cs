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
            lbContract = new Label();
            roundedTextBox1 = new Environmental_Monitoring.View.Components.RoundedTextBox();
            pnContent = new Environmental_Monitoring.View.Components.RoundedPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            roundedButton1 = new Environmental_Monitoring.View.Components.RoundedButton();
            btnReal = new Environmental_Monitoring.View.Components.RoundedButton();
            btnPlan = new Environmental_Monitoring.View.Components.RoundedButton();
            btnBusiness = new Environmental_Monitoring.View.Components.RoundedButton();
            btnResult = new Environmental_Monitoring.View.Components.RoundedButton();
            btnManager = new Environmental_Monitoring.View.Components.RoundedButton();
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
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 7F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 1F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 83F));
            tableLayoutPanel1.Size = new Size(1221, 709);
            tableLayoutPanel1.TabIndex = 32;
            // 
            // lbContract
            // 
            lbContract.AutoSize = true;
            lbContract.BackColor = Color.Transparent;
            lbContract.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lbContract.ForeColor = Color.Black;
            lbContract.Location = new Point(64, 21);
            lbContract.Name = "lbContract";
            lbContract.Size = new Size(291, 38);
            lbContract.TabIndex = 26;
            lbContract.Text = "QUẢN LÍ HỢP ĐỒNG";
            lbContract.Click += lbContract_Click;
            // 
            // roundedTextBox1
            // 
            roundedTextBox1.Anchor = AnchorStyles.None;
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
            roundedTextBox1.Size = new Size(531, 43);
            roundedTextBox1.TabIndex = 19;
            roundedTextBox1.UseSystemPasswordChar = false;
            // 
            // pnContent
            // 
            pnContent.BackColor = Color.White;
            pnContent.BorderColor = Color.Transparent;
            pnContent.BorderRadius = 20;
            pnContent.BorderSize = 0;
            tableLayoutPanel1.SetColumnSpan(pnContent, 5);
            pnContent.Location = new Point(3, 122);
            pnContent.Name = "pnContent";
            pnContent.Size = new Size(1215, 584);
            pnContent.TabIndex = 23;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.FromArgb(217, 244, 227);
            tableLayoutPanel2.ColumnCount = 8;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 5);
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel2.Controls.Add(roundedButton1, 4, 0);
            tableLayoutPanel2.Controls.Add(btnReal, 3, 0);
            tableLayoutPanel2.Controls.Add(btnPlan, 2, 0);
            tableLayoutPanel2.Controls.Add(btnBusiness, 1, 0);
            tableLayoutPanel2.Controls.Add(btnResult, 5, 0);
            tableLayoutPanel2.Controls.Add(btnManager, 6, 0);
            tableLayoutPanel2.Location = new Point(3, 80);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1215, 36);
            tableLayoutPanel2.TabIndex = 21;
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.Transparent;
            roundedButton1.BaseColor = Color.Transparent;
            roundedButton1.BorderColor = Color.Black;
            roundedButton1.BorderRadius = 10;
            roundedButton1.BorderSize = 0;
            roundedButton1.Cursor = Cursors.Hand;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.HoverColor = Color.LightGray;
            roundedButton1.Location = new Point(609, 3);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Padding = new Padding(30, 15, 30, 15);
            roundedButton1.Size = new Size(176, 30);
            roundedButton1.TabIndex = 31;
            roundedButton1.Text = "Phòng Thí Nghiệm";
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click;
            // 
            // btnReal
            // 
            btnReal.BackColor = Color.Transparent;
            btnReal.BaseColor = Color.Transparent;
            btnReal.BorderColor = Color.Black;
            btnReal.BorderRadius = 10;
            btnReal.BorderSize = 0;
            btnReal.Cursor = Cursors.Hand;
            btnReal.FlatAppearance.BorderSize = 0;
            btnReal.FlatStyle = FlatStyle.Flat;
            btnReal.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            btnReal.ForeColor = Color.Black;
            btnReal.HoverColor = Color.LightGray;
            btnReal.Location = new Point(415, 3);
            btnReal.Name = "btnReal";
            btnReal.Padding = new Padding(30, 15, 30, 15);
            btnReal.Size = new Size(176, 30);
            btnReal.TabIndex = 29;
            btnReal.Text = "Hiện Trường";
            btnReal.UseVisualStyleBackColor = false;
            btnReal.Click += btnReal_Click;
            // 
            // btnPlan
            // 
            btnPlan.BackColor = Color.Transparent;
            btnPlan.BaseColor = Color.Transparent;
            btnPlan.BorderColor = Color.Black;
            btnPlan.BorderRadius = 10;
            btnPlan.BorderSize = 0;
            btnPlan.Cursor = Cursors.Hand;
            btnPlan.FlatAppearance.BorderSize = 0;
            btnPlan.FlatStyle = FlatStyle.Flat;
            btnPlan.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            btnPlan.ForeColor = Color.Black;
            btnPlan.HoverColor = Color.LightGray;
            btnPlan.Location = new Point(221, 3);
            btnPlan.Name = "btnPlan";
            btnPlan.Padding = new Padding(30, 15, 30, 15);
            btnPlan.Size = new Size(176, 30);
            btnPlan.TabIndex = 28;
            btnPlan.Text = "Kế Hoạch";
            btnPlan.UseVisualStyleBackColor = false;
            btnPlan.Click += btnPlan_Click;
            // 
            // btnBusiness
            // 
            btnBusiness.BackColor = Color.Transparent;
            btnBusiness.BaseColor = Color.Transparent;
            btnBusiness.BorderColor = Color.Black;
            btnBusiness.BorderRadius = 10;
            btnBusiness.BorderSize = 0;
            btnBusiness.Cursor = Cursors.Hand;
            btnBusiness.FlatAppearance.BorderSize = 0;
            btnBusiness.FlatStyle = FlatStyle.Flat;
            btnBusiness.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            btnBusiness.ForeColor = Color.Black;
            btnBusiness.HoverColor = Color.LightGray;
            btnBusiness.Location = new Point(27, 3);
            btnBusiness.Name = "btnBusiness";
            btnBusiness.Padding = new Padding(30, 15, 30, 15);
            btnBusiness.Size = new Size(176, 30);
            btnBusiness.TabIndex = 27;
            btnBusiness.Text = "Kinh Doanh";
            btnBusiness.UseVisualStyleBackColor = false;
            btnBusiness.Click += btnBusiness_Click;
            // 
            // btnResult
            // 
            btnResult.BackColor = Color.Transparent;
            btnResult.BaseColor = Color.Transparent;
            btnResult.BorderColor = Color.Black;
            btnResult.BorderRadius = 10;
            btnResult.BorderSize = 0;
            btnResult.Cursor = Cursors.Hand;
            btnResult.FlatAppearance.BorderSize = 0;
            btnResult.FlatStyle = FlatStyle.Flat;
            btnResult.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            btnResult.ForeColor = Color.Black;
            btnResult.HoverColor = Color.LightGray;
            btnResult.Location = new Point(803, 3);
            btnResult.Name = "btnResult";
            btnResult.Padding = new Padding(30, 15, 30, 15);
            btnResult.Size = new Size(176, 30);
            btnResult.TabIndex = 30;
            btnResult.Text = "Kết Quả";
            btnResult.UseVisualStyleBackColor = false;
            btnResult.Click += btnResult_Click;
            // 
            // btnManager
            // 
            btnManager.BackColor = Color.Transparent;
            btnManager.BaseColor = Color.Transparent;
            btnManager.BorderColor = Color.Black;
            btnManager.BorderRadius = 10;
            btnManager.BorderSize = 0;
            btnManager.Cursor = Cursors.Hand;
            btnManager.FlatAppearance.BorderSize = 0;
            btnManager.FlatStyle = FlatStyle.Flat;
            btnManager.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            btnManager.ForeColor = Color.Black;
            btnManager.HoverColor = Color.LightGray;
            btnManager.Location = new Point(997, 3);
            btnManager.Name = "btnManager";
            btnManager.Padding = new Padding(30, 15, 30, 15);
            btnManager.Size = new Size(176, 30);
            btnManager.TabIndex = 32;
            btnManager.Text = "Quản Lý";
            btnManager.UseVisualStyleBackColor = false;
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

        private Components.RoundedButton btnManager;
    }
}