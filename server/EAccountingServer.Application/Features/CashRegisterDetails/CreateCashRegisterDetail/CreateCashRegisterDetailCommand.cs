using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisterDetails.CreateCashRegisterDetail
{
    public sealed record CreateCashRegisterDetailCommand(
        Guid? CashRegisterId,
        DateOnly Date, 
        int Type, // 0 => Deposit, 1 => withdraw
        decimal Amount,
        Guid? OppositeCashRegisterId,
        string Description) : IRequest<Result<string>>;

}
