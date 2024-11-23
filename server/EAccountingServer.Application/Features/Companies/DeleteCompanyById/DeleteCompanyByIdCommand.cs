﻿using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Companies.DeleteCompanyById
{
    public sealed record DeleteCompanyByIdCommand(Guid Id) : IRequest<Result<string>>;
}
