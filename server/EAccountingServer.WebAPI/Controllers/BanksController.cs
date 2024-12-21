using EAccountingServer.Application.Features.Banks.CreateBank;
using EAccountingServer.Application.Features.Banks.DeleteBankById;
using EAccountingServer.Application.Features.Banks.GetAllBanks;
using EAccountingServer.Application.Features.Banks.UpdateBank;
using EAccountingServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EAccountingServer.WebAPI.Controllers
{
    public class BanksController(IMediator mediator) : ApiController(mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllBanksQuery(), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        //[HttpGet("{request.Id}")]
        //public async Task<IActionResult> GetById([FromRoute] GetBankByIdCommand request, CancellationToken cancellationToken)
        //{
        //    var response = await _mediator.Send(request, cancellationToken);
        //    return StatusCode(response.StatusCode, response);
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBankCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateBankCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] DeleteBankByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
