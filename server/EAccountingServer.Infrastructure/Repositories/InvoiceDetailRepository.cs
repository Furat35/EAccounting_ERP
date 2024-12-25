using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using EAccountingServer.Infrastructure.Context;
using GenericRepository;

namespace EAccountingServer.Infrastructure.Repositories
{
    public class InvoiceDetailRepository(CompanyDbContext context) : Repository<InvoiceDetail, CompanyDbContext>(context), IInvoiceDetailRepository
    {
    }
}
