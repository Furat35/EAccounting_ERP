using AutoMapper;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Companies.UpdateCompany
{
    public sealed class UpdateCompanyCommandHandler(
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<UpdateCompanyCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await companyRepository
                .GetByExpressionWithTrackingAsync(c => c.Id == request.Id, cancellationToken);
            if (company is null)
                return Result<string>.Failure("Şirket bulunamadı.");

            if (company.TaxNumber != request.TaxNumber
                && await companyRepository.AnyAsync(c => c.TaxNumber == request.TaxNumber, cancellationToken))
                return Result<string>.Failure("Bu vergi numarası daha önce kaydedilmiş.");

            mapper.Map(request, company);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            cacheService.Remove("companies");

            return "Şirket bilgisi başarıyla güncellendi.";
        }
    }
}
