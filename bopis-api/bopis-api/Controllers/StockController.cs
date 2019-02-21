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
    public class StockController : ControllerBase
    {
        private StockServiceImpl stockServiceImpl = new StockServiceImpl();

        private LogServiceImpl logServiceImpl = new LogServiceImpl();

        private JwtTokenServiceImpl jwtTokenServiceImpl = new JwtTokenServiceImpl();

        /// <summary>
        /// Actualiza la cantidad de un registro de la tabla Stock.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="stock">Id, Quantity</param> 
        /// <response code="200">Retorna un mensaje de que se actualizo el Stock correctamente.</response>
        /// <response code="404">Retorna un mensaje de que no se pudo actualizo el Stock.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpPut]
        [Route("updateQuantityByIdAndStatusEqualToOne")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult updateQuantityByIdAndStatusEqualToOne([FromBody]Stock stock, [FromHeader(Name = "Authorization")]string token)
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

                        if (stock.Id.ToString() == null || stock.Id.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Id es requerido."

                            });
                        }
                        else if (stock.Quantity.ToString() == null || stock.Quantity.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "La cantidad es requerida."

                            });
                        }
                        else
                        {
                            Stock stockExist = stockServiceImpl.updateQuantityByIdAndStatusEqualToOne(stock);

                            if (stockExist == null)
                            {
                                return Ok(new
                                {

                                    statusCode = HttpStatusCode.NotFound,
                                    message = "No se pudo actualizar el stock."

                                });
                            }
                            else
                            {

                                Log log = new Log();

                                log.TypeLogId = 1;
                                log.Controller = "StockController";
                                log.Method = "updateQuantityByIdAndStatusEqualToOne";
                                log.Description = "The stock updated correctly.";

                                logServiceImpl.create(log);

                                return Ok(new
                                {

                                    statusCode = HttpStatusCode.OK,
                                    message = "Se actualizo el stock correctamente."

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
                log.Controller = "StockController";
                log.Method = "updateQuantityByIdAndStatusEqualToOne";
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
        /// Obtiene todos los Cilindros de la tabla CylinderByLocal por el Id del local.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="localId"></param> 
        /// <response code="200">Retorna un array del objecto Stock de la tabla Stock.</response>
        /// <response code="204">Retorna un array vacío del objecto Stock.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findByLocalId/{localId}")]
        [ProducesResponseType(200, Type = typeof(List<Stock>))]
        [ProducesResponseType(204, Type = typeof(List<Stock>))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult findByLocalId([FromHeader(Name = "Authorization")]string token, long localId)
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
                        List<Stock> stocks = stockServiceImpl.findByLocalId(localId);

                        if (stocks.Count > 0)
                        {
                            return Ok(new
                            {

                                stocks = stocks,
                                countRows = stocks.Count,
                                statusCode = HttpStatusCode.OK

                            });
                        }
                        else
                        {
                            return Ok(new
                            {

                                stocks = stocks,
                                countRows = stocks.Count,
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
                log.Controller = "StockController";
                log.Method = "findByLocalId";
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
        /// Obtiene el stock de los cilindros de la tabla Stock
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="cylinderByLocalId"></param> 
        /// <response code="200">Retorna un objecto Stock de la tabla Stock.</response>
        /// <response code="404">Retorna un mensaje de que no existe el Stock.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findByCylinderByLocalIdAndStatusEqualToOne/{cylinderByLocalId}")]
        [ProducesResponseType(200, Type = typeof(Stock))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult findByCylinderByLocalIdAndStatusEqualToOne([FromHeader(Name = "Authorization")]string token, long cylinderByLocalId)
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
                        Stock stock = stockServiceImpl.findByCylinderByLocalIdAndStatusEqualToOne(cylinderByLocalId);

                        if (stock != null)
                        {
                            return Ok(new
                            {

                                stock = stock,
                                statusCode = HttpStatusCode.OK

                            });
                        }
                        else
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NotFound,
                                message = "El stock no existe."

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
                log.Controller = "StockController";
                log.Method = "findByCylinderByLocalIdAndStatusEqualToOne";
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