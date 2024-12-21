using AutoMapper;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Repositories;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisters.UpdateCashRegister
{
    public class UpdateCashRegisterCommandHandler(
        ICashRegisterRepository cashRegisterRepository,
        IUnitOfWorkCompany unitOfWorkCompany,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<UpdateCashRegisterCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateCashRegisterCommand request, CancellationToken cancellationToken)
        {
            var cashRegister = await cashRegisterRepository
                .GetByExpressionWithTrackingAsync(c => c.Id == request.Id, cancellationToken);

            if (cashRegister is null)
                return Result<string>.Failure("Kasa kaydı bulunamadı.");

            if (cashRegister.Name != request.Name)
            {
                var nameExists = await cashRegisterRepository.AnyAsync(c => c.Name == request.Name, cancellationToken);
                if (nameExists)
                    return Result<string>.Failure("Bu kasa adı daha önce kullanılmıştır.");
            }

            mapper.Map(request, cashRegister);

            await unitOfWorkCompany.SaveChangesAsync(cancellationToken);
            cacheService.Remove("cashRegisters");

            return "Kasa başarıyla güncellendi.";
        }
    }
}
