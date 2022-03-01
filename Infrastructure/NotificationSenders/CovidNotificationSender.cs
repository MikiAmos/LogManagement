using System;
using System.Threading.Tasks;
using Application.NotificationSenders;
using Domain;
using Domain.Enums;

namespace Infrastructure.NotificationSenders
{
    public class CovidNotificationSender:INotificationSender
    {
        public NotificationType Type { get; } = NotificationType.Covid;
        public Task SendMessage(NotificationInfo info)
        {
            Console.WriteLine(
                $"Dear {info.FirstName} {info.LastName} in the past few days you was exposed to employee that positive to corona," +
                "you will need to be in quarantine for the next 5 days");
           return Task.CompletedTask;
        }
    }
}