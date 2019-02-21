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
    public class ConfigurationController : ControllerBase
    {
        ConfigurationServiceImpl configurationServiceImpl = new ConfigurationServiceImpl();

        LogServiceImpl logServiceImpl = new LogServiceImpl();

        JwtTokenServiceImpl jwtTokenServiceImpl = new JwtTokenServiceImpl();

        /// <summary>
        /// Retorna un Objeto Configuration de la tabla Configuration.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="id"></param> 
        /// <response code="200">Retorna un objeto Configuration.</response>
        /// <response code="404">Retorna un mensaje de que no existe la Configuración.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findByIdAndStatusEqualToOne/{id}")]
        [ProducesResponseType(200, Type = typeof(Configuration))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult findByIdAndStatusEqualToOne([FromHeader(Name = "Authorization")]string token, long id)
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
                        Configuration configuration = configurationServiceImpl.findByIdAndStatusEqualToOne(id);

                        if (configuration == null)
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NotFound,
                                message = "La configuración no existe."

                            });
                        }
                        else
                        {
                            return Ok(new
                            {

                                configuration = configuration,
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
                log.UserId = id;
                log.Controller = "ConfigurationController";
                log.Method = "findByIdAndStatusEqualToOne";
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
        /// Obtiene todas las Configuraciones del sistema de la tabla Configuration.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="page">Predeterminado 1.</param> 
        /// <param name="sort">1 = orderBy, 2 = orderByDescending, predeterminado 1.</param> 
        /// <param name="column">Orden por columnas disponibles : Id, Description, Key.</param> 
        /// <response code="200">Retorna un array del objecto Configuration de la tabla Configuration.</response>
        /// <response code="204">Retorna un array vacío del objecto Configuration.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findAllPaged/{page}")]
        [ProducesResponseType(200, Type = typeof(List<Configuration>))]
        [ProducesResponseType(204, Type = typeof(List<Configuration>))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult findAllPaged([FromHeader(Name = "Authorization")]string token, int page = 1, int sort = 1, string column = "Id")
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
                        List<Configuration> configurations = configurationServiceImpl.findAllPaged(page, sort, column);
                        List<Configuration> configurationsCount = configurationServiceImpl.findAll(sort, column);

                        if (configurations.Count > 0)
                        {
                            return Ok(new
                            {

                                configurations = configurations,
                                countRows = configurationsCount.Count,
                                countRowsByPage = configurations.Count,
                                currentPage = page,
                                statusCode = HttpStatusCode.OK

                            });
                        }
                        else
                        {
                            return Ok(new
                            {

                                configurations = configurations,
                                countRows = configurationsCount.Count,
                                countRowsByPage = configurations.Count,
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
                log.Controller = "ConfigurationController";
                log.Method = "findAllPaged";
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
        /// Actualiza los datos de un registro de la tabla Configuration.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="configuration">Id, Value, ReadOnly.</param> 
        /// <response code="200">Retorna un objeto Configuration actualizado recientemente.</response>
        /// <response code="404">Retorna un objeto Configuration que no se pudo actualizar.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpPut]
        [Route("updateValueAndReadOnlyByIdAndStatusEqualToOne")]
        [ProducesResponseType(200, Type = typeof(Configuration))]
        [ProducesResponseType(404, Type = typeof(Configuration))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult updateValueAndReadOnlyByIdAndStatusEqualToOne([FromBody]Configuration configuration, [FromHeader(Name = "Authorization")]string token)
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
                        if (configuration.Id.ToString() == null || configuration.Id.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Id es requerido."

                            });
                        }
                        else if (configuration.Value == null || configuration.Value == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El valor es requerido."

                            });
                        }
                        else if (configuration.ReadOnly.ToString() == null || configuration.ReadOnly.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "Si es editable es requerido."

                            });
                        }
                        else
                        {

                            Configuration configurationExisting = configurationServiceImpl.updateValueAndReadOnlyByIdAndStatusEqualToOne(configuration);

                            if (configurationExisting == null)
                            {
                                return Ok(new
                                {

                                    statusCode = HttpStatusCode.NotFound,
                                    message = "No se pudo actualizar el local."

                                });
                            }
                            else
                            {
                                Log log = new Log();

                                log.TypeLogId = 1;
                                log.Controller = "ConfigurationController";
                                log.Method = "updateValueAndReadOnlyByIdAndStatusEqualToOne";
                                log.Description = "The configuration updated correctly.";

                                logServiceImpl.create(log);

                                return Ok(new
                                {

                                    statusCode = HttpStatusCode.OK,
                                    message = "Se actualizo el local correctamente."

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
                log.Controller = "ConfigurationController";
                log.Method = "updateValueAndReadOnlyByIdAndStatusEqualToOne";
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