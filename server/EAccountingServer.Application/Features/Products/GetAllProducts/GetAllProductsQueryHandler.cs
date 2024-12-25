using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.Products.GetAllProducts
{
    public sealed class GetAllProductsQueryHandler(
        IProductRepository productRepository,
        ICacheService cacheService) : IRequestHandler<GetAllProductsQuery, Result<List<Product>>>
    {
        public async Task<Result<List<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = cacheService.Get<List<Product>>("products");
            if (products is null)
            {
                products = await productRepository.GetAll().OrderBy(p => p.Name).ToListAsync(cancellationToken);
                cacheService.Set("products", products);
            }

            return products;
        }
    }
}
