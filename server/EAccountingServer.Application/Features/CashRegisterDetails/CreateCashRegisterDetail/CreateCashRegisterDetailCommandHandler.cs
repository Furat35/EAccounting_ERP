using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Enums;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisterDetails.CreateCashRegisterDetail
{
    public sealed class CreateCashRegisterDetailCommandHandler(
        ICustomerRepository customerRepository,
        ICustomerDetailRepository customerDetailRepository,
        IBankRepository bankRepository,
        IBankDetailRepository bankDetailRepository,
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
            if (request.OppositeCashRegisterId != null || request.BankId != null)
            {
                cashRegister.WithdrawalAmount += request.Amount;
                cashRegisterDetail = new CashRegisterDetail()
                {
                    Date = request.Date,
                    DepositAmount = 0,
                    WithdrawalAmount = request.Amount,
                    Description = request.Description,
                    CashRegisterId = request.CashRegisterId,
                    IsCreatedByThis = true
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
                    CashRegisterId = request.CashRegisterId,
                    IsCreatedByThis = true
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
                await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            }
            else if (request.BankId is not null)
            {
                var bank = await bankRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == request.BankId, cancellationToken);
                bank.DepositAmount += request.Amount;

                var bankDetail = new BankDetail()
                {
                    Date = request.Date,
                    DepositAmount = request.Amount,
                    WithdrawalAmount = 0,
                    CashRegisterDetailId = cashRegisterDetail.Id,
                    BankId = request.BankId,
                    Description = request.Description,
                };
                cashRegisterDetail.BankDetailId = bankDetail.Id;
                await bankDetailRepository.AddAsync(bankDetail, cancellationToken);
                await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            }
            else if (request.CustomerId is not null)
            {
                var customer = await customerRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == request.CustomerId, cancellationToken);

                customer.DepositAmount += request.Type == 1 ? request.Amount : 0;
                customer.WithdrawalAmount += request.Type == 0 ? request.Amount : 0;
                var customerDetail = new CustomerDetail()
                {
                    CustomerId = customer.Id,
                    CashRegisterDetailId = cashRegister.Id,
                    Date = request.Date,
                    Description = request.Description,
                    DepositAmount = request.Type == 1 ? request.Amount : 0,
                    WithdrawalAmount = request.Type == 0 ? request.Amount : 0,
                    Type = CustomerDetailTypeEnum.CashRegister
                };

                cashRegisterDetail.CustomerDetailId = customerDetail.Id;
                await customerDetailRepository.AddAsync(customerDetail, cancellationToken);
                await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            }

            cacheService.Remove("cashRegisters");
            cacheService.Remove("banks");
            cacheService.Remove("customers");

            return "Kasa hareketi başarıyla işlendi.";
        }
    }

}
