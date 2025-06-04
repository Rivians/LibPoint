using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Notifications;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Notifications.Queries
{
    public class GetNotificationsByUserIdQueryHandler : IRequestHandler<GetNotificationsByUserIdQueryRequest, ResponseModel<List<NotificationModel>>>
    {
        private readonly IRepository<Notification> _notificationModel;

        public Task<ResponseModel<List<NotificationModel>>> Handle(GetNotificationsByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
