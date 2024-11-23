using EAccountingServer.Application.Features.Auth.Login;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Auth.ChangeCompany
{
    public sealed record ChangeCompanyCommand(Guid CompanyId) : IRequest<Result<LoginCommandResponse>>;
}
