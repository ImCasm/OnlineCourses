using System.Threading.Tasks;
using Application.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : MyBaseController
    {   

        [HttpPost("login")]
        public async Task<ActionResult<AuthUserData>> Login(Login.Run loginParams)
        {
            return await _mediator.Send(loginParams);
        }

        [HttpPost("signup")]
        public async Task<ActionResult<AuthUserData>> Signup(Register.SignUp registerParams)
        {
            return await _mediator.Send(registerParams);
        }

        [HttpGet]
        public async Task<ActionResult<AuthUserData>> GetUser()
        {
            return await _mediator.Send(new CurrentUser.Current());
        }
    }
}
