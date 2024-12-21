using EAccountingServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.BankDetails.GetAllBankDetails
{
    public sealed record GetAllBankDetailsQuery(
        Guid BankId,
        DateOnly StartDate,
        DateOnly EndDate,
        bool GetAll) : IRequest<Result<Bank>>;
}
