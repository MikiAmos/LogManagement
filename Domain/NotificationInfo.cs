using Domain.Enums;

namespace Domain
{
    public record NotificationInfo()
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}