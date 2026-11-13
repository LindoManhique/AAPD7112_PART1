
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace MunicipalServicesApp
{
    public partial class ReportIssues : Form
    {
        private static List<Issue> reportedIssues = new List<Issue>();

        private static RequestBinarySearchTree requestTree =
            new RequestBinarySearchTree();

        private static RequestAVLTree avlTree =
            new RequestAVLTree();

        private static RequestRedBlackTree redBlackTree =
            new RequestRedBlackTree();

        private static RequestPriorityHeap requestHeap =
            new RequestPriorityHeap();

        private static ServiceAreaGraph serviceAreaGraph =
            new ServiceAreaGraph();

        private static bool graphInitialized = false;

        private static string storageFile =
            Path.Combine(Application.StartupPath, "ServiceRequests.xml");

        private static bool requestsLoaded = false;

        private static void SaveRequests()
        {
            XmlSerializer serializer =
                new XmlSerializer(typeof(List<Issue>));

            using (FileStream stream = new FileStream(
                storageFile, FileMode.Create))
            {
                serializer.Serialize(stream, reportedIssues);
            }
        }

        private static void LoadRequests()
        {
            if (requestsLoaded)
            {
                return;
            }

            requestsLoaded = true;

            if (!File.Exists(storageFile))
            {
                return;
            }

            try
            {
                XmlSerializer serializer =
                    new XmlSerializer(typeof(List<Issue>));

                using (FileStream stream = new FileStream(
                    storageFile, FileMode.Open))
                {
                    reportedIssues =
                        (List<Issue>)serializer.Deserialize(stream);
                }

                foreach (Issue issue in reportedIssues)
                {
                    requestTree.Insert(issue);
                    avlTree.Insert(issue);
                    redBlackTree.Insert(issue);
                    requestHeap.Insert(issue);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Saved service requests could not be loaded: " + ex.Message,
                    "Storage Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public static IReadOnlyList<Issue> ReportedIssues
        {
            get
            {
                LoadRequests();
                return reportedIssues.AsReadOnly();
            }
        }

        public static Issue FindRequestByID(string requestID)
        {
            LoadRequests();
            return requestTree.Search(requestID);
        }

        public static Issue FindRequestByID_AVL(string requestID)
        {
            LoadRequests();
            return avlTree.Search(requestID);
        }

        public static Issue FindRequestByID_RedBlack(string requestID)
        {
            LoadRequests();
            return redBlackTree.Search(requestID);
        }

        public static Issue GetOldestRequest()
        {
            LoadRequests();
            return requestHeap.Peek();
        }

        public static int GetHeapCount()
        {
            LoadRequests();
            return requestHeap.Count;
        }

        public static void SaveRequestChanges()
        {
            SaveRequests();
        }

        public static ServiceAreaGraph GetServiceAreaGraph()
        {
            if (!graphInitialized)
            {
                serviceAreaGraph.AddEdge("Zone A", "Zone B", 5);
                serviceAreaGraph.AddEdge("Zone A", "Zone C", 8);
                serviceAreaGraph.AddEdge("Zone B", "Zone C", 3);
                serviceAreaGraph.AddEdge("Zone B", "Zone D", 7);
                serviceAreaGraph.AddEdge("Zone C", "Zone D", 4);
                serviceAreaGraph.AddEdge("Zone C", "Zone E", 6);
                serviceAreaGraph.AddEdge("Zone D", "Zone E", 2);

                graphInitialized = true;
            }

            return serviceAreaGraph;
        }

        private string attachmentPath = "";

        public ReportIssues()
        {
            InitializeComponent();
            LoadRequests();
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

            LoadRequests();

            Issue newIssue = new Issue
            {
                RequestID = "SR-" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(),
                Location = txtLocation.Text.Trim(),
                Category = DropDownStyle.SelectedItem.ToString(),
                Description = rtbDescription.Text.Trim(),
                AttachmentPath = attachmentPath,
                DateReported = DateTime.Now,
                Status = "Submitted",
                LastUpdated = DateTime.Now
            };

            reportedIssues.Add(newIssue);

            requestTree.Insert(newIssue);
            avlTree.Insert(newIssue);
            redBlackTree.Insert(newIssue);
            requestHeap.Insert(newIssue);

            try
            {
                SaveRequests();

                MessageBox.Show(
                    "Your issue has been submitted successfully and saved.\n\nThank you for helping improve municipal services.",
                    "Report Submitted",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                reportedIssues.Remove(newIssue);

                MessageBox.Show(
                    "The request was created but could not be saved: " + ex.Message,
                    "Storage Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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
