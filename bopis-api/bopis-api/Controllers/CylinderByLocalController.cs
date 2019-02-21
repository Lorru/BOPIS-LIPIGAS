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
    public class CylinderByLocalController : ControllerBase
    {
        private CylinderByLocalServiceImpl cylinderByLocalServiceImpl = new CylinderByLocalServiceImpl();

        private LogServiceImpl logServiceImpl = new LogServiceImpl();

        private JwtTokenServiceImpl jwtTokenServiceImpl = new JwtTokenServiceImpl();


        /// <summary>
        /// Actualiza el estado de un registro de la tabla CylinderByLocal.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="cylinderByLocal">Id, Status</param> 
        /// <response code="200">Retorna un mensaje de que se actualizo el Local correctamente.</response>
        /// <response code="404">Retorna un mensaje de que no se pudo actualizo el Local.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpPut]
        [Route("updateStatusFindById")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult updateStatusFindById([FromBody]CylinderByLocal cylinderByLocal, [FromHeader(Name = "Authorization")]string token)
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

                        if (cylinderByLocal.Id.ToString() == null || cylinderByLocal.Id.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Id es requerido."

                            });
                        }
                        else if (cylinderByLocal.Status.ToString() == null || cylinderByLocal.Status.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Status es requerido."

                            });
                        }
                        else
                        {
                            CylinderByLocal cylinderByLocalExisting = cylinderByLocalServiceImpl.updateStatusFindById(cylinderByLocal);

                            if (cylinderByLocalExisting == null)
                            {
                                return Ok(new
                                {

                                    statusCode = HttpStatusCode.NotFound,
                                    message = "No se pudo actualizar el cilindro."

                                });
                            }
                            else
                            {

                                Log log = new Log();

                                log.TypeLogId = 1;
                                log.Controller = "CylinderByLocalController";
                                log.Method = "updateStatusFindById";
                                log.Description = "The cylinder updated correctly.";

                                logServiceImpl.create(log);

                                return Ok(new
                                {

                                    statusCode = HttpStatusCode.OK,
                                    message = "Se actualizo el cilindro correctamente."

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
                log.Controller = "CylinderByLocalController";
                log.Method = "updateStatusFindById";
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
        /// Obtiene todos los Cilindros de la tabla CylinderByLocal por el Id del local con Status = 1.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="localId"></param> 
        /// <response code="200">Retorna un array del objecto CylinderByLocal de la tabla CylinderByLocal.</response>
        /// <response code="204">Retorna un array vacío del objecto CylinderByLocal.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findByLocalIdAndStatusEqualToOne/{localId}")]
        [ProducesResponseType(200, Type = typeof(List<CylinderByLocal>))]
        [ProducesResponseType(204, Type = typeof(List<CylinderByLocal>))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult findByLocalIdAndStatusEqualToOne([FromHeader(Name = "Authorization")]string token, long localId)
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
                        List<CylinderByLocal> cylinderByLocals = cylinderByLocalServiceImpl.findByLocalIdAndStatusEqualToOne(localId);

                        if (cylinderByLocals.Count > 0)
                        {
                            return Ok(new
                            {

                                cylinderByLocals = cylinderByLocals,
                                countRows = cylinderByLocals.Count,
                                statusCode = HttpStatusCode.OK

                            });
                        }
                        else
                        {
                            return Ok(new
                            {

                                cylinderByLocals = cylinderByLocals,
                                countRows = cylinderByLocals.Count,
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
                log.Controller = "CylinderByLocalController";
                log.Method = "findByLocalIdAndStatusEqualToOne";
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
        /// Actualiza los datos de un registro de la tabla CylinderByLocal.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="cylinderByLocal">Id, ZonePrice, Discount, FinalPrice.</param> 
        /// <response code="200">Retorna un objeto CylinderByLocal actualizado recientemente.</response>
        /// <response code="404">Retorna un objeto CylinderByLocal que no se pudo actualizar.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpPut]
        [Route("updateByIdAndStatusEqualToOne")]
        [ProducesResponseType(200, Type = typeof(CylinderByLocal))]
        [ProducesResponseType(404, Type = typeof(CylinderByLocal))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult updateByIdAndStatusEqualToOne([FromBody]CylinderByLocal cylinderByLocal, [FromHeader(Name = "Authorization")]string token)
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
                        if (cylinderByLocal.Id.ToString() == null || cylinderByLocal.Id.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Id es requerido."

                            });
                        }
                        else if (cylinderByLocal.FinalPrice.ToString() == null || cylinderByLocal.FinalPrice.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El precio final es requerido."

                            });
                        }
                        else if (cylinderByLocal.Discount.ToString() == null || cylinderByLocal.Discount.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El descuento es requerido."

                            });
                        }
                        else if (cylinderByLocal.ZonePrice.ToString() == null || cylinderByLocal.ZonePrice.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El precio zona es requerido."

                            });
                        }
                        else
                        {

                            CylinderByLocal cylinderByLocalExist = cylinderByLocalServiceImpl.updateByIdAndStatusEqualToOne(cylinderByLocal);

                            if (cylinderByLocalExist == null)
                            {
                                return Ok(new
                                {
                                    cylinderByLocalExist = cylinderByLocalExist,
                                    statusCode = HttpStatusCode.NotFound,
                                    message = "No se pudo actualizar el cilindro."

                                });
                            }
                            else
                            {
                                Log log = new Log();

                                log.TypeLogId = 1;
                                log.Controller = "CylinderByLocalController";
                                log.Method = "updateByIdAndStatusEqualToOne";
                                log.Description = "The cylinder updated correctly.";

                                logServiceImpl.create(log);

                                return Ok(new
                                {
                                    cylinderByLocalExist = cylinderByLocalExist,
                                    statusCode = HttpStatusCode.OK,
                                    message = "Se actualizo el cilindro correctamente."

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
                log.Controller = "CylinderByLocalController";
                log.Method = "updateByIdAndStatusEqualToOne";
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