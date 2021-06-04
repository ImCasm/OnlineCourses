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
            return await _mediator.Send(new QueryAll.Execute());
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<List<string>>> GetUserRoles(string userName)
        {
            return await _mediator.Send(new GetUserRoles.Execute { UserName = userName });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(NewUserRole.Execute role)
        {
            return await _mediator.Send(role);
        }

        [HttpPost("add/user")]
        public async Task<ActionResult<Unit>> AddRoleUser(AddRoleUser.Execute roleAllocator)
        {
            return await _mediator.Send(roleAllocator);
        }

        [HttpPost("remove/user")]
        public async Task<ActionResult<Unit>> RemoveRoleUser(RemoveRoleUser.Execute roleRemover)
        {
            return await _mediator.Send(roleRemover);
        }

        [HttpDelete]
        public async Task<ActionResult<Unit>> Delete(DeleteRole.Execute role)
        {
            return await _mediator.Send(role);
        }
    }
}
