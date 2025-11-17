namespace Environmental_Monitoring.View.Components
{
    partial class NotificationItem
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
            lblContent = new Label();
            lblTime = new Label();
            SuspendLayout();
            // 
            // lblContent
            // 
            lblContent.AutoSize = true;
            lblContent.Dock = DockStyle.Top;
            lblContent.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            lblContent.ForeColor = Color.Black;
            lblContent.Location = new Point(0, 0);
            lblContent.Name = "lblContent";
            lblContent.Padding = new Padding(5, 5, 5, 0);
            lblContent.Size = new Size(65, 28);
            lblContent.TabIndex = 0;
            lblContent.Text = "label1";
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Dock = DockStyle.Top;
            lblTime.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 163);
            lblTime.Location = new Point(0, 28);
            lblTime.Name = "lblTime";
            lblTime.Padding = new Padding(5, 0, 5, 5);
            lblTime.Size = new Size(53, 22);
            lblTime.TabIndex = 1;
            lblTime.Text = "label1";
            // 
            // NotificationItem
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblTime);
            Controls.Add(lblContent);
            ForeColor = Color.DimGray;
            Name = "NotificationItem";
            Size = new Size(290, 90);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblContent;
        private Label lblTime;
    }
}
