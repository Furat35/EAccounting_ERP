using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using EAccountingServer.Infrastructure.Context;
using GenericRepository;

namespace EAccountingServer.Infrastructure.Repositories
{
    public class CashRegisterRepository(CompanyDbContext context)
        : Repository<CashRegister, CompanyDbContext>(context), ICashRegisterRepository
    {
    }
}
