using System.Threading.Tasks;
using Application.Security;
using Application.Security.AppUser.Commands;
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
            return await Mediator.Send(loginParams);
        }

        [HttpPost("signup")]
        public async Task<ActionResult<AuthUserData>> Signup(Register.SignUp registerParams)
        {
            return await Mediator.Send(registerParams);
        }

        [HttpPut]
        public async Task<ActionResult<AuthUserData>> Update(UpdateUser.Execute user)
        {
            return await Mediator.Send(user);
        }

        [HttpGet]
        public async Task<ActionResult<AuthUserData>> GetUser()
        {
            return await Mediator.Send(new CurrentUser.Current());
        }
    }
}
