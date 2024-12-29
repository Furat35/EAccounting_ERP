using EAccountingServer.Domain.Dtos;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Invoices.CreateInvoice
{
    public record CreateInvoiceCommand(
        int TypeValue,
        DateOnly Date,
        string InvoiceNumber,
        Guid CustomerId,
        List<InvoiceDetailDto> Details) : IRequest<Result<string>>;
}
