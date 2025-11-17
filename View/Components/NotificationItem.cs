using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.Components
{
    public partial class NotificationItem : UserControl
    {
        public NotificationItem()
        {
            InitializeComponent();
        }

        public void SetData(string content, string time)
        {
            lblContent.Text = content;
            lblTime.Text = time;
        }

        public void SetContentWidth(int width)
        {
            lblContent.MaximumSize = new Size(width - 10, 0);
        }
    }
}
