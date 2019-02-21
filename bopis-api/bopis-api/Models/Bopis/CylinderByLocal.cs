using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace bopis_api.Models.Bopis
{
    public partial class CylinderByLocal
    {
        public CylinderByLocal()
        {
            Order = new HashSet<Order>();
            Stock = new HashSet<Stock>();
        }

        public long Id { get; set; }
        public long LocalId { get; set; }
        public long CylinderId { get; set; }
        public long ZonePrice { get; set; }
        public int Discount { get; set; }
        public long FinalPrice { get; set; }
        public bool Status { get; set; }

        public virtual Cylinder Cylinder { get; set; }
        public virtual Local Local { get; set; }
        [JsonIgnore] public virtual ICollection<Order> Order { get; set; }
        [JsonIgnore] public virtual ICollection<Stock> Stock { get; set; }
    }
}
