using System;
using System.Collections.Generic;

namespace bopis_api.Models.Bopis
{
    public partial class Order
    {
        public long Id { get; set; }
        public long OrderStatusId { get; set; }
        public long CylinderByLocalId { get; set; }
        public long UserId { get; set; }
        public string ClientRun { get; set; }
        public string ClientAddress { get; set; }
        public string ClientFullName { get; set; }
        public int Quantity { get; set; }
        public string ScheduleOfRetirement { get; set; }
        public DateTime? DateOfDelivery { get; set; }
        public DateTime DateOfRequest { get; set; }

        public virtual CylinderByLocal CylinderByLocal { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual User User { get; set; }
    }
}
