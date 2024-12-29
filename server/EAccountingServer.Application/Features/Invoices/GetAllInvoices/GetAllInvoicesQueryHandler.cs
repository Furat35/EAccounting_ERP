using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.Invoices.GetAllInvoices
{
    public sealed class GetAllInvoicesQueryHandler(
        IInvoiceRepository invoiceRepository,
        ICacheService cacheService) : IRequestHandler<GetAllInvoicesQuery, Result<List<Invoice>>>
    {
        public async Task<Result<List<Invoice>>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            List<Invoice>? invoices;
            string key = "invoices";
            invoices = cacheService.Get<List<Invoice>>(key);
            if (invoices is null)
            {
                invoices = await invoiceRepository
                    .GetAll()
                    .Include(p => p.Customer)
                    .Include(p => p.Details)
                    .ThenInclude(p => p.Product)
                    .OrderBy(p => p.Date)
                    .ToListAsync(cancellationToken);
                cacheService.Set(key, invoices);
            }

            return invoices;
        }
    }
}
