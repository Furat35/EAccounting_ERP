using AutoMapper;
using EAccountingServer.Application.Models.Dtos.Users;
using EAccountingServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace EAccountingServer.Application.Features.Users.GetUserById
{
    public class GetUserByIdCommandHandler(UserManager<AppUser> userManager, IMapper mapper) : IRequestHandler<GetUserByIdCommand, Result<UserListDto>>
    {
        public async Task<Result<UserListDto>> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            if (user is null)
                return Result<UserListDto>.Failure(StatusCodes.Status404NotFound, "Kullanıcı bulunamadı.");

            return mapper.Map<UserListDto>(user);
        }
    }
}
