using EAccountingServer.WebAPI.Abstractions;
using FluentEmail.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EAccountingServer.WebAPI.Controllers
{
    [AllowAnonymous]
    public class TestController(IMediator mediator, IFluentEmail fluentEmail) : ApiController(mediator)
    {
        [HttpGet]
        public async Task<IActionResult> SendTestEmail()
        {
            await fluentEmail
                .To("firat@hotmail.com")
                .Subject("Test Maili")
                .Body("<h1>Mail gönderme testi </h1>")
                .SendAsync();

            return NoContent();
        }
    }
}
