using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EAccountingServer.WebAPI.Abstractions
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public abstract class ApiController(IMediator mediator) : ControllerBase
    {
        public readonly IMediator _mediator = mediator;
    }
}
