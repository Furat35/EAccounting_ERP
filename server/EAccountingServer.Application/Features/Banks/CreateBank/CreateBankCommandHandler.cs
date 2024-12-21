using AutoMapper;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Banks.CreateBank
{
    public class CreateBankCommandHandler(
        IBankRepository bankRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<CreateBankCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateBankCommand request, CancellationToken cancellationToken)
        {
            var IBANExists = await bankRepository.AnyAsync(p => p.IBAN == request.IBAN, cancellationToken);
            if (IBANExists)
                return Result<string>.Failure("IBAN daha önce kaydedilmiştir");

            var bank = mapper.Map<Bank>(request);

            await bankRepository.AddAsync(bank, cancellationToken);
            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            cacheService.Remove("banks");

            return "Banka başarıyla kaydedildi.";
        }
    }
}
