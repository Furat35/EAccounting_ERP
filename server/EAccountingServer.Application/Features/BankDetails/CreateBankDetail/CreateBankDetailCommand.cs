using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.BankDetails.CreateBankDetail
{
    public sealed record CreateBankDetailCommand(
        Guid? BankId,
        DateOnly Date,
        int Type, // 0 => Deposit, 1 => withdraw
        decimal Amount,
        Guid? OppositeBankId,
        Guid? CashRegisterId,
        string Description) : IRequest<Result<string>>;

}
