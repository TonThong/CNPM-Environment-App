namespace Environmental_Monitoring.View
{
    partial class Contract : Mainlayout
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
            labelContract = new Label();
            labelBusiness = new Label();
            labelPlan = new Label();
            labelExperiment = new Label();
            labelResult = new Label();
            background.SuspendLayout();
            SuspendLayout();
            // 
            // background
            // 
            background.Controls.Add(labelResult);
            background.Controls.Add(labelExperiment);
            background.Controls.Add(labelPlan);
            background.Controls.Add(labelBusiness);
            background.Controls.Add(labelContract);
            background.Controls.SetChildIndex(labelContract, 0);
            background.Controls.SetChildIndex(labelBusiness, 0);
            background.Controls.SetChildIndex(labelPlan, 0);
            background.Controls.SetChildIndex(labelExperiment, 0);
            background.Controls.SetChildIndex(labelResult, 0);
            // 
            // labelContract
            // 
            labelContract.AutoSize = true;
            labelContract.Font = new Font("Segoe UI Historic", 22.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelContract.Location = new Point(3, 5);
            labelContract.Name = "labelContract";
            labelContract.Size = new Size(342, 50);
            labelContract.TabIndex = 1;
            labelContract.Text = "Quản Lí Hợp Đồng";
            // 
            // labelBusiness
            // 
            labelBusiness.AutoSize = true;
            labelBusiness.Font = new Font("Segoe UI Emoji", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelBusiness.Location = new Point(30, 74);
            labelBusiness.Name = "labelBusiness";
            labelBusiness.Size = new Size(124, 27);
            labelBusiness.TabIndex = 2;
            labelBusiness.Text = "Kinh Doanh";
            labelBusiness.Click += labelBusiness_Click;
            // 
            // labelPlan
            // 
            labelPlan.AutoSize = true;
            labelPlan.Font = new Font("Segoe UI Emoji", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPlan.Location = new Point(219, 74);
            labelPlan.Name = "labelPlan";
            labelPlan.Size = new Size(105, 27);
            labelPlan.TabIndex = 3;
            labelPlan.Text = "Kế Hoạch";
            // 
            // labelExperiment
            // 
            labelExperiment.AutoSize = true;
            labelExperiment.Font = new Font("Segoe UI Emoji", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelExperiment.Location = new Point(394, 74);
            labelExperiment.Name = "labelExperiment";
            labelExperiment.Size = new Size(86, 27);
            labelExperiment.TabIndex = 4;
            labelExperiment.Text = "HT/PTN";
            // 
            // labelResult
            // 
            labelResult.AutoSize = true;
            labelResult.Font = new Font("Segoe UI Emoji", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelResult.Location = new Point(553, 74);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(91, 27);
            labelResult.TabIndex = 5;
            labelResult.Text = "Kết Quả";
            // 
            // Contract
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(831, 498);
            Name = "Contract";
            Text = "Contract_Business";
            background.ResumeLayout(false);
            background.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label labelContract;
        protected Label labelResult;
        protected Label labelExperiment;
        protected Label labelPlan;
        protected Label labelBusiness;
    }
}