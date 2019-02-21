using bopis_api.Models.Bopis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopis_api.Services.Bopis
{
    interface IStockService
    {
        Stock updateQuantityByIdAndStatusEqualToOne(Stock stock);

        List<Stock> findByLocalId(long localId);

        Stock findByCylinderByLocalIdAndStatusEqualToOne(long cylinderByLocalId);

        Stock updateQuantityByCylinderByLocalIdAndStatusEqualToOne(Stock stock);

        Stock create(long cylinderByLocalId);
    }
}
