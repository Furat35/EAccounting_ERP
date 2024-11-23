using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Companies.DeleteCompanyById
{
    public sealed class DeleteCompanyByIdCommandHandler(
        ICompanyRepository companyRepository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService) : IRequestHandler<DeleteCompanyByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteCompanyByIdCommand request, CancellationToken cancellationToken)
        {
            var company = await companyRepository
                .GetByExpressionWithTrackingAsync(c => c.Id == request.Id, cancellationToken);
            if (company is null)
                return Result<string>.Failure("Şirket bulunamadı.");
            company.IsDeleted = true;
            await unitOfWork.SaveChangesAsync(cancellationToken);
            cacheService.Remove("companies");

            return "Şirket başarıyla silindi!";
        }
    }
}
