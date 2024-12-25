using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisterDetails.UpdateCashRegisterDetail
{
    public sealed class UpdateCashRegisterDetailCommandHandler(
        IBankRepository bankRepository,
        IBankDetailRepository bankDetailRepository,
        ICashRegisterRepository cashRegisterRepository,
        ICashRegisterDetailRepository cashRegisterDetailRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        ICacheService cacheService) : IRequestHandler<UpdateCashRegisterDetailCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateCashRegisterDetailCommand request, CancellationToken cancellationToken)
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


            if (cashRegisterDetail.CashRegisterDetailOppositeId is not null)
            {
                cashRegisterDetail.CashRegisterDetailOpposite = await cashRegisterDetailRepository
                    .GetByExpressionWithTrackingAsync(c => c.Id == cashRegisterDetail.CashRegisterDetailOppositeId, cancellationToken);

                // update cash register
                var oppositeCashRegister = await cashRegisterRepository
                        .GetByExpressionWithTrackingAsync(c => c.Id == cashRegisterDetail.CashRegisterDetailOpposite.CashRegisterId, cancellationToken);
                cashRegister.WithdrawalAmount += request.Amount - cashRegisterDetail.WithdrawalAmount;
                oppositeCashRegister.DepositAmount += request.Amount - oppositeCashRegister.DepositAmount;

                // update cash register detail
                var oppositeCashRegisterDetail = await cashRegisterDetailRepository
                    .GetByExpressionWithTrackingAsync(c => c.Id == cashRegisterDetail.CashRegisterDetailOppositeId, cancellationToken);
                cashRegisterDetail.WithdrawalAmount = request.Amount;
                cashRegisterDetail.Description = request.Description;
                oppositeCashRegisterDetail.DepositAmount = request.Amount;
                oppositeCashRegisterDetail.Description = request.Description;
            }
            else if (cashRegisterDetail.BankDetailId is not null)
            {
                cashRegisterDetail.BankDetail = await bankDetailRepository
                    .GetByExpressionWithTrackingAsync(c => c.Id == cashRegisterDetail.BankDetailId, cancellationToken);

                // update bank
                var oppositeBank = await bankRepository
                        .GetByExpressionWithTrackingAsync(c => c.Id == cashRegisterDetail.BankDetail.BankId, cancellationToken);
                cashRegister.WithdrawalAmount += request.Amount - cashRegisterDetail.WithdrawalAmount;
                oppositeBank.DepositAmount += request.Amount - oppositeBank.DepositAmount;

                // update bank detail
                var oppositeBankDetail = await bankDetailRepository
                    .GetByExpressionWithTrackingAsync(c => c.Id == cashRegisterDetail.BankDetailId, cancellationToken);
                cashRegisterDetail.WithdrawalAmount = request.Amount;
                cashRegisterDetail.Description = request.Description;
                oppositeBankDetail.DepositAmount = request.Amount;
                oppositeBankDetail.Description = request.Description;
            }
            else
            {
                if (request.Type == 0)
                {
                    cashRegister.DepositAmount = cashRegister.DepositAmount - cashRegisterDetail.DepositAmount + request.Amount;
                    cashRegisterDetail.Description = request.Description;
                    cashRegisterDetail.DepositAmount = request.Amount;
                }
                else
                {
                    cashRegister.WithdrawalAmount = cashRegister.DepositAmount + cashRegisterDetail.WithdrawalAmount - request.Amount;
                    cashRegisterDetail.Description = request.Description;
                    cashRegisterDetail.WithdrawalAmount = request.Amount;
                }
            }

            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

            cacheService.Remove("cashRegisters");
            cacheService.Remove("banks");

            return "Kasa hareketi başarıyla silindi.";
        }
    }

}
