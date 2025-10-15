namespace Environmental_Monitoring.View.Contract_pages
{
    partial class ContractPlan
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
            labelListContract = new Label();
            labelContractID = new Label();
            labelEnv = new Label();
            labelPara = new Label();
            listEnv = new CheckedListBox();
            btnSave = new Button();
            btnCancel = new Button();
            background.SuspendLayout();
            SuspendLayout();
            // 
            // labelPlan
            // 
            labelPlan.BackColor = SystemColors.ControlDark;
            // 
            // background
            // 
            background.Controls.Add(btnCancel);
            background.Controls.Add(btnSave);
            background.Controls.Add(listEnv);
            background.Controls.Add(labelPara);
            background.Controls.Add(labelEnv);
            background.Controls.Add(labelContractID);
            background.Controls.Add(labelListContract);
            background.Controls.SetChildIndex(labelBusiness, 0);
            background.Controls.SetChildIndex(labelPlan, 0);
            background.Controls.SetChildIndex(labelExperiment, 0);
            background.Controls.SetChildIndex(labelResult, 0);
            background.Controls.SetChildIndex(labelListContract, 0);
            background.Controls.SetChildIndex(labelContractID, 0);
            background.Controls.SetChildIndex(labelEnv, 0);
            background.Controls.SetChildIndex(labelPara, 0);
            background.Controls.SetChildIndex(listEnv, 0);
            background.Controls.SetChildIndex(btnSave, 0);
            background.Controls.SetChildIndex(btnCancel, 0);
            // 
            // labelListContract
            // 
            labelListContract.AutoSize = true;
            labelListContract.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelListContract.Location = new Point(3, 125);
            labelListContract.Name = "labelListContract";
            labelListContract.Size = new Size(244, 31);
            labelListContract.TabIndex = 6;
            labelListContract.Text = "Danh Sách Hợp Đồng";
            // 
            // labelContractID
            // 
            labelContractID.AutoSize = true;
            labelContractID.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelContractID.Location = new Point(463, 122);
            labelContractID.Name = "labelContractID";
            labelContractID.Size = new Size(165, 31);
            labelContractID.TabIndex = 8;
            labelContractID.Text = "Mã Hợp Đồng";
            // 
            // labelEnv
            // 
            labelEnv.AutoSize = true;
            labelEnv.Location = new Point(14, 156);
            labelEnv.Name = "labelEnv";
            labelEnv.Size = new Size(157, 20);
            labelEnv.TabIndex = 9;
            labelEnv.Text = "Chọn Mẫu Môi Trường";
            // 
            // labelPara
            // 
            labelPara.AutoSize = true;
            labelPara.Location = new Point(258, 156);
            labelPara.Name = "labelPara";
            labelPara.Size = new Size(167, 20);
            labelPara.TabIndex = 10;
            labelPara.Text = "Chọn Mẫu Thông Số Đo";
            // 
            // listEnv
            // 
            listEnv.BackColor = SystemColors.Menu;
            listEnv.FormattingEnabled = true;
            listEnv.Items.AddRange(new object[] { "Mẫu Đất", "Mẫu Nước", "Mẫu Khí Thải", "Mẫu Không Khí" });
            listEnv.Location = new Point(21, 185);
            listEnv.Name = "listEnv";
            listEnv.Size = new Size(150, 114);
            listEnv.TabIndex = 11;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(463, 307);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 12;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(583, 307);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // ContractPlan
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "ContractPlan";
            Text = "ContractPlan";
            background.ResumeLayout(false);
            background.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private Label labelListContract;
        private Label labelContractID;
        private Label labelPara;
        private Label labelEnv;
        private CheckedListBox listEnv;
        private Button btnCancel;
        private Button btnSave;
    }
}