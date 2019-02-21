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
    public class UserController : ControllerBase
    {
        private UserServiceImpl userServiceImpl = new UserServiceImpl();

        private LogServiceImpl logServiceImpl = new LogServiceImpl();

        private JwtTokenServiceImpl jwtTokenServiceImpl = new JwtTokenServiceImpl();

        private RoleServiceImpl roleServiceImpl = new RoleServiceImpl();

        /// <summary>
        /// Obtiene el usuario logueado, su perfil y además del token de acceso para consumir los servicios REST.
        /// </summary>
        /// <param name="user">Run, Password</param> 
        /// <response code="200">Retorna un objeto User del usuario logueado, un string para el token de acceso para consumir los servicios REST y un array con el perfil del usuario logueado.</response>
        /// <response code="403">Retorna un mensaje de que las credenciales son incorrectas.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult login([FromBody]User user)
        {
            try
            {
                if (user.Run == null || user.Run == "")
                {
                    return Ok(new
                    {

                        statusCode = HttpStatusCode.NoContent,
                        message = "El RUT es requerido."

                    });
                }
                else if (user.Password == null || user.Password == "")
                {
                    return Ok(new
                    {

                        statusCode = HttpStatusCode.NoContent,
                        message = "La contraseña es requerida."

                    });
                }
                else
                {
                    User userExisting = userServiceImpl.login(user);

                    if (userExisting == null)
                    {
                        return Ok(new
                        {

                            message = "Credenciales incorrectas.",
                            statusCode = HttpStatusCode.Forbidden

                        });
                    }
                    else
                    {
                        string token = jwtTokenServiceImpl.generate(userExisting.Email);
                        List<Role> roles = roleServiceImpl.findByProfileIdAndStatusEqualToOne(userExisting.ProfileId);

                        Log log = new Log();

                        log.TypeLogId = 1;
                        log.UserId = userExisting.Id;
                        log.Controller = "UserController";
                        log.Method = "login";
                        log.Description = "User logged in correctly.";

                        logServiceImpl.create(log);

                        return Ok(new
                        {

                            userExisting = userExisting,
                            roles = roles,
                            token = token,
                            statusCode = HttpStatusCode.OK

                        });

                    }
                }
            }
            catch (Exception exception)
            {

                Log log = new Log();

                log.TypeLogId = 2;
                log.Controller = "UserController";
                log.Method = "login";
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
        /// Actualiza la clave del usuario mediante el correo registrado en la tabla User.
        /// </summary>
        /// <param name="user">Email</param> 
        /// <response code="200">Retorna un mensaje de que se envió la nueva contraseña al correo ingresado.</response>
        /// <response code="404">Retorna un mensaje de que el usuario no existe o no se pudo actualizar la clave.</response>    
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpPut]
        [Route("updatePasswordByEmailAndStatusEqualToOne")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult updatePasswordByEmailAndStatusEqualToOne([FromBody]User user)
        {
            try
            {
                if (user.Email == null || user.Password == "")
                {
                    return Ok(new
                    {

                        statusCode = HttpStatusCode.NoContent,
                        message = "El correo es requerido."

                    });
                }
                else
                {
                    User validateUserExisting = userServiceImpl.findByEmailAndStatusEqualToOne(user.Email);

                    if (validateUserExisting == null)
                    {
                        return Ok(new
                        {

                            statusCode = HttpStatusCode.NotFound,
                            message = "El usuario no existe."

                        });
                    }
                    else
                    {
                        User userExisting = userServiceImpl.updatePasswordByEmailAndStatusEqualToOne(user);

                        if (userExisting == null)
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NotFound,
                                message = "No se pudo actualizar el usuario."

                            });
                        }
                        else
                        {
                            Log log = new Log();

                            log.TypeLogId = 1;
                            log.UserId = userExisting.Id;
                            log.Controller = "UserController";
                            log.Method = "updatePasswordByEmailAndStatusEqualToOne";
                            log.Description = "The user updated correctly.";

                            logServiceImpl.create(log);

                            return Ok(new
                            {

                                statusCode = HttpStatusCode.OK,
                                message = "Se envió la nueva contraseña al correo " + userExisting.Email

                            });
                        }
                    }
                }
            }
            catch (Exception exception)
            {

                Log log = new Log();

                log.TypeLogId = 2;
                log.Controller = "UserController";
                log.Method = "updatePasswordByEmailAndStatusEqualToOne";
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
        /// Obtiene el total de usuarios registrados de la tabla User.
        /// </summary>
        /// <param name="token"></param> 
        /// <response code="200">Retorna el total de usuarios de la tabla User.</response>
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
                        int count = userServiceImpl.findAllCount();

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
                log.Controller = "UserController";
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
        /// Obtiene todos los usuarios de la tabla User.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="page">Predeterminado 1.</param> 
        /// <param name="filter">Filtros disponibles por Nombre y RUT.</param> 
        /// <param name="sort">1 = orderBy, 2 = orderByDescending, predeterminado 1.</param> 
        /// <param name="column">Orden por columnas disponibles : Id, FullName, Run, Email, Profile</param> 
        /// <response code="200">Retorna un array del objecto User de la tabla User.</response>
        /// <response code="204">Retorna un array vacío del objecto User.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findAllPaged/{page}")]
        [ProducesResponseType(200, Type = typeof(List<User>))]
        [ProducesResponseType(204, Type = typeof(List<User>))]
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
                        List<User> users = userServiceImpl.findAllPaged(page, filter, sort, column);
                        List<User> usersCount = userServiceImpl.findAll(filter, sort, column);

                        if (users.Count > 0)
                        {
                            return Ok(new
                            {

                                users = users,
                                countRows = usersCount.Count,
                                countRowsByPage = users.Count,
                                currentPage = page,
                                statusCode = HttpStatusCode.OK

                            });
                        }
                        else
                        {
                            return Ok(new
                            {

                                users = users,
                                countRows = usersCount.Count,
                                countRowsByPage = users.Count,
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
                log.Controller = "UserController";
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
        /// Actualiza el Status del Usuario de la tabla User
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="user">Id, Status .</param> 
        /// <response code="200">Retorna un mensaje de que se actualizo correctamente el Usuario.</response>
        /// <response code="404">Retorna un mensaje de que no se pudo actualizar el Usuario.</response>   
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
                        if (user.Status.ToString() == null || user.Status.ToString() == "")
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

                            User userExisting = userServiceImpl.updateStatusFindById(user);

                            if (userExisting == null)
                            {
                                return Ok(new
                                {

                                    statusCode = HttpStatusCode.NotFound,
                                    message = "No se pudo actualizar el usuario."

                                });
                            }
                            else
                            {
                                Log log = new Log();

                                log.TypeLogId = 1;
                                log.UserId = user.Id;
                                log.Controller = "UserController";
                                log.Method = "updateStatusFindById";
                                log.Description = "The user updated correctly.";

                                logServiceImpl.create(log);

                                return Ok(new
                                {

                                    statusCode = HttpStatusCode.OK,
                                    message = "Se actualizo el usuario correctamente."

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
                log.Controller = "UserController";
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
        /// Actualiza la clave del Usuario de la tabla User
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="user">Id, Password .</param> 
        /// <response code="200">Retorna un mensaje de que se actualizo correctamente el Usuario.</response>
        /// <response code="404">Retorna un mensaje de que no se pudo actualizar el Usuario.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpPut]
        [Route("updatePasswordByIdAndStatusEqualToOne")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult updatePasswordByIdAndStatusEqualToOne([FromBody]User user, [FromHeader(Name = "Authorization")]string token)
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
                        if (user.Password == null || user.Password == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "La clave es requerida."

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

                            User userExisting = userServiceImpl.updatePasswordByIdAndStatusEqualToOne(user);

                            if (userExisting == null)
                            {
                                return Ok(new
                                {

                                    statusCode = HttpStatusCode.NotFound,
                                    message = "No se pudo actualizar el usuario."

                                });
                            }
                            else
                            {
                                Log log = new Log();

                                log.TypeLogId = 1;
                                log.UserId = user.Id;
                                log.Controller = "UserController";
                                log.Method = "updatePasswordByIdAndStatusEqualToOne";
                                log.Description = "The user updated correctly.";

                                logServiceImpl.create(log);

                                return Ok(new
                                {

                                    statusCode = HttpStatusCode.OK,
                                    message = "Se actualizo el usuario correctamente."

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
                log.Controller = "UserController";
                log.Method = "updatePasswordByIdAndStatusEqualToOne";
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
        /// Actualiza los datos de un registro de la tabla User.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="user">Id, ProfileId, LocalId, FullName, Run, Email</param> 
        /// <response code="200">Retorna un mensaje de que se pudo actualizar el Usuario.</response>
        /// <response code="404">Retorna un mensaje de que no se pudo actualizar el Usuario.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpPut]
        [Route("updatebyIdAndStatusEqualToOne")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(string))]
        public IActionResult updatebyIdAndStatusEqualToOne([FromBody]User user, [FromHeader(Name = "Authorization")]string token)
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
                        if (user.Id.ToString() == null || user.Id.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El Id es requerido."

                            });
                        }
                        else if (user.ProfileId.ToString() == null || user.ProfileId.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El perfil es requerido."

                            });
                        }
                        else if (user.FullName == null || user.FullName == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El nombre completo es requerido."

                            });
                        }
                        else if (user.Run == null || user.Run == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El RUT es requerido."

                            });
                        }
                        else if (user.Email == null || user.Email == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El correo es requerido."

                            });
                        }
                        else
                        {

                            User userExist = userServiceImpl.updatebyIdAndStatusEqualToOne(user);

                            if (userExist == null)
                            {
                                return Ok(new
                                {
                                    statusCode = HttpStatusCode.NotFound,
                                    message = "No se pudo actualizar el usuario."

                                });
                            }
                            else
                            {
                                Log log = new Log();

                                log.TypeLogId = 1;
                                log.UserId = userExist.Id;
                                log.Controller = "UserController";
                                log.Method = "updatebyIdAndStatusEqualToOne";
                                log.Description = "The user updated correctly.";

                                logServiceImpl.create(log);

                                return Ok(new
                                {
                                    statusCode = HttpStatusCode.OK,
                                    message = "Se actualizo el usuario correctamente."

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
                log.Controller = "UserController";
                log.Method = "updatebyIdAndStatusEqualToOne";
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
        /// Crea un registro en la tabla User.
        /// </summary>
        /// <param name="token"></param> 
        /// <param name="user">ProfileId, LocalId, FullName, Run, Email, Password</param> 
        /// <response code="201">Retorna un mensaje de que se pudo crear el Usuario.</response>
        /// <response code="409">Retorna un mensaje de que el Usuario ya existe.</response>   
        /// <response code="404">Retorna un mensaje de que no se pudo crear el Usuario.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(201, Type = typeof(string))]
        [ProducesResponseType(409, Type = typeof(string))]
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
                        if (user.ProfileId.ToString() == null || user.ProfileId.ToString() == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El perfil es requerido."

                            });
                        }
                        else if (user.FullName == null || user.FullName == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El nombre completo es requerido."

                            });
                        }
                        else if (user.Run == null || user.Run == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El RUT es requerido."

                            });
                        }
                        else if (user.Email == null || user.Email == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "El correo es requerido."

                            });
                        }
                        else if (user.Password == null || user.Password == "")
                        {
                            return Ok(new
                            {

                                statusCode = HttpStatusCode.NoContent,
                                message = "La clave es requerida."

                            });
                        }
                        else
                        {
                            User validateUser = userServiceImpl.findByRunAndEmail(user.Run, user.Email);

                            if (validateUser == null)
                            {
                                User userCreate = userServiceImpl.create(user);

                                if (userCreate == null)
                                {
                                    return Ok(new
                                    {
                                        statusCode = HttpStatusCode.NotFound,
                                        message = "No se pudo crear el usuario."

                                    });
                                }
                                else
                                {
                                    Log log = new Log();

                                    log.TypeLogId = 1;
                                    log.UserId = userCreate.Id;
                                    log.Controller = "UserController";
                                    log.Method = "create";
                                    log.Description = "Correctly registered user";

                                    logServiceImpl.create(log);

                                    return Ok(new
                                    {

                                        statusCode = HttpStatusCode.Created,
                                        message = "Se creo el usuario correctamente."

                                    });
                                }

                            }
                            else
                            {

                                return Ok(new
                                {
                                    message = "El usuario ya existe.",
                                    statusCode = HttpStatusCode.Conflict

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
                log.Controller = "UserController";
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
        /// Obtiene todos los usuario de la tabla User donde status es igual a 1.
        /// </summary>
        /// <param name="token"></param> 
        /// <response code="200">Retorna un array del objecto User.</response>
        /// <response code="204">Retorna un array vacío del objeto User.</response>   
        /// <response code="403">Retorna un mensaje de que el token esta expirado.</response>   
        /// <response code="500">Retorna un mensaje con error interno del servicio.</response>
        [HttpGet]
        [Route("findAllStatusEqualToOne")]
        [ProducesResponseType(200, Type = typeof(List<User>))]
        [ProducesResponseType(204, Type = typeof(List<User>))]
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
                        List<User> users = userServiceImpl.findAllStatusEqualToOne();

                        if (users.Count > 0)
                        {
                            return Ok(new
                            {

                                users = users,
                                countRows = users.Count,
                                statusCode = HttpStatusCode.OK

                            });
                        }
                        else
                        {
                            return Ok(new
                            {

                                users = users,
                                countRows = users.Count,
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
                log.Controller = "UserConroller";
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