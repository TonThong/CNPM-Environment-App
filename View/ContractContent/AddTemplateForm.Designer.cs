namespace Environmental_Monitoring.View.ContractContent
{
    partial class AddTemplateForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtTemplateName;
        private System.Windows.Forms.CheckedListBox lstParameters;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;

        private void InitializeComponent()
        {
            txtTemplateName = new TextBox();
            lstParameters = new CheckedListBox();
            btnOK = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // txtTemplateName
            // 
            txtTemplateName.Location = new Point(20, 20);
            txtTemplateName.Name = "txtTemplateName";
            txtTemplateName.Size = new Size(360, 25);
            txtTemplateName.TabIndex = 0;
            // 
            // lstParameters
            // 
            lstParameters.Location = new Point(20, 60);
            lstParameters.Name = "lstParameters";
            lstParameters.Size = new Size(360, 204);
            lstParameters.TabIndex = 1;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(80, 300);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(100, 35);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(200, 300);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 35);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "H?y";
            btnCancel.Click += btnCancel_Click;
            // 
            // AddTemplateForm
            // 
            ClientSize = new Size(400, 360);
            Controls.Add(txtTemplateName);
            Controls.Add(lstParameters);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "AddTemplateForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tạo Mẫu";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}