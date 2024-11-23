using EAccountingServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisters.GetAllCashRegisters
{
    public sealed record GetAllCashRegistersQuery() : IRequest<Result<List<CashRegister>>>;
}
