using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Banks.DeleteBankById
{
    public record DeleteBankByIdCommand(Guid Id) : IRequest<Result<string>>;
}
