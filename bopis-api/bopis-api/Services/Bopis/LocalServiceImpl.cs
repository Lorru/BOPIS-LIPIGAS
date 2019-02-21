using bopis_api.Models.Bopis;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopis_api.Services.Bopis
{
    public class LocalServiceImpl : ILocalService
    {
        private ModelContext modelContext = new ModelContext();

        private ConfigurationServiceImpl configurationServiceImpl = new ConfigurationServiceImpl();

        private WeekServiceImpl weekServiceImpl = new WeekServiceImpl();

        private CylinderServiceImpl cylinderServiceImpl = new CylinderServiceImpl();

        private ScheduleOfAttentionServiceImpl scheduleOfAttentionServiceImpl = new ScheduleOfAttentionServiceImpl();

        private CylinderByLocalServiceImpl cylinderByLocalServiceImpl = new CylinderByLocalServiceImpl();

        private string key = "SYSTEM";

        private string key2 = "BD";

        public LocalServiceImpl()
        {

        }

        public Local create(Local local)
        {
            List<Configuration> configurations = configurationServiceImpl.findByKeyAndStatusEqualToOne(key2);

            long Id = Convert.ToInt64(configurations[4].Value);

            local.Id = Id;
            local.Open = true;
            local.Status = true;

            modelContext.Local.Add(local);
            modelContext.SaveChanges();

            Configuration configuration = new Configuration();

            configuration.Id = configurations[4].Id;
            configuration.Value = (Id + 1).ToString();

            configurationServiceImpl.updateValueByIdAndStatusEqualToOne(configuration);

            List<Week> weeks = weekServiceImpl.findAllAndStatusEqualToOne();

            List<Cylinder> cylinders = cylinderServiceImpl.findAllAndStatusEqualToOne();

            foreach (Week week in weeks)
            {
                scheduleOfAttentionServiceImpl.create(Id, week.Id);
            }

            foreach (Cylinder cylinder in cylinders)
            {
                cylinderByLocalServiceImpl.create(Id, cylinder.Id);
            }

            return local;
        }

        public List<Local> findAll(string filter, int sort, string column)
        {
            List<Local> locals = null;

            if (filter != null)
            {
                locals = (from l in modelContext.Local
                          where l.Name.Contains(filter)
                          select l).ToList();
            }
            else
            {
                locals = (from l in modelContext.Local
                          select l).ToList();
            }

            if (sort == 1 && column == "Id")
            {
                locals = locals.OrderBy(l => l.Id).ToList();
            }
            else if (sort == 0 && column == "Id")
            {
                locals = locals.OrderByDescending(l => l.Id).ToList();
            }
            else if (sort == 1 && column == "Name")
            {
                locals = locals.OrderBy(l => l.Name).ToList();
            }
            else if (sort == 0 && column == "Name")
            {
                locals = locals.OrderByDescending(l => l.Name).ToList();
            }
            else if (sort == 1 && column == "Radio")
            {
                locals = locals.OrderBy(l => l.Radio).ToList();
            }
            else if (sort == 0 && column == "Radio")
            {
                locals = locals.OrderByDescending(l => l.Radio).ToList();
            }

            return locals;
        }

        public int findAllCount()
        {
            List<Local> locals = (from l in modelContext.Local
                                  select l).ToList();

            return locals.Count;
        }

        public List<Local> findAllPaged(int page, string filter, int sort, string column)
        {
            List<Configuration> configurations = configurationServiceImpl.findByKeyAndStatusEqualToOne(key);

            int size = Convert.ToInt32(configurations[3].Value);

            List<Local> locals = null;

            if (filter != null)
            {
                locals = (from l in modelContext.Local
                          where l.Name.Contains(filter)
                          select l).ToList();
            }
            else
            {
                locals = (from l in modelContext.Local
                          select l).ToList();
            }

            if (sort == 1 && column == "Id")
            {
                locals = locals.OrderBy(l => l.Id).ToList();
            }
            else if (sort == 0 && column == "Id")
            {
                locals = locals.OrderByDescending(l => l.Id).ToList();
            }
            else if (sort == 1 && column == "Name")
            {
                locals = locals.OrderBy(l => l.Name).ToList();
            }
            else if (sort == 0 && column == "Name")
            {
                locals = locals.OrderByDescending(l => l.Name).ToList();
            }
            else if (sort == 1 && column == "Radio")
            {
                locals = locals.OrderBy(l => l.Radio).ToList();
            }
            else if (sort == 0 && column == "Radio")
            {
                locals = locals.OrderByDescending(l => l.Radio).ToList();
            }

            locals = locals.ToPagedList(page, size).ToList();

            return locals;
        }

        public List<Local> findAllStatusEqualToOne()
        {
            List<Local> locals = (from l in modelContext.Local
                                  where l.Status == true
                                  select l).ToList();

            return locals;
        }

        public int findByStatusEqualToOneCount()
        {
            List<Local> locals = (from l in modelContext.Local
                                  where l.Status == true
                                  select l).ToList();

            return locals.Count;
        }

        public Local updateByIdAndStatusEqualToOne(Local local)
        {
            Local localExisting = (from l in modelContext.Local
                                where l.Id == local.Id && l.Status == true
                                select l).FirstOrDefault();

            localExisting.Latitude = local.Latitude;
            localExisting.Length = local.Length;
            localExisting.Name = local.Name;
            localExisting.Radio = local.Radio;

            modelContext.SaveChanges();

            return localExisting;
        }

        public Local updateOpenFindByIdAndStatusEqualToOne(Local local)
        {
            Local localExisting = (from l in modelContext.Local
                                where l.Id == local.Id && l.Status == true
                                select l).FirstOrDefault();

            localExisting.Open = local.Open;

            modelContext.SaveChanges();

            return localExisting;
        }

        public Local updateStatusFindById(Local local)
        {
            Local localExisting = (from l in modelContext.Local
                                   where l.Id == local.Id
                                   select l).FirstOrDefault();

            localExisting.Status = local.Status;

            modelContext.SaveChanges();

            return localExisting;
        }
    }
}
