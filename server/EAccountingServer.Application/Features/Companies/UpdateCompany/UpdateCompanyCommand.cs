using EAccountingServer.Domain.ValueObjects;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Companies.UpdateCompany
{
    public sealed record UpdateCompanyCommand(
        Guid Id,
        string Name,
        string FullAddress,
        string TaxDepartment,
        string TaxNumber,
        Database Database) : IRequest<Result<string>>;
}
