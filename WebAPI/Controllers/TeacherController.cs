using Application.Teachers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Dapper.Teacher;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : MyBaseController
    {
        [Authorize(Roles = Domain.Roles.ADMIN)]
        [HttpGet]
        public async Task<ActionResult<List<Teacher>>> Get()
        {
            return await Mediator.Send(new Query.TeachersList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetById(Guid id)
        {
            return await Mediator.Send(new QueryById.Execute { TeacherId = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Insert(Create.Execute teacher)
        {
            return await Mediator.Send(teacher);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(Guid id, Edit.Execute teacher)
        {
            teacher.TeacherId = id;
            return await Mediator.Send(teacher);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Execute { TeacherId = id });
        }
    }
}
