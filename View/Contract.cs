﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Environmental_Monitoring.View.ContractContent;

namespace Environmental_Monitoring.View
{
    internal class Contract : UserControl
    {
        public Contract()
        {
            InitializeComponent();
            LoadPage(new BusinessContent());
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
            tableLayoutPanel3 = new TableLayoutPanel();
            roundedTextBox1 = new Environmental_Monitoring.View.Components.RoundedTextBox();
            lbContract = new Label();
            pnContent = new Environmental_Monitoring.View.Components.RoundedPanel();
            roundedPanel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
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
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 3);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 1);
            tableLayoutPanel1.Controls.Add(pnContent, 0, 4);
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 74F));
            tableLayoutPanel1.Size = new Size(1221, 709);
            tableLayoutPanel1.TabIndex = 32;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 7;
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
            roundedButton1.BackColor = Color.FromArgb(217, 217, 217);
            roundedButton1.BaseColor = Color.FromArgb(217, 217, 217);
            roundedButton1.BorderColor = Color.Transparent;
            roundedButton1.BorderRadius = 25;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            roundedButton1.ForeColor = Color.Black;
            roundedButton1.HoverColor = Color.FromArgb(34, 139, 34);
            roundedButton1.Location = new Point(718, 3);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Padding = new Padding(30, 15, 30, 15);
            roundedButton1.Size = new Size(214, 58);
            roundedButton1.TabIndex = 31;
            roundedButton1.Text = "Phòng Thí Nghiệm";
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // btnReal
            // 
            btnReal.BackColor = Color.FromArgb(217, 217, 217);
            btnReal.BaseColor = Color.FromArgb(217, 217, 217);
            btnReal.BorderColor = Color.Transparent;
            btnReal.BorderRadius = 25;
            btnReal.BorderSize = 0;
            btnReal.FlatAppearance.BorderSize = 0;
            btnReal.FlatStyle = FlatStyle.Flat;
            btnReal.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReal.ForeColor = Color.Black;
            btnReal.HoverColor = Color.FromArgb(34, 139, 34);
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
            btnPlan.BackColor = Color.FromArgb(217, 217, 217);
            btnPlan.BaseColor = Color.FromArgb(217, 217, 217);
            btnPlan.BorderColor = Color.Transparent;
            btnPlan.BorderRadius = 25;
            btnPlan.BorderSize = 0;
            btnPlan.FlatAppearance.BorderSize = 0;
            btnPlan.FlatStyle = FlatStyle.Flat;
            btnPlan.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPlan.ForeColor = Color.Black;
            btnPlan.HoverColor = Color.FromArgb(34, 139, 34);
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
            btnBusiness.BackColor = Color.FromArgb(217, 217, 217);
            btnBusiness.BaseColor = Color.FromArgb(217, 217, 217);
            btnBusiness.BorderColor = Color.Transparent;
            btnBusiness.BorderRadius = 25;
            btnBusiness.BorderSize = 0;
            btnBusiness.FlatAppearance.BorderSize = 0;
            btnBusiness.FlatStyle = FlatStyle.Flat;
            btnBusiness.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBusiness.ForeColor = Color.Black;
            btnBusiness.HoverColor = Color.FromArgb(34, 139, 34);
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
            btnResult.BackColor = Color.FromArgb(217, 217, 217);
            btnResult.BaseColor = Color.FromArgb(217, 217, 217);
            btnResult.BorderColor = Color.Transparent;
            btnResult.BorderRadius = 25;
            btnResult.BorderSize = 0;
            btnResult.FlatAppearance.BorderSize = 0;
            btnResult.FlatStyle = FlatStyle.Flat;
            btnResult.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnResult.ForeColor = Color.Black;
            btnResult.HoverColor = Color.FromArgb(34, 139, 34);
            btnResult.Location = new Point(938, 3);
            btnResult.Name = "btnResult";
            btnResult.Padding = new Padding(30, 15, 30, 15);
            btnResult.Size = new Size(214, 58);
            btnResult.TabIndex = 30;
            btnResult.Text = "Kết Quả";
            btnResult.UseVisualStyleBackColor = false;
            btnResult.Click += btnResult_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 4;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel3.Controls.Add(roundedTextBox1, 2, 0);
            tableLayoutPanel3.Controls.Add(lbContract, 1, 0);
            tableLayoutPanel3.Location = new Point(3, 24);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(1215, 64);
            tableLayoutPanel3.TabIndex = 22;
            // 
            // roundedTextBox1
            // 
            roundedTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            roundedTextBox1.BackColor = Color.White;
            roundedTextBox1.BorderRadius = 15;
            roundedTextBox1.BorderThickness = 1;
            roundedTextBox1.FocusBorderColor = SystemColors.ControlDark;
            roundedTextBox1.HoverBorderColor = Color.DarkGray;
            roundedTextBox1.Location = new Point(615, 3);
            roundedTextBox1.Multiline = false;
            roundedTextBox1.Name = "roundedTextBox1";
            roundedTextBox1.NormalBorderColor = Color.DarkGray;
            roundedTextBox1.Padding = new Padding(9, 12, 9, 9);
            roundedTextBox1.PasswordChar = '\0';
            roundedTextBox1.PlaceholderText = "Tìm Kiếm...";
            roundedTextBox1.ReadOnly = false;
            roundedTextBox1.Size = new Size(534, 45);
            roundedTextBox1.TabIndex = 19;
            roundedTextBox1.UseSystemPasswordChar = false;
            // 
            // lbContract
            // 
            lbContract.AutoSize = true;
            lbContract.BackColor = Color.Transparent;
            lbContract.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lbContract.ForeColor = Color.Black;
            lbContract.Location = new Point(63, 0);
            lbContract.Name = "lbContract";
            lbContract.Size = new Size(383, 50);
            lbContract.TabIndex = 26;
            lbContract.Text = "QUẢN LÍ HỢP ĐỒNG";
            // 
            // pnContent
            // 
            pnContent.BackColor = Color.White;
            pnContent.BorderColor = Color.Transparent;
            pnContent.BorderRadius = 20;
            pnContent.BorderSize = 0;
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
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);

        }

        private void btnBusiness_Click(object sender, EventArgs e)
        {
            LoadPage(new BusinessContent());
        }

        private void LoadPage(UserControl pageToLoad)
        {
            pnContent.Controls.Clear();
            pageToLoad.Dock = DockStyle.Fill;
            pnContent.Controls.Add(pageToLoad);
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

        private void btnPlan_Click(object sender, EventArgs e)
        {
            LoadPage(new PlanContent());
        }

        private void btnReal_Click(object sender, EventArgs e)
        {
            LoadPage(new RealContent());
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            LoadPage(new ResultContent());
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
