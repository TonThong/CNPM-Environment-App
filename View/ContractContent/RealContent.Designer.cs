namespace Environmental_Monitoring.View.ContractContent
{
    partial class RealContent
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
            btnCancel = new Environmental_Monitoring.View.Components.RoundedButton();
            btnMail = new Environmental_Monitoring.View.Components.RoundedButton();
            btnSearch = new Environmental_Monitoring.View.Components.RoundedButton();
            roundedDataGridView2 = new Environmental_Monitoring.View.Components.RoundedDataGridView();
            btnContracts = new Environmental_Monitoring.View.Components.RoundedButton();
            btnCreateTemplate = new Environmental_Monitoring.View.Components.RoundedButton();
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView2).BeginInit();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(217, 217, 217);
            btnCancel.BaseColor = Color.FromArgb(217, 217, 217);
            btnCancel.BorderColor = Color.Transparent;
            btnCancel.BorderRadius = 25;
            btnCancel.BorderSize = 0;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Black;
            btnCancel.HoverColor = Color.FromArgb(34, 139, 34);
            btnCancel.Location = new Point(1022, 355);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(178, 50);
            btnCancel.TabIndex = 64;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnMail
            // 
            btnMail.BackColor = Color.SeaGreen;
            btnMail.BaseColor = Color.SeaGreen;
            btnMail.BorderColor = Color.Transparent;
            btnMail.BorderRadius = 25;
            btnMail.BorderSize = 0;
            btnMail.FlatAppearance.BorderSize = 0;
            btnMail.FlatStyle = FlatStyle.Flat;
            btnMail.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnMail.ForeColor = Color.White;
            btnMail.HoverColor = Color.FromArgb(34, 139, 34);
            btnMail.Location = new Point(682, 355);
            btnMail.Name = "btnMail";
            btnMail.Size = new Size(178, 50);
            btnMail.TabIndex = 63;
            btnMail.Text = "Lưu";
            btnMail.UseVisualStyleBackColor = false;
            btnMail.Click += btnMail_Click_1;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(217, 217, 217);
            btnSearch.BaseColor = Color.FromArgb(217, 217, 217);
            btnSearch.BorderColor = Color.Transparent;
            btnSearch.BorderRadius = 25;
            btnSearch.BorderSize = 0;
            btnSearch.Enabled = false;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSearch.ForeColor = Color.Black;
            btnSearch.HoverColor = Color.FromArgb(34, 139, 34);
            btnSearch.Location = new Point(883, 45);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(341, 50);
            btnSearch.TabIndex = 60;
            btnSearch.Text = "Mã Hợp Đồng: ";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // roundedDataGridView2
            // 
            roundedDataGridView2.BackgroundColor = Color.White;
            roundedDataGridView2.BorderRadius = 20;
            roundedDataGridView2.BorderStyle = BorderStyle.None;
            roundedDataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.None;
            roundedDataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            roundedDataGridView2.EnableHeadersVisualStyles = false;
            roundedDataGridView2.Location = new Point(18, 137);
            roundedDataGridView2.Name = "roundedDataGridView2";
            roundedDataGridView2.RowHeadersWidth = 51;
            roundedDataGridView2.Size = new Size(1192, 188);
            roundedDataGridView2.TabIndex = 65;
            // 
            // btnContracts
            // 
            btnContracts.BackColor = Color.SeaGreen;
            btnContracts.BaseColor = Color.SeaGreen;
            btnContracts.BorderColor = Color.Transparent;
            btnContracts.BorderRadius = 25;
            btnContracts.BorderSize = 0;
            btnContracts.FlatAppearance.BorderSize = 0;
            btnContracts.FlatStyle = FlatStyle.Flat;
            btnContracts.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnContracts.ForeColor = Color.White;
            btnContracts.HoverColor = Color.FromArgb(34, 139, 34);
            btnContracts.Location = new Point(18, 45);
            btnContracts.Name = "btnContracts";
            btnContracts.Size = new Size(341, 50);
            btnContracts.TabIndex = 66;
            btnContracts.Text = "Danh Sách Hợp Đồng";
            btnContracts.UseVisualStyleBackColor = false;
            btnContracts.Click += btnContracts_Click;
            // 
            // btnCreateTemplate
            // 
            btnCreateTemplate.BackColor = Color.SteelBlue;
            btnCreateTemplate.BaseColor = Color.SteelBlue;
            btnCreateTemplate.BorderColor = Color.Transparent;
            btnCreateTemplate.BorderRadius = 25;
            btnCreateTemplate.BorderSize = 0;
            btnCreateTemplate.FlatAppearance.BorderSize = 0;
            btnCreateTemplate.FlatStyle = FlatStyle.Flat;
            btnCreateTemplate.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCreateTemplate.ForeColor = Color.White;
            btnCreateTemplate.HoverColor = Color.DodgerBlue;
            btnCreateTemplate.Location = new Point(866, 355);
            btnCreateTemplate.Name = "btnCreateTemplate";
            btnCreateTemplate.Size = new Size(150, 50);
            btnCreateTemplate.TabIndex = 67;
            btnCreateTemplate.Text = "Tạo mẫu";
            btnCreateTemplate.UseVisualStyleBackColor = false;
            btnCreateTemplate.Click += btnCreateTemplate_Click;
            // 
            // RealContent
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnCreateTemplate);
            Controls.Add(btnContracts);
            Controls.Add(roundedDataGridView2);
            Controls.Add(btnCancel);
            Controls.Add(btnMail);
            Controls.Add(btnSearch);
            Name = "RealContent";
            Size = new Size(1227, 507);
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Components.RoundedButton btnCancel;
        private Components.RoundedButton btnMail;
        private Components.RoundedButton btnSearch;
        private Components.RoundedDataGridView roundedDataGridView2;
        private Components.RoundedButton btnContracts;
        private Components.RoundedButton btnCreateTemplate;
    }
}
