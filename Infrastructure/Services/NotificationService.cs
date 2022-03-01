using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.NotificationSenders;
using Application.Repository;
using Application.Services;
using Domain;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services
{
    public class NotificationService:INotificationService
    {
        private readonly ILogManagementRepository _repository;
        private readonly Dictionary<NotificationType,INotificationSender> _senders;

        public NotificationService(ILogManagementRepository repository,IEnumerable<INotificationSender>senders)
        {
            _repository = repository;
            _senders = senders.ToDictionary(k => k.Type, v => v);
        }

        public Task NotifyEmployees(IEnumerable<NotificationInfo> notifications, CancellationToken cancellationToken)
        {
            foreach (var info in notifications)
            {
                if (_senders.ContainsKey(info.NotificationType))
                    _senders[info.NotificationType].SendMessage(info);
            }
            return Task.CompletedTask;
        }
    }
}