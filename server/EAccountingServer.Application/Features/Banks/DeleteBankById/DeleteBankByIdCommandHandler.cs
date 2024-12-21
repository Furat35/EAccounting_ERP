using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Banks.DeleteBankById
{
    public sealed class DeleteBankByIdCommandHandler(
        IBankRepository bankRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        ICacheService cacheService) : IRequestHandler<DeleteBankByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteBankByIdCommand request, CancellationToken cancellationToken)
        {
            var bank = await bankRepository.GetByExpressionWithTrackingAsync(b => b.Id == request.Id, cancellationToken);
            if (bank is null)
                return Result<string>.Failure("Banka bulunamadı!");
            bank.IsDeleted = true;

            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            cacheService.Remove("banks");

            return "Banka başarıyla silindi";

        }
    }
}
