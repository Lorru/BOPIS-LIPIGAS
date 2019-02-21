using bopis_api.Models.Bopis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopis_api.Services.Others
{
    interface IReportService
    {
        List<Order> findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId(string dateStart, string dateFinish, string localId, string userId, string orderStatusId);

        string findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusIdExcel(string dateStart, string dateFinish, string localId, string userId, string orderStatusId);
    }
}
