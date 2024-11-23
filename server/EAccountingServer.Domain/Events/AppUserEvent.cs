using MediatR;

namespace EAccountingServer.Domain.Events
{
    public sealed class AppUserEvent(Guid userId) : INotification
    {
        public Guid UserId { get; set; } = userId;
    }
}
