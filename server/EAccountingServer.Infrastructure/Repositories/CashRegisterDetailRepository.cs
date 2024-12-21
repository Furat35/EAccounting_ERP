using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using EAccountingServer.Infrastructure.Context;
using GenericRepository;

namespace EAccountingServer.Infrastructure.Repositories
{
    public class CashRegisterDetailRepository(CompanyDbContext context)
        : Repository<CashRegisterDetail, CompanyDbContext>(context), ICashRegisterDetailRepository
    {
    }
}
