using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAccountingServer.Domain.Events
{
    public sealed class AppUserEvent(Guid userId) : INotification
    {
        public Guid UserId { get; set; } = userId;
    }
}
