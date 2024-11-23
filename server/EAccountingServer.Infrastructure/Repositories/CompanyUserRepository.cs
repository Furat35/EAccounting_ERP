using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using EAccountingServer.Infrastructure.Context;
using GenericRepository;

namespace EAccountingServer.Infrastructure.Repositories
{
    public sealed class CompanyUserRepository(ApplicationDbContext context)
        : Repository<CompanyUser, ApplicationDbContext>(context), ICompanyUserRepository
    {
    }
}
