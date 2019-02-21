using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bopis_api.Models.Bopis;

namespace bopis_api.Services.Bopis
{
    public class ScheduleOfAttentionServiceImpl : IScheduleOfAttentionService
    {

        private ModelContext modelContext = new ModelContext();

        private ConfigurationServiceImpl configurationServiceImpl = new ConfigurationServiceImpl();

        private string key = "BD";

        public ScheduleOfAttentionServiceImpl()
        {

        }

        public ScheduleOfAttention create(long localId, long weekId)
        {
            List<Configuration> configurations = configurationServiceImpl.findByKeyAndStatusEqualToOne(key);

            long Id = Convert.ToInt64(configurations[3].Value);

            ScheduleOfAttention scheduleOfAttention = new ScheduleOfAttention();

            scheduleOfAttention.Id = Id;
            scheduleOfAttention.Finish = "18:00";
            scheduleOfAttention.LocalId = localId;
            scheduleOfAttention.Start = "08:00";
            scheduleOfAttention.Status = true;
            scheduleOfAttention.WeekId = weekId;

            modelContext.ScheduleOfAttention.Add(scheduleOfAttention);
            modelContext.SaveChanges();

            Configuration configuration = new Configuration();

            configuration.Id = configurations[3].Id;
            configuration.Value = (Id + 1).ToString();

            configurationServiceImpl.updateValueByIdAndStatusEqualToOne(configuration);

            return scheduleOfAttention;
        }

        public List<ScheduleOfAttention> findByLocalIdAndStatusEqualToOne(long localId)
        {
            List<ScheduleOfAttention> scheduleOfAttentions = (from s in modelContext.ScheduleOfAttention
                                                              join w in modelContext.Week on s.WeekId equals w.Id
                                                              where s.LocalId == localId && s.Status == true
                                                              select new ScheduleOfAttention()
                                                              {
                                                                  Finish = s.Finish,
                                                                  Id = s.Id,
                                                                  LocalId = s.LocalId,
                                                                  Start = s.Start,
                                                                  Status = s.Status,
                                                                  Week = new Week()
                                                                  {
                                                                      DayOfWeek = w.DayOfWeek,
                                                                      Id = w.Id,
                                                                      Status = w.Status
                                                                  },
                                                                  WeekId = s.WeekId
                                                              }).ToList();

            return scheduleOfAttentions;
        }

        public ScheduleOfAttention updateStartAndFinishFindByIdAndStatusEqualToOne(ScheduleOfAttention scheduleOfAttention)
        {
            ScheduleOfAttention scheduleOfAttentionExist = (from s in modelContext.ScheduleOfAttention
                                                            where s.Id == scheduleOfAttention.Id && s.Status == true
                                                            select s).FirstOrDefault();

            scheduleOfAttentionExist.Start = scheduleOfAttention.Start;
            scheduleOfAttentionExist.Finish = scheduleOfAttention.Finish;

            modelContext.SaveChanges();

            return scheduleOfAttentionExist;
        }
    }
}
