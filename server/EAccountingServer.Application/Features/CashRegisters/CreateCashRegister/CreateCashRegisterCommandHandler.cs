using AutoMapper;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisters.CreateCashRegister
{
    public sealed class CreateCashRegisterCommandHandler(
        ICashRegisterRepository cashRegisterRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<CreateCashRegisterCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateCashRegisterCommand request, CancellationToken cancellationToken)
        {
            var nameExists = await cashRegisterRepository
                .AnyAsync(c => c.Name == request.Name, cancellationToken);

            if (nameExists)
                return Result<string>.Failure("Bu kasa adı daha önce kullanılmıştır.");

            var cashRegister = mapper.Map<CashRegister>(request);

            await cashRegisterRepository.AddAsync(cashRegister, cancellationToken);
            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            cacheService.Remove("cashRegisters");

            return "Kasa kaydı başarıyla tamamlandı.";
        }
    }
}
