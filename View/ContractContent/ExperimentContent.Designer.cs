namespace Environmental_Monitoring.View
{
    partial class ExperimentContent
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
            roundedDataGridView2 = new Environmental_Monitoring.View.Components.RoundedDataGridView();
            btnCancel = new Environmental_Monitoring.View.Components.RoundedButton();
            btnSave = new Environmental_Monitoring.View.Components.RoundedButton();
            btnSearch = new Environmental_Monitoring.View.Components.RoundedButton();
            btnContracts = new Environmental_Monitoring.View.Components.RoundedButton();
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView2).BeginInit();
            SuspendLayout();
            // 
            // roundedDataGridView2
            // 
            roundedDataGridView2.BackgroundColor = Color.White;
            roundedDataGridView2.BorderRadius = 20;
            roundedDataGridView2.BorderStyle = BorderStyle.None;
            roundedDataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.None;
            roundedDataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            roundedDataGridView2.EnableHeadersVisualStyles = false;
            roundedDataGridView2.Location = new Point(18, 165);
            roundedDataGridView2.Name = "roundedDataGridView2";
            roundedDataGridView2.RowHeadersWidth = 51;
            roundedDataGridView2.Size = new Size(1192, 188);
            roundedDataGridView2.TabIndex = 70;
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
            btnCancel.Location = new Point(947, 383);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(178, 50);
            btnCancel.TabIndex = 69;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.SeaGreen;
            btnSave.BaseColor = Color.SeaGreen;
            btnSave.BorderColor = Color.Transparent;
            btnSave.BorderRadius = 25;
            btnSave.BorderSize = 0;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.HoverColor = Color.FromArgb(34, 139, 34);
            btnSave.Location = new Point(682, 383);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(178, 50);
            btnSave.TabIndex = 68;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
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
            btnSearch.TabIndex = 67;
            btnSearch.Text = "Mã Hợp Đồng: ";
            btnSearch.UseVisualStyleBackColor = false;
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
            btnContracts.TabIndex = 71;
            btnContracts.Text = "Danh Sách Hợp Đồng";
            btnContracts.UseVisualStyleBackColor = false;
            btnContracts.Click += btnContracts_Click;
            // 
            // ExperimentContent
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnContracts);
            Controls.Add(roundedDataGridView2);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(btnSearch);
            Name = "ExperimentContent";
            Size = new Size(1227, 507);
            Load += ExperimentContent_Load_1;
            ((System.ComponentModel.ISupportInitialize)roundedDataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Components.RoundedDataGridView roundedDataGridView2;
        private Components.RoundedButton btnCancel;
        private Components.RoundedButton btnSave;
        private Components.RoundedButton btnSearch;
        private Components.RoundedButton btnContracts;
    }
}
