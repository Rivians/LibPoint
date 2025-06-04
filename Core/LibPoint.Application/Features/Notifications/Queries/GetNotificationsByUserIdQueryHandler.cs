using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Entities.Identity;
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
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IRepository<AppUser> _userRepository;
        public GetNotificationsByUserIdQueryHandler(IRepository<Notification> notificationRepository, IRepository<AppUser> userRepository)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }

        public async Task<ResponseModel<List<NotificationModel>>> Handle(GetNotificationsByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.AppUserId);
            if (user is null)
                return new ResponseModel<List<NotificationModel>>("User not found");

            var notifications = await _notificationRepository.GetAllAsync(n => n.AppUserId == request.AppUserId);

            if (notifications is null)
            {
                return new ResponseModel<List<NotificationModel>>()
                {
                    Success = true,
                    Data = null,
                    Messages = ["User has not any notifications"],
                    StatusCode = 200,
                };
            }

            var orderedNotifications = notifications.OrderByDescending(n => n.CreatedTime).Select(n => new NotificationModel
            {
                AppUserId = request.AppUserId,
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                Type = n.Type
            }).ToList();

            return new ResponseModel<List<NotificationModel>>(orderedNotifications);
        }
    }
}
