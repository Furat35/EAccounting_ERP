using EAccountingServer.Application.Features.CashRegisterDetails.DeleteCashRegisterDetailById;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace EAccountingServer.Application.Features.BankDetails.DeleteBankDetailById
{
    public sealed class DeleteBankDetailByIdCommandHandler(
        ICustomerDetailRepository customerDetailRepository,
        ICustomerRepository customerRepository,
        ICashRegisterRepository cashRegisterRepository,
        ICashRegisterDetailRepository cashRegisterDetailRepository,
        IBankRepository bankRepository,
        IBankDetailRepository bankDetailRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        ICacheService cacheService) : IRequestHandler<DeleteBankDetailByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteBankDetailByIdCommand request, CancellationToken cancellationToken)
        {
            var bankDetail = await bankDetailRepository
                .GetByExpressionWithTrackingAsync(c => c.Id == request.Id, cancellationToken);

            if (bankDetail is null)
                return Result<string>.Failure("Banka hareketi bulunamadı.");

            if (!bankDetail.IsCreatedByThis)
                return Result<string>.Failure(StatusCodes.Status400BadRequest, "Yetkisiz işlem.");

            var bank = await bankRepository
                .GetByExpressionWithTrackingAsync(c => c.Id == bankDetail.BankId, cancellationToken);

            if (bank is null)
                return Result<string>.Failure("Banka bulunamadı.");

            bank.DepositAmount -= bankDetail.DepositAmount;
            bank.WithdrawalAmount -= bankDetail.WithdrawalAmount;
            bankDetail.IsDeleted = true;

            bankDetailRepository.Update(bankDetail);
            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);

            if (bankDetail.BankDetailOppositeId is not null)
            {
                var oppositeBankDetail = await bankDetailRepository
                    .GetByExpressionWithTrackingAsync(c => c.Id == bankDetail.BankDetailOppositeId, cancellationToken);
                oppositeBankDetail.IsDeleted = true;

                var oppositeBank = await bankRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == oppositeBankDetail.BankId, cancellationToken);

                oppositeBank.DepositAmount -= oppositeBankDetail.DepositAmount;
                oppositeBank.WithdrawalAmount -= oppositeBankDetail.WithdrawalAmount;

                bankDetailRepository.Update(oppositeBankDetail);
                await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            }
            else if (bankDetail.CashRegisterDetailId is not null)
            {
                var cashRegisterDetail = await cashRegisterDetailRepository
                    .GetByExpressionWithTrackingAsync(c => c.Id == bankDetail.CashRegisterDetailId, cancellationToken);
                cashRegisterDetail.IsDeleted = true;

                var cashRegister = await cashRegisterRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == cashRegisterDetail.CashRegisterId, cancellationToken);

                cashRegister.DepositAmount -= cashRegisterDetail.DepositAmount;
                cashRegister.WithdrawalAmount -= cashRegisterDetail.WithdrawalAmount;

                cashRegisterDetailRepository.Update(cashRegisterDetail);
                await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            }
            else if (bankDetail.CustomerDetailId is not null)
            {
                var customerDetail = await customerDetailRepository
                    .GetByExpressionWithTrackingAsync(c => c.Id == bankDetail.CustomerDetailId, cancellationToken);
                customerDetail.IsDeleted = true;

                var customer = await customerRepository
                    .GetByExpressionWithTrackingAsync(p => p.Id == customerDetail.CustomerId, cancellationToken);

                customer.DepositAmount -= customerDetail.DepositAmount;
                customer.WithdrawalAmount -= customerDetail.WithdrawalAmount;

                customerDetailRepository.Update(customerDetail);
                await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            }

            cacheService.Remove("banks");
            cacheService.Remove("cashRegisters");
            cacheService.Remove("customers");

            return "Banka hareketi başarıyla silindi.";
        }
    }
}
