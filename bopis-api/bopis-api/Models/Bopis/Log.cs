using System;
using System.Collections.Generic;

namespace bopis_api.Models.Bopis
{
    public partial class Log
    {
        public long Id { get; set; }
        public long TypeLogId { get; set; }
        public long? UserId { get; set; }
        public string Controller { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public virtual TypeLog TypeLog { get; set; }
        public virtual User User { get; set; }
    }
}
