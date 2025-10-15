namespace Environmental_Monitoring.View.Contract_pages
{
    partial class ContractExperiment
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
            btnCancel = new Button();
            btnSave = new Button();
            labelContractID = new Label();
            labelListContract = new Label();
            background.SuspendLayout();
            SuspendLayout();
            // 
            // labelExperiment
            // 
            labelExperiment.BackColor = SystemColors.ButtonShadow;
            // 
            // background
            // 
            background.Controls.Add(labelContractID);
            background.Controls.Add(labelListContract);
            background.Controls.Add(btnCancel);
            background.Controls.Add(btnSave);
            background.Controls.SetChildIndex(labelBusiness, 0);
            background.Controls.SetChildIndex(labelPlan, 0);
            background.Controls.SetChildIndex(labelExperiment, 0);
            background.Controls.SetChildIndex(labelResult, 0);
            background.Controls.SetChildIndex(btnSave, 0);
            background.Controls.SetChildIndex(btnCancel, 0);
            background.Controls.SetChildIndex(labelListContract, 0);
            background.Controls.SetChildIndex(labelContractID, 0);
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(583, 299);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(463, 299);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 14;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // labelContractID
            // 
            labelContractID.AutoSize = true;
            labelContractID.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelContractID.Location = new Point(463, 122);
            labelContractID.Name = "labelContractID";
            labelContractID.Size = new Size(165, 31);
            labelContractID.TabIndex = 17;
            labelContractID.Text = "Mã Hợp Đồng";
            // 
            // labelListContract
            // 
            labelListContract.AutoSize = true;
            labelListContract.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelListContract.Location = new Point(3, 122);
            labelListContract.Name = "labelListContract";
            labelListContract.Size = new Size(244, 31);
            labelListContract.TabIndex = 16;
            labelListContract.Text = "Danh Sách Hợp Đồng";
            // 
            // ContractExperiment
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "ContractExperiment";
            Text = "ContractExperiment";
            background.ResumeLayout(false);
            background.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private Button btnSave;
        private Label labelContractID;
        private Label labelListContract;
    }
}