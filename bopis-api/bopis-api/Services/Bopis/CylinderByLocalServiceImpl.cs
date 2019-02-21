using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bopis_api.Models.Bopis;

namespace bopis_api.Services.Bopis
{
    public class CylinderByLocalServiceImpl : ICylinderByLocalService
    {

        private ModelContext modelContext = new ModelContext();

        private ConfigurationServiceImpl configurationServiceImpl = new ConfigurationServiceImpl();

        private StockServiceImpl stockServiceImpl = new StockServiceImpl();

        private string key = "BD";

        public CylinderByLocalServiceImpl()
        {

        }

        public CylinderByLocal create(long localId, long cylinderId)
        {
            List<Configuration> configurations = configurationServiceImpl.findByKeyAndStatusEqualToOne(key);

            long Id = Convert.ToInt64(configurations[5].Value);
            int ZonePrice = 0;
            int FinalPrice = 0;


            if (cylinderId == 1)
            {
                ZonePrice = 16000;
                FinalPrice = (25 * ZonePrice) / 100;
            }
            else if (cylinderId == 2)
            {
                ZonePrice = 9000;
                FinalPrice = (25 * ZonePrice) / 100;
            }
            else if (cylinderId == 3)
            {
                ZonePrice = 14000;
                FinalPrice = (25 * ZonePrice) / 100;
            }
            else if (cylinderId == 4)
            {
                ZonePrice = 24000;
                FinalPrice = (25 * ZonePrice) / 100;
            }
            else if (cylinderId == 5)
            {
                ZonePrice = 54000;
                FinalPrice = (25 * ZonePrice) / 100;
            }

            CylinderByLocal cylinderByLocal = new CylinderByLocal();

            cylinderByLocal.CylinderId = cylinderId;
            cylinderByLocal.Discount = 25;
            cylinderByLocal.Id = Id;
            cylinderByLocal.LocalId = localId;
            cylinderByLocal.Status = true;
            cylinderByLocal.ZonePrice = ZonePrice;
            cylinderByLocal.FinalPrice = FinalPrice;

            modelContext.CylinderByLocal.Add(cylinderByLocal);
            modelContext.SaveChanges();

            Configuration configuration = new Configuration();

            configuration.Id = configurations[5].Id;
            configuration.Value = (Id + 1).ToString();

            configurationServiceImpl.updateValueByIdAndStatusEqualToOne(configuration);

            stockServiceImpl.create(Id);

            return cylinderByLocal;
        }

        public List<CylinderByLocal> findByLocalIdAndStatusEqualToOne(long localId)
        {
            List<CylinderByLocal> cylinderByLocals = (from cl in modelContext.CylinderByLocal
                                                      join c in modelContext.Cylinder on cl.CylinderId equals c.Id
                                                      join l in modelContext.Local on cl.LocalId equals l.Id
                                                      where cl.LocalId == localId && cl.Status == true
                                                      select new CylinderByLocal()
                                                      {
                                                          Cylinder = new Cylinder()
                                                          {
                                                              Id = c.Id,
                                                              Image = c.Image,
                                                              Kg = c.Kg,
                                                              Name = c.Name,
                                                              Status = c.Status
                                                          },
                                                          CylinderId = cl.CylinderId,
                                                          Discount = cl.Discount,
                                                          FinalPrice = cl.FinalPrice,
                                                          Id = cl.Id,
                                                          LocalId = cl.LocalId,
                                                          ZonePrice = cl.ZonePrice,
                                                          Status = cl.Status,
                                                          Local = new Local()
                                                          {
                                                              Id = l.Id,
                                                              Latitude = l.Latitude,
                                                              Length = l.Length,
                                                              Name = l.Name,
                                                              Open = l.Open,
                                                              Radio = l.Radio,
                                                              Status = l.Status
                                                          }
                                                      }).ToList();

            return cylinderByLocals;
        }

        public CylinderByLocal updateByIdAndStatusEqualToOne(CylinderByLocal cylinderByLocal)
        {
            CylinderByLocal cylinderByLocalExist = (from cl in modelContext.CylinderByLocal
                                                    where cl.Id == cylinderByLocal.Id && cl.Status == true
                                                    select cl).FirstOrDefault();

            cylinderByLocalExist.Discount = cylinderByLocal.Discount;
            cylinderByLocalExist.FinalPrice = cylinderByLocal.FinalPrice;
            cylinderByLocalExist.ZonePrice = cylinderByLocal.ZonePrice;

            modelContext.SaveChanges();

            return cylinderByLocalExist;
        }

        public CylinderByLocal updateStatusFindById(CylinderByLocal cylinderByLocal)
        {
            CylinderByLocal cylinderByLocalExist = (from cl in modelContext.CylinderByLocal
                                                    where cl.Id == cylinderByLocal.Id
                                                    select cl).FirstOrDefault();

            cylinderByLocalExist.Status = cylinderByLocal.Status;

            modelContext.SaveChanges();

            return cylinderByLocal;
        }
    }
}
