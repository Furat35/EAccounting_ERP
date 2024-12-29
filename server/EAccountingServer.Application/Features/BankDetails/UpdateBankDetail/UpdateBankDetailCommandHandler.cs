using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace EAccountingServer.Application.Features.BankDetails.UpdateBankDetail
{
    public sealed class UpdateBankDetailCommandHandler(
        ICashRegisterRepository cashRegisterRepository,
        ICashRegisterDetailRepository cashRegisterDetailRepository,
        IBankRepository bankRepository,
        IBankDetailRepository bankDetailRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        ICacheService cacheService) : IRequestHandler<UpdateBankDetailCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateBankDetailCommand request, CancellationToken cancellationToken)
        {
            var bankDetail = await bankDetailRepository
               .GetByExpressionWithTrackingAsync(c => c.Id == request.Id, cancellationToken);

            if (bankDetail is null)
                return Result<string>.Failure("Banka hareketi bulunamadı.");

            if (bankDetail.IsCreatedByThis)
                return Result<string>.Failure(StatusCodes.Status400BadRequest, "Yetkisiz işlem.");

            var bank = await bankRepository
                .GetByExpressionWithTrackingAsync(c => c.Id == bankDetail.BankId, cancellationToken);

            if (bank is null)
                return Result<string>.Failure("Banka bulunamadı.");


            if (bankDetail.BankDetailOppositeId != null)
            {
                bankDetail.BankDetailOpposite = await bankDetailRepository
                    .GetByExpressionWithTrackingAsync(c => c.Id == bankDetail.BankDetailOppositeId, cancellationToken);

                // update bank
                var oppositeBank = await bankRepository
                        .GetByExpressionWithTrackingAsync(c => c.Id == bankDetail.BankDetailOpposite.BankId, cancellationToken);
                bank.WithdrawalAmount += request.Amount - bankDetail.WithdrawalAmount;
                oppositeBank.DepositAmount += request.Amount - oppositeBank.DepositAmount;

                // update bank detail
                var oppositeBankDetail = await bankDetailRepository
                    .GetByExpressionWithTrackingAsync(c => c.Id == bankDetail.BankDetailOppositeId, cancellationToken);
                bankDetail.WithdrawalAmount = request.Amount;
                bankDetail.Description = request.Description;
                oppositeBankDetail.DepositAmount = request.Amount;
                oppositeBankDetail.Description = request.Description;
            }
            else if (bankDetail.CashRegisterDetailId != null)
            {
                bankDetail.CashRegisterDetail = await cashRegisterDetailRepository
                    .GetByExpressionWithTrackingAsync(c => c.Id == bankDetail.CashRegisterDetailId, cancellationToken);

                // update bank
                var oppositeCashRegister = await cashRegisterRepository
                        .GetByExpressionWithTrackingAsync(c => c.Id == bankDetail.CashRegisterDetail.CashRegisterId, cancellationToken);
                bank.WithdrawalAmount += request.Amount - bankDetail.WithdrawalAmount;
                oppositeCashRegister.DepositAmount += request.Amount - bankDetail.DepositAmount;

                // update bank detail
                var oppositeCashRegisterDetail = await cashRegisterDetailRepository
                    .GetByExpressionWithTrackingAsync(c => c.Id == bankDetail.CashRegisterDetailId, cancellationToken);
                bankDetail.WithdrawalAmount = request.Amount;
                bankDetail.Description = request.Description;
                oppositeCashRegisterDetail.DepositAmount = request.Amount;
                oppositeCashRegisterDetail.Description = request.Description;
            }
            else
            {
                if (request.Type == 0)
                {
                    bank.DepositAmount = bank.DepositAmount - bankDetail.DepositAmount + request.Amount;
                    bankDetail.Description = request.Description;
                    bankDetail.WithdrawalAmount = 0;
                    bankDetail.DepositAmount = request.Amount;
                }
                else
                {
                    // todo : eski bank detaili update etmek yerine bu silindi olarak işaretlenip yeni bir bankdetail oluşturulması daha iyi.
                    bank.WithdrawalAmount = bank.DepositAmount + bankDetail.WithdrawalAmount - request.Amount;
                    bankDetail.Description = request.Description;
                    bankDetail.WithdrawalAmount = request.Amount;
                    bankDetail.DepositAmount = 0;
                }
            }

            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

            cacheService.Remove("banks");
            cacheService.Remove("cashRegisters");

            return "Banka hareketi başarıyla silindi.";
        }
    }

}
