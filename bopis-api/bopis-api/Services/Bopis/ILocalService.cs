using bopis_api.Models.Bopis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopis_api.Services.Bopis
{
    interface ILocalService
    {
        int findAllCount();

        int findByStatusEqualToOneCount();

        Local updateOpenFindByIdAndStatusEqualToOne(Local local);

        List<Local> findAll(string filter, int sort, string column);

        List<Local> findAllPaged(int page, string filter, int sort, string column);

        Local updateStatusFindById(Local local);

        Local updateByIdAndStatusEqualToOne(Local local);

        Local create(Local local);

        List<Local> findAllStatusEqualToOne();
    }
}
