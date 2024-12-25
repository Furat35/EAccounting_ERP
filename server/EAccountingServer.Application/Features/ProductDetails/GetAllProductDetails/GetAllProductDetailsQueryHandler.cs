using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.ProductDetails.GetAllProductDetails
{
    public sealed class GetAllProductDetailsQueryHandler(
        IProductRepository productRepository,
        ICacheService cacheService) : IRequestHandler<GetAllProductDetailsQuery, Result<Product>>
    {
        public async Task<Result<Product>> Handle(GetAllProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.Where(p => p.Id == request.ProductId).Include(p => p.Details).FirstOrDefaultAsync(cancellationToken);
            if (product is null)
            {
                return Result<Product>.Failure("Ürün bulunamadı.");
            }

            return product;
        }
    }
}
