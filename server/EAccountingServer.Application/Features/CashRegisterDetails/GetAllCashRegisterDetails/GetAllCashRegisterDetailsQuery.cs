using EAccountingServer.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisterDetails.GetAllCashRegisterDetails
{
    public sealed record GetAllCashRegisterDetailsQuery(
        Guid CashRegisterId, 
        DateOnly StartDate, 
        DateOnly EndDate) 
        : IRequest<Result<CashRegister>>;
}
