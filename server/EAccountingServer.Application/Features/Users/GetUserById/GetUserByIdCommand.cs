using EAccountingServer.Application.Models.Dtos.Users;
using MediatR;
using TS.Result;

namespace EAccountingServer.Application.Features.Users.GetUserById
{
    public record GetUserByIdCommand(string Id) : IRequest<Result<UserListDto>>;
}
