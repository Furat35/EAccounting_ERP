using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using EAccountingServer.Infrastructure.Context;
using GenericRepository;

namespace EAccountingServer.Infrastructure.Repositories
{
    public class ProductDetailRepository(CompanyDbContext context)
        : Repository<ProductDetail, CompanyDbContext>(context), IProductDetailRepository
    {
    }
}
