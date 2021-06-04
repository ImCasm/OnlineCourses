using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyBaseController : ControllerBase
    {

        private IMediator Mediator;

        protected IMediator _mediator => 
            Mediator ?? (Mediator = HttpContext.RequestServices.GetService<IMediator>());      
    }
}
