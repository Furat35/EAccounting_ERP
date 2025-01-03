﻿using EAccountingServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.CashRegisterDetails.GetAllCashRegisterDetails
{
    public sealed record GetAllCashRegisterDetailsQuery(
        Guid CashRegisterId,
        DateOnly StartDate,
        DateOnly EndDate,
        bool GetAll) : IRequest<Result<CashRegister>>;
}
