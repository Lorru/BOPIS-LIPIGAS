using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace bopis_api.Models.Bopis
{
    public partial class Local
    {
        public Local()
        {
            CylinderByLocal = new HashSet<CylinderByLocal>();
            ScheduleOfAttention = new HashSet<ScheduleOfAttention>();
            User = new HashSet<User>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Length { get; set; }
        public decimal Radio { get; set; }
        public bool Open { get; set; }
        public bool Status { get; set; }

        [JsonIgnore] public virtual ICollection<CylinderByLocal> CylinderByLocal { get; set; }
        [JsonIgnore] public virtual ICollection<ScheduleOfAttention> ScheduleOfAttention { get; set; }
        [JsonIgnore] public virtual ICollection<User> User { get; set; }
    }
}
