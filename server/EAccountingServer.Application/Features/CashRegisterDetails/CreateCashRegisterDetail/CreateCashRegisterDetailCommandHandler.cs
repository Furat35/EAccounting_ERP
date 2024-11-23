using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisterDetails.CreateCashRegisterDetail
{
    public sealed class CreateCashRegisterDetailCommandHandler(
        ICashRegisterRepository cashRegisterRepository,
        ICashRegisterDetailRepository cashRegisterDetailRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        ICacheService cacheService) : IRequestHandler<CreateCashRegisterDetailCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateCashRegisterDetailCommand request, CancellationToken cancellationToken)
        {
            var cashRegister = await cashRegisterRepository
                .GetByExpressionWithTrackingAsync(p => p.Id == request.CashRegisterId, cancellationToken);

            CashRegisterDetail cashRegisterDetail = null;
            if (request.OppositeCashRegisterId != null)
            {
                cashRegister.WithdrawalAmount += request.Amount;
                cashRegisterDetail = new CashRegisterDetail()
                {
                    Date = request.Date,
                    DepositAmount = 0,
                    WithdrawalAmount = request.Amount,
                    Description = request.Description,
                    CashRegisterId = request.CashRegisterId
                };
            }
            else
            {
                cashRegister.DepositAmount += (request.Type == 0 ? request.Amount : 0);
                cashRegister.WithdrawalAmount += (request.Type == 1 ? request.Amount : 0);
                cashRegisterDetail = new CashRegisterDetail()
                {
                    Date = request.Date,
                    DepositAmount = request.Type == 0 ? request.Amount : 0,
                    WithdrawalAmount = request.Type == 1 ? request.Amount : 0,
                    Description = request.Description,
                    CashRegisterId = request.CashRegisterId
                };
            }


            await cashRegisterDetailRepository.AddAsync(cashRegisterDetail, cancellationToken);
            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

            if (request.OppositeCashRegisterId is not null)
            {
                var oppositeCashRegister = await cashRegisterRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == request.OppositeCashRegisterId, cancellationToken);
                oppositeCashRegister.DepositAmount += request.Amount;

                var oppositeCashRegisterDetail = new CashRegisterDetail()
                {
                    Date = request.Date,
                    DepositAmount = request.Amount,
                    WithdrawalAmount = 0,
                    CashRegisterDetailOppositeId = cashRegisterDetail.Id,
                    Description = request.Description,
                    CashRegisterId = request.OppositeCashRegisterId
                };
                cashRegisterDetail.CashRegisterDetailOppositeId = oppositeCashRegisterDetail.Id;
                await cashRegisterDetailRepository.AddAsync(oppositeCashRegisterDetail, cancellationToken);
            }
            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            cacheService.Remove("cashRegisters");

            return "Kasa hareketi başarıyla işlendi.";
        }
    }

}
