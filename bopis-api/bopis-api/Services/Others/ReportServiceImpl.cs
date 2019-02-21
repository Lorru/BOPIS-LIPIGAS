using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bopis_api.Models.Bopis;
using OfficeOpenXml;

namespace bopis_api.Services.Others
{
    public class ReportServiceImpl : IReportService
    {

        private ModelContext modelContext = new ModelContext();

        public ReportServiceImpl()
        {

        }

        public List<Order> findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId(string dateStart, string dateFinish, string localId, string userId, string orderStatusId)
        {
            List<Order> orders = null;

            if (localId == "Todos" && userId == "Todos" && orderStatusId == "Todos")
            {
                orders = (from o in modelContext.Order
                        join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                        where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                            Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd"))
                        select o
                      ).ToList();
            }
            else if(localId != "Todos" && userId != "Todos" && orderStatusId != "Todos")
            {

                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              c.LocalId == Convert.ToInt64(localId) && o.UserId == Convert.ToInt64(userId) && o.OrderStatusId == Convert.ToInt64(orderStatusId)
                          select o
                        ).ToList();

            }
            else if (localId != "Todos" && userId == "Todos" && orderStatusId == "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              c.LocalId == Convert.ToInt64(localId)
                          select o
                        ).ToList();

            }
            else if (localId != "Todos" && userId != "Todos" && orderStatusId == "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              c.LocalId == Convert.ToInt64(localId) && o.UserId == Convert.ToInt64(userId) 
                          select o
                        ).ToList();

            }
            else if (localId != "Todos" && userId == "Todos" && orderStatusId != "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              c.LocalId == Convert.ToInt64(localId) && o.OrderStatusId == Convert.ToInt64(orderStatusId)
                          select o
                        ).ToList();

            }
            else if (userId != "Todos" && localId == "Todos" && orderStatusId == "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              o.UserId == Convert.ToInt64(userId)
                          select o
                        ).ToList();

            }
            else if (userId != "Todos" && localId != "Todos" && orderStatusId == "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              c.LocalId == Convert.ToInt64(localId) && o.UserId == Convert.ToInt64(userId)
                          select o
                        ).ToList();

            }
            else if (userId != "Todos" && localId == "Todos" && orderStatusId != "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              o.UserId == Convert.ToInt64(userId) && o.OrderStatusId == Convert.ToInt64(orderStatusId)
                          select o
                        ).ToList();

            }
            else if (orderStatusId != "Todos" && localId == "Todos" && userId == "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              o.OrderStatusId == Convert.ToInt64(orderStatusId)
                          select o
                        ).ToList();

            }
            else if (orderStatusId != "Todos" && localId != "Todos" && userId == "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              c.LocalId == Convert.ToInt64(localId) && o.OrderStatusId == Convert.ToInt64(orderStatusId)
                          select o
                        ).ToList();

            }
            else if (orderStatusId != "Todos" && localId == "Todos" && userId != "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              o.UserId == Convert.ToInt64(userId) && o.OrderStatusId == Convert.ToInt64(orderStatusId)
                          select o
                        ).ToList();

            }


            return orders;
        }

        public string findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusIdExcel(string dateStart, string dateFinish, string localId, string userId, string orderStatusId)
        {

            List<Order> orders = null;

            if (localId == "Todos" && userId == "Todos" && orderStatusId == "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd"))
                          select o
                      ).ToList();
            }
            else if (localId != "Todos" && userId != "Todos" && orderStatusId != "Todos")
            {

                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              c.LocalId == Convert.ToInt64(localId) && o.UserId == Convert.ToInt64(userId) && o.OrderStatusId == Convert.ToInt64(orderStatusId)
                          select o
                        ).ToList();

            }
            else if (localId != "Todos" && userId == "Todos" && orderStatusId == "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              c.LocalId == Convert.ToInt64(localId)
                          select o
                        ).ToList();

            }
            else if (localId != "Todos" && userId != "Todos" && orderStatusId == "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              c.LocalId == Convert.ToInt64(localId) && o.UserId == Convert.ToInt64(userId)
                          select o
                        ).ToList();

            }
            else if (localId != "Todos" && userId == "Todos" && orderStatusId != "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              c.LocalId == Convert.ToInt64(localId) && o.OrderStatusId == Convert.ToInt64(orderStatusId)
                          select o
                        ).ToList();

            }
            else if (userId != "Todos" && localId == "Todos" && orderStatusId == "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              o.UserId == Convert.ToInt64(userId)
                          select o
                        ).ToList();

            }
            else if (userId != "Todos" && localId != "Todos" && orderStatusId == "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              c.LocalId == Convert.ToInt64(localId) && o.UserId == Convert.ToInt64(userId)
                          select o
                        ).ToList();

            }
            else if (userId != "Todos" && localId == "Todos" && orderStatusId != "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              o.UserId == Convert.ToInt64(userId) && o.OrderStatusId == Convert.ToInt64(orderStatusId)
                          select o
                        ).ToList();

            }
            else if (orderStatusId != "Todos" && localId == "Todos" && userId == "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              o.OrderStatusId == Convert.ToInt64(orderStatusId)
                          select o
                        ).ToList();

            }
            else if (orderStatusId != "Todos" && localId != "Todos" && userId == "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              c.LocalId == Convert.ToInt64(localId) && o.OrderStatusId == Convert.ToInt64(orderStatusId)
                          select o
                        ).ToList();

            }
            else if (orderStatusId != "Todos" && localId == "Todos" && userId != "Todos")
            {
                orders = (from o in modelContext.Order
                          join c in modelContext.CylinderByLocal on o.CylinderByLocalId equals c.Id
                          where Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(Convert.ToDateTime(dateStart).ToString("yyyy-MM-dd")) &&
                              Convert.ToDateTime(o.DateOfRequest.ToString("yyyy-MM-dd")) <= Convert.ToDateTime(Convert.ToDateTime(dateFinish).ToString("yyyy-MM-dd")) &&
                              o.UserId == Convert.ToInt64(userId) && o.OrderStatusId == Convert.ToInt64(orderStatusId)
                          select o
                        ).ToList();

            }


            ExcelPackage excelPackage = new ExcelPackage();

            ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("BOPIS");

            excelWorksheet.Cells[1, 1].Value = "Pedido";
            excelWorksheet.Cells[1, 2].Value = "Cliente";
            excelWorksheet.Cells[1, 3].Value = "Rut";
            excelWorksheet.Cells[1, 4].Value = "Nombre completo";
            excelWorksheet.Cells[1, 5].Value = "Fecha entrega";
            excelWorksheet.Cells[1, 6].Value = "Horario Retiro";
            excelWorksheet.Cells[1, 7].Value = "Fecha solicitud";

            int row = 1;

            foreach (Order order in orders)
            {
                row++;
                excelWorksheet.Cells[row, 1].Value = order.Id;
                excelWorksheet.Cells[row, 2].Value = order.ClientAddress;
                excelWorksheet.Cells[row, 3].Value = order.ClientRun;
                excelWorksheet.Cells[row, 4].Value = order.ClientFullName;
                excelWorksheet.Cells[row, 5].Value = order.DateOfDelivery;
                excelWorksheet.Cells[row, 6].Value = order.ScheduleOfRetirement;
                excelWorksheet.Cells[row, 7].Value = order.DateOfRequest;
            }

            excelWorksheet.Cells[excelWorksheet.Dimension.Address].AutoFitColumns();

            byte[] excelByte = excelPackage.GetAsByteArray();

            return Convert.ToBase64String(excelByte);
        }
    }
}
