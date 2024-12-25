using EAccountingServer.Application.Features.ProductDetails.GetAllProductDetails;
using EAccountingServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EAccountingServer.WebAPI.Controllers
{
    public class ProductDetailsController(IMediator mediator) : ApiController(mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
