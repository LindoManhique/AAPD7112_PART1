using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class ReportIssues : Form
    {
        private static List<Issue> reportedIssues = new List<Issue>();
        private string attachmentPath = "";

        public ReportIssues()
        {
            InitializeComponent();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProgress();
        }

        private void ReportIssues_Load(object sender, EventArgs e)
        {

        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select Supporting File";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png|Document Files|*.pdf;*.doc;*.docx|All Supported Files|*.jpg;*.jpeg;*.png;*.pdf;*.doc;*.docx";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    attachmentPath = openFileDialog.FileName;
                    lblFileName.Text = System.IO.Path.GetFileName(attachmentPath);

                    UpdateProgress();
                }
            }
        }

        private void UpdateProgress()
        {
            int completedSteps = 0;

            if (!string.IsNullOrWhiteSpace(txtLocation.Text))
                completedSteps++;

            if (DropDownStyle.SelectedIndex >= 0)
                completedSteps++;

            if (!string.IsNullOrWhiteSpace(rtbDescription.Text))
                completedSteps++;

            if (!string.IsNullOrWhiteSpace(attachmentPath))
                completedSteps++;

            int progress = completedSteps * 25;

            progressBarReport.Value = progress;
            lblProgress.Text = progress + "% Complete";
        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            UpdateProgress();
        }

        private void rtbDescription_TextChanged(object sender, EventArgs e)
        {
            UpdateProgress();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show(
                    "Please enter the location of the issue.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtLocation.Focus();
                return;
            }

            if (DropDownStyle.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Please select a category.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                DropDownStyle.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(rtbDescription.Text))
            {
                MessageBox.Show(
                    "Please provide a description of the issue.",
                    "Missing Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                rtbDescription.Focus();
                return;
            }

            Issue newIssue = new Issue
            {
                Location = txtLocation.Text.Trim(),
                Category = DropDownStyle.SelectedItem.ToString(),
                Description = rtbDescription.Text.Trim(),
                AttachmentPath = attachmentPath,
                DateReported = DateTime.Now
            };

            reportedIssues.Add(newIssue);

            MessageBox.Show(
                "Your issue has been submitted successfully.\n\nThank you for helping improve municipal services.",
                "Report Submitted",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblProgress_Click(object sender, EventArgs e)
        {

        }
    }
}