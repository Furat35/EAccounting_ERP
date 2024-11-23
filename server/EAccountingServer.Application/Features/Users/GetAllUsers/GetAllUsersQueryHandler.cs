using AutoMapper;
using EAccountingServer.Application.Models.Dtos.Users;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.Users.GetAllUsers
{
    internal sealed class GetAllUsersQueryHandler(
        UserManager<AppUser> userManager,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<GetAllUsersQuery, Result<List<UserListDto>>>
    {
        public async Task<Result<List<UserListDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = cacheService.Get<List<UserListDto>>("users");
            if (users is null)
            {
                var appUsers = await userManager.Users
                   .Include(u => u.CompanyUsers)
                   .ThenInclude(cu => cu.Company)
                   .OrderBy(u => u.FirstName)
                   .ToListAsync(cancellationToken);
                users = mapper.Map<List<UserListDto>>(appUsers);
                cacheService.Set("users", users);
            }

            return mapper.Map<List<UserListDto>>(users);
        }
    }
}
