using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Invoices.DeleteInvoiceById
{
    public sealed record DeleteInvoiceByIdCommand(Guid Id) : IRequest<Result<string>>;
}
