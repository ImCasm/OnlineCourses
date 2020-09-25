using Aplicacion.Courses;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : MyBaseController
    {

        [HttpGet]
        public async Task<ActionResult<List<Course>>> Get()
        {
            return await _mediator.Send(new Query.CoursesList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetById(int id)
        {
            return await _mediator.Send(new QueryId.CourseId { Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.New data)
        {
            return await _mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(int id, Edit.EditCourse data)
        {
            data.CourseId = id;
            return await _mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new Delete.DeleteCourse { Id = id});
        }
    }
}
