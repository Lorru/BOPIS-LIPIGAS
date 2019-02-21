using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace bopis_api.Models.Bopis
{
    public partial class TypeLog
    {
        public TypeLog()
        {
            Log = new HashSet<Log>();
        }

        public long Id { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        [JsonIgnore] public virtual ICollection<Log> Log { get; set; }
    }
}
