using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp
{
    public class Event
    {
        public string EventId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public Event(string eventId, string title, string category,
                     DateTime eventDate, string location, string description)
        {
            EventId = eventId;
            Title = title;
            Category = category;
            EventDate = eventDate;
            Location = location;
            Description = description;
        }
    }
}