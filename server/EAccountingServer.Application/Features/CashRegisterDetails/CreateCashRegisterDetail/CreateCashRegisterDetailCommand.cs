using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisterDetails.CreateCashRegisterDetail
{
    public sealed record CreateCashRegisterDetailCommand(
        Guid? CashRegisterId,
        Guid? BankId,
        DateOnly Date,
        int Type, // 0 => Deposit, 1 => withdraw
        decimal Amount,
        Guid? OppositeCashRegisterId,
        Guid? CustomerId,
        string Description) : IRequest<Result<string>>;

}
