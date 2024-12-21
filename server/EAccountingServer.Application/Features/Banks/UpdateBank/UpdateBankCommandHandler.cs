using AutoMapper;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Banks.UpdateBank
{
    public sealed class UpdateBankCommandHandler(
        IBankRepository bankRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<UpdateBankCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateBankCommand request, CancellationToken cancellationToken)
        {
            var bank = await bankRepository.GetByExpressionWithTrackingAsync(b => b.Id == request.Id, cancellationToken);
            if (bank is null)
                return Result<string>.Failure("Banka bulunamadı!");
            if (bank.IBAN != request.IBAN)
            {
                var IBANExists = await bankRepository.AnyAsync(p => p.IBAN == request.IBAN, cancellationToken);
                if (IBANExists)
                    return Result<string>.Failure("IBAN daha önce kaydedilmiştir");
            }

            mapper.Map(request, bank);
            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            cacheService.Remove("banks");

            return "Banka bilgileri başarıyla güncellendi.";
        }
    }
}
