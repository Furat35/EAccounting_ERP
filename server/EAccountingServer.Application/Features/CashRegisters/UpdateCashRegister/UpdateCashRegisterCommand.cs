using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisters.UpdateCashRegister
{
    public sealed record UpdateCashRegisterCommand(
        Guid Id,
        string Name,
        int CurrencyType)
        : IRequest<Result<string>>;
}
