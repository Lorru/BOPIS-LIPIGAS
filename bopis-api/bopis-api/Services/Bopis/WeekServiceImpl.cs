using bopis_api.Models.Bopis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopis_api.Services.Bopis
{
    public class WeekServiceImpl : IWeekService
    {
        ModelContext modelContext = new ModelContext();

        public WeekServiceImpl()
        {

        }

        public List<Week> findAllAndStatusEqualToOne()
        {
            List<Week> weeks = (from w in modelContext.Week
                                where w.Status == true
                                select w).ToList();

            return weeks;
        }
    }
}
