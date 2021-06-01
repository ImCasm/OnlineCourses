using Application.Teachers.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Dapper.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : MyBaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Teacher>>> Get()
        {
            return await _mediator.Send(new Query.TeachersList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetById(Guid id)
        {
            return await _mediator.Send(new QueryById.Execute { TeacherId = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Insert(Create.Execute teacher)
        {
            return await _mediator.Send(teacher);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(Guid id, Edit.Execute teacher)
        {
            teacher.TeacherId = id;
            return await _mediator.Send(teacher);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await _mediator.Send(new Delete.Execute { TeacherId = id });
        }
    }
}
