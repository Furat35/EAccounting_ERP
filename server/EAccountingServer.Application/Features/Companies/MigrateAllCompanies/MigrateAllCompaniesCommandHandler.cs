using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.Companies.MigrateAllCompanies
{
    public sealed class MigrateAllCompaniesCommandHandler(
        ICompanyRepository companyRepository, 
        ICompanyService companyService,
        ICacheService cacheService) : IRequestHandler<MigrateAllCompaniesCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(MigrateAllCompaniesCommand request, CancellationToken cancellationToken)
        {
            var companies = await companyRepository.GetAll().ToListAsync(cancellationToken);
            companyService.MigrateAll(companies);
            cacheService.Remove("companies");

            return "Şirket database'leri başarıyla güncellendi.";
        }
    }

}
