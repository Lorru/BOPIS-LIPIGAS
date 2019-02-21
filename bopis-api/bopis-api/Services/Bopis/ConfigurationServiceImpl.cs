using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bopis_api.Models.Bopis;
using PagedList;

namespace bopis_api.Services.Bopis
{
    public class ConfigurationServiceImpl : IConfigurationService
    {
        private ModelContext modelContext = new ModelContext();

        private string key = "SYSTEM";

        public ConfigurationServiceImpl()
        {

        }

        public List<Configuration> findAll(int sort, string column)
        {
            List<Configuration> configurations = (from c in modelContext.Configuration
                                                  select c).ToList();

            if (sort == 1 && column == "Id")
            {
                configurations = configurations.OrderBy(c => c.Id).ToList();
            }
            else if (sort == 0 && column == "Id")
            {
                configurations = configurations.OrderByDescending(c => c.Id).ToList();
            }
            else if (sort == 1 && column == "Description")
            {
                configurations = configurations.OrderBy(c => c.Description).ToList();
            }
            else if (sort == 0 && column == "Description")
            {
                configurations = configurations.OrderByDescending(c => c.Description).ToList();
            }
            else if (sort == 1 && column == "Key")
            {
                configurations = configurations.OrderBy(c => c.Key).ToList();
            }
            else if (sort == 0 && column == "Key")
            {
                configurations = configurations.OrderByDescending(c => c.Key).ToList();
            }

            return configurations;
        }

        public List<Configuration> findAllPaged(int page, int sort, string column)
        {
            List<Configuration> configurationss = findByKeyAndStatusEqualToOne(key);

            int size = Convert.ToInt32(configurationss[3].Value);

            List<Configuration> configurations = (from c in modelContext.Configuration
                                                  select c).ToList();

            if (sort == 1 && column == "Id")
            {
                configurations = configurations.OrderBy(c => c.Id).ToList();
            }
            else if (sort == 0 && column == "Id")
            {
                configurations = configurations.OrderByDescending(c => c.Id).ToList();
            }
            else if (sort == 1 && column == "Description")
            {
                configurations = configurations.OrderBy(c => c.Description).ToList();
            }
            else if (sort == 0 && column == "Description")
            {
                configurations = configurations.OrderByDescending(c => c.Description).ToList();
            }
            else if (sort == 1 && column == "Key")
            {
                configurations = configurations.OrderBy(c => c.Key).ToList();
            }
            else if (sort == 0 && column == "Key")
            {
                configurations = configurations.OrderByDescending(c => c.Key).ToList();
            }

            configurations = configurations.ToPagedList(page, size).ToList();

            return configurations;
        }

        public Configuration findByIdAndStatusEqualToOne(long id)
        {
            Configuration configuration = (from c in modelContext.Configuration
                                           where c.Id == id && c.Status == true
                                           select c).FirstOrDefault();

            return configuration;
        }

        public List<Configuration> findByKeyAndStatusEqualToOne(string key)
        {
            List<Configuration> configurations = (from c in modelContext.Configuration
                                                  where c.Key == key && c.Status == true
                                                  select c).ToList();

            return configurations;
        }

        public Configuration updateValueAndReadOnlyByIdAndStatusEqualToOne(Configuration configuration)
        {
            Configuration configurationExist = (from c in modelContext.Configuration
                                                where c.Id == configuration.Id && c.Status == true
                                                select c).FirstOrDefault();

            configurationExist.Value = configuration.Value;
            configurationExist.ReadOnly = configuration.ReadOnly;

            modelContext.SaveChanges();

            return configurationExist;
        }

        public Configuration updateValueByIdAndStatusEqualToOne(Configuration configuration)
        {
            Configuration configurationExisting = (from c in modelContext.Configuration
                                                   where c.Id == configuration.Id && c.Status == true
                                                   select c).FirstOrDefault();

            configurationExisting.Value = configuration.Value;

            modelContext.SaveChanges();

            return configurationExisting;
        }
    }
}
