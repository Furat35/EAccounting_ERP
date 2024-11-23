using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisterDetails.GetAllCashRegisterDetails
{
    public sealed class GetAllCashRegisterDetailsQueryHandler(ICashRegisterRepository cashRegisterRepository)
        : IRequestHandler<GetAllCashRegisterDetailsQuery, Result<CashRegister>>
    {
        public async Task<Result<CashRegister>> Handle(GetAllCashRegisterDetailsQuery request, CancellationToken cancellationToken)
        {
            var cashRegister = await cashRegisterRepository
                .Where(c => c.Id == request.CashRegisterId)
                .Include(c => c.Details.Where(d => d.Date >= request.StartDate  && d.Date <= request.EndDate))
                .FirstOrDefaultAsync(cancellationToken);

            return cashRegister == null 
                ? Result<CashRegister>.Failure("Kasa bulunamadı!")
                : cashRegister;
        }
    }
}
