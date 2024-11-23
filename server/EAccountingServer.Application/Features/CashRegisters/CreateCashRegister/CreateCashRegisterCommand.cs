using EAccountingServer.Domain.Enums;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisters.CreateCashRegister
{
    public sealed record CreateCashRegisterCommand(
        string Name, 
        int CurrencyType)
        : IRequest<Result<string>>;
}
