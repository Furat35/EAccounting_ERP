using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.BankDetails.GetAllBankDetails
{
    public sealed class GetAllBankDetailsQueryHandler(IBankRepository BankRepository)
        : IRequestHandler<GetAllBankDetailsQuery, Result<Bank>>
    {
        public async Task<Result<Bank>> Handle(GetAllBankDetailsQuery request, CancellationToken cancellationToken)
        {
            Bank bank;
            if (request.GetAll)
            {
                bank = await BankRepository
                    .Where(c => c.Id == request.BankId)
                    .Include(c => c.Details)
                    .FirstOrDefaultAsync(cancellationToken);
            }
            else
            {
                bank = await BankRepository
                    .Where(c => c.Id == request.BankId)
                    .Include(c => c.Details.Where(d => d.Date >= request.StartDate && d.Date <= request.EndDate))
                    .FirstOrDefaultAsync(cancellationToken);
            }
            

            return bank ?? Result<Bank>.Failure("Banka bulunamadı!");
        }
    }
}
