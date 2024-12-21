using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisters.GetAllCashRegisters
{
    public sealed class GetAllCashRegistersQueryHandler(
        ICashRegisterRepository cashRegisterRepository,
        ICacheService cacheService) : IRequestHandler<GetAllCashRegistersQuery, Result<List<CashRegister>>>
    {
        public async Task<Result<List<CashRegister>>> Handle(GetAllCashRegistersQuery request, CancellationToken cancellationToken)
        {
            var cashRegisters = cacheService.Get<List<CashRegister>>("cashRegisters");
            if (cashRegisters is null)
            {
                cashRegisters = await cashRegisterRepository
                    .GetAll()
                    .Include(_ => _.Details)
                    .OrderBy(_ => _.Name)
                    .ToListAsync(cancellationToken);
                cacheService.Set("cashRegisters", cashRegisters);
            }


            return cashRegisters;
        }
    }
}
