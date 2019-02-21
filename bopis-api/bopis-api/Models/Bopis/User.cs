using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace bopis_api.Models.Bopis
{
    public partial class User
    {
        public User()
        {
            Log = new HashSet<Log>();
            Order = new HashSet<Order>();
        }

        public long Id { get; set; }
        public long ProfileId { get; set; }
        public long? LocalId { get; set; }
        public string FullName { get; set; }
        public string Run { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }

        public virtual Local Local { get; set; }
        public virtual Profile Profile { get; set; }
        [JsonIgnore] public virtual ICollection<Log> Log { get; set; }
        [JsonIgnore] public virtual ICollection<Order> Order { get; set; }
    }
}
