using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Auth;
using Dominio;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MyBaseController
    {
        
        // POST api/<UserController>
        [HttpPost("login")]
        public async Task<ActionResult<AuthUserData>> Login(Login.Run loginParams)
        {
            return await _mediator.Send(loginParams);
        }
    }
}
