namespace Environmental_Monitoring.View.Components
{
    partial class NotificationBell
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
            flowPanelNotifications = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // flowPanelNotifications
            // 
            flowPanelNotifications.AutoScroll = true;
            flowPanelNotifications.Dock = DockStyle.Fill;
            flowPanelNotifications.FlowDirection = FlowDirection.TopDown;
            flowPanelNotifications.Location = new Point(0, 0);
            flowPanelNotifications.Name = "flowPanelNotifications";
            flowPanelNotifications.Size = new Size(400, 300);
            flowPanelNotifications.TabIndex = 0;
            flowPanelNotifications.WrapContents = false;
            // 
            // NotificationBell
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 300);
            Controls.Add(flowPanelNotifications);
            FormBorderStyle = FormBorderStyle.None;
            Name = "NotificationBell";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "NotificationBell";
            Load += NotificationForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowPanelNotifications;
    }
}