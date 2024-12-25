using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Enums;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.BankDetails.CreateBankDetail
{
    public sealed class CreateBankDetailCommandHandler(
        ICustomerDetailRepository customerDetailRepository,
        ICustomerRepository customerRepository,
        ICashRegisterRepository cashRegisterRepository,
        ICashRegisterDetailRepository cashRegisterDetailRepository,
        IBankRepository bankRepository,
        IBankDetailRepository bankDetailRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        ICacheService cacheService) : IRequestHandler<CreateBankDetailCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateBankDetailCommand request, CancellationToken cancellationToken)
        {
            if (request.BankId == request.OppositeBankId)
                return Result<string>.Failure("Aynı bankaya transfer işlemi yapılamaz.");

            var bank = await bankRepository
                .GetByExpressionWithTrackingAsync(p => p.Id == request.BankId, cancellationToken);

            BankDetail bankDetail = null;
            if (request.OppositeBankId != null || request.CashRegisterId != null)
            {
                bank.WithdrawalAmount += request.Amount;
                bankDetail = new BankDetail()
                {
                    Date = request.Date,
                    DepositAmount = 0,
                    WithdrawalAmount = request.Amount,
                    Description = request.Description,
                    BankId = request.BankId,
                    IsCreatedByThis = true
                };
            }
            else
            {
                bank.DepositAmount += (request.Type == 0 ? request.Amount : 0);
                bank.WithdrawalAmount += (request.Type == 1 ? request.Amount : 0);
                bankDetail = new BankDetail()
                {
                    Date = request.Date,
                    DepositAmount = request.Type == 0 ? request.Amount : 0,
                    WithdrawalAmount = request.Type == 1 ? request.Amount : 0,
                    Description = request.Description,
                    BankId = request.BankId,
                    IsCreatedByThis = true
                };
            }

            await bankDetailRepository.AddAsync(bankDetail, cancellationToken);
            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

            if (request.OppositeBankId is not null)
            {
                var oppositeBank = await bankRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == request.OppositeBankId, cancellationToken);
                oppositeBank.DepositAmount += request.Amount;

                var oppositeBankDetail = new BankDetail()
                {
                    Date = request.Date,
                    DepositAmount = request.Amount,
                    WithdrawalAmount = 0,
                    BankDetailOppositeId = bankDetail.Id,
                    Description = request.Description,
                    BankId = request.OppositeBankId
                };
                bankDetail.BankDetailOppositeId = oppositeBankDetail.Id;
                await bankDetailRepository.AddAsync(oppositeBankDetail, cancellationToken);
                await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            }
            else if (request.CashRegisterId is not null)
            {
                var cashRegister = await cashRegisterRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == request.CashRegisterId, cancellationToken);
                cashRegister.DepositAmount += request.Amount;

                var cashRegisterDetail = new CashRegisterDetail()
                {
                    Date = request.Date,
                    DepositAmount = request.Amount,
                    WithdrawalAmount = 0,
                    BankDetailId = bankDetail.Id,
                    Description = request.Description,
                    CashRegisterId = request.CashRegisterId
                };
                bankDetail.CashRegisterDetailId = cashRegisterDetail.Id;
                await cashRegisterDetailRepository.AddAsync(cashRegisterDetail, cancellationToken);
                await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            }
            else if (request.CustomerId is not null)
            {
                var customer = await customerRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.CustomerId, cancellationToken);
                if (customer is null)
                    Result<string>.Failure("Cari bulunamadı");

                customer.DepositAmount += request.Type == 1 ? request.Amount : 0;
                customer.WithdrawalAmount += request.Type == 0 ? request.Amount : 1;

                var customerDetail = new CustomerDetail()
                {
                    CustomerId = customer.Id,
                    BankDetailId = bankDetail.Id,
                    Date = request.Date,
                    Description = request.Description,
                    DepositAmount = request.Type == 1 ? request.Amount : 0,
                    WithdrawalAmount = request.Type == 0 ? request.Amount : 1,
                    Type = CustomerDetailTypeEnum.Bank
                };
                bankDetail.CustomerDetailId = customerDetail.Id;
                await customerDetailRepository.AddAsync(customerDetail, cancellationToken);
                await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            }

            cacheService.Remove("banks");
            cacheService.Remove("cashRegisters");
            cacheService.Remove("customers");

            return "Banka hareketi başarıyla işlendi.";
        }
    }
}
