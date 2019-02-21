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
    public class ScheduleOfAttentionController : ControllerBase
    {
        private ScheduleOfAttentionServiceImpl scheduleOfAttentionServiceImpl = new ScheduleOfAttentionServiceImpl();

        private LogServiceImpl logServiceImpl = new LogServiceImpl();

        private JwtTokenServiceImpl jwtTokenServiceImpl = new JwtTokenServiceImpl();

        /// <summary>
        /// Obtiene todos los Horarios por local de la tabla ScheduleOfAttention por el Id del local con Status = 1.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="localId"></param> 
        /// <response code="200">Retorna un array del objecto ScheduleOfAttention de la tabla ScheduleOfAttention.</response>
        /// <response code="204">Retorna un array vacío del objecto ScheduleOfAttention.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findByLocalIdAndStatusEqualToOne/{localId}")]
        [ProducesResponseType(200, Type = typeof(List<ScheduleOfAttention>))]
        [ProducesResponseType(204, Type = typeof(List<ScheduleOfAttention>))]
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
                        List<ScheduleOfAttention> scheduleOfAttentions = scheduleOfAttentionServiceImpl.findByLocalIdAndStatusEqualToOne(localId);

                        if (scheduleOfAttentions.Count > 0)
                        {
                            return Ok(new
                            {

                                scheduleOfAttentions = scheduleOfAttentions,
                                countRows = scheduleOfAttentions.Count,
                                statusCode = HttpStatusCode.OK

                            });
                        }
                        else
                        {
                            return Ok(new
                            {

                                scheduleOfAttentions = scheduleOfAttentions,
                                countRows = scheduleOfAttentions.Count,
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
                log.Controller = "ScheduleOfAttentionController";
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
        /// Actualiza los datos de un registro de la tabla ScheduleOfAttention.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="scheduleOfAttention">Id, Start, Finish .</param> 
        /// <response code="200">Retorna un objeto ScheduleOfAttention actualizado recientemente.</response>
        /// <response code="404">Retorna un objeto ScheduleOfAttention que no se pudo actualizar.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpPut]
        [Route("updateStartAndFinishFindByIdAndStatusEqualToOne")]
        [ProducesResponseType(200, Type = typeof(ScheduleOfAttention))]
        [ProducesResponseType(404, Type = typeof(ScheduleOfAttention))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult updateStartAndFinishFindByIdAndStatusEqualToOne([FromBody]ScheduleOfAttention scheduleOfAttention, [FromHeader(Name = "Authorization")]string token)
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
                        if (scheduleOfAttention.Id.ToString() == null || scheduleOfAttention.Id.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Id es requerido."

                            });
                        }
                        else if (scheduleOfAttention.Start == null || scheduleOfAttention.Start == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "La hora de inicio es requerido."

                            });
                        }
                        else if (scheduleOfAttention.Finish == null || scheduleOfAttention.Finish == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "La hora de cierre es requerido."

                            });
                        }
                        else
                        {

                            ScheduleOfAttention scheduleOfAttentionExisting = scheduleOfAttentionServiceImpl.updateStartAndFinishFindByIdAndStatusEqualToOne(scheduleOfAttention);

                            if (scheduleOfAttentionExisting == null)
                            {
                                return Ok(new
                                {

                                    statusCode = HttpStatusCode.NotFound,
                                    message = "No se pudo actualizar el horario de atención."

                                });
                            }
                            else
                            {
                                Log log = new Log();

                                log.TypeLogId = 1;
                                log.Controller = "ScheduleOfAttentionController";
                                log.Method = "updateStartAndFinishFindByIdAndStatusEqualToOne";
                                log.Description = "The schedule of attention updated correctly.";

                                logServiceImpl.create(log);

                                return Ok(new
                                {

                                    statusCode = HttpStatusCode.OK,
                                    message = "Se actualizo el horario de atención correctamente."

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
                log.Controller = "ScheduleOfAttentionController";
                log.Method = "updateStartAndFinishFindByIdAndStatusEqualToOne";
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