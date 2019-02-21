using bopis_api.Models.Bopis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopis_api.Services.Bopis
{
    interface IScheduleOfAttentionService
    {
        List<ScheduleOfAttention> findByLocalIdAndStatusEqualToOne(long localId);

        ScheduleOfAttention updateStartAndFinishFindByIdAndStatusEqualToOne(ScheduleOfAttention scheduleOfAttention);

        ScheduleOfAttention create(long localId, long weekId);
    }
}
