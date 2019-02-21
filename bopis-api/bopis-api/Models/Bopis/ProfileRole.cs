using System;
using System.Collections.Generic;

namespace bopis_api.Models.Bopis
{
    public partial class ProfileRole
    {
        public long Id { get; set; }
        public long ProfileId { get; set; }
        public long RoleId { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual Role Role { get; set; }
    }
}
