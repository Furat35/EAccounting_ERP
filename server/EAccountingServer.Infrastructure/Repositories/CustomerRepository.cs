using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using EAccountingServer.Infrastructure.Context;
using GenericRepository;

namespace EAccountingServer.Infrastructure.Repositories
{
    public class CustomerRepository(CompanyDbContext context) : Repository<Customer, CompanyDbContext>(context), ICustomerRepository
    {
    }
}
