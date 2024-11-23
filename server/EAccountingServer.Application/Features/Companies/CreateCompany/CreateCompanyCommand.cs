using EAccountingServer.Domain.ValueObjects;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Companies.CreateCompany
{
    public sealed record CreateCompanyCommand(
        string Name,
        string FullAddress,
        string TaxDepartment,
        string TaxNumber,
        Database Database) : IRequest<Result<string>>;
}
