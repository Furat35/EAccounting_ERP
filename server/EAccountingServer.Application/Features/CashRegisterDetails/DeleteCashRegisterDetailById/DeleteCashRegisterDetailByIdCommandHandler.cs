using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisterDetails.DeleteCashRegisterDetailById
{
    public sealed class DeleteBankDetailByIdCommandHandler(
        IBankDetailRepository bankDetailRepository,
        IBankRepository bankRepository,
        ICashRegisterRepository cashRegisterRepository,
        ICashRegisterDetailRepository cashRegisterDetailRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        ICacheService cacheService) : IRequestHandler<DeleteCashRegisterDetailByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteCashRegisterDetailByIdCommand request, CancellationToken cancellationToken)
        {
            var cashRegisterDetail = await cashRegisterDetailRepository
                .GetByExpressionWithTrackingAsync(c => c.Id == request.Id, cancellationToken);

            if (cashRegisterDetail is null)
                return Result<string>.Failure("Kasa hareketi bulunamadı.");

            if (cashRegisterDetail.IsCreatedByThis)
                return Result<string>.Failure(StatusCodes.Status400BadRequest, "Yetkisiz işlem.");

            var cashRegister = await cashRegisterRepository
                .GetByExpressionWithTrackingAsync(c => c.Id == cashRegisterDetail.CashRegisterId, cancellationToken);

            if (cashRegister is null)
                return Result<string>.Failure("Kasa bulunamadı.");

            cashRegister.DepositAmount -= cashRegisterDetail.DepositAmount;
            cashRegister.WithdrawalAmount -= cashRegisterDetail.WithdrawalAmount;
            cashRegisterDetail.IsDeleted = true;

            cashRegisterDetailRepository.Update(cashRegisterDetail);
            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

            if (cashRegisterDetail.CashRegisterDetailOppositeId is not null)
            {
                var oppositeCashRegisterDetail = await cashRegisterDetailRepository
                    .GetByExpressionWithTrackingAsync(c => c.Id == cashRegisterDetail.CashRegisterDetailOppositeId, cancellationToken);
                oppositeCashRegisterDetail.IsDeleted = true;
                var oppositeCashRegister = await cashRegisterRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == oppositeCashRegisterDetail.CashRegisterId, cancellationToken);

                oppositeCashRegister.DepositAmount -= oppositeCashRegisterDetail.DepositAmount;
                oppositeCashRegister.WithdrawalAmount -= oppositeCashRegisterDetail.WithdrawalAmount;

                cashRegisterDetailRepository.Update(oppositeCashRegisterDetail);
                await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            }
            else if (cashRegisterDetail.BankDetailId is not null)
            {
                var oppositeBankDetail = await bankDetailRepository
                    .GetByExpressionWithTrackingAsync(c => c.Id == cashRegisterDetail.BankDetailId, cancellationToken);
                oppositeBankDetail.IsDeleted = true;

                var oppositeBank = await bankRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == oppositeBankDetail.BankId, cancellationToken);

                oppositeBank.DepositAmount -= oppositeBankDetail.DepositAmount;
                oppositeBank.WithdrawalAmount -= oppositeBankDetail.WithdrawalAmount;

                bankDetailRepository.Update(oppositeBankDetail);
                await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            }

            cacheService.Remove("cashRegisters");
            cacheService.Remove("banks");

            return "Kasa hareketi başarıyla silindi.";
        }
    }
}
