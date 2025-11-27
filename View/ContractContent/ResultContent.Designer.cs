namespace Environmental_Monitoring.View.ContractContent
{
    partial class ResultContent
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            roundedDataGridView2 = new Environmental_Monitoring.View.Components.RoundedDataGridView();
            lbContractID = new Environmental_Monitoring.View.Components.RoundedButton();
            btnDuyet = new Environmental_Monitoring.View.Components.RoundedButton();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnPDF = new Environmental_Monitoring.View.Components.RoundedButton();
            btnMail = new Environmental_Monitoring.View.Components.RoundedButton();
            btnRequest = new Environmental_Monitoring.View.Components.RoundedButton();
            btnContract = new Environmental_Monitoring.View.Components.RoundedButton();
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView2).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // roundedDataGridView2
            // 
            roundedDataGridView2.BackgroundColor = Color.White;
            roundedDataGridView2.BorderRadius = 20;
            roundedDataGridView2.BorderStyle = BorderStyle.None;
            roundedDataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.None;
            roundedDataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel1.SetColumnSpan(roundedDataGridView2, 4);
            roundedDataGridView2.EnableHeadersVisualStyles = false;
            roundedDataGridView2.Location = new Point(27, 107);
            roundedDataGridView2.Name = "roundedDataGridView2";
            roundedDataGridView2.RowHeadersWidth = 51;
            roundedDataGridView2.Size = new Size(1086, 367);
            roundedDataGridView2.TabIndex = 70;
            // 
            // lbContractID
            // 
            lbContractID.Anchor = AnchorStyles.Right;
            lbContractID.BackColor = Color.FromArgb(217, 217, 217);
            lbContractID.BaseColor = Color.FromArgb(217, 217, 217);
            lbContractID.BorderColor = Color.Transparent;
            lbContractID.BorderRadius = 15;
            lbContractID.BorderSize = 0;
            tableLayoutPanel1.SetColumnSpan(lbContractID, 2);
            lbContractID.Enabled = false;
            lbContractID.FlatAppearance.BorderSize = 0;
            lbContractID.FlatStyle = FlatStyle.Flat;
            lbContractID.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lbContractID.ForeColor = Color.Black;
            lbContractID.HoverColor = Color.FromArgb(34, 139, 34);
            lbContractID.Location = new Point(609, 26);
            lbContractID.Name = "lbContractID";
            lbContractID.Size = new Size(576, 52);
            lbContractID.TabIndex = 67;
            lbContractID.Text = "Khách Hàng";
            lbContractID.UseVisualStyleBackColor = false;
            lbContractID.Click += btnSearch_Click_1;
            // 
            // btnDuyet
            // 
            btnDuyet.BackColor = Color.FromArgb(0, 113, 0);
            btnDuyet.BaseColor = Color.FromArgb(0, 113, 0);
            btnDuyet.BorderColor = Color.Transparent;
            btnDuyet.BorderRadius = 15;
            btnDuyet.BorderSize = 0;
            btnDuyet.Cursor = Cursors.Hand;
            btnDuyet.Dock = DockStyle.Fill;
            btnDuyet.FlatAppearance.BorderSize = 0;
            btnDuyet.FlatStyle = FlatStyle.Flat;
            btnDuyet.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnDuyet.ForeColor = Color.White;
            btnDuyet.HoverColor = Color.FromArgb(34, 139, 34);
            btnDuyet.Location = new Point(27, 503);
            btnDuyet.Name = "btnDuyet";
            btnDuyet.Size = new Size(285, 52);
            btnDuyet.TabIndex = 71;
            btnDuyet.Text = "Duyệt";
            btnDuyet.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2F));
            tableLayoutPanel1.Controls.Add(btnPDF, 3, 5);
            tableLayoutPanel1.Controls.Add(btnMail, 4, 5);
            tableLayoutPanel1.Controls.Add(btnRequest, 2, 5);
            tableLayoutPanel1.Controls.Add(btnContract, 1, 1);
            tableLayoutPanel1.Controls.Add(roundedDataGridView2, 1, 3);
            tableLayoutPanel1.Controls.Add(btnDuyet, 1, 5);
            tableLayoutPanel1.Controls.Add(lbContractID, 3, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 64F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4F));
            tableLayoutPanel1.Size = new Size(1215, 584);
            tableLayoutPanel1.TabIndex = 73;
            // 
            // btnPDF
            // 
            btnPDF.BackColor = Color.FromArgb(214, 66, 16);
            btnPDF.BaseColor = Color.FromArgb(214, 66, 16);
            btnPDF.BorderColor = Color.Transparent;
            btnPDF.BorderRadius = 15;
            btnPDF.BorderSize = 0;
            btnPDF.Cursor = Cursors.Hand;
            btnPDF.Dock = DockStyle.Fill;
            btnPDF.FlatAppearance.BorderSize = 0;
            btnPDF.FlatStyle = FlatStyle.Flat;
            btnPDF.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnPDF.ForeColor = Color.White;
            btnPDF.HoverColor = Color.FromArgb(255, 128, 128);
            btnPDF.Location = new Point(609, 503);
            btnPDF.Name = "btnPDF";
            btnPDF.Size = new Size(285, 52);
            btnPDF.TabIndex = 77;
            btnPDF.Text = "PDF";
            btnPDF.UseVisualStyleBackColor = false;
            // 
            // btnMail
            // 
            btnMail.BackColor = Color.FromArgb(55, 63, 81);
            btnMail.BaseColor = Color.FromArgb(55, 63, 81);
            btnMail.BorderColor = Color.Transparent;
            btnMail.BorderRadius = 15;
            btnMail.BorderSize = 0;
            btnMail.Cursor = Cursors.Hand;
            btnMail.Dock = DockStyle.Fill;
            btnMail.FlatAppearance.BorderSize = 0;
            btnMail.FlatStyle = FlatStyle.Flat;
            btnMail.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnMail.ForeColor = Color.White;
            btnMail.HoverColor = Color.FromArgb(64, 64, 64);
            btnMail.Location = new Point(900, 503);
            btnMail.Name = "btnMail";
            btnMail.Size = new Size(285, 52);
            btnMail.TabIndex = 75;
            btnMail.Text = "Mail";
            btnMail.UseVisualStyleBackColor = false;
            // 
            // btnRequest
            // 
            btnRequest.BackColor = Color.FromArgb(131, 138, 154);
            btnRequest.BaseColor = Color.FromArgb(131, 138, 154);
            btnRequest.BorderColor = Color.Transparent;
            btnRequest.BorderRadius = 15;
            btnRequest.BorderSize = 0;
            btnRequest.Cursor = Cursors.Hand;
            btnRequest.Dock = DockStyle.Fill;
            btnRequest.FlatAppearance.BorderSize = 0;
            btnRequest.FlatStyle = FlatStyle.Flat;
            btnRequest.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnRequest.ForeColor = Color.White;
            btnRequest.HoverColor = Color.FromArgb(192, 192, 255);
            btnRequest.Location = new Point(318, 503);
            btnRequest.Name = "btnRequest";
            btnRequest.Size = new Size(285, 52);
            btnRequest.TabIndex = 76;
            btnRequest.Text = "Yêu Cầu";
            btnRequest.UseVisualStyleBackColor = false;
            // 
            // btnContract
            // 
            btnContract.BackColor = Color.FromArgb(0, 113, 0);
            btnContract.BaseColor = Color.FromArgb(0, 113, 0);
            btnContract.BorderColor = Color.Transparent;
            btnContract.BorderRadius = 15;
            btnContract.BorderSize = 0;
            btnContract.Cursor = Cursors.Hand;
            btnContract.FlatAppearance.BorderSize = 0;
            btnContract.FlatStyle = FlatStyle.Flat;
            btnContract.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnContract.ForeColor = Color.White;
            btnContract.HoverColor = Color.FromArgb(34, 139, 34);
            btnContract.Location = new Point(27, 26);
            btnContract.Name = "btnContract";
            btnContract.Size = new Size(267, 52);
            btnContract.TabIndex = 74;
            btnContract.Text = "Danh Sách Hợp Đồng";
            btnContract.UseVisualStyleBackColor = false;
            btnContract.Click += btnContract_Click;
            // 
            // ResultContent
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "ResultContent";
            Size = new Size(1215, 584);
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView2).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Components.RoundedDataGridView roundedDataGridView2;
        private Components.RoundedButton lbContractID;
        private Components.RoundedButton btnContract;
        private Components.RoundedButton btnDuyet;
        private TableLayoutPanel tableLayoutPanel1;
        private Components.RoundedButton btnPDF;
        private Components.RoundedButton btnMail;
        private Components.RoundedButton btnRequest;
    }
}
