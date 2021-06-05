using Application.Comments.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : MyBaseController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Insert(Create.Execute comment)
        {
            return await Mediator.Send(comment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Execute { CommentId = id });
        }
    }
}
