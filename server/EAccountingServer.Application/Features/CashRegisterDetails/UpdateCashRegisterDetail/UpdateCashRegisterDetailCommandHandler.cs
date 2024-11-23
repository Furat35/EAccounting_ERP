using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisterDetails.UpdateCashRegisterDetail
{
    public sealed class UpdateCashRegisterDetailCommandHandler(
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

            var cashRegister = await cashRegisterRepository
                .GetByExpressionWithTrackingAsync(c => c.Id == cashRegisterDetail.CashRegisterId, cancellationToken);

            if (cashRegister is null)
                return Result<string>.Failure("Kasa bulunamadı.");

            
            if (cashRegisterDetail.CashRegisterId != null)
            {
                cashRegisterDetail.CashRegisterDetailOpposite = await cashRegisterDetailRepository
                    .GetByExpressionWithTrackingAsync(c => c.Id == cashRegisterDetail.CashRegisterDetailOppositeId, cancellationToken);

                // update cash register
                var oppositeCashRegister = await cashRegisterRepository
                        .GetByExpressionWithTrackingAsync(c => c.Id == cashRegisterDetail.CashRegisterDetailOpposite.CashRegisterId, cancellationToken);
                cashRegister.WithdrawalAmount += request.Amount - cashRegisterDetail.WithdrawalAmount;
                oppositeCashRegister.DepositAmount +=  request.Amount - oppositeCashRegister.DepositAmount;

                // update cash register detail
                var oppositeCashRegisterDetail = await cashRegisterDetailRepository
                    .GetByExpressionWithTrackingAsync(c => c.Id == cashRegisterDetail.CashRegisterDetailOppositeId, cancellationToken);
                cashRegisterDetail.WithdrawalAmount = request.Amount;
                cashRegisterDetail.Description = request.Description;
                oppositeCashRegisterDetail.DepositAmount = request.Amount;
                oppositeCashRegisterDetail.Description = request.Description;
            }
            else
            {
                if (request.Type == 0)
                {
                    cashRegister.DepositAmount = cashRegister.DepositAmount - cashRegisterDetail.DepositAmount + request.Amount;
                    cashRegister.WithdrawalAmount = 0;
                    cashRegisterDetail.Description = request.Description;
                }
                else
                {
                    cashRegister.DepositAmount = 0;
                    cashRegister.WithdrawalAmount = cashRegister.DepositAmount + cashRegisterDetail.WithdrawalAmount - request.Amount;
                    cashRegisterDetail.Description = request.Description;
                }
            }   

            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

            cacheService.Remove("cashRegisters");

            return "Kasa hareketi başarıyla silindi.";
        }
    }

}
