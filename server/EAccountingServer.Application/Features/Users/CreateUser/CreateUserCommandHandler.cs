using AutoMapper;
using EAccountingServer.Application.Services;
using EAccountingServer.Domain.Entities;
using EAccountingServer.Domain.Events;
using EAccountingServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EAccountingServer.Application.Features.Users.CreateUser
{
    public sealed class CreateUserCommandHandler(
        IMediator mediator, 
        UserManager<AppUser> userManager, 
        IMapper mapper,
        ICacheService cacheService)
        : IRequestHandler<CreateUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            bool userNameExists = await userManager.Users.AnyAsync(p => p.UserName == request.UserName, cancellationToken);
            if (userNameExists)
                return Result<string>.Failure("Bu kullanıcı adı daha önce kullanılmış.");

            var emailExists = await userManager.Users.AnyAsync(p => p.Email == request.Email, cancellationToken);
            if (emailExists)
                return Result<string>.Failure("Bu mail adresi daha önce kullanılmış.");

            var appUser = mapper.Map<AppUser>(request);
            appUser.CompanyUsers = request.CompanyIds.Select(id => new CompanyUser
            {
                CompanyId = Guid.Parse(id)
            }).ToList();

            var identityResult = await userManager.CreateAsync(appUser, request.Password);
            if (!identityResult.Succeeded)
                return Result<string>.Failure(identityResult.Errors.Select(s => s.Description).ToList());
            
            await mediator.Publish(new AppUserEvent(appUser.Id), cancellationToken);
            cacheService.Remove("users");

            return "Kullanıcı kaydı başarıyla tamamlandı.";
        }
    }
}
