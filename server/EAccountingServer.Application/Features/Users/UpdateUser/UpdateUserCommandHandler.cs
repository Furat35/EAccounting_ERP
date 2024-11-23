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

namespace EAccountingServer.Application.Features.Users.UpdateUser
{
    internal sealed class UpdateUserCommandHandler(
        IMediator mediator,
        UserManager<AppUser> userManager,
        IMapper mapper,
        ICompanyUserRepository companyUserRepository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService) : IRequestHandler<UpdateUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var appUser = await userManager.Users
                .Where(u => u.Id == request.Id)
                .Include(c => c.CompanyUsers)
                .FirstOrDefaultAsync(cancellationToken);
            bool mailChanged = false;

            if (appUser is null)
                return Result<string>.Failure("Kullanıcı bulunamadı.");

            if (appUser.UserName != request.UserName)
            {
                var usernameExists = await userManager.Users
                    .AnyAsync(p => p.UserName == request.UserName, cancellationToken);
                if (usernameExists)
                    return Result<string>.Failure("Bu kullanıcı adı daha önce kullanılmış.");
            }

            if (appUser.Email != request.Email)
            {
                var emailExists = await userManager.Users
                    .AnyAsync(p => p.Email == request.Email, cancellationToken);
                if (emailExists)
                    return Result<string>.Failure("Bu mail adresi daha önce kullanılmış.");

                mailChanged = true;
                appUser.EmailConfirmed = false;
            }

            mapper.Map(request, appUser);
            var identityResult = await userManager.UpdateAsync(appUser);

            if (!identityResult.Succeeded)
                return Result<string>.Failure(identityResult.Errors.Select(s => s.Description).ToList());

            if (!string.IsNullOrEmpty(request.Password))
            {
                string token = await userManager.GeneratePasswordResetTokenAsync(appUser);
                identityResult = await userManager.ResetPasswordAsync(appUser, token, request.Password);

                if (!identityResult.Succeeded)
                    return Result<string>.Failure(identityResult.Errors.Select(s => s.Description).ToList());
            }

            companyUserRepository.DeleteRange(appUser.CompanyUsers);
            var companyUsers = request.CompanyIds.Select(id => new CompanyUser
            {
                UserId = appUser.Id,
                CompanyId = id
            }).ToList();

            await companyUserRepository.AddRangeAsync(companyUsers, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            if (mailChanged)
                await mediator.Publish(new AppUserEvent(appUser.Id), cancellationToken);

            cacheService.Remove("users");
            return "Kullanıcı başarıyla güncellendi.";
        }
    }
}
