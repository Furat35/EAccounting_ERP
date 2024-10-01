using EAccountingServer.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
