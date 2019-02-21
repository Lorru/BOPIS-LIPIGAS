using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bopis_api.Models.Bopis;

namespace bopis_api.Services.Bopis
{
    public class StockServiceImpl : IStockService
    {

        private ModelContext modelContext = new ModelContext();

        private ConfigurationServiceImpl configurationServiceImpl = new ConfigurationServiceImpl();

        private string key = "BD";

        public StockServiceImpl()
        {

        }

        public Stock create(long cylinderByLocalId)
        {
            List<Configuration> configurations = configurationServiceImpl.findByKeyAndStatusEqualToOne(key);

            long Id = Convert.ToInt64(configurations[6].Value);

            Stock stock = new Stock();

            stock.CylinderByLocalId = cylinderByLocalId;
            stock.Id = Id;
            stock.Quantity = 1;
            stock.Status = true;

            modelContext.Stock.Add(stock);
            modelContext.SaveChanges();

            Configuration configuration = new Configuration();

            configuration.Id = configurations[6].Id;
            configuration.Value = (Id + 1).ToString();

            configurationServiceImpl.updateValueByIdAndStatusEqualToOne(configuration);


            return stock;
        }

        public Stock findByCylinderByLocalIdAndStatusEqualToOne(long cylinderByLocalId)
        {
            Stock stock = (from s in modelContext.Stock
                           where s.CylinderByLocalId == cylinderByLocalId && s.Status == true
                           select s).FirstOrDefault();

            return stock;
        }

        public List<Stock> findByLocalId(long localId)
        {
            List<Stock> stocks = (from s in modelContext.Stock
                                  join cl in modelContext.CylinderByLocal on s.CylinderByLocalId equals cl.Id
                                  join c in modelContext.Cylinder on cl.CylinderId equals c.Id
                                  join l in modelContext.Local on cl.LocalId equals l.Id
                                  where l.Id == localId
                                  select new Stock()
                                  {
                                      CylinderByLocal = new CylinderByLocal()
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
                                          Local = new Local()
                                          {
                                              Id = l.Id,
                                              Latitude = l.Latitude,
                                              Length = l.Length,
                                              Name = l.Name,
                                              Radio = l.Radio,
                                              Open = l.Open,
                                              Status = l.Status
                                          },
                                          LocalId = cl.LocalId,
                                          Status = cl.Status,
                                          ZonePrice = cl.ZonePrice

                                      },
                                      CylinderByLocalId = s.CylinderByLocalId,
                                      Id = s.Id,
                                      Quantity = s.Quantity,
                                      Status = s.Status
                                  }).ToList();


            return stocks;
        }

        public Stock updateQuantityByCylinderByLocalIdAndStatusEqualToOne(Stock stock)
        {
            Stock stockExisting = (from s in modelContext.Stock
                                   where s.CylinderByLocalId == stock.CylinderByLocalId && s.Status == true
                                   select s).FirstOrDefault();

            stockExisting.Quantity = stockExisting.Quantity - stock.Quantity;

            CylinderByLocal cylinderByLocal = (from cl in modelContext.CylinderByLocal
                                               where cl.Id == stockExisting.CylinderByLocalId
                                               select cl).FirstOrDefault();

            if (stockExisting.Quantity <= 0)
            {

                cylinderByLocal.Status = false;
            }
            else
            {
                cylinderByLocal.Status = true;
            }

            modelContext.SaveChanges();

            return stockExisting;
        }

        public Stock updateQuantityByIdAndStatusEqualToOne(Stock stock)
        {
            Stock stockExisting = (from s in modelContext.Stock
                                where s.Id == stock.Id && s.Status == true
                                select s).FirstOrDefault();

            stockExisting.Quantity = stock.Quantity;

            CylinderByLocal cylinderByLocal = (from cl in modelContext.CylinderByLocal
                                               where cl.Id == stockExisting.CylinderByLocalId
                                               select cl).FirstOrDefault();

            if (stockExisting.Quantity <= 0)
            {

                cylinderByLocal.Status = false;
            }
            else
            {
                cylinderByLocal.Status = true;
            }

            modelContext.SaveChanges();

            return stockExisting;
        }
    }
}
