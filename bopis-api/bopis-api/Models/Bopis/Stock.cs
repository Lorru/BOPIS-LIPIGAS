using System;
using System.Collections.Generic;

namespace bopis_api.Models.Bopis
{
    public partial class Stock
    {
        public long Id { get; set; }
        public long CylinderByLocalId { get; set; }
        public long Quantity { get; set; }
        public bool Status { get; set; }

        public virtual CylinderByLocal CylinderByLocal { get; set; }
    }
}
