using System;
using System.Collections.Generic;

namespace bopis_api.Models.Bopis
{
    public partial class ScheduleOfAttention
    {
        public long Id { get; set; }
        public long LocalId { get; set; }
        public long WeekId { get; set; }
        public string Start { get; set; }
        public string Finish { get; set; }
        public bool? Status { get; set; }

        public virtual Local Local { get; set; }
        public virtual Week Week { get; set; }
    }
}
