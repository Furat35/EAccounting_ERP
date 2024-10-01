﻿using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Users.CreateUser
{
    public sealed record CreateUserCommand(
        string FirstName, 
        string LastName, 
        string UserName, 
        string Email, 
        string Password,
        List<string> CompanyIds) : IRequest<Result<string>>;
}