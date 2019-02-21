using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bopis_api.Models.Bopis;

namespace bopis_api.Services.Bopis
{
    public class LogServiceImpl : ILogService
    {
        private ModelContext modelContext = new ModelContext();

        private ConfigurationServiceImpl configurationServiceImpl = new ConfigurationServiceImpl();

        private string key = "BD";

        public LogServiceImpl()
        {

        }

        public Log create(Log log)
        {
            List<Configuration> configurations = configurationServiceImpl.findByKeyAndStatusEqualToOne(key);

            long Id = Convert.ToInt64(configurations[1].Value);

            log.Id = Id;
            log.Date = DateTime.Now;

            modelContext.Log.Add(log);
            modelContext.SaveChanges();

            Configuration configuration = new Configuration();

            configuration.Id = configurations[1].Id;
            configuration.Value = (Id + 1).ToString();

            configurationServiceImpl.updateValueByIdAndStatusEqualToOne(configuration);

            return log;
        }
    }
}
