using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisterDetails.UpdateCashRegisterDetail
{
    public sealed record UpdateCashRegisterDetailCommand(
        Guid Id,
        Guid? CashRegisterId,
        int Type,
        decimal Amount,
        string Description) : IRequest<Result<string>>;

}
