using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisterDetails.DeleteCashRegisterDetailById
{
    public sealed record DeleteCashRegisterDetailByIdCommand(
        Guid Id) : IRequest<Result<string>>;
}
