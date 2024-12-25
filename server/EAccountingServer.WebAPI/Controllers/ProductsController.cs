using EAccountingServer.Application.Features.Products.CreateProduct;
using EAccountingServer.Application.Features.Products.DeleteProductById;
using EAccountingServer.Application.Features.Products.GetAllProducts;
using EAccountingServer.Application.Features.Products.UpdateProduct;
using EAccountingServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EAccountingServer.WebAPI.Controllers
{
    public class ProductsController(IMediator mediator) : ApiController(mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
