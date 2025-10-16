namespace Environmental_Monitoring.View.Contract_pages
{
    partial class ContractResult
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelContractID = new Label();
            labelListContract = new Label();
            btnExportPDF = new Button();
            btnApprove = new Button();
            btnRequestEdit = new Button();
            btnMail = new Button();
            background.SuspendLayout();
            SuspendLayout();
            // 
            // labelResult
            // 
            labelResult.BackColor = SystemColors.ControlDark;
            // 
            // background
            // 
            background.Controls.Add(btnRequestEdit);
            background.Controls.Add(btnMail);
            background.Controls.Add(btnExportPDF);
            background.Controls.Add(btnApprove);
            background.Controls.Add(labelContractID);
            background.Controls.Add(labelListContract);
            background.Controls.SetChildIndex(labelBusiness, 0);
            background.Controls.SetChildIndex(labelPlan, 0);
            background.Controls.SetChildIndex(labelExperiment, 0);
            background.Controls.SetChildIndex(labelResult, 0);
            background.Controls.SetChildIndex(labelListContract, 0);
            background.Controls.SetChildIndex(labelContractID, 0);
            background.Controls.SetChildIndex(btnApprove, 0);
            background.Controls.SetChildIndex(btnExportPDF, 0);
            background.Controls.SetChildIndex(btnMail, 0);
            background.Controls.SetChildIndex(btnRequestEdit, 0);
            // 
            // labelContractID
            // 
            labelContractID.AutoSize = true;
            labelContractID.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelContractID.Location = new Point(463, 119);
            labelContractID.Name = "labelContractID";
            labelContractID.Size = new Size(165, 31);
            labelContractID.TabIndex = 19;
            labelContractID.Text = "Mã Hợp Đồng";
            // 
            // labelListContract
            // 
            labelListContract.AutoSize = true;
            labelListContract.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelListContract.Location = new Point(3, 119);
            labelListContract.Name = "labelListContract";
            labelListContract.Size = new Size(244, 31);
            labelListContract.TabIndex = 18;
            labelListContract.Text = "Danh Sách Hợp Đồng";
            // 
            // btnExportPDF
            // 
            btnExportPDF.Location = new Point(201, 299);
            btnExportPDF.Name = "btnExportPDF";
            btnExportPDF.Size = new Size(123, 42);
            btnExportPDF.TabIndex = 21;
            btnExportPDF.Text = "Xuất PDF";
            btnExportPDF.UseVisualStyleBackColor = true;
            // 
            // btnApprove
            // 
            btnApprove.Location = new Point(30, 299);
            btnApprove.Name = "btnApprove";
            btnApprove.Size = new Size(123, 42);
            btnApprove.TabIndex = 20;
            btnApprove.Text = "Phê Duyệt";
            btnApprove.UseVisualStyleBackColor = true;
            btnApprove.Click += btnSave_Click;
            // 
            // btnRequestEdit
            // 
            btnRequestEdit.Location = new Point(554, 299);
            btnRequestEdit.Name = "btnRequestEdit";
            btnRequestEdit.Size = new Size(123, 42);
            btnRequestEdit.TabIndex = 23;
            btnRequestEdit.Text = "Yêu Cầu Chỉnh Sửa";
            btnRequestEdit.UseVisualStyleBackColor = true;
            // 
            // btnMail
            // 
            btnMail.Location = new Point(394, 299);
            btnMail.Name = "btnMail";
            btnMail.Size = new Size(123, 42);
            btnMail.TabIndex = 22;
            btnMail.Text = "Gửi Mail";
            btnMail.UseVisualStyleBackColor = true;
            // 
            // ContractResult
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "ContractResult";
            Text = "ContractResult";
            background.ResumeLayout(false);
            background.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelContractID;
        private Label labelListContract;
        private Button btnRequestEdit;
        private Button btnMail;
        private Button btnExportPDF;
        private Button btnApprove;
    }
}