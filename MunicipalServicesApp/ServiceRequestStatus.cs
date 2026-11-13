
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class ServiceRequestStatus : Form
    {
        public ServiceRequestStatus()
        {
            InitializeComponent();
        }

        private void ServiceRequestStatus_Load(object sender, EventArgs e)
        {
            if (cmbStatusFilter.Items.Count > 0)
            {
                cmbStatusFilter.SelectedIndex = 0;
            }

            DisplayRequests();
        }

        private void DisplayRequests()
        {
            var requests = ReportIssues.ReportedIssues.AsEnumerable();

            if (cmbStatusFilter.SelectedItem != null &&
                cmbStatusFilter.SelectedItem.ToString() != "All")
            {
                string selectedStatus = cmbStatusFilter.SelectedItem.ToString();

                requests = requests.Where(issue =>
                    issue.Status == selectedStatus);
            }

            string searchText = txtSearch.Text.Trim();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                requests = requests.Where(issue =>
                    (issue.RequestID ?? "").IndexOf(searchText,
                        StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (issue.Location ?? "").IndexOf(searchText,
                        StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (issue.Category ?? "").IndexOf(searchText,
                        StringComparison.OrdinalIgnoreCase) >= 0);
            }

            dgvRequests.DataSource = requests.Select(issue => new
            {
                RequestID = issue.RequestID,
                Location = issue.Location,
                Category = issue.Category,
                DateReported = issue.DateReported,
                Status = issue.Status,
                LastUpdated = issue.LastUpdated
            }).ToList();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            if (searchText.StartsWith("SR-", StringComparison.OrdinalIgnoreCase))
            {
                Issue foundIssue = ReportIssues.FindRequestByID_AVL(searchText);

                if (foundIssue != null)
                {
                    dgvRequests.DataSource = new List<Issue> { foundIssue }
                        .Select(issue => new
                        {
                            RequestID = issue.RequestID,
                            Location = issue.Location,
                            Category = issue.Category,
                            DateReported = issue.DateReported,
                            Status = issue.Status,
                            LastUpdated = issue.LastUpdated
                        }).ToList();

                    return;
                }
            }

            DisplayRequests();
        }

        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayRequests();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DisplayRequests();

            MessageBox.Show(
                "Service requests have been refreshed.",
                "Refresh Complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a service request first.",
                    "No Request Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string requestID = Convert.ToString(
                dgvRequests.SelectedRows[0].Cells["RequestID"].Value);

            Issue selectedIssue = ReportIssues.FindRequestByID_RedBlack(requestID);

            if (selectedIssue != null)
            {
                MessageBox.Show(
                    "Request ID: " + selectedIssue.RequestID +
                    "\nLocation: " + selectedIssue.Location +
                    "\nCategory: " + selectedIssue.Category +
                    "\nDescription: " + selectedIssue.Description +
                    "\nAttachment: " + selectedIssue.AttachmentPath +
                    "\nDate Reported: " + selectedIssue.DateReported +
                    "\nStatus: " + selectedIssue.Status +
                    "\nLast Updated: " + selectedIssue.LastUpdated,
                    "Service Request Details",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "The selected service request could not be found.",
                    "Request Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bbtnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a service request first.",
                    "No Request Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (cmbNewStatus.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a new status.",
                    "Status Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string requestID = Convert.ToString(
                dgvRequests.SelectedRows[0].Cells["RequestID"].Value);

            Issue selectedIssue = ReportIssues.FindRequestByID(requestID);

            if (selectedIssue == null)
            {
                MessageBox.Show(
                    "The selected service request could not be found.",
                    "Request Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            string previousStatus = selectedIssue.Status;
            DateTime previousLastUpdated = selectedIssue.LastUpdated;

            selectedIssue.Status = cmbNewStatus.SelectedItem.ToString();
            selectedIssue.LastUpdated = DateTime.Now;

            try
            {
                ReportIssues.SaveRequestChanges();

                DisplayRequests();

                MessageBox.Show(
                    "Service request status updated and saved successfully!",
                    "Update Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                selectedIssue.Status = previousStatus;
                selectedIssue.LastUpdated = previousLastUpdated;

                DisplayRequests();

                MessageBox.Show(
                    "The status could not be saved. The previous status has been restored.\n\n" + ex.Message,
                    "Storage Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnOldestRequest_Click(object sender, EventArgs e)
        {
            Issue oldestRequest = ReportIssues.GetOldestRequest();

            if (oldestRequest == null)
            {
                MessageBox.Show(
                    "There are currently no service requests available.",
                    "No Requests Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            MessageBox.Show(
                "Oldest Service Request\n\n" +
                "Request ID: " + oldestRequest.RequestID +
                "\nLocation: " + oldestRequest.Location +
                "\nCategory: " + oldestRequest.Category +
                "\nDescription: " + oldestRequest.Description +
                "\nDate Reported: " + oldestRequest.DateReported +
                "\nStatus: " + oldestRequest.Status,
                "Oldest Request",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnGraphAnalysis_Click(object sender, EventArgs e)
        {
            ServiceAreaGraph graph = ReportIssues.GetServiceAreaGraph();

            List<string> bfsResults =
                graph.BreadthFirstSearch("Zone A");

            List<string> dfsResults =
                graph.DepthFirstSearch("Zone A");

            List<string> mstResults =
                graph.MinimumSpanningTree();

            string message =
                "MUNICIPAL SERVICE AREA GRAPH ANALYSIS\n\n" +
                "BREADTH-FIRST SEARCH (BFS)\n" +
                string.Join(" -> ", bfsResults) +
                "\n\nDEPTH-FIRST SEARCH (DFS)\n" +
                string.Join(" -> ", dfsResults) +
                "\n\nMINIMUM SPANNING TREE (KRUSKAL)\n" +
                string.Join("\n", mstResults) +
                "\n\nThese distances are illustrative values in kilometres.";

            MessageBox.Show(
                message,
                "Graph Analysis Results",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
