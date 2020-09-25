using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
