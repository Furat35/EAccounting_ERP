  using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisterDetails.DeleteCashRegisterDetailById
{
    public sealed class DeleteCashRegisterDetailByIdCommandHandler(
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

                if (cashRegisterDetail is null)
                    return Result<string>.Failure("Kasa hareketi bulunamadı.");

                var oppositeCashRegister = await cashRegisterRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == oppositeCashRegisterDetail.CashRegisterId, cancellationToken);

                if (oppositeCashRegister is null)
                    return Result<string>.Failure("Kasa bulunamadı.");

                oppositeCashRegister.DepositAmount -= oppositeCashRegisterDetail.DepositAmount;
                oppositeCashRegister.WithdrawalAmount -= oppositeCashRegisterDetail.WithdrawalAmount;

                cashRegisterDetailRepository.Update(oppositeCashRegisterDetail);
                await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            }

           
            cacheService.Remove("cashRegisters");

            return "Kasa hareketi başarıyla silindi.";
        }
    } 
 }
