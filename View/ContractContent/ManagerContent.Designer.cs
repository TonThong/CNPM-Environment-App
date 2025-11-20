namespace Environmental_Monitoring.View.ContractContent
{
    partial class ManagerContent
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
            dgvManager = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvManager).BeginInit();
            SuspendLayout();
            // 
            // dgvManager
            // 
            dgvManager.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvManager.Dock = DockStyle.Fill;
            dgvManager.Location = new Point(0, 0);
            dgvManager.Name = "dgvManager";
            dgvManager.RowHeadersWidth = 51;
            dgvManager.Size = new Size(1215, 521);
            dgvManager.TabIndex = 0;
            // 
            // ManagerContent
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvManager);
            Name = "ManagerContent";
            Size = new Size(1215, 521);
            ((System.ComponentModel.ISupportInitialize)dgvManager).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvManager;
    }
}
