using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.Companies.GetAllCompanies
{

    public sealed class GetAllCompaniesQueryHandler(
        ICompanyRepository companyRepository,
        ICacheService cacheService) : IRequestHandler<GetAllCompaniesQuery, Result<List<Company>>>
    {
        public async Task<Result<List<Company>>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = cacheService.Get<List<Company>>("companies");
            if (companies is null)
            {
                companies = await companyRepository
                    .GetAll()
                    .OrderBy(c => c.Name)
                    .ToListAsync(cancellationToken);
                cacheService.Set("companies", companies);
            }
            return companies;
        }
    }
}