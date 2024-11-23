using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisters.DeleteCashRegisterById
{
    public sealed class DeleteCashRegisterByIdCommandHandler(
        ICashRegisterRepository cashRegisterRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        ICacheService cacheService) : IRequestHandler<DeleteCashRegisterByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteCashRegisterByIdCommand request, CancellationToken cancellationToken)
        {
            var cashRegister = await cashRegisterRepository
                           .GetByExpressionWithTrackingAsync(c => c.Id == request.Id, cancellationToken);

            if (cashRegister == null)
                return Result<string>.Failure("Kasa kaydı bulunamadı.");

            cashRegister.IsDeleted = true;
            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            cacheService.Remove("cashRegisters");

            return Result<string>.Succeed("Kasa kaydı başarıyla silindi.");
        }
    }
}
