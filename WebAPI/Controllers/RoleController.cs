using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Security.Roles.Commands;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : MyBaseController
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetAll()
        {
            return await Mediator.Send(new QueryAll.Execute());
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<List<string>>> GetUserRoles(string userName)
        {
            return await Mediator.Send(new GetUserRoles.Execute { UserName = userName });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(NewUserRole.Execute role)
        {
            return await Mediator.Send(role);
        }

        [HttpPost("add/user")]
        public async Task<ActionResult<Unit>> AddRoleUser(AddRoleUser.Execute roleAllocator)
        {
            return await Mediator.Send(roleAllocator);
        }

        [HttpPost("remove/user")]
        public async Task<ActionResult<Unit>> RemoveRoleUser(RemoveRoleUser.Execute roleRemover)
        {
            return await Mediator.Send(roleRemover);
        }

        [HttpDelete]
        public async Task<ActionResult<Unit>> Delete(DeleteRole.Execute role)
        {
            return await Mediator.Send(role);
        }
    }
}
