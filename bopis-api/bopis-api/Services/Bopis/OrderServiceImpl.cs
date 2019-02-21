using bopis_api.Models.Bopis;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopis_api.Services.Bopis
{
    public class OrderServiceImpl : IOrderService
    {
        private ModelContext modelContext = new ModelContext();

        private ConfigurationServiceImpl configurationServiceImpl = new ConfigurationServiceImpl();

        private StockServiceImpl stockServiceImpl = new StockServiceImpl();

        private string key = "SYSTEM";

        private string key2 = "BD";

        public OrderServiceImpl()
        {

        }

        public Order create(Order order)
        {
            List<Configuration> configurations = configurationServiceImpl.findByKeyAndStatusEqualToOne(key2);

            long Id = Convert.ToInt64(configurations[2].Value);

            order.Id = Id;
            order.OrderStatusId = 2;
            order.DateOfRequest = DateTime.Now;

            modelContext.Order.Add(order);
            modelContext.SaveChanges();

            Stock stock = new Stock();

            stock.CylinderByLocalId = order.CylinderByLocalId;
            stock.Quantity = order.Quantity;

            stockServiceImpl.updateQuantityByCylinderByLocalIdAndStatusEqualToOne(stock);

            Configuration configuration = new Configuration();

            configuration.Id = configurations[2].Id;
            configuration.Value = (Id + 1).ToString();

            configurationServiceImpl.updateValueByIdAndStatusEqualToOne(configuration);

            return order;

        }

        public Order deleteById(long id)
        {
            Order order = (from o in modelContext.Order
                           where o.Id == id
                           select o).FirstOrDefault();

            order.OrderStatusId = 3;

            modelContext.SaveChanges();

            return order;
        }

        public List<Order> findByIdAndClientRun(long id, string runClient, int sort, string column)
        {
            List<Order> orders = null;

            if (id != 0 && runClient == null)
            {
                orders = (from o in modelContext.Order
                          join cl in modelContext.CylinderByLocal on o.CylinderByLocalId equals cl.Id
                          join c in modelContext.Cylinder on cl.CylinderId equals c.Id
                          where o.Id == id
                          select new Order()
                          {
                              ClientAddress = o.ClientAddress,
                              ClientFullName = o.ClientFullName,
                              ClientRun = o.ClientRun,
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
                                  LocalId = cl.LocalId,
                                  Status = cl.Status,
                                  ZonePrice = cl.ZonePrice
                              },
                              CylinderByLocalId = o.CylinderByLocalId,
                              DateOfDelivery = o.DateOfDelivery,
                              DateOfRequest = o.DateOfRequest,
                              Id = o.Id,
                              OrderStatusId = o.OrderStatusId,
                              Quantity = o.Quantity,
                              ScheduleOfRetirement = o.ScheduleOfRetirement,
                              UserId = o.UserId
                          }).ToList();
            }
            else if (runClient != null && id == 0)
            {
                orders = (from o in modelContext.Order
                          join cl in modelContext.CylinderByLocal on o.CylinderByLocalId equals cl.Id
                          join c in modelContext.Cylinder on cl.CylinderId equals c.Id
                          where o.ClientRun == runClient
                          select new Order()
                          {
                              ClientAddress = o.ClientAddress,
                              ClientFullName = o.ClientFullName,
                              ClientRun = o.ClientRun,
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
                                  LocalId = cl.LocalId,
                                  Status = cl.Status,
                                  ZonePrice = cl.ZonePrice
                              },
                              CylinderByLocalId = o.CylinderByLocalId,
                              DateOfDelivery = o.DateOfDelivery,
                              DateOfRequest = o.DateOfRequest,
                              Id = o.Id,
                              OrderStatusId = o.OrderStatusId,
                              Quantity = o.Quantity,
                              ScheduleOfRetirement = o.ScheduleOfRetirement,
                              UserId = o.UserId
                          }).ToList();
            }
            else if (runClient != null && id != 0)
            {
                orders = (from o in modelContext.Order
                          join cl in modelContext.CylinderByLocal on o.CylinderByLocalId equals cl.Id
                          join c in modelContext.Cylinder on cl.CylinderId equals c.Id
                          where o.ClientRun == runClient && o.Id == id
                          select new Order()
                          {
                              ClientAddress = o.ClientAddress,
                              ClientFullName = o.ClientFullName,
                              ClientRun = o.ClientRun,
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
                                  LocalId = cl.LocalId,
                                  Status = cl.Status,
                                  ZonePrice = cl.ZonePrice
                              },
                              CylinderByLocalId = o.CylinderByLocalId,
                              DateOfDelivery = o.DateOfDelivery,
                              DateOfRequest = o.DateOfRequest,
                              Id = o.Id,
                              OrderStatusId = o.OrderStatusId,
                              Quantity = o.Quantity,
                              ScheduleOfRetirement = o.ScheduleOfRetirement,
                              UserId = o.UserId
                          }).ToList();
            }
            else
            {
                orders = (from o in modelContext.Order
                          join cl in modelContext.CylinderByLocal on o.CylinderByLocalId equals cl.Id
                          join c in modelContext.Cylinder on cl.CylinderId equals c.Id
                          select new Order()
                          {
                              ClientAddress = o.ClientAddress,
                              ClientFullName = o.ClientFullName,
                              ClientRun = o.ClientRun,
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
                                  LocalId = cl.LocalId,
                                  Status = cl.Status,
                                  ZonePrice = cl.ZonePrice
                              },
                              CylinderByLocalId = o.CylinderByLocalId,
                              DateOfDelivery = o.DateOfDelivery,
                              DateOfRequest = o.DateOfRequest,
                              Id = o.Id,
                              OrderStatusId = o.OrderStatusId,
                              Quantity = o.Quantity,
                              ScheduleOfRetirement = o.ScheduleOfRetirement,
                              UserId = o.UserId
                          }).ToList();
            }

            if (sort == 1 && column == "Id")
            {
                orders = orders.OrderBy(o => o.Id).ToList();
            }
            else if (sort == 0 && column == "Id")
            {
                orders = orders.OrderByDescending(o => o.Id).ToList();
            }
            else if (sort == 1 && column == "ClientAddress")
            {
                orders = orders.OrderBy(o => o.ClientAddress).ToList();
            }
            else if (sort == 0 && column == "ClientAddress")
            {
                orders = orders.OrderByDescending(o => o.ClientAddress).ToList();
            }
            else if (sort == 1 && column == "ClientRun")
            {
                orders = orders.OrderBy(o => o.ClientRun).ToList();
            }
            else if (sort == 0 && column == "ClientRun")
            {
                orders = orders.OrderByDescending(o => o.ClientRun).ToList();
            }
            else if (sort == 1 && column == "ClientFullName")
            {
                orders = orders.OrderBy(o => o.ClientFullName).ToList();
            }
            else if (sort == 0 && column == "ClientFullName")
            {
                orders = orders.OrderByDescending(o => o.ClientFullName).ToList();
            }
            else if (sort == 1 && column == "DateOfDelivery")
            {
                orders = orders.OrderBy(o => o.DateOfDelivery).ToList();
            }
            else if (sort == 0 && column == "DateOfDelivery")
            {
                orders = orders.OrderByDescending(o => o.DateOfDelivery).ToList();
            }
            else if (sort == 1 && column == "ScheduleOfRetirement")
            {
                orders = orders.OrderBy(o => o.ScheduleOfRetirement).ToList();
            }
            else if (sort == 0 && column == "ScheduleOfRetirement")
            {
                orders = orders.OrderByDescending(o => o.ScheduleOfRetirement).ToList();
            }
            else if (sort == 1 && column == "DateOfRequest")
            {
                orders = orders.OrderBy(o => o.DateOfRequest).ToList();
            }
            else if (sort == 0 && column == "DateOfRequest")
            {
                orders = orders.OrderByDescending(o => o.DateOfRequest).ToList();
            }

            return orders;
        }

        public List<Order> findByIdAndClientRunPaged(int page, long id, string runClient, int sort, string column)
        {
            List<Configuration> configurations = configurationServiceImpl.findByKeyAndStatusEqualToOne(key);

            int size = Convert.ToInt32(configurations[3].Value);

            List<Order> orders = null;

            if (id != 0 && runClient == null)
            {
                orders = (from o in modelContext.Order
                          join cl in modelContext.CylinderByLocal on o.CylinderByLocalId equals cl.Id
                          join c in modelContext.Cylinder on cl.CylinderId equals c.Id
                          where o.Id == id
                          select new Order()
                          {
                              ClientAddress = o.ClientAddress,
                              ClientFullName = o.ClientFullName,
                              ClientRun = o.ClientRun,
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
                                  LocalId = cl.LocalId,
                                  Status = cl.Status,
                                  ZonePrice = cl.ZonePrice
                              },
                              CylinderByLocalId = o.CylinderByLocalId,
                              DateOfDelivery = o.DateOfDelivery,
                              DateOfRequest = o.DateOfRequest,
                              Id = o.Id,
                              OrderStatusId = o.OrderStatusId,
                              Quantity = o.Quantity,
                              ScheduleOfRetirement = o.ScheduleOfRetirement,
                              UserId = o.UserId
                          }).ToList();
            }
            else if (runClient != null && id == 0)
            {
                orders = (from o in modelContext.Order
                          join cl in modelContext.CylinderByLocal on o.CylinderByLocalId equals cl.Id
                          join c in modelContext.Cylinder on cl.CylinderId equals c.Id
                          where o.ClientRun == runClient
                          select new Order()
                          {
                              ClientAddress = o.ClientAddress,
                              ClientFullName = o.ClientFullName,
                              ClientRun = o.ClientRun,
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
                                  LocalId = cl.LocalId,
                                  Status = cl.Status,
                                  ZonePrice = cl.ZonePrice
                              },
                              CylinderByLocalId = o.CylinderByLocalId,
                              DateOfDelivery = o.DateOfDelivery,
                              DateOfRequest = o.DateOfRequest,
                              Id = o.Id,
                              OrderStatusId = o.OrderStatusId,
                              Quantity = o.Quantity,
                              ScheduleOfRetirement = o.ScheduleOfRetirement,
                              UserId = o.UserId
                          }).ToList();
            }
            else if (runClient != null && id != 0)
            {
                orders = (from o in modelContext.Order
                          join cl in modelContext.CylinderByLocal on o.CylinderByLocalId equals cl.Id
                          join c in modelContext.Cylinder on cl.CylinderId equals c.Id
                          where o.ClientRun == runClient && o.Id == id
                          select new Order()
                          {
                              ClientAddress = o.ClientAddress,
                              ClientFullName = o.ClientFullName,
                              ClientRun = o.ClientRun,
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
                                  LocalId = cl.LocalId,
                                  Status = cl.Status,
                                  ZonePrice = cl.ZonePrice
                              },
                              CylinderByLocalId = o.CylinderByLocalId,
                              DateOfDelivery = o.DateOfDelivery,
                              DateOfRequest = o.DateOfRequest,
                              Id = o.Id,
                              OrderStatusId = o.OrderStatusId,
                              Quantity = o.Quantity,
                              ScheduleOfRetirement = o.ScheduleOfRetirement,
                              UserId = o.UserId
                          }).ToList();
            }
            else
            {
                orders = (from o in modelContext.Order
                          join cl in modelContext.CylinderByLocal on o.CylinderByLocalId equals cl.Id
                          join c in modelContext.Cylinder on cl.CylinderId equals c.Id
                          select new Order()
                          {
                              ClientAddress = o.ClientAddress,
                              ClientFullName = o.ClientFullName,
                              ClientRun = o.ClientRun,
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
                                  LocalId = cl.LocalId,
                                  Status = cl.Status,
                                  ZonePrice = cl.ZonePrice
                              },
                              CylinderByLocalId = o.CylinderByLocalId,
                              DateOfDelivery = o.DateOfDelivery,
                              DateOfRequest = o.DateOfRequest,
                              Id = o.Id,
                              OrderStatusId = o.OrderStatusId,
                              Quantity = o.Quantity,
                              ScheduleOfRetirement = o.ScheduleOfRetirement,
                              UserId = o.UserId
                          }).ToList();
            }

            if (sort == 1 && column == "Id")
            {
                orders = orders.OrderBy(o => o.Id).ToList();
            }
            else if (sort == 0 && column == "Id")
            {
                orders = orders.OrderByDescending(o => o.Id).ToList();
            }
            else if (sort == 1 && column == "ClientAddress")
            {
                orders = orders.OrderBy(o => o.ClientAddress).ToList();
            }
            else if (sort == 0 && column == "ClientAddress")
            {
                orders = orders.OrderByDescending(o => o.ClientAddress).ToList();
            }
            else if (sort == 1 && column == "ClientRun")
            {
                orders = orders.OrderBy(o => o.ClientRun).ToList();
            }
            else if (sort == 0 && column == "ClientRun")
            {
                orders = orders.OrderByDescending(o => o.ClientRun).ToList();
            }
            else if (sort == 1 && column == "ClientFullName")
            {
                orders = orders.OrderBy(o => o.ClientFullName).ToList();
            }
            else if (sort == 0 && column == "ClientFullName")
            {
                orders = orders.OrderByDescending(o => o.ClientFullName).ToList();
            }
            else if (sort == 1 && column == "DateOfDelivery")
            {
                orders = orders.OrderBy(o => o.DateOfDelivery).ToList();
            }
            else if (sort == 0 && column == "DateOfDelivery")
            {
                orders = orders.OrderByDescending(o => o.DateOfDelivery).ToList();
            }
            else if (sort == 1 && column == "ScheduleOfRetirement")
            {
                orders = orders.OrderBy(o => o.ScheduleOfRetirement).ToList();
            }
            else if (sort == 0 && column == "ScheduleOfRetirement")
            {
                orders = orders.OrderByDescending(o => o.ScheduleOfRetirement).ToList();
            }
            else if (sort == 1 && column == "DateOfRequest")
            {
                orders = orders.OrderBy(o => o.DateOfRequest).ToList();
            }
            else if (sort == 0 && column == "DateOfRequest")
            {
                orders = orders.OrderByDescending(o => o.DateOfRequest).ToList();
            }

            orders = orders.ToPagedList(page, size).ToList();

            return orders;
        }

        public List<Order> findByLocalId(long localId, string filter, int sort, string column)
        {
            List<Order> orders = null;

            if (filter != null)
            {
                orders = (from o in modelContext.Order
                          join cl in modelContext.CylinderByLocal on o.CylinderByLocalId equals cl.Id
                          join c in modelContext.Cylinder on cl.CylinderId equals c.Id
                          where cl.LocalId == localId && (o.ClientRun.Contains(filter) || o.ClientFullName.Contains(filter))
                          select new Order()
                          {
                              ClientAddress = o.ClientAddress,
                              ClientFullName = o.ClientFullName,
                              ClientRun = o.ClientRun,
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
                                  LocalId = cl.LocalId,
                                  Status = cl.Status,
                                  ZonePrice = cl.ZonePrice
                              },
                              CylinderByLocalId = o.CylinderByLocalId,
                              DateOfDelivery = o.DateOfDelivery,
                              DateOfRequest = o.DateOfRequest,
                              Id = o.Id,
                              OrderStatusId = o.OrderStatusId,
                              Quantity = o.Quantity,
                              ScheduleOfRetirement = o.ScheduleOfRetirement,
                              UserId = o.UserId
                          }).ToList();
            }
            else
            {
                orders = (from o in modelContext.Order
                          join cl in modelContext.CylinderByLocal on o.CylinderByLocalId equals cl.Id
                          join c in modelContext.Cylinder on cl.CylinderId equals c.Id
                          where cl.LocalId == localId
                          select new Order()
                          {
                              ClientAddress = o.ClientAddress,
                              ClientFullName = o.ClientFullName,
                              ClientRun = o.ClientRun,
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
                                  LocalId = cl.LocalId,
                                  Status = cl.Status,
                                  ZonePrice = cl.ZonePrice
                              },
                              CylinderByLocalId = o.CylinderByLocalId,
                              DateOfDelivery = o.DateOfDelivery,
                              DateOfRequest = o.DateOfRequest,
                              Id = o.Id,
                              OrderStatusId = o.OrderStatusId,
                              Quantity = o.Quantity,
                              ScheduleOfRetirement = o.ScheduleOfRetirement,
                              UserId = o.UserId
                          }).ToList();
            }

            if (sort == 1 && column == "Id")
            {
                orders = orders.OrderBy(o => o.Id).ToList();
            }
            else if (sort == 0 && column == "Id")
            {
                orders = orders.OrderByDescending(o => o.Id).ToList();
            }
            else if (sort == 1 && column == "ClientAddress")
            {
                orders = orders.OrderBy(o => o.ClientAddress).ToList();
            }
            else if (sort == 0 && column == "ClientAddress")
            {
                orders = orders.OrderByDescending(o => o.ClientAddress).ToList();
            }
            else if (sort == 1 && column == "ClientRun")
            {
                orders = orders.OrderBy(o => o.ClientRun).ToList();
            }
            else if (sort == 0 && column == "ClientRun")
            {
                orders = orders.OrderByDescending(o => o.ClientRun).ToList();
            }
            else if (sort == 1 && column == "ClientFullName")
            {
                orders = orders.OrderBy(o => o.ClientFullName).ToList();
            }
            else if (sort == 0 && column == "ClientFullName")
            {
                orders = orders.OrderByDescending(o => o.ClientFullName).ToList();
            }
            else if (sort == 1 && column == "DateOfDelivery")
            {
                orders = orders.OrderBy(o => o.DateOfDelivery).ToList();
            }
            else if (sort == 0 && column == "DateOfDelivery")
            {
                orders = orders.OrderByDescending(o => o.DateOfDelivery).ToList();
            }
            else if (sort == 1 && column == "ScheduleOfRetirement")
            {
                orders = orders.OrderBy(o => o.ScheduleOfRetirement).ToList();
            }
            else if (sort == 0 && column == "ScheduleOfRetirement")
            {
                orders = orders.OrderByDescending(o => o.ScheduleOfRetirement).ToList();
            }
            else if (sort == 1 && column == "DateOfRequest")
            {
                orders = orders.OrderBy(o => o.DateOfRequest).ToList();
            }
            else if (sort == 0 && column == "DateOfRequest")
            {
                orders = orders.OrderByDescending(o => o.DateOfRequest).ToList();
            }

            return orders;
        }

        public List<Order> findByLocalIdPaged(int page, long localId, string filter, int sort, string column)
        {
            List<Configuration> configurations = configurationServiceImpl.findByKeyAndStatusEqualToOne(key);

            int size = Convert.ToInt32(configurations[3].Value);

            List<Order> orders = null;

            if (filter != null)
            {
                orders = (from o in modelContext.Order
                          join cl in modelContext.CylinderByLocal on o.CylinderByLocalId equals cl.Id
                          join c in modelContext.Cylinder on cl.CylinderId equals c.Id
                          where cl.LocalId == localId && (o.ClientRun.Contains(filter) || o.ClientFullName.Contains(filter))
                          select new Order()
                          {
                              ClientAddress = o.ClientAddress,
                              ClientFullName = o.ClientFullName,
                              ClientRun = o.ClientRun,
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
                                  LocalId = cl.LocalId,
                                  Status = cl.Status,
                                  ZonePrice = cl.ZonePrice
                              },
                              CylinderByLocalId = o.CylinderByLocalId,
                              DateOfDelivery = o.DateOfDelivery,
                              DateOfRequest = o.DateOfRequest,
                              Id = o.Id,
                              OrderStatusId = o.OrderStatusId,
                              Quantity = o.Quantity,
                              ScheduleOfRetirement = o.ScheduleOfRetirement,
                              UserId = o.UserId
                          }).ToList();
            }
            else
            {
                orders = (from o in modelContext.Order
                          join cl in modelContext.CylinderByLocal on o.CylinderByLocalId equals cl.Id
                          join c in modelContext.Cylinder on cl.CylinderId equals c.Id
                          where cl.LocalId == localId
                          select new Order()
                          {
                              ClientAddress = o.ClientAddress,
                              ClientFullName = o.ClientFullName,
                              ClientRun = o.ClientRun,
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
                                  LocalId = cl.LocalId,
                                  Status = cl.Status,
                                  ZonePrice = cl.ZonePrice
                              },
                              CylinderByLocalId = o.CylinderByLocalId,
                              DateOfDelivery = o.DateOfDelivery,
                              DateOfRequest = o.DateOfRequest,
                              Id = o.Id,
                              OrderStatusId = o.OrderStatusId,
                              Quantity = o.Quantity,
                              ScheduleOfRetirement = o.ScheduleOfRetirement,
                              UserId = o.UserId
                          }).ToList();
            }

            if (sort == 1 && column == "Id")
            {
                orders = orders.OrderBy(o => o.Id).ToList();
            }
            else if (sort == 0 && column == "Id")
            {
                orders = orders.OrderByDescending(o => o.Id).ToList();
            }
            else if (sort == 1 && column == "ClientAddress")
            {
                orders = orders.OrderBy(o => o.ClientAddress).ToList();
            }
            else if (sort == 0 && column == "ClientAddress")
            {
                orders = orders.OrderByDescending(o => o.ClientAddress).ToList();
            }
            else if (sort == 1 && column == "ClientRun")
            {
                orders = orders.OrderBy(o => o.ClientRun).ToList();
            }
            else if (sort == 0 && column == "ClientRun")
            {
                orders = orders.OrderByDescending(o => o.ClientRun).ToList();
            }
            else if (sort == 1 && column == "ClientFullName")
            {
                orders = orders.OrderBy(o => o.ClientFullName).ToList();
            }
            else if (sort == 0 && column == "ClientFullName")
            {
                orders = orders.OrderByDescending(o => o.ClientFullName).ToList();
            }
            else if (sort == 1 && column == "DateOfDelivery")
            {
                orders = orders.OrderBy(o => o.DateOfDelivery).ToList();
            }
            else if (sort == 0 && column == "DateOfDelivery")
            {
                orders = orders.OrderByDescending(o => o.DateOfDelivery).ToList();
            }
            else if (sort == 1 && column == "ScheduleOfRetirement")
            {
                orders = orders.OrderBy(o => o.ScheduleOfRetirement).ToList();
            }
            else if (sort == 0 && column == "ScheduleOfRetirement")
            {
                orders = orders.OrderByDescending(o => o.ScheduleOfRetirement).ToList();
            }
            else if (sort == 1 && column == "DateOfRequest")
            {
                orders = orders.OrderBy(o => o.DateOfRequest).ToList();
            }
            else if (sort == 0 && column == "DateOfRequest")
            {
                orders = orders.OrderByDescending(o => o.DateOfRequest).ToList();
            }

            orders = orders.ToPagedList(page, size).ToList();

            return orders;
        }

        public int findByOrderStatusIdAndLocalIdCount(long orderStatusId, long localId)
        {
            List<Order> orders = (from o in modelContext.Order
                                  join cl in modelContext.CylinderByLocal on o.CylinderByLocalId equals cl.Id
                                  where o.OrderStatusId == orderStatusId && cl.Id == localId
                                  select o).ToList();

            return orders.Count;
        }

        public int findByOrderStatusIdCount(long orderStatusId)
        {
            List<Order> orders = (from o in modelContext.Order
                                  where o.OrderStatusId == orderStatusId
                                  select o).ToList();

            return orders.Count;
        }

        public Order updateById(Order order)
        {
            Order orderExisting = (from o in modelContext.Order
                                where o.Id == order.Id
                                select o).FirstOrDefault();

            orderExisting.OrderStatusId = 1;
            orderExisting.DateOfDelivery = DateTime.Now;
            orderExisting.ScheduleOfRetirement = DateTime.Now.Hour + ":" + DateTime.Now.Minute;

            modelContext.SaveChanges();

            return orderExisting;
        }
    }
}
