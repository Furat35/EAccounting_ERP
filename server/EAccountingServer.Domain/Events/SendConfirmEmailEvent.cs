using EAccountingServer.Domain.Entities;
using FluentEmail.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EAccountingServer.Domain.Events
{
    public sealed class SendConfirmEmailEvent(UserManager<AppUser> userManager, IFluentEmail fluentEmail) : INotificationHandler<AppUserEvent>
    {
        public async Task Handle(AppUserEvent notification, CancellationToken cancellationToken)
        {
            var appUser = await userManager.FindByIdAsync(notification.UserId.ToString());
            if (appUser is not null)
            {
                try
                {
                    await fluentEmail
                   .To("firat@hotmail.com")
                   .Subject("Mail Onayı")
                   .Body(CreateBody(appUser), true)
                   .SendAsync();
                }
                catch
                {
                    Console.WriteLine("hata eklenebilir");
                }

            }
        }

        private static string CreateBody(AppUser appUser)
        {
            string body = $@"<h1>Mail adresinizi onaylamak için aşağıdaki linke tıklayınız.</h1>
<a href='https://localhost:7054/api/auth/confirmemail/{appUser.Email}' target='_blank'>Maili Onayla</a>";

            return body;
        }
    }
}
