using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using EAccountingServer.Infrastructure.Context;
using GenericRepository;

namespace EAccountingServer.Infrastructure.Repositories
{
    public sealed class CustomerDetailRepository : Repository<CustomerDetail, CompanyDbContext>, ICustomerDetailRepository
    {
        public CustomerDetailRepository(CompanyDbContext context) : base(context)
        {
        }
    }
}
