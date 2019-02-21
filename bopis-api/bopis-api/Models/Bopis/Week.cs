using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace bopis_api.Models.Bopis
{
    public partial class Week
    {
        public Week()
        {
            ScheduleOfAttention = new HashSet<ScheduleOfAttention>();
        }

        public long Id { get; set; }
        public string DayOfWeek { get; set; }
        public bool Status { get; set; }

        [JsonIgnore] public virtual ICollection<ScheduleOfAttention> ScheduleOfAttention { get; set; }
    }
}
