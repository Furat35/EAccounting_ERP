using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Banks.CreateBank
{
    public record CreateBankCommand(string Name, string IBAN, int CurrencyType) : IRequest<Result<string>>;
}
