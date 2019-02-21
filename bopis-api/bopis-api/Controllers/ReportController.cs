using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using bopis_api.Models.Bopis;
using bopis_api.Services.Bopis;
using bopis_api.Services.Others;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bopis_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReportController : ControllerBase
    {
        private ReportServiceImpl reportServiceImpl = new ReportServiceImpl();

        private LogServiceImpl logServiceImpl = new LogServiceImpl();

        private JwtTokenServiceImpl jwtTokenServiceImpl = new JwtTokenServiceImpl();

        /// <summary>
        /// Obtiene todas las ordenes de la tabla Order asociada a los filtros.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="dateStart"></param> 
        /// <param name="dateFinish"></param> 
        /// <param name="localId"></param> 
        /// <param name="userId"></param> 
        /// <param name="orderStatusId"></param> 
        /// <response code="204">Retorna un array vacío del objecto Order.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId/{dateStart}/{dateFinish}/{localId}/{userId}/{orderStatusId}")]
        [ProducesResponseType(200, Type = typeof(List<Order>))]
        [ProducesResponseType(204, Type = typeof(List<Order>))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId([FromHeader(Name = "Authorization")]string token, string dateStart, string dateFinish, string localId, string userId, string orderStatusId)
        {
            try
            {
                if (token == null || token == "")
                {
                    return Ok(new
                    {

                        statusCode = HttpStatusCode.NoContent,
                        message = "El token es requerido."

                    });
                }
                else
                {


                    bool validateToken = jwtTokenServiceImpl.validate(token);

                    if (validateToken)
                    {

                        if (dateStart == null || dateStart == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "La fecha de inicio es requerida."

                            });
                        }
                        else if (dateFinish == null || dateFinish == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "La fecha final es requerida."

                            });
                        }
                        else
                        {
                            List<Order> orders = reportServiceImpl.findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId(dateStart, dateFinish, localId, userId, orderStatusId);

                            if (orders.Count > 0)
                            {
                                return Ok(new
                                {

                                    orders = orders,
                                    countRows = orders.Count,
                                    statusCode = HttpStatusCode.OK

                                });
                            }
                            else
                            {
                                return Ok(new
                                {

                                    orders = orders,
                                    countRows = orders.Count,
                                    statusCode = HttpStatusCode.NoContent

                                });
                            }
                        }

                    }
                    else
                    {
                        return Ok(new
                        {

                            statusCode = HttpStatusCode.Forbidden,
                            message = "El token esta expirado."

                        });
                    }
                }
            }
            catch (Exception exception)
            {

                Log log = new Log();

                log.TypeLogId = 2;
                log.Controller = "ReportController";
                log.Method = "findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusId";
                log.Description = exception.Message;

                logServiceImpl.create(log);

                return Ok(new
                {

                    message = exception.Message,
                    statusCode = HttpStatusCode.InternalServerError

                });
            }
        }

        /// <summary>
        /// Obtiene un string base64 del reporte para exportar a Excel.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="dateStart"></param> 
        /// <param name="dateFinish"></param> 
        /// <param name="localId"></param> 
        /// <param name="userId"></param> 
        /// <param name="orderStatusId"></param> 
        /// <response code="200">Retorna un string con el archivo en base64.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusIdExcel/{dateStart}/{dateFinish}/{localId}/{userId}/{orderStatusId}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(204, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusIdExcel([FromHeader(Name = "Authorization")]string token, string dateStart, string dateFinish, string localId, string userId, string orderStatusId)
        {
            try
            {
                if (token == null || token == "")
                {
                    return Ok(new
                    {

                        statusCode = HttpStatusCode.NoContent,
                        message = "El token es requerido."

                    });
                }
                else
                {


                    bool validateToken = jwtTokenServiceImpl.validate(token);

                    if (validateToken)
                    {

                        if (dateStart == null || dateStart == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "La fecha de inicio es requerida."

                            });
                        }
                        else if (dateFinish == null || dateFinish == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "La fecha final es requerida."

                            });
                        }
                        else
                        {
                            string file = reportServiceImpl.findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusIdExcel(dateStart, dateFinish, localId, userId, orderStatusId);
                            string fileName = "Reporte BOPIS - " + dateStart + " - " + dateFinish;
                            string dataType = "xlsx";

                            return Ok(new
                            {

                                file = file,
                                fileName = fileName,
                                dataType = dataType,
                                statusCode = HttpStatusCode.OK

                            });
                        }
                    }
                    else
                    {
                        return Ok(new
                        {

                            statusCode = HttpStatusCode.Forbidden,
                            message = "El token esta expirado."

                        });
                    }
                }
            }
            catch (Exception exception)
            {

                Log log = new Log();

                log.TypeLogId = 2;
                log.Controller = "ReportController";
                log.Method = "findByDateOfRequestAndLocalsIdAndUsersIdAndOrderStatusIdExcel";
                log.Description = exception.Message;

                logServiceImpl.create(log);

                return Ok(new
                {

                    message = exception.Message,
                    statusCode = HttpStatusCode.InternalServerError

                });
            }
        }
    }
}