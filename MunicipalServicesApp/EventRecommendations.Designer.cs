namespace MunicipalServicesApp
{
    partial class EventRecommendations
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lvRecommendations = new System.Windows.Forms.ListView();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblExplanation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(23, 27);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(155, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Recommended Events For You";
            // 
            // lvRecommendations
            // 
            this.lvRecommendations.HideSelection = false;
            this.lvRecommendations.Location = new System.Drawing.Point(12, 43);
            this.lvRecommendations.Name = "lvRecommendations";
            this.lvRecommendations.Size = new System.Drawing.Size(776, 292);
            this.lvRecommendations.TabIndex = 1;
            this.lvRecommendations.UseCompatibleStateImageBehavior = false;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(361, 415);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(92, 23);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Back to Events";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // lblExplanation
            // 
            this.lblExplanation.AutoSize = true;
            this.lblExplanation.Location = new System.Drawing.Point(254, 27);
            this.lblExplanation.Name = "lblExplanation";
            this.lblExplanation.Size = new System.Drawing.Size(359, 13);
            this.lblExplanation.TabIndex = 3;
            this.lblExplanation.Text = "Recommendations are based on your search activity and upcoming events";
            // 
            // EventRecommendations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 450);
            this.Controls.Add(this.lblExplanation);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lvRecommendations);
            this.Controls.Add(this.lblTitle);
            this.Name = "EventRecommendations";
            this.Text = "Event Recommendations";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListView lvRecommendations;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblExplanation;
    }
}