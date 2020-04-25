using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SiteSec.Models
{
    public partial class  Agenda : ISchedulerEvent
    {
        #region propriedades para uso do task

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("StartTimezone")]
        public string StartTimezone { get; set; }

        [JsonProperty("Start")]
        public DateTime Start { get; set; }

        [JsonProperty("End")]
        public DateTime End { get; set; }

        [JsonProperty("EndTimezone")]
        public string EndTimezone { get; set; }

        [JsonProperty("RecurrenceRule")]
        public string RecurrenceRule { get; set; }

        [JsonProperty("RecurrenceException")]
        public string RecurrenceException { get; set; }

        [JsonProperty("IsAllDay")]
        public bool IsAllDay { get; set; }

        #endregion

        #region propriedades para uso do custom editor

        public long TaskId { get; set; }

        public long OwerId { get; set; }

        #endregion
    }
}