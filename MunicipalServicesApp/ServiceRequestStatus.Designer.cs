namespace MunicipalServicesApp
{
    partial class ServiceRequestStatus
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
            this.dgvRequests = new System.Windows.Forms.DataGridView();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblStatusFilter = new System.Windows.Forms.Label();
            this.cmbStatusFilter = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cmbNewStatus = new System.Windows.Forms.ComboBox();
            this.bbtnUpdateStatus = new System.Windows.Forms.Button();
            this.btnOldestRequest = new System.Windows.Forms.Button();
            this.btnGraphAnalysis = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRequests
            // 
            this.dgvRequests.AllowUserToAddRows = false;
            this.dgvRequests.AllowUserToDeleteRows = false;
            this.dgvRequests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRequests.Location = new System.Drawing.Point(19, 233);
            this.dgvRequests.MultiSelect = false;
            this.dgvRequests.Name = "dgvRequests";
            this.dgvRequests.ReadOnly = true;
            this.dgvRequests.RowHeadersVisible = false;
            this.dgvRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRequests.Size = new System.Drawing.Size(953, 248);
            this.dgvRequests.TabIndex = 0;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(16, 62);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(87, 13);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Search Request:\r\n";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(117, 55);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(257, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblStatusFilter
            // 
            this.lblStatusFilter.AutoSize = true;
            this.lblStatusFilter.Location = new System.Drawing.Point(16, 116);
            this.lblStatusFilter.Name = "lblStatusFilter";
            this.lblStatusFilter.Size = new System.Drawing.Size(79, 26);
            this.lblStatusFilter.TabIndex = 3;
            this.lblStatusFilter.Text = "Filter by Status:\r\n\r\n";
            // 
            // cmbStatusFilter
            // 
            this.cmbStatusFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusFilter.FormattingEnabled = true;
            this.cmbStatusFilter.Items.AddRange(new object[] {
            "All",
            "Submitted",
            "In Progress",
            "Resolved",
            "Rejected"});
            this.cmbStatusFilter.Location = new System.Drawing.Point(117, 113);
            this.cmbStatusFilter.Name = "cmbStatusFilter";
            this.cmbStatusFilter.Size = new System.Drawing.Size(257, 21);
            this.cmbStatusFilter.TabIndex = 4;
            this.cmbStatusFilter.SelectedIndexChanged += new System.EventHandler(this.cmbStatusFilter_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(19, 504);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh Requests";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.Location = new System.Drawing.Point(163, 504);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(75, 23);
            this.btnViewDetails.TabIndex = 6;
            this.btnViewDetails.Text = "View Details";
            this.btnViewDetails.UseVisualStyleBackColor = true;
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(857, 504);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(101, 23);
            this.btnBack.TabIndex = 7;
            this.btnBack.Text = "Back to Menu";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(408, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(233, 30);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "Service Request Status\r\n";
            // 
            // cmbNewStatus
            // 
            this.cmbNewStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNewStatus.FormattingEnabled = true;
            this.cmbNewStatus.Items.AddRange(new object[] {
            "Submitted",
            "In Progress",
            "Resolved",
            "Rejected"});
            this.cmbNewStatus.Location = new System.Drawing.Point(117, 158);
            this.cmbNewStatus.Name = "cmbNewStatus";
            this.cmbNewStatus.Size = new System.Drawing.Size(257, 21);
            this.cmbNewStatus.TabIndex = 9;
            // 
            // bbtnUpdateStatus
            // 
            this.bbtnUpdateStatus.Location = new System.Drawing.Point(19, 158);
            this.bbtnUpdateStatus.Name = "bbtnUpdateStatus";
            this.bbtnUpdateStatus.Size = new System.Drawing.Size(75, 23);
            this.bbtnUpdateStatus.TabIndex = 10;
            this.bbtnUpdateStatus.Text = "Update Status";
            this.bbtnUpdateStatus.UseVisualStyleBackColor = true;
            this.bbtnUpdateStatus.Click += new System.EventHandler(this.bbtnUpdateStatus_Click);
            // 
            // btnOldestRequest
            // 
            this.btnOldestRequest.Location = new System.Drawing.Point(323, 504);
            this.btnOldestRequest.Name = "btnOldestRequest";
            this.btnOldestRequest.Size = new System.Drawing.Size(75, 23);
            this.btnOldestRequest.TabIndex = 11;
            this.btnOldestRequest.Text = "\tView Oldest Request";
            this.btnOldestRequest.UseVisualStyleBackColor = true;
            this.btnOldestRequest.Click += new System.EventHandler(this.btnOldestRequest_Click);
            // 
            // btnGraphAnalysis
            // 
            this.btnGraphAnalysis.Location = new System.Drawing.Point(469, 504);
            this.btnGraphAnalysis.Name = "btnGraphAnalysis";
            this.btnGraphAnalysis.Size = new System.Drawing.Size(75, 23);
            this.btnGraphAnalysis.TabIndex = 12;
            this.btnGraphAnalysis.Text = "View Graph Analysis";
            this.btnGraphAnalysis.UseVisualStyleBackColor = true;
            this.btnGraphAnalysis.Click += new System.EventHandler(this.btnGraphAnalysis_Click);
            // 
            // ServiceRequestStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.btnGraphAnalysis);
            this.Controls.Add(this.btnOldestRequest);
            this.Controls.Add(this.bbtnUpdateStatus);
            this.Controls.Add(this.cmbNewStatus);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnViewDetails);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.cmbStatusFilter);
            this.Controls.Add(this.lblStatusFilter);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.dgvRequests);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ServiceRequestStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Municipal Service Request Status";
            this.Load += new System.EventHandler(this.ServiceRequestStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRequests;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblStatusFilter;
        private System.Windows.Forms.ComboBox cmbStatusFilter;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox cmbNewStatus;
        private System.Windows.Forms.Button bbtnUpdateStatus;
        private System.Windows.Forms.Button btnOldestRequest;
        private System.Windows.Forms.Button btnGraphAnalysis;
    }
}