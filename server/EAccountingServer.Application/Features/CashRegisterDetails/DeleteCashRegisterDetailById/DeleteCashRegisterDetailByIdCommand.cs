using EAccountingServer.Application.Features.CashRegisters.DeleteCashRegisterById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisterDetails.DeleteCashRegisterDetailById
{
    public sealed record DeleteCashRegisterDetailByIdCommand(
        Guid Id) : IRequest<Result<string>>;
 }
