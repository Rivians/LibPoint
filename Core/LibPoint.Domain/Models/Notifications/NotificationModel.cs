using LibPoint.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Domain.Models.Notifications
{
    public class NotificationModel
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; } = NotificationType.SystemOnly;
    }
}
