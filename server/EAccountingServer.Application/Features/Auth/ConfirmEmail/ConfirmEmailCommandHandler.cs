using EAccountingServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace EAccountingServer.Application.Features.Auth.ConfirmEmail
{
    public sealed class ConfirmEmailCommandHandler(UserManager<AppUser> userManager)
        : IRequestHandler<ConfirmEmailCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var appUser = await userManager.FindByEmailAsync(request.Email);
            if (appUser is null)
                return "Mail adresi sistemde kayıtlı değil!";

            if (appUser.EmailConfirmed)
                return "Mail adresi zaten onaylı!";

            appUser.EmailConfirmed = true;
            await userManager.UpdateAsync(appUser);

            return "Mail adresiniz başarıyla onaylanmıştır!";
        }
    }

}
