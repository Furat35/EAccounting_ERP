using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using EAccountingServer.Infrastructure.Context;
using GenericRepository;

namespace EAccountingServer.Infrastructure.Repositories
{
    public sealed class CompanyRepository(ApplicationDbContext context)
        : Repository<Company, ApplicationDbContext>(context), ICompanyRepository
    {
    }
}
