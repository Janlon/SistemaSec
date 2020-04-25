using Kendo.Mvc.UI;
using System;

namespace SiteSec.Models
{
    public class MeetingViewModel: ISchedulerEvent
    {
        public string Title { get; set; }
        public bool IsAllDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public string StartTimezone { get; set; }
        public string EndTimezone { get; set; }
        public string RecurrenceRule { get; set; }
        public string RecurrenceException { get; set; }
        public object TaskID { get; internal set; }
        public object RecurrenceID { get; internal set; }
        public object OwnerID { get; internal set; }
    }
}