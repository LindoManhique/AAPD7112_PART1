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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            ReportIssues reportIssuesForm = new ReportIssues();
            reportIssuesForm.ShowDialog();
        }
        private void btnEvents_Click(object sender, EventArgs e)
        {
            LocalEvents eventsForm = new LocalEvents();
            eventsForm.ShowDialog();
        }

        private void exitbttn(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

