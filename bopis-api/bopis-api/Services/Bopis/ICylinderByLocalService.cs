using bopis_api.Models.Bopis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopis_api.Services.Bopis
{
    interface ICylinderByLocalService
    {

        CylinderByLocal updateStatusFindById(CylinderByLocal cylinderByLocal);

        List<CylinderByLocal> findByLocalIdAndStatusEqualToOne(long localId);

        CylinderByLocal updateByIdAndStatusEqualToOne(CylinderByLocal cylinderByLocal);

        CylinderByLocal create(long localId, long cylinderId);
    }
}
