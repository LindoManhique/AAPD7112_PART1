
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class LocalEvents : Form
    {
        // Queue manages upcoming events in chronological order.
        private Queue<Event> upcomingEvents = new Queue<Event>();

        // Stack records searches for the recommendation feature.
        private Stack<string> searchHistory = new Stack<string>();

        // Dictionary stores events using their unique IDs.
        private Dictionary<string, Event> eventsById =
            new Dictionary<string, Event>();

        // SortedDictionary groups events by date.
        private SortedDictionary<DateTime, List<Event>> eventsByDate =
            new SortedDictionary<DateTime, List<Event>>();

        // HashSets store unique event categories and dates.
        private HashSet<string> uniqueCategories =
            new HashSet<string>();

        private HashSet<DateTime> uniqueDates =
            new HashSet<DateTime>();

        // List stores all events for searching and recommendations.
        private List<Event> allEvents = new List<Event>();

        public LocalEvents()
        {
            InitializeComponent();
        }

        private void LocalEvents_Load(object sender, EventArgs e)
        {
            // Configure the events ListView.
            lvEvents.View = View.Details;
            lvEvents.FullRowSelect = true;
            lvEvents.GridLines = true;
            lvEvents.MultiSelect = false;

            lvEvents.Columns.Clear();
            lvEvents.Columns.Add("Event ID", 80);
            lvEvents.Columns.Add("Event Name", 180);
            lvEvents.Columns.Add("Category", 110);
            lvEvents.Columns.Add("Date", 100);
            lvEvents.Columns.Add("Location", 150);
            lvEvents.Columns.Add("Description", 250);

            LoadSampleEvents();
            PopulateCategories();

            // Display upcoming events from the queue.
            DisplayUpcomingEvents();

            dtpDate.Value = DateTime.Today;
            chkAllDates.Checked = true;
        }

        private void LoadSampleEvents()
        {
            upcomingEvents.Clear();
            searchHistory.Clear();
            eventsById.Clear();
            eventsByDate.Clear();
            uniqueCategories.Clear();
            uniqueDates.Clear();
            allEvents.Clear();

            DateTime today = DateTime.Today;

            AddEvent(new Event(
                "EV001", "Community Clean-Up", "Community",
                today.AddDays(2), "Braamfontein Park",
                "Join local volunteers to clean and improve the park."));

            AddEvent(new Event(
                "EV002", "Job Readiness Workshop", "Education",
                today.AddDays(4), "Community Hall",
                "Learn CV writing, interview preparation and job-search skills."));

            AddEvent(new Event(
                "EV003", "Small Business Information Day", "Business",
                today.AddDays(6), "Civic Centre",
                "Get information about starting and managing a small business."));

            AddEvent(new Event(
                "EV004", "Community Health Awareness", "Health",
                today.AddDays(8), "Local Clinic",
                "Attend a community session about general health awareness."));

            AddEvent(new Event(
                "EV005", "Youth Skills Programme", "Education",
                today.AddDays(10), "Youth Centre",
                "Explore skills development and training opportunities."));

            AddEvent(new Event(
                "EV006", "Public Safety Meeting", "Community",
                today.AddDays(12), "Community Hall",
                "Discuss local safety concerns and community initiatives."));

            AddEvent(new Event(
                "EV007", "Local Market Day", "Business",
                today.AddDays(14), "Town Square",
                "Visit local traders and discover products made in the community."));

            AddEvent(new Event(
                "EV008", "Recycling Awareness Campaign", "Environment",
                today.AddDays(16), "Braamfontein Park",
                "Learn about recycling and responsible waste disposal."));

            // Build the queue in chronological order.
            upcomingEvents = new Queue<Event>(
                allEvents
                    .Where(ev => ev.EventDate.Date >= DateTime.Today)
                    .OrderBy(ev => ev.EventDate));
        }

        private void AddEvent(Event newEvent)
        {
            allEvents.Add(newEvent);

            // Store the event for lookup by ID.
            eventsById[newEvent.EventId] = newEvent;

            // Group events by date.
            DateTime date = newEvent.EventDate.Date;

            if (!eventsByDate.ContainsKey(date))
            {
                eventsByDate[date] = new List<Event>();
            }

            eventsByDate[date].Add(newEvent);

            // Store unique categories and dates.
            uniqueCategories.Add(newEvent.Category);
            uniqueDates.Add(date);
        }

        private void PopulateCategories()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All Categories");

            foreach (string category in uniqueCategories.OrderBy(c => c))
            {
                cmbCategory.Items.Add(category);
            }

            cmbCategory.SelectedIndex = 0;
        }

        private void DisplayUpcomingEvents()
        {
            // Read the queue without removing its events.
            DisplayEvents(upcomingEvents.ToList());
        }

        private void DisplayEvents(List<Event> eventsToDisplay)
        {
            lvEvents.Items.Clear();

            foreach (Event ev in eventsToDisplay.OrderBy(ev => ev.EventDate))
            {
                ListViewItem item = new ListViewItem(ev.EventId);

                item.SubItems.Add(ev.Title);
                item.SubItems.Add(ev.Category);
                item.SubItems.Add(ev.EventDate.ToString("dd MMM yyyy"));
                item.SubItems.Add(ev.Location);
                item.SubItems.Add(ev.Description);

                lvEvents.Items.Add(item);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchEvents();
        }

        private void SearchEvents()
        {
            string category = cmbCategory.SelectedItem == null
                ? "All Categories"
                : cmbCategory.SelectedItem.ToString();

            string keyword = txtSearch.Text.Trim();

            // Record meaningful searches for recommendations.
            if (keyword.Length > 0 || category != "All Categories")
            {
                searchHistory.Push(category + "|" + keyword);
            }

            List<Event> candidateEvents;

            if (chkAllDates.Checked)
            {
                // Retrieve all events from the ID dictionary.
                candidateEvents = eventsById.Values.ToList();
            }
            else
            {
                DateTime selectedDate = dtpDate.Value.Date;

                // First check whether the selected date exists.
                if (uniqueDates.Contains(selectedDate))
                {
                    // Retrieve events for that date from the sorted dictionary.
                    List<Event> eventsOnDate;

                    if (eventsByDate.TryGetValue(
                        selectedDate, out eventsOnDate))
                    {
                        candidateEvents = eventsOnDate;
                    }
                    else
                    {
                        candidateEvents = new List<Event>();
                    }
                }
                else
                {
                    // No event is scheduled for the selected date.
                    candidateEvents = new List<Event>();
                }
            }

            // Apply category and keyword filters.
            List<Event> results = candidateEvents.Where(ev =>
                (category == "All Categories" ||
                 ev.Category == category) &&

                (keyword.Length == 0 ||
                 ev.Title.IndexOf(
                     keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||

                 ev.Description.IndexOf(
                     keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||

                 ev.Location.IndexOf(
                     keyword, StringComparison.OrdinalIgnoreCase) >= 0)
            ).ToList();

            DisplayEvents(results);

            if (results.Count == 0)
            {
                MessageBox.Show(
                    "No events match your search. Try another keyword, category or date.",
                    "No Events Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();

            if (cmbCategory.Items.Count > 0)
            {
                cmbCategory.SelectedIndex = 0;
            }

            dtpDate.Value = DateTime.Today;
            chkAllDates.Checked = true;

            DisplayUpcomingEvents();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Opens recommendations and passes event data and search history.
        private void btnRecommendations_Click(object sender, EventArgs e)
        {
            EventRecommendations recommendationsForm =
                new EventRecommendations(
                    allEvents,
                    searchHistory.ToList());

            recommendationsForm.ShowDialog();
        }

        // Retained for compatibility with existing Designer event connections.
        private void button1_Click(object sender, EventArgs e)
        {
            SearchEvents();
        }

        private void lvEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            // No additional action is required when selection changes.
        }

        // Retained for compatibility with existing Designer event connections.
        private void button1_Click_1(object sender, EventArgs e)
        {
            SearchEvents();
        }
    }
}
