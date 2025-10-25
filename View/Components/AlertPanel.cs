using System;
using System.Drawing;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.Components 
{
    public partial class AlertPanel : UserControl
    {
        public enum AlertType
        {
            Success,
            Error,
        }

        public event EventHandler OnClose;

        public AlertPanel()
        {
            InitializeComponent();
      
            this.Visible = false;
          
            this.timerClose.Tick += TimerClose_Tick;
        }

        public void ShowAlert(string message, AlertType type)
        {
            this.Visible = false; 
            lblMessage.Text = message;

            switch (type)
            {
                case AlertType.Success:
                    panelBody.BackColor = Color.FromArgb(223, 240, 216); 
                    lblMessage.ForeColor = Color.FromArgb(60, 118, 61); 
                    picIcon.Image = Properties.Resources.success; 
                    break;
                case AlertType.Error:
                    panelBody.BackColor = Color.FromArgb(242, 222, 222); 
                    lblMessage.ForeColor = Color.FromArgb(169, 68, 66); 
                    picIcon.Image = Properties.Resources.error; 
                    break;
            }

            if (this.Parent != null)
            {
                this.Left = this.Parent.Width - this.Width - 30;
                this.Top = 10; 
            }

            this.Visible = true;
            this.BringToFront();

            timerClose.Start();
        }

        private void TimerClose_Tick(object sender, EventArgs e)
        {
            timerClose.Stop();
            this.Visible = false;

            OnClose?.Invoke(this, EventArgs.Empty);
        }
    }
}