using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Companies.MigrateAllCompanies
{
    public sealed record MigrateAllCompaniesCommand() : IRequest<Result<string>>;
}
