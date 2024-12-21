using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.Banks.GetAllBanks
{
    public sealed class GetAllBanksQueryHandler(
        IBankRepository bankRepository,
        ICacheService cacheService) : IRequestHandler<GetAllBanksQuery, Result<List<Bank>>>
    {
        public async Task<Result<List<Bank>>> Handle(GetAllBanksQuery request, CancellationToken cancellationToken)
        {
            var banks = cacheService.Get<List<Bank>>("banks");
            if (banks is null)
            {
                banks = await bankRepository.GetAll().OrderBy(p => p.Name).ToListAsync(cancellationToken);
                cacheService.Set("banks", banks);
            }

            return banks;
        }
    }
}
