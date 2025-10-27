using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Environmental_Monitoring.View.ContractContent;

namespace Environmental_Monitoring.View
{
    internal class Contract : UserControl
    {
        public Contract()
        {
            InitializeComponent();
            LoadPage(new BusinessContent());
        }

        private void InitializeComponent()
        {
            roundedPanel1 = new Environmental_Monitoring.View.Components.RoundedPanel();
            pnContent = new Environmental_Monitoring.View.Components.RoundedPanel();
            btnResult = new Environmental_Monitoring.View.Components.RoundedButton();
            btnReal = new Environmental_Monitoring.View.Components.RoundedButton();
            btnPlan = new Environmental_Monitoring.View.Components.RoundedButton();
            btnBusiness = new Environmental_Monitoring.View.Components.RoundedButton();
            lbContract = new Label();
            btnExperiment = new Environmental_Monitoring.View.Components.RoundedButton();
            roundedPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // roundedPanel1
            // 
            roundedPanel1.BackColor = Color.Honeydew;
            roundedPanel1.BorderColor = Color.Transparent;
            roundedPanel1.BorderRadius = 20;
            roundedPanel1.BorderSize = 0;
            roundedPanel1.Controls.Add(btnExperiment);
            roundedPanel1.Controls.Add(pnContent);
            roundedPanel1.Controls.Add(btnResult);
            roundedPanel1.Controls.Add(btnReal);
            roundedPanel1.Controls.Add(btnPlan);
            roundedPanel1.Controls.Add(btnBusiness);
            roundedPanel1.Controls.Add(lbContract);
            roundedPanel1.Location = new Point(0, 0);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(1227, 712);
            roundedPanel1.TabIndex = 0;
            // 
            // pnContent
            // 
            pnContent.BackColor = Color.White;
            pnContent.BorderColor = Color.Transparent;
            pnContent.BorderRadius = 20;
            pnContent.BorderSize = 0;
            pnContent.Location = new Point(3, 192);
            pnContent.Name = "pnContent";
            pnContent.Size = new Size(1227, 507);
            pnContent.TabIndex = 31;
            // 
            // btnResult
            // 
            btnResult.BackColor = Color.FromArgb(217, 217, 217);
            btnResult.BaseColor = Color.FromArgb(217, 217, 217);
            btnResult.BorderColor = Color.Transparent;
            btnResult.BorderRadius = 25;
            btnResult.BorderSize = 0;
            btnResult.FlatAppearance.BorderSize = 0;
            btnResult.FlatStyle = FlatStyle.Flat;
            btnResult.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnResult.ForeColor = Color.Black;
            btnResult.HoverColor = Color.FromArgb(34, 139, 34);
            btnResult.Location = new Point(964, 100);
            btnResult.Name = "btnResult";
            btnResult.Padding = new Padding(30, 15, 30, 15);
            btnResult.Size = new Size(225, 60);
            btnResult.TabIndex = 30;
            btnResult.Text = "Kết Quả";
            btnResult.UseVisualStyleBackColor = false;
            btnResult.Click += btnResult_Click;
            // 
            // btnReal
            // 
            btnReal.BackColor = Color.FromArgb(217, 217, 217);
            btnReal.BaseColor = Color.FromArgb(217, 217, 217);
            btnReal.BorderColor = Color.Transparent;
            btnReal.BorderRadius = 25;
            btnReal.BorderSize = 0;
            btnReal.FlatAppearance.BorderSize = 0;
            btnReal.FlatStyle = FlatStyle.Flat;
            btnReal.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReal.ForeColor = Color.Black;
            btnReal.HoverColor = Color.FromArgb(34, 139, 34);
            btnReal.Location = new Point(502, 100);
            btnReal.Name = "btnReal";
            btnReal.Padding = new Padding(30, 15, 30, 15);
            btnReal.Size = new Size(225, 60);
            btnReal.TabIndex = 29;
            btnReal.Text = "Hiện Trường";
            btnReal.UseVisualStyleBackColor = false;
            btnReal.Click += btnReal_Click;
            // 
            // btnPlan
            // 
            btnPlan.BackColor = Color.FromArgb(217, 217, 217);
            btnPlan.BaseColor = Color.FromArgb(217, 217, 217);
            btnPlan.BorderColor = Color.Transparent;
            btnPlan.BorderRadius = 25;
            btnPlan.BorderSize = 0;
            btnPlan.FlatAppearance.BorderSize = 0;
            btnPlan.FlatStyle = FlatStyle.Flat;
            btnPlan.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPlan.ForeColor = Color.Black;
            btnPlan.HoverColor = Color.FromArgb(34, 139, 34);
            btnPlan.Location = new Point(271, 100);
            btnPlan.Name = "btnPlan";
            btnPlan.Padding = new Padding(30, 15, 30, 15);
            btnPlan.Size = new Size(225, 60);
            btnPlan.TabIndex = 28;
            btnPlan.Text = "Kế Hoạch";
            btnPlan.UseVisualStyleBackColor = false;
            btnPlan.Click += btnPlan_Click;
            // 
            // btnBusiness
            // 
            btnBusiness.BackColor = Color.FromArgb(217, 217, 217);
            btnBusiness.BaseColor = Color.FromArgb(217, 217, 217);
            btnBusiness.BorderColor = Color.Transparent;
            btnBusiness.BorderRadius = 25;
            btnBusiness.BorderSize = 0;
            btnBusiness.FlatAppearance.BorderSize = 0;
            btnBusiness.FlatStyle = FlatStyle.Flat;
            btnBusiness.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBusiness.ForeColor = Color.Black;
            btnBusiness.HoverColor = Color.FromArgb(34, 139, 34);
            btnBusiness.Location = new Point(40, 100);
            btnBusiness.Name = "btnBusiness";
            btnBusiness.Padding = new Padding(30, 15, 30, 15);
            btnBusiness.Size = new Size(225, 60);
            btnBusiness.TabIndex = 27;
            btnBusiness.Text = "Kinh Doanh";
            btnBusiness.UseVisualStyleBackColor = false;
            btnBusiness.Click += btnBusiness_Click;
            // 
            // lbContract
            // 
            lbContract.AutoSize = true;
            lbContract.BackColor = Color.Transparent;
            lbContract.Font = new Font("Times New Roman", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lbContract.ForeColor = Color.Black;
            lbContract.Location = new Point(40, 12);
            lbContract.Name = "lbContract";
            lbContract.Size = new Size(488, 53);
            lbContract.TabIndex = 26;
            lbContract.Text = "QUẢN LÍ HỢP ĐỒNG";
            // 
            // btnExperiment
            // 
            btnExperiment.BackColor = Color.FromArgb(217, 217, 217);
            btnExperiment.BaseColor = Color.FromArgb(217, 217, 217);
            btnExperiment.BorderColor = Color.Transparent;
            btnExperiment.BorderRadius = 25;
            btnExperiment.BorderSize = 0;
            btnExperiment.FlatAppearance.BorderSize = 0;
            btnExperiment.FlatStyle = FlatStyle.Flat;
            btnExperiment.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExperiment.ForeColor = Color.Black;
            btnExperiment.HoverColor = Color.FromArgb(34, 139, 34);
            btnExperiment.Location = new Point(733, 100);
            btnExperiment.Name = "btnExperiment";
            btnExperiment.Padding = new Padding(30, 15, 30, 15);
            btnExperiment.Size = new Size(225, 60);
            btnExperiment.TabIndex = 32;
            btnExperiment.Text = "Thí Nghiệm";
            btnExperiment.UseVisualStyleBackColor = false;
            btnExperiment.Click += btnExperiment_Click;
            // 
            // Contract
            // 
            BackColor = Color.Transparent;
            Controls.Add(roundedPanel1);
            Name = "Contract";
            Size = new Size(1227, 715);
            roundedPanel1.ResumeLayout(false);
            roundedPanel1.PerformLayout();
            ResumeLayout(false);

        }

        private void btnBusiness_Click(object sender, EventArgs e)
        {
            LoadPage(new BusinessContent());
        }

        private void LoadPage(UserControl pageToLoad)
        {
            pnContent.Controls.Clear();
            pageToLoad.Dock = DockStyle.Fill;
            pnContent.Controls.Add(pageToLoad);
        }

        private Components.RoundedPanel roundedPanel1;
        private Components.RoundedPanel pnContent;
        private Components.RoundedButton btnResult;
        private Components.RoundedButton btnReal;
        private Components.RoundedButton btnPlan;
        private Components.RoundedButton btnBusiness;
        private Components.RoundedButton btnExperiment;
        private Label lbContract;

        private void btnPlan_Click(object sender, EventArgs e)
        {
            LoadPage(new PlanContent());
        }

        private void btnReal_Click(object sender, EventArgs e)
        {
            LoadPage(new RealContent());
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            LoadPage(new ResultContent());
        }

        private void btnExperiment_Click(object sender, EventArgs e)
        {
            LoadPage(new ExperimentContent());
        }
    }
}
