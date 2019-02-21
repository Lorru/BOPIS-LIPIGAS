using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace bopis_api.Models.Bopis
{
    public partial class Profile
    {
        public Profile()
        {
            ProfileRole = new HashSet<ProfileRole>();
            User = new HashSet<User>();
        }

        public long Id { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        [JsonIgnore] public virtual ICollection<ProfileRole> ProfileRole { get; set; }
        [JsonIgnore] public virtual ICollection<User> User { get; set; }
    }
}
