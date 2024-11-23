using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisters.GetAllCashRegisters
{
    public sealed class GetAllCashRegistersQueryHandler(
        ICashRegisterRepository cashRegisterRepository,
        ICacheService cacheService)
        : IRequestHandler<GetAllCashRegistersQuery, Result<List<CashRegister>>>
    {
        public async Task<Result<List<CashRegister>>> Handle(GetAllCashRegistersQuery request, CancellationToken cancellationToken)
        {
            //var cashRegisters = cacheService.Get<List<CashRegister>>("cashRegisters");
            List<CashRegister> cashRegisters = null;
            //List<CashRegister> cashRegisters = null;
            if (cashRegisters is null)
            {
                cashRegisters = await cashRegisterRepository
                    .GetAll()
                    .Include(_ => _.Details)
                    .OrderBy(_ => _.Name)
                    .ToListAsync(cancellationToken);
                //mapperCashRegisters = mapper.Map<List<CashRegister>>(cashRegisters);
                cacheService.Set("cashRegisters", cashRegisters);
            }


            return cashRegisters;
        }
    }
}
