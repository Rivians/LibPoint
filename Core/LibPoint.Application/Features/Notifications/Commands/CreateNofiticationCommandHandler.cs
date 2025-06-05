using LibPoint.Application.Abstractions;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Entities.Enums;
using LibPoint.Domain.Entities.Identity;
using LibPoint.Domain.Models.Emails;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Notifications.Commands
{
    public class CreateNofiticationCommandHandler : IRequestHandler<CreateNofiticationCommandRequest, ResponseModel<bool>>
    {
        private readonly IRepository<Notification> _notificationrepository;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IEmailService _emailService;
        public CreateNofiticationCommandHandler(IRepository<Notification> notificationrepository, IRepository<AppUser> userRepository, IEmailService emailService)
        {
            _notificationrepository = notificationrepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<ResponseModel<bool>> Handle(CreateNofiticationCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.AppUserId);
            if (user is null)
                return new ResponseModel<bool>("User not found");

            var notification = new Notification
            {
                AppUserId = user.Id,
                Title = request.Title,
                Message = request.Message,
                Type = Enum.Parse<NotificationType>(request.Type.ToString())
            };

            var addResult = await _notificationrepository.AddAsync(notification);
            if (!addResult)
                return new ResponseModel<bool>("Creating notification failed");

            var saveResult = await _notificationrepository.SaveChangesAsync();

            if ((NotificationType)request.Type == NotificationType.EmailOnly || (NotificationType)request.Type == NotificationType.EmailAndSystem)
            {
                var sendEmailModel = new SendEmailModel
                {
                    To = user.Email,
                    Subject = request.Title,
                    Body = request.Message,
                };

                var sendEmailResult = await _emailService.SendEmailAsync(sendEmailModel);
            }

            if (addResult)
                return new ResponseModel<bool>(await _notificationrepository.SaveChangesAsync());
            else
                return new ResponseModel<bool>("Creating notification operation failed");
        }
    }
}
