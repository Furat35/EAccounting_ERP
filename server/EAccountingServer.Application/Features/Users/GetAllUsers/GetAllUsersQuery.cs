﻿using EAccountingServer.Application.Models.Dtos.Users;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Users.GetAllUsers
{
    public sealed class GetAllUsersQuery : IRequest<Result<List<UserListDto>>>;
}
