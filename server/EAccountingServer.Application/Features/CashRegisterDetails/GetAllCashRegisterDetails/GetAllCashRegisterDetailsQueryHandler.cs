using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisterDetails.GetAllCashRegisterDetails
{
    public sealed class GetAllCashRegisterDetailsQueryHandler(ICashRegisterRepository cashRegisterRepository)
        : IRequestHandler<GetAllBankDetailsQuery, Result<CashRegister>>
    {
        public async Task<Result<CashRegister>> Handle(GetAllBankDetailsQuery request, CancellationToken cancellationToken)
        {
            CashRegister cashRegister;
            if(request.GetAll)
                cashRegister = await cashRegisterRepository
                    .Where(c => c.Id == request.CashRegisterId)
                    .Include(c => c.Details)
                    .FirstOrDefaultAsync(cancellationToken);
            else
                cashRegister = await cashRegisterRepository
                    .Where(c => c.Id == request.CashRegisterId)
                    .Include(c => c.Details.Where(d => d.Date >= request.StartDate && d.Date <= request.EndDate))
                    .FirstOrDefaultAsync(cancellationToken);

            return cashRegister ?? Result<CashRegister>.Failure("Kasa bulunamadı!");
        }
    }
}
