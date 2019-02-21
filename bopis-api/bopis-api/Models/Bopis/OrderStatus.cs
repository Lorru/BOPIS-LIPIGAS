using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace bopis_api.Models.Bopis
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            Order = new HashSet<Order>();
        }

        public long Id { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        [JsonIgnore] public virtual ICollection<Order> Order { get; set; }
    }
}
