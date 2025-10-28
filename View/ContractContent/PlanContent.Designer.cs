namespace Environmental_Monitoring.View.ContractContent
{
    partial class PlanContent
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
            btnSearch = new Environmental_Monitoring.View.Components.RoundedButton();
            panel1 = new Panel();
            checkedListBox1 = new CheckedListBox();
            label1 = new Label();
            label2 = new Label();
            btnContracts = new Environmental_Monitoring.View.Components.RoundedButton();
            btnCancel = new Environmental_Monitoring.View.Components.RoundedButton();
            roundedButton2 = new Environmental_Monitoring.View.Components.RoundedButton();
            roundedDataGridView1 = new Environmental_Monitoring.View.Components.RoundedDataGridView();
            lblParamNameValue = new Label();
            lblUnitValue = new Label();
            lblDeptValue = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView1).BeginInit();
            SuspendLayout();
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
            btnSearch.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnSearch.ForeColor = Color.Black;
            btnSearch.HoverColor = Color.FromArgb(34, 139, 34);
            btnSearch.Location = new Point(883, 15);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(341, 50);
            btnSearch.TabIndex = 52;
            btnSearch.Text = "Mã Hợp Đồng: ";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(checkedListBox1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(btnContracts);
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(roundedButton2);
            panel1.Controls.Add(roundedDataGridView1);
            panel1.Controls.Add(btnSearch);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1227, 507);
            panel1.TabIndex = 1;
            panel1.Paint += panel1_Paint;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(38, 141);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(150, 114);
            checkedListBox1.TabIndex = 68;
            checkedListBox1.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged_1;
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(38, 84);
            label1.Name = "label1";
            label1.Size = new Size(235, 25);
            label1.TabIndex = 58;
            label1.Text = "Chọn mẫu môi trường";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(520, 84);
            label2.Name = "label2";
            label2.Size = new Size(189, 25);
            label2.TabIndex = 59;
            label2.Text = "Chọn thông số đo";
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
            btnContracts.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnContracts.ForeColor = Color.White;
            btnContracts.HoverColor = Color.FromArgb(34, 139, 34);
            btnContracts.Location = new Point(29, 15);
            btnContracts.Name = "btnContracts";
            btnContracts.Size = new Size(341, 50);
            btnContracts.TabIndex = 57;
            btnContracts.Text = "Danh Sách Hợp Đồng";
            btnContracts.UseVisualStyleBackColor = false;
            btnContracts.Click += btnContracts_Click;
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
            btnCancel.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Black;
            btnCancel.HoverColor = Color.FromArgb(34, 139, 34);
            btnCancel.Location = new Point(864, 381);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(178, 50);
            btnCancel.TabIndex = 56;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.SeaGreen;
            roundedButton2.BaseColor = Color.SeaGreen;
            roundedButton2.BorderColor = Color.Transparent;
            roundedButton2.BorderRadius = 25;
            roundedButton2.BorderSize = 0;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            roundedButton2.ForeColor = Color.White;
            roundedButton2.HoverColor = Color.FromArgb(34, 139, 34);
            roundedButton2.Location = new Point(572, 381);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(178, 50);
            roundedButton2.TabIndex = 55;
            roundedButton2.Text = "Lưu";
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // roundedDataGridView1
            // 
            roundedDataGridView1.BackgroundColor = Color.White;
            roundedDataGridView1.BorderRadius = 20;
            roundedDataGridView1.BorderStyle = BorderStyle.None;
            roundedDataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            roundedDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            roundedDataGridView1.EnableHeadersVisualStyles = false;
            roundedDataGridView1.Location = new Point(520, 141);
            roundedDataGridView1.Name = "roundedDataGridView1";
            roundedDataGridView1.RowHeadersWidth = 51;
            roundedDataGridView1.Size = new Size(674, 188);
            roundedDataGridView1.TabIndex = 67;
            // 
            // lblParamNameValue
            // 
            lblParamNameValue.AutoSize = true;
            lblParamNameValue.Font = new Font("Times New Roman", 11F);
            lblParamNameValue.Location = new Point(640, 370);
            lblParamNameValue.Name = "lblParamNameValue";
            lblParamNameValue.Size = new Size(0, 21);
            lblParamNameValue.TabIndex = 62;
            // 
            // lblUnitValue
            // 
            lblUnitValue.AutoSize = true;
            lblUnitValue.Font = new Font("Times New Roman", 11F);
            lblUnitValue.Location = new Point(640, 395);
            lblUnitValue.Name = "lblUnitValue";
            lblUnitValue.Size = new Size(0, 21);
            lblUnitValue.TabIndex = 64;
            // 
            // lblDeptValue
            // 
            lblDeptValue.AutoSize = true;
            lblDeptValue.Font = new Font("Times New Roman", 11F);
            lblDeptValue.Location = new Point(640, 420);
            lblDeptValue.Name = "lblDeptValue";
            lblDeptValue.Size = new Size(0, 21);
            lblDeptValue.TabIndex = 66;
            // 
            // PlanContent
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "PlanContent";
            Size = new Size(1227, 507);
            Load += PlanContent_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Components.RoundedButton btnSearch;
        private Panel panel1;
        private Components.RoundedDataGridView roundedDataGridView1;
        private Components.RoundedButton btnCancel;
        private Components.RoundedButton roundedButton2;
        private Components.RoundedButton btnContracts;
        private Label label2;
        private Label label1;
        private ComboBox cbbParameters;
        private Label lblParamTitle;
        private Label lblParamNameValue;
        private Label lblUnitTitle;
        private Label lblUnitValue;
        private Label lblDeptTitle;
        private Label lblDeptValue;
        private CheckedListBox checkedListBox1;
    }
}
