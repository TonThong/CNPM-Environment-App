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
            lbCustomer = new Label();
            lbContract = new Label();
            btnSave = new Environmental_Monitoring.View.Components.RoundedButton();
            btnSearch = new Environmental_Monitoring.View.Components.RoundedButton();
            checkedListBox1 = new CheckedListBox();
            panel1 = new Panel();
            roundedDataGridView1 = new Environmental_Monitoring.View.Components.RoundedDataGridView();
            btnCancel = new Environmental_Monitoring.View.Components.RoundedButton();
            roundedButton2 = new Environmental_Monitoring.View.Components.RoundedButton();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lbCustomer
            // 
            lbCustomer.AutoSize = true;
            lbCustomer.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbCustomer.Location = new Point(29, 96);
            lbCustomer.Name = "lbCustomer";
            lbCustomer.Size = new Size(235, 25);
            lbCustomer.TabIndex = 41;
            lbCustomer.Text = "Chọn mẫu môi trường";
            lbCustomer.Click += lbCustomer_Click;
            // 
            // lbContract
            // 
            lbContract.AutoSize = true;
            lbContract.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbContract.Location = new Point(520, 96);
            lbContract.Name = "lbContract";
            lbContract.Size = new Size(189, 25);
            lbContract.TabIndex = 42;
            lbContract.Text = "Chọn thông số đo";
            lbContract.Click += lbContract_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.SeaGreen;
            btnSave.BaseColor = Color.SeaGreen;
            btnSave.BorderColor = Color.Transparent;
            btnSave.BorderRadius = 25;
            btnSave.BorderSize = 0;
            btnSave.Enabled = false;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.HoverColor = Color.FromArgb(34, 139, 34);
            btnSave.Location = new Point(3, 15);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(341, 50);
            btnSave.TabIndex = 51;
            btnSave.Text = "Danh Sách Hợp Đồng";
            btnSave.UseVisualStyleBackColor = false;
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
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(38, 141);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(150, 136);
            checkedListBox1.TabIndex = 53;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(roundedButton2);
            panel1.Controls.Add(roundedDataGridView1);
            panel1.Controls.Add(checkedListBox1);
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(lbContract);
            panel1.Controls.Add(lbCustomer);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1227, 507);
            panel1.TabIndex = 1;
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
            roundedDataGridView1.Size = new Size(591, 188);
            roundedDataGridView1.TabIndex = 54;
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
            // PlanContent
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "PlanContent";
            Size = new Size(1227, 507);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lbCustomer;
        private Label lbContract;
        private Components.RoundedButton btnSave;
        private Components.RoundedButton btnSearch;
        private CheckedListBox checkedListBox1;
        private Panel panel1;
        private Components.RoundedDataGridView roundedDataGridView1;
        private Components.RoundedButton btnCancel;
        private Components.RoundedButton roundedButton2;
    }
}
