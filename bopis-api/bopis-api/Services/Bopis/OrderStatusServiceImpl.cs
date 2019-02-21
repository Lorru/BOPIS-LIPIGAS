using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bopis_api.Models.Bopis;

namespace bopis_api.Services.Bopis
{
    public class OrderStatusServiceImpl : IOrderStatusService
    {

        private ModelContext modelContext = new ModelContext();

        public OrderStatusServiceImpl()
        {

        }

        public List<OrderStatus> findAllStatusEqualToOne()
        {
            List<OrderStatus> orderStatuses = (from o in modelContext.OrderStatus
                                               where o.Status == true
                                               select o).ToList();

            return orderStatuses;
        }
    }
}
