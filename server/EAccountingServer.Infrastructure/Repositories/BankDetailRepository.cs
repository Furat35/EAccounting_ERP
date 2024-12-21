using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using EAccountingServer.Infrastructure.Context;
using GenericRepository;

namespace EAccountingServer.Infrastructure.Repositories
{
    public class BankDetailRepository(CompanyDbContext context)
        : Repository<BankDetail, CompanyDbContext>(context), IBankDetailRepository
    {
    }
}
