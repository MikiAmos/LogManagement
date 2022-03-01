using System.Threading.Tasks;
using Domain;
using Domain.Enums;

namespace Application.NotificationSenders
{
    public interface INotificationSender
    {
        NotificationType Type { get; }
        Task SendMessage(NotificationInfo info);
    }
}