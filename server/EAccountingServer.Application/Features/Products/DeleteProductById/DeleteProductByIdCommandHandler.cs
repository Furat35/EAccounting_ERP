using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Products.DeleteProductById
{
    public sealed class DeleteProductByIdCommandHandler(
        IProductRepository ProductRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        ICacheService cacheService) : IRequestHandler<DeleteProductByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = await ProductRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
            if (product is null)
                return Result<string>.Failure("Ürün bulunamadı.");

            product.IsDeleted = true;
            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            cacheService.Remove("products");

            return "Ürün başarıyla silindi.";
        }
    }
}
