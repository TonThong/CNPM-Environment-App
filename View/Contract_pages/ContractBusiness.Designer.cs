namespace Environmental_Monitoring.View.Contract_pages
{
    partial class ContractBusiness : Contract
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
            labelnfo = new Label();
            labelCustomer = new Label();
            labelContracts = new Label();
            txbCustomerName = new TextBox();
            txbPhone = new TextBox();
            txbAddress = new TextBox();
            txbOwner = new TextBox();
            btnSave = new Button();
            btnSearch = new Button();
            txbEmployeeID = new TextBox();
            txbDateEnd = new TextBox();
            txbDateStart = new TextBox();
            txbContractID = new TextBox();
            background.SuspendLayout();
            SuspendLayout();
            // 
            // labelBusiness
            // 
            labelBusiness.BackColor = SystemColors.ControlDark;
            // 
            // background
            // 
            background.Controls.Add(btnSearch);
            background.Controls.Add(txbEmployeeID);
            background.Controls.Add(txbDateEnd);
            background.Controls.Add(txbDateStart);
            background.Controls.Add(txbContractID);
            background.Controls.Add(btnSave);
            background.Controls.Add(txbOwner);
            background.Controls.Add(txbAddress);
            background.Controls.Add(txbPhone);
            background.Controls.Add(txbCustomerName);
            background.Controls.Add(labelContracts);
            background.Controls.Add(labelCustomer);
            background.Controls.Add(labelnfo);
            background.Controls.SetChildIndex(labelBusiness, 0);
            background.Controls.SetChildIndex(labelPlan, 0);
            background.Controls.SetChildIndex(labelExperiment, 0);
            background.Controls.SetChildIndex(labelResult, 0);
            background.Controls.SetChildIndex(labelnfo, 0);
            background.Controls.SetChildIndex(labelCustomer, 0);
            background.Controls.SetChildIndex(labelContracts, 0);
            background.Controls.SetChildIndex(txbCustomerName, 0);
            background.Controls.SetChildIndex(txbPhone, 0);
            background.Controls.SetChildIndex(txbAddress, 0);
            background.Controls.SetChildIndex(txbOwner, 0);
            background.Controls.SetChildIndex(btnSave, 0);
            background.Controls.SetChildIndex(txbContractID, 0);
            background.Controls.SetChildIndex(txbDateStart, 0);
            background.Controls.SetChildIndex(txbDateEnd, 0);
            background.Controls.SetChildIndex(txbEmployeeID, 0);
            background.Controls.SetChildIndex(btnSearch, 0);
            // 
            // labelnfo
            // 
            labelnfo.AutoSize = true;
            labelnfo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelnfo.Location = new Point(12, 108);
            labelnfo.Name = "labelnfo";
            labelnfo.Size = new Size(359, 28);
            labelnfo.TabIndex = 6;
            labelnfo.Text = "Thông Tin Khách Hàng và Hợp Đồng";
            labelnfo.Click += label1_Click;
            // 
            // labelCustomer
            // 
            labelCustomer.AutoSize = true;
            labelCustomer.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCustomer.Location = new Point(30, 136);
            labelCustomer.Name = "labelCustomer";
            labelCustomer.Size = new Size(126, 28);
            labelCustomer.TabIndex = 7;
            labelCustomer.Text = "Khách Hàng";
            // 
            // labelContracts
            // 
            labelContracts.AutoSize = true;
            labelContracts.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelContracts.Location = new Point(394, 136);
            labelContracts.Name = "labelContracts";
            labelContracts.Size = new Size(109, 28);
            labelContracts.TabIndex = 8;
            labelContracts.Text = "Hợp Đồng";
            // 
            // txbCustomerName
            // 
            txbCustomerName.Location = new Point(42, 174);
            txbCustomerName.Name = "txbCustomerName";
            txbCustomerName.Size = new Size(203, 27);
            txbCustomerName.TabIndex = 9;
            txbCustomerName.TextChanged += textBox1_TextChanged;
            // 
            // txbPhone
            // 
            txbPhone.Location = new Point(42, 207);
            txbPhone.Name = "txbPhone";
            txbPhone.Size = new Size(203, 27);
            txbPhone.TabIndex = 10;
            // 
            // txbAddress
            // 
            txbAddress.Location = new Point(42, 240);
            txbAddress.Name = "txbAddress";
            txbAddress.Size = new Size(203, 27);
            txbAddress.TabIndex = 11;
            // 
            // txbOwner
            // 
            txbOwner.Location = new Point(42, 273);
            txbOwner.Name = "txbOwner";
            txbOwner.Size = new Size(203, 27);
            txbOwner.TabIndex = 12;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(42, 306);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(203, 29);
            btnSave.TabIndex = 13;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(394, 305);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(203, 29);
            btnSearch.TabIndex = 18;
            btnSearch.Text = "Trình Duyệt";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // txbEmployeeID
            // 
            txbEmployeeID.Location = new Point(394, 272);
            txbEmployeeID.Name = "txbEmployeeID";
            txbEmployeeID.Size = new Size(203, 27);
            txbEmployeeID.TabIndex = 17;
            // 
            // txbDateEnd
            // 
            txbDateEnd.Location = new Point(394, 239);
            txbDateEnd.Name = "txbDateEnd";
            txbDateEnd.Size = new Size(203, 27);
            txbDateEnd.TabIndex = 16;
            // 
            // txbDateStart
            // 
            txbDateStart.Location = new Point(394, 206);
            txbDateStart.Name = "txbDateStart";
            txbDateStart.Size = new Size(203, 27);
            txbDateStart.TabIndex = 15;
            // 
            // txbContractID
            // 
            txbContractID.Location = new Point(394, 173);
            txbContractID.Name = "txbContractID";
            txbContractID.Size = new Size(203, 27);
            txbContractID.TabIndex = 14;
            // 
            // ContractBusiness
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(810, 493);
            Name = "ContractBusiness";
            Text = "ContractBusiness";
            background.ResumeLayout(false);
            background.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelnfo;
        private Label labelContracts;
        private Label labelCustomer;
        private TextBox txbOwner;
        private TextBox txbAddress;
        private TextBox txbPhone;
        private TextBox txbCustomerName;
        private Button btnSave;
        private Button btnSearch;
        private TextBox txbEmployeeID;
        private TextBox txbDateEnd;
        private TextBox txbDateStart;
        private TextBox txbContractID;
    }
}