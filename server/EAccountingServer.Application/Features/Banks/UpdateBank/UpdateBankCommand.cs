using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Banks.UpdateBank
{
    public record UpdateBankCommand(Guid Id, string Name, string IBAN, int CurrencyType) : IRequest<Result<string>>;
}
