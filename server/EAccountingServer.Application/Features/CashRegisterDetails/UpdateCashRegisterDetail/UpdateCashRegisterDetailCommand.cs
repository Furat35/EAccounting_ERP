using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisterDetails.UpdateCashRegisterDetail
{
    public sealed record UpdateCashRegisterDetailCommand(
        Guid Id,
        Guid? CashRegisterId,
        int Type,
        decimal Amount,
        string Description) : IRequest<Result<string>>;

}
