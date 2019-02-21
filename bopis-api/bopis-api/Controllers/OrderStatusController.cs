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
    public class OrderStatusController : ControllerBase
    {

        private OrderStatusServiceImpl orderStatusServiceImpl = new OrderStatusServiceImpl();

        private LogServiceImpl logServiceImpl = new LogServiceImpl();

        private JwtTokenServiceImpl jwtTokenServiceImpl = new JwtTokenServiceImpl();

        /// <summary>
        /// Obtiene todos los usuario de la tabla OrderStatus donde status es igual a 1.
        /// </summary>
        /// <param name="token"></param> 
        /// <response code="200">Retorna un array del objecto OrderStatus.</response>
        /// <response code="204">Retorna un array vacío del objeto OrderStatus.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findAllStatusEqualToOne")]
        [ProducesResponseType(200, Type = typeof(List<OrderStatus>))]
        [ProducesResponseType(204, Type = typeof(List<OrderStatus>))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult findAllStatusEqualToOne([FromHeader(Name = "Authorization")]string token)
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
                        List<OrderStatus> orderStatuses = orderStatusServiceImpl.findAllStatusEqualToOne();

                        if (orderStatuses.Count > 0)
                        {
                            return Ok(new
                            {

                                orderStatuses = orderStatuses,
                                countRows = orderStatuses.Count,
                                statusCode = HttpStatusCode.OK

                            });
                        }
                        else
                        {
                            return Ok(new
                            {

                                orderStatuses = orderStatuses,
                                countRows = orderStatuses.Count,
                                statusCode = HttpStatusCode.NoContent

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
                log.Controller = "OrderStatusController";
                log.Method = "findAllStatusEqualToOne";
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