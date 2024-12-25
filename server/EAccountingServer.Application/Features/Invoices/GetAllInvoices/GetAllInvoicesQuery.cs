using EAccountingServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Invoices.GetAllInvoices
{
    public sealed record GetAllInvoicesQuery(int Type) : IRequest<Result<List<Invoice>>>;
}
