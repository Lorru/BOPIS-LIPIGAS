using bopis_api.Models.Bopis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopis_api.Services.Bopis
{
    interface IOrderService
    {
        int findByOrderStatusIdCount(long orderStatusId);

        int findByOrderStatusIdAndLocalIdCount(long orderStatusId, long localId);

        List<Order> findByLocalId(long localId, string filter, int sort, string column);

        List<Order> findByLocalIdPaged(int page, long localId, string filter, int sort, string column);

        Order deleteById(long id);

        Order updateById(Order order);

        List<Order> findByIdAndClientRun(long id, string runClient, int sort, string column);

        List<Order> findByIdAndClientRunPaged(int page, long id, string runClient, int sort, string column);

        Order create(Order order);
    }
}
