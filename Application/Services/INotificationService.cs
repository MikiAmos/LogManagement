using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;

namespace Application.Services
{
    public interface INotificationService
    {
        Task NotifyEmployees(IEnumerable<NotificationInfo> notifications, CancellationToken cancellationToken);
    }
}