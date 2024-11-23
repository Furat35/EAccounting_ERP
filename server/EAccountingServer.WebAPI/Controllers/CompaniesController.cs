using EAccountingServer.Application.Features.Companies.CreateCompany;
using EAccountingServer.Application.Features.Companies.DeleteCompanyById;
using EAccountingServer.Application.Features.Companies.GetAllCompanies;
using EAccountingServer.Application.Features.Companies.MigrateAllCompanies;
using EAccountingServer.Application.Features.Companies.UpdateCompany;
using EAccountingServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EAccountingServer.WebAPI.Controllers
{
    public class CompaniesController(IMediator mediator) : ApiController(mediator)
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllCompaniesQuery(), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] DeleteCompanyByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> MigrateAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new MigrateAllCompaniesCommand(), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
