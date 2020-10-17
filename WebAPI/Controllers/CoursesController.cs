using Aplicacion.Courses.CQRS;
using Aplicacion.Courses.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : MyBaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<CourseDTO>>> Get()
        {
            return await _mediator.Send(new Query.CoursesList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetById(Guid id)
        {
            return await _mediator.Send(new QueryById.CourseById { Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.New data)
        {
            return await _mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, EditCourse.Edit data)
        {
            data.CourseId = id;
            return await _mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await _mediator.Send(new Delete.DeleteCourse { CourseId = id});
        }
    }
}
