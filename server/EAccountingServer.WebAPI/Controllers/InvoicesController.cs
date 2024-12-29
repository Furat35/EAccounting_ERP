using EAccountingServer.Application.Features.Invoices.CreateInvoice;
using EAccountingServer.Application.Features.Invoices.DeleteInvoiceById;
using EAccountingServer.Application.Features.Invoices.GetAllInvoices;
using EAccountingServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EAccountingServer.WebAPI.Controllers
{
    public class InvoicesController(IMediator mediator) : ApiController(mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] DeleteInvoiceByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
