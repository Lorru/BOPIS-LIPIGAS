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
    public class LocalController : ControllerBase
    {
        private LocalServiceImpl localServiceImpl = new LocalServiceImpl();

        private LogServiceImpl logServiceImpl = new LogServiceImpl();

        private JwtTokenServiceImpl jwtTokenServiceImpl = new JwtTokenServiceImpl();

        /// <summary>
        /// Obtiene el total de Locales registrados de la tabla Local.
        /// </summary>
        /// <param name="token"></param> 
        /// <response code="200">Retorna el total de Locales de la tabla Local.</response>
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findAllCount")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult findAllCount([FromHeader(Name = "Authorization")]string token)
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
                        int count = localServiceImpl.findAllCount();

                        return Ok(new
                        {
                            count = count,
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
                log.Controller = "LocalController";
                log.Method = "findAllCount";
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
        /// Obtiene el total de Locales registrados de la tabla Local con Status = 1.
        /// </summary>
        /// <param name="token"></param> 
        /// <response code="200">Retorna el total de Locales de la tabla Local con Status = 1.</response>
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findByStatusEqualToOneCount")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult findByStatusEqualToOneCount([FromHeader(Name = "Authorization")]string token)
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
                        int count = localServiceImpl.findByStatusEqualToOneCount();

                        return Ok(new
                        {
                            count = count,
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
                log.Controller = "LocalController";
                log.Method = "findByStatusEqualToOneCount";
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
        /// Actualiza si esta abierto o cerrado el Local de un registro de la tabla Local.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="user">Id, Local.Open .</param> 
        /// <response code="200">Retorna un mensaje de que se actualizo correctamente el Local.</response>
        /// <response code="404">Retorna un mensaje de que no se pudo actualizar el Local.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpPut]
        [Route("updateOpenFindByIdAndStatusEqualToOne")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult updateOpenFindByIdAndStatusEqualToOne([FromBody]User user, [FromHeader(Name = "Authorization")]string token)
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
                        if (user.Local.Id.ToString() == null || user.Local.Id.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Id del local es requerido."

                            });
                        }
                        else if (user.Local.Open.ToString() == null || user.Local.Open.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "Open es requerido."

                            });
                        }
                        else if (user.Id.ToString() == null || user.Id.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Id del usuario es requerido."

                            });
                        }
                        else
                        {

                            Local local = localServiceImpl.updateOpenFindByIdAndStatusEqualToOne(user.Local);

                            if (local == null)
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
                                log.UserId = user.Id;
                                log.Controller = "LocalController";
                                log.Method = "updateOpenFindByIdAndStatusEqualToOne";
                                log.Description = "The local updated correctly.";

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
                log.UserId = user.Id;
                log.Controller = "LocalController";
                log.Method = "updateOpenFindByIdAndStatusEqualToOne";
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
        /// Obtiene todos los locales de la tabla Local.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="page">Predeterminado 1.</param> 
        /// <param name="filter">Filtros disponibles por Nombre.</param> 
        /// <param name="sort">1 = orderBy, 2 = orderByDescending, predeterminado 1.</param> 
        /// <param name="column">Orden por columnas disponibles : Id, Name, Radio.</param> 
        /// <response code="200">Retorna un array del objecto Local de la tabla Local.</response>
        /// <response code="204">Retorna un array vacío del objecto Local.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findAllPaged/{page}")]
        [ProducesResponseType(200, Type = typeof(List<Local>))]
        [ProducesResponseType(204, Type = typeof(List<Local>))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult findAllPaged([FromHeader(Name = "Authorization")]string token, int page = 1, string filter = null, int sort = 1, string column = "Id")
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
                        List<Local> locals = localServiceImpl.findAllPaged(page, filter, sort, column);
                        List<Local> localsCount = localServiceImpl.findAll(filter, sort, column);

                        if (locals.Count > 0)
                        {
                            return Ok(new
                            {

                                locals = locals,
                                countRows = localsCount.Count,
                                countRowsByPage = locals.Count,
                                currentPage = page,
                                statusCode = HttpStatusCode.OK

                            });
                        }
                        else
                        {
                            return Ok(new
                            {

                                locals = locals,
                                countRows = localsCount.Count,
                                countRowsByPage = locals.Count,
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
                log.Controller = "LocalController";
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
        /// Actualiza el Status del Local de la tabla Local
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="user">Id, Local.Status .</param> 
        /// <response code="200">Retorna un mensaje de que se actualizo correctamente el Local.</response>
        /// <response code="404">Retorna un mensaje de que no se pudo actualizar el Local.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpPut]
        [Route("updateStatusFindById")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult updateStatusFindById([FromBody]User user, [FromHeader(Name = "Authorization")]string token)
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
                        if (user.Local.Id.ToString() == null || user.Local.Id.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Id del local es requerido."

                            });
                        }
                        else if (user.Local.Status.ToString() == null || user.Local.Status.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "el Status es requerido."

                            });
                        }
                        else if (user.Id.ToString() == null || user.Id.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Id del usuario es requerido."

                            });
                        }
                        else
                        {

                            Local local = localServiceImpl.updateStatusFindById(user.Local);

                            if (local == null)
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
                                log.UserId = user.Id;
                                log.Controller = "LocalController";
                                log.Method = "updateStatusFindById";
                                log.Description = "The local updated correctly.";

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
                log.UserId = user.Id;
                log.Controller = "LocalController";
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
        /// Actualiza los datos de un registro de la tabla Local.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="user">Id, Local.Id, Local.Latitude, Local.Length, Local.Name, Local.Radio.</param> 
        /// <response code="200">Retorna un mensaje de que se actualizo correctamente el Local.</response>
        /// <response code="404">Retorna un mensaje de que no se pudo actualizar el Local.</response>    
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpPut]
        [Route("updateByIdAndStatusEqualToOne")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult updateByIdAndStatusEqualToOne([FromBody]User user, [FromHeader(Name = "Authorization")]string token)
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
                        if (user.Local.Id.ToString() == null || user.Local.Id.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Id del local es requerido."

                            });
                        }
                        else if (user.Local.Latitude.ToString() == null || user.Local.Latitude.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "La latitude es requerida."

                            });
                        }
                        else if (user.Local.Length.ToString() == null || user.Local.Length.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "La longitud es requerida."

                            });
                        }
                        else if (user.Local.Name == null || user.Local.Name == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El nombre es requerido."

                            });
                        }
                        else if (user.Local.Radio.ToString() == null || user.Local.Radio.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El radio es requerido."

                            });
                        }
                        else if (user.Id.ToString() == null || user.Id.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Id del usuario es requerido."

                            });
                        }
                        else
                        {

                            Local local = localServiceImpl.updateByIdAndStatusEqualToOne(user.Local);

                            if (local == null)
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
                                log.UserId = user.Id;
                                log.Controller = "LocalController";
                                log.Method = "updateByIdAndStatusEqualToOne";
                                log.Description = "The local updated correctly.";

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
                log.UserId = user.Id;
                log.Controller = "LocalController";
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

        /// <summary>
        /// Crea un registro en la tabla Local.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="user">Id, Local.Latitude, Local.Length, Local.Name, Local.Radio.</param> 
        /// <response code="201">Retorna un mensaje de que se creo el Local correctamente.</response>
        /// <response code="404">Retorna un mensaje de que no se pudo crear el Local.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(201, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult create([FromBody]User user, [FromHeader(Name = "Authorization")]string token)
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
                        if (user.Local.Latitude.ToString() == null || user.Local.Latitude.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "La latitude es requerida."

                            });
                        }
                        else if (user.Local.Length.ToString() == null || user.Local.Length.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "La longitud es requerida."

                            });
                        }
                        else if (user.Local.Name == null || user.Local.Name == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El nombre es requerido."

                            });
                        }
                        else if (user.Local.Radio.ToString() == null || user.Local.Radio.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El radio es requerido."

                            });
                        }
                        else if (user.Id.ToString() == null || user.Id.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Id del usuario es requerido."

                            });
                        }
                        else
                        {
                            Local local = localServiceImpl.create(user.Local);

                            if (local == null)
                            {
                                return Ok(new
                                {

                                    statusCode = HttpStatusCode.NotFound,
                                    message = "No se pudo crear el local."

                                });
                            }
                            else
                            {
                                Log log = new Log();

                                log.TypeLogId = 1;
                                log.UserId = user.Id;
                                log.Controller = "LocalController";
                                log.Method = "create";
                                log.Description = "Correctly registered local";

                                logServiceImpl.create(log);

                                return Ok(new
                                {

                                    statusCode = HttpStatusCode.Created,
                                    message = "Se creo el local correctamente."

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
                log.UserId = user.Id;
                log.Controller = "LocalController";
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

        /// <summary>
        /// Obtiene todos los locales de la tabla Local donde status es igual a 1.
        /// </summary>
        /// <param name="token"></param> 
        /// <response code="200">Retorna un array del objecto Local.</response>
        /// <response code="204">Retorna un array vacío del objecto Local.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findAllStatusEqualToOne")]
        [ProducesResponseType(200, Type = typeof(List<Local>))]
        [ProducesResponseType(204, Type = typeof(List<Local>))]
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
                        List<Local> locals = localServiceImpl.findAllStatusEqualToOne();

                        if (locals.Count > 0)
                        {
                            return Ok(new
                            {

                                locals = locals,
                                countRows = locals.Count,
                                statusCode = HttpStatusCode.OK

                            });
                        }
                        else
                        {
                            return Ok(new
                            {

                                locals = locals,
                                countRows = locals.Count,
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
                log.Controller = "LocalController";
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