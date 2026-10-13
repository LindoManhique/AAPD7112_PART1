
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class EventRecommendations : Form
    {
        private List<Event> events;
        private List<string> searchHistory;

        // Stores the number of times each category was searched.
        private Dictionary<string, int> categoryPreferences =
            new Dictionary<string, int>(
                StringComparer.OrdinalIgnoreCase);

        // Stores the number of times each keyword was searched.
        private Dictionary<string, int> keywordPreferences =
            new Dictionary<string, int>(
                StringComparer.OrdinalIgnoreCase);

        // Holds the score and explanation for each recommendation.
        private class RecommendationResult
        {
            public Event RecommendedEvent { get; set; }
            public int Score { get; set; }
            public string Reason { get; set; }
        }

        public EventRecommendations(
            List<Event> events,
            List<string> searchHistory)
        {
            InitializeComponent();

            this.events = events;
            this.searchHistory = searchHistory;

            this.Load += EventRecommendations_Load;
            btnBack.Click += btnBack_Click;
        }

        private void EventRecommendations_Load(
            object sender, EventArgs e)
        {
            // Configure the recommendations list.
            lvRecommendations.View = View.Details;
            lvRecommendations.FullRowSelect = true;
            lvRecommendations.GridLines = true;
            lvRecommendations.MultiSelect = false;

            lvRecommendations.Columns.Clear();
            lvRecommendations.Columns.Add("Event", 190);
            lvRecommendations.Columns.Add("Category", 110);
            lvRecommendations.Columns.Add("Date", 110);
            lvRecommendations.Columns.Add("Location", 150);
            lvRecommendations.Columns.Add("Why Recommended", 260);

            GenerateRecommendations();
        }

        private void AnalyseSearchHistory()
        {
            categoryPreferences.Clear();
            keywordPreferences.Clear();

            foreach (string search in searchHistory)
            {
                // Each search is stored as Category|Keyword.
                string[] parts = search.Split(
                    new char[] { '|' }, 2);

                if (parts.Length == 0)
                {
                    continue;
                }

                string category = parts[0].Trim();

                // Count searches for specific categories.
                if (!string.IsNullOrWhiteSpace(category) &&
                    !category.Equals(
                        "All Categories",
                        StringComparison.OrdinalIgnoreCase))
                {
                    if (!categoryPreferences.ContainsKey(category))
                    {
                        categoryPreferences[category] = 0;
                    }

                    categoryPreferences[category]++;
                }

                // Count searches containing a keyword.
                if (parts.Length > 1)
                {
                    string keyword = parts[1].Trim();

                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        if (!keywordPreferences.ContainsKey(keyword))
                        {
                            keywordPreferences[keyword] = 0;
                        }

                        keywordPreferences[keyword]++;
                    }
                }
            }
        }

        private void GenerateRecommendations()
        {
            lvRecommendations.Items.Clear();

            // Step 1: Analyse the user's search history.
            AnalyseSearchHistory();

            // Step 2: Consider only upcoming events.
            List<Event> upcomingEvents = events
                .Where(ev => ev.EventDate.Date >= DateTime.Today)
                .ToList();

            // Step 3: Calculate a personal score for each event.
            List<RecommendationResult> recommendations =
                new List<RecommendationResult>();

            foreach (Event ev in upcomingEvents)
            {
                int score = 0;

                List<string> reasons = new List<string>();

                // Category preferences receive 10 points per search.
                if (categoryPreferences.ContainsKey(ev.Category))
                {
                    int categoryCount =
                        categoryPreferences[ev.Category];

                    score += categoryCount * 10;

                    reasons.Add(
                        "Category searched " +
                        categoryCount + " time(s)");
                }

                // Search keywords are compared with event information.
                foreach (KeyValuePair<string, int> preference
                    in keywordPreferences)
                {
                    string keyword = preference.Key;

                    bool matches =
                        ev.Title.IndexOf(
                            keyword,
                            StringComparison.OrdinalIgnoreCase) >= 0 ||

                        ev.Description.IndexOf(
                            keyword,
                            StringComparison.OrdinalIgnoreCase) >= 0 ||

                        ev.Location.IndexOf(
                            keyword,
                            StringComparison.OrdinalIgnoreCase) >= 0;

                    if (matches)
                    {
                        // Matching keywords receive 8 points per search.
                        score += preference.Value * 8;

                        reasons.Add(
                            "Matches '" + keyword +
                            "' searched " + preference.Value +
                            " time(s)");
                    }
                }

                string explanation;

                if (reasons.Count > 0)
                {
                    explanation = string.Join("; ", reasons.ToArray());
                }
                else
                {
                    explanation = "Upcoming event to explore";
                }

                recommendations.Add(new RecommendationResult
                {
                    RecommendedEvent = ev,
                    Score = score,
                    Reason = explanation
                });
            }

            // Step 4: Rank the highest-scoring events first.
            List<RecommendationResult> topRecommendations =
                recommendations
                    .OrderByDescending(result => result.Score)
                    .ThenBy(result =>
                        result.RecommendedEvent.EventDate)
                    .Take(5)
                    .ToList();

            // Step 5: Display the top five recommendations.
            foreach (RecommendationResult result
                in topRecommendations)
            {
                Event ev = result.RecommendedEvent;

                ListViewItem item = new ListViewItem(ev.Title);

                item.SubItems.Add(ev.Category);
                item.SubItems.Add(
                    ev.EventDate.ToString("dd MMM yyyy"));
                item.SubItems.Add(ev.Location);
                item.SubItems.Add(result.Reason);

                lvRecommendations.Items.Add(item);
            }

            // Step 6: Explain how the recommendations were generated.
            if (topRecommendations.Count == 0)
            {
                lblExplanation.Text =
                    "There are no upcoming events to recommend right now.";
            }
            else if (categoryPreferences.Count == 0 &&
                     keywordPreferences.Count == 0)
            {
                lblExplanation.Text =
                    "These are upcoming events. Search for categories " +
                    "or keywords to personalise your recommendations.";
            }
            else
            {
                lblExplanation.Text =
                    "Recommendations are ranked using your search " +
                    "history. Repeated interests receive higher scores, " +
                    "and matching events appear first.";
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
