using EAccountingServer.Application.Features.CustomerDetails.GetAllCustomerDetails;
using EAccountingServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EAccountingServer.WebAPI.Controllers
{
    public class CustomerDetailsController(IMediator mediator) : ApiController(mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
