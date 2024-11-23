using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Auth.ConfirmEmail
{
    public sealed record ConfirmEmailCommand(string Email) : IRequest<Result<string>>;
}
