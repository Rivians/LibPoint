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
    public class GetNotificationsByUserIdQueryRequest : IRequest<ResponseModel<List<NotificationModel>>>
    {
        public Guid Id { get; set; }
        public GetNotificationsByUserIdQueryRequest(Guid ıd)
        {
            Id = ıd;
        }
    }
}
