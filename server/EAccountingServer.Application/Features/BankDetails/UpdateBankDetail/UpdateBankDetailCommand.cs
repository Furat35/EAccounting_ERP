using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.BankDetails.UpdateBankDetail
{
    public sealed record UpdateBankDetailCommand(
        Guid Id,
        Guid? BankId,
        int Type,
        decimal Amount,
        string Description) : IRequest<Result<string>>;

}
