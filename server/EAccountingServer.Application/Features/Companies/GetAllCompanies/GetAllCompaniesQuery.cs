using EAccountingServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Companies.GetAllCompanies
{
    public class GetAllCompaniesQuery() : IRequest<Result<List<Company>>>;
}
