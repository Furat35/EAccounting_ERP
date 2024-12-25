using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using EAccountingServer.Infrastructure.Context;
using GenericRepository;

namespace EAccountingServer.Infrastructure.Repositories
{
    public class InvoiceRepository(CompanyDbContext context) : Repository<Invoice, CompanyDbContext>(context), IInvoiceRepository
    {
    }
}
