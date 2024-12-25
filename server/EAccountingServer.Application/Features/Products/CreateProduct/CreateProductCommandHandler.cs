using AutoMapper;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Products.CreateProduct
{
    public sealed class CreateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<CreateProductCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var nameExists = await productRepository.AnyAsync(p => p.Name == request.Name, cancellationToken);
            if (nameExists)
                return Result<string>.Failure("Ürün adı daha önce kullanılmış");

            var product = mapper.Map<Product>(request);
            await productRepository.AddAsync(product, cancellationToken);
            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            cacheService.Remove("products");

            return "Ürün kaydı başarıyla tamamlandı.";
        }
    }
}
