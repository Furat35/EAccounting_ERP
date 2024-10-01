using AutoMapper;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Companies.CreateCompany
{
    public sealed class CreateCompanyCommandHandler(
        ICompanyRepository companyRepository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper,
        ICacheService cacheService): IRequestHandler<CreateCompanyCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var taxNumberExists = await companyRepository.AnyAsync(c => c.TaxNumber == request.TaxNumber, cancellationToken);
            if(taxNumberExists) 
                return Result<string>.Failure("Bu vergi numarası daha önce kaydedilmiş.");
            var company = mapper.Map<Company>(request);
            await companyRepository.AddAsync(company, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            cacheService.Remove("companies");

            return "Şirket başarıyla oluşturuldu.";
        }
    }
}
