using AutoMapper;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Products.UpdateProduct
{
    public class UpdateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<UpdateProductCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
            if (product is null)
                return Result<string>.Failure("Ürün bulunamadı.");

            if (product.Name != request.Name)
            {
                var nameExists = await productRepository.AnyAsync(p => p.Name == request.Name, cancellationToken);
                if (nameExists)
                    return Result<string>.Failure("Ürün adı daha önce kullanılmış");
            }
            mapper.Map(request, product);
            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            cacheService.Remove("products");

            return "Ürün başarıyla güncellendi.";
        }
    }
}
