using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using EAccountingServer.Infrastructure.Context;
using GenericRepository;

namespace EAccountingServer.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product, CompanyDbContext>, IProductRepository
    {
        public ProductRepository(CompanyDbContext context) : base(context)
        {
        }
    }
}
