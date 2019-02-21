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
    public class OrderController : ControllerBase
    {
        private OrderServiceImpl orderServiceImpl = new OrderServiceImpl();

        private LogServiceImpl logServiceImpl = new LogServiceImpl();

        private JwtTokenServiceImpl jwtTokenServiceImpl = new JwtTokenServiceImpl();

        /// <summary>
        /// Obtiene el total de ordenes Entregadas, Pendientes y Anuladas por el Local.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="localId"></param> 
        /// <response code="200">Retorna un entero del total de Ordenes por el Local.</response>
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findByOrderStatusIdAndLocalIdCount/{localId}")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult findByOrderStatusIdAndLocalIdCount([FromHeader(Name = "Authorization")]string token, long localId)
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
                        int delivered = orderServiceImpl.findByOrderStatusIdAndLocalIdCount(1, localId);
                        int slopes = orderServiceImpl.findByOrderStatusIdAndLocalIdCount(2, localId);
                        int canceled = orderServiceImpl.findByOrderStatusIdAndLocalIdCount(3, localId);

                        return Ok(new
                        {
                            delivered = delivered,
                            slopes = slopes,
                            canceled = canceled,
                            statusCode = HttpStatusCode.OK

                        });
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
                log.Controller = "OrderController";
                log.Method = "findByOrderStatusIdAndLocalIdCount";
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
        /// Obtiene el total de ordenes Entregadas, Pendientes y Anuladas.
        /// </summary>
        /// <param name="token"></param>
        /// <response code="200">Retorna un entero del total de Ordenes por el Local.</response>
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findByOrderStatusIdCount")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult findByOrderStatusIdCount([FromHeader(Name = "Authorization")]string token)
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
                        int delivered = orderServiceImpl.findByOrderStatusIdCount(1);
                        int slopes = orderServiceImpl.findByOrderStatusIdCount(2);
                        int canceled = orderServiceImpl.findByOrderStatusIdCount(3);

                        return Ok(new
                        {
                            delivered = delivered,
                            slopes = slopes,
                            canceled = canceled,
                            statusCode = HttpStatusCode.OK

                        });
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
                log.Controller = "OrderController";
                log.Method = "findByOrderStatusIdCount";
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
        /// Obtiene todas las ordenes por el Local de la tabla Order.
        /// </summary>
        /// <param name="token"></param>  
        /// <param name="localId"></param> 
        /// <param name="page">Predeterminado 1.</param>
        /// <param name="filter">Filtros disponibles por ClientRun, ClientFullName.</param> 
        /// <param name="sort">1 = orderBy, 2 = orderByDescending, predeterminado 1.</param> 
        /// <param name="column">Orden por columnas disponibles : Id, ClientAddress, ClientRun, ClientFullName, DateOfDelivery, ScheduleOfRetirement, DateOfRequest</param> 
        /// <response code="200">Retorna un array del objecto Order de la tabla Order.</response>
        /// <response code="204">Retorna un array vacío del objecto Order.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findByLocalIdPaged/{page}/{localId}")]
        [ProducesResponseType(200, Type = typeof(List<Order>))]
        [ProducesResponseType(204, Type = typeof(List<Order>))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult findByLocalIdPaged([FromHeader(Name = "Authorization")]string token, long localId, int page = 1, string filter = null, int sort = 1, string column = "Id")
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
                        List<Order> orders = orderServiceImpl.findByLocalIdPaged(page, localId, filter, sort, column);
                        List<Order> ordersCount = orderServiceImpl.findByLocalId(localId, filter, sort, column);

                        if (orders.Count > 0)
                        {
                            return Ok(new
                            {

                                orders = orders,
                                countRows = ordersCount.Count,
                                countRowsByPage = orders.Count,
                                currentPage = page,
                                statusCode = HttpStatusCode.OK

                            });
                        }
                        else
                        {
                            return Ok(new
                            {

                                orders = orders,
                                countRows = ordersCount.Count,
                                countRowsByPage = orders.Count,
                                currentPage = page,
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
                log.Controller = "OrderController";
                log.Method = "findByLocalIdPaged";
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
        /// Actualiza un registro de la tabla Order dejando el orderStatusId = 3.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="id"></param> 
        /// <response code="200">Retorna un mensaje de que se eliminó la Orden correctamente.</response>
        /// <response code="404">Retorna un mensaje de que no se pudo eliminar laa Orden.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpDelete]
        [Route("deleteById/{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult deleteById([FromHeader(Name = "Authorization")]string token, long id)
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
                        Order order = orderServiceImpl.deleteById(id);

                        if (order.OrderStatusId != 3)
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NotFound,
                                message = "No se pudo eliminar la orden."

                            });
                        }
                        else
                        {

                            Log log = new Log();

                            log.TypeLogId = 1;
                            log.Controller = "OrderController";
                            log.Method = "deleteById";
                            log.Description = "The order updated correctly.";

                            logServiceImpl.create(log);

                            return Ok(new
                            {

                                statusCode = HttpStatusCode.OK,
                                message = "Se anulo la orden correctamente."

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
                log.Controller = "OrderController";
                log.Method = "deleteById";
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
        /// Actualiza los datos de un registro de la tabla Order.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="order">Id, UserId.</param> 
        /// <response code="200">Retorna un mensaje de que se actualizo la Orden.</response>
        /// <response code="404">Retorna un mensaje de que no se pudo actualizar la Orden.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpPut]
        [Route("updateById")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult updateById([FromBody]Order order, [FromHeader(Name = "Authorization")]string token)
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
                        if (order.Id.ToString() == null || order.Id.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Id es requerido."

                            });
                        }
                        else if (order.UserId.ToString() == null || order.UserId.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Id del usuario es requerido."

                            });
                        }
                        else
                        {

                            Order orderExisting = orderServiceImpl.updateById(order);

                            if (orderExisting == null)
                            {
                                return Ok(new
                                {
                                    statusCode = HttpStatusCode.NotFound,
                                    message = "No se pudo actualizar la orden."

                                });
                            }
                            else
                            {
                                Log log = new Log();

                                log.TypeLogId = 1;
                                log.UserId = order.UserId;
                                log.Controller = "OrderController";
                                log.Method = "updateById";
                                log.Description = "The order updated correctly.";

                                logServiceImpl.create(log);

                                return Ok(new
                                {
                                    statusCode = HttpStatusCode.OK,
                                    message = "Se atendió la orden correctamente."

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
                log.UserId = order.UserId;
                log.Controller = "OrderController";
                log.Method = "updateById";
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
        /// Obtiene todas las ordenes por el RUT del cliente o el Id de la tabla Order.
        /// </summary>
        /// <param name="token"></param>  
        /// <param name="id"></param> 
        /// <param name="clientRun"></param> 
        /// <param name="page">Predeterminado 1.</param>
        /// <param name="filter">Filtros disponibles por ClientRun, Id.</param> 
        /// <param name="sort">1 = orderBy, 2 = orderByDescending, predeterminado 1.</param> 
        /// <param name="column">Orden por columnas disponibles : Id, ClientAddress, ClientRun, ClientFullName, DateOfDelivery, ScheduleOfRetirement, DateOfRequest</param> 
        /// <response code="200">Retorna un array del objecto Order de la tabla Order.</response>
        /// <response code="204">Retorna un array vacío del objecto Order.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findByIdAndClientRunPaged/{page}")]
        [ProducesResponseType(200, Type = typeof(List<Order>))]
        [ProducesResponseType(204, Type = typeof(List<Order>))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult findByIdAndClientRunPaged([FromHeader(Name = "Authorization")]string token, long id = 0, string clientRun = null, int page = 1, string filter = null, int sort = 1, string column = "Id")
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
                        List<Order> orders = orderServiceImpl.findByIdAndClientRunPaged(page, id, clientRun, sort, column);
                        List<Order> ordersCount = orderServiceImpl.findByIdAndClientRun(id, clientRun, sort, column);

                        if (orders.Count > 0)
                        {
                            return Ok(new
                            {

                                orders = orders,
                                countRows = ordersCount.Count,
                                countRowsByPage = orders.Count,
                                currentPage = page,
                                statusCode = HttpStatusCode.OK

                            });
                        }
                        else
                        {
                            return Ok(new
                            {

                                orders = orders,
                                countRows = ordersCount.Count,
                                countRowsByPage = orders.Count,
                                currentPage = page,
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
                log.Controller = "OrderController";
                log.Method = "findByIdAndClientRunPaged";
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
        /// Crea un registro en la tabla Order.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="order">ClientAddress, ClientFullName, ClientRun, CylinderByLocalId, Quantity, UserId.</param> 
        /// <response code="201">Retorna un mensaje de que la Orden se creo recientemente.</response>
        /// <response code="404">Retorna un mensaje de que la Orden no se pudo crear.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(201, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult create([FromBody]Order order, [FromHeader(Name = "Authorization")]string token)
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
                        if (order.ClientAddress == null || order.ClientAddress == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "La dirección del cliente es requerido."

                            });
                        }
                        else if (order.ClientFullName == null || order.ClientFullName == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El nombre completo del cliente es requerido."

                            });
                        }
                        else if (order.ClientRun == null || order.ClientRun == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El RUT del cliente es requerido."

                            });
                        }
                        else if (order.CylinderByLocalId.ToString() == null || order.CylinderByLocalId.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El cilindro es requerido."

                            });
                        }
                        else if (order.Quantity.ToString() == null || order.Quantity.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "La cantidad es requerida."

                            });
                        }
                        else if (order.UserId.ToString() == null || order.UserId.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Id del usuario es requerido."

                            });
                        }
                        else
                        {
                            Order orderCreate = orderServiceImpl.create(order);

                            if (orderCreate == null)
                            {
                                return Ok(new
                                {

                                    statusCode = HttpStatusCode.NotFound,
                                    message = "No se pudo crear la Orden."

                                });
                            }
                            else
                            {
                                Log log = new Log();

                                log.TypeLogId = 1;
                                log.UserId = order.UserId;
                                log.Controller = "OrderController";
                                log.Method = "create";
                                log.Description = "Correctly registered Order";

                                logServiceImpl.create(log);

                                return Ok(new
                                {

                                    statusCode = HttpStatusCode.Created,
                                    message = "Se creo la orden correctamente."

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
                log.UserId = order.Id;
                log.Controller = "OrderController";
                log.Method = "create";
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