namespace Environmental_Monitoring.View.Components
{
    partial class AlertPanel
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
            components = new System.ComponentModel.Container();
            panelBody = new Panel();
            lblMessage = new Label();
            picIcon = new PictureBox();
            timerClose = new System.Windows.Forms.Timer(components);
            panelBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picIcon).BeginInit();
            SuspendLayout();
            // 
            // panelBody
            // 
            panelBody.Controls.Add(lblMessage);
            panelBody.Controls.Add(picIcon);
            panelBody.Dock = DockStyle.Fill;
            panelBody.Location = new Point(0, 0);
            panelBody.Name = "panelBody";
            panelBody.Size = new Size(400, 100);
            panelBody.TabIndex = 1;
            // 
            // lblMessage
            // 
            lblMessage.Location = new Point(49, 15);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(298, 71);
            lblMessage.TabIndex = 1;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // picIcon
            // 
            picIcon.BackgroundImageLayout = ImageLayout.Stretch;
            picIcon.Image = Properties.Resources.success;
            picIcon.Location = new Point(8, 30);
            picIcon.Name = "picIcon";
            picIcon.Size = new Size(40, 40);
            picIcon.SizeMode = PictureBoxSizeMode.Zoom;
            picIcon.TabIndex = 0;
            picIcon.TabStop = false;
            // 
            // timerClose
            // 
            timerClose.Interval = 2000;
            // 
            // AlertPanel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelBody);
            Name = "AlertPanel";
            Size = new Size(400, 100);
            panelBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelBody;
        private Label lblMessage;
        private PictureBox picIcon;
        private System.Windows.Forms.Timer timerClose;
    }
}
