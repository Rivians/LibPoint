using LibPoint.Application.Abstractions;
using LibPoint.Application.Features.Notifications.Commands;
using LibPoint.Domain.Entities.Enums;
using LibPoint.Domain.Entities.Identity;
using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.User.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, ResponseModel<UserRegisterModel>>
    {
        private readonly IAuthService _authService;
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IMediator _mediator;

        public RegisterCommandHandler(IAuthService authService, IRepository<AppUser> appUserRepository, IMediator mediator)
        {
            _authService = authService;
            _appUserRepository = appUserRepository;
            _mediator = mediator;
        }

        public async Task<ResponseModel<UserRegisterModel>> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            UserRegisterModel registerResult = await _authService.RegisterAsync(request);

            if (registerResult.Errors.Any())
                return new ResponseModel<UserRegisterModel>(registerResult.Errors, 400);

            var user = await _appUserRepository.GetByIdAsync(registerResult.UserId);

            var createNotificationCommand = new CreateNofiticationCommandRequest()
            {
                AppUserId = user.Id,
                Title = "Kayıt İşlemi",
                Message = $"Merhaba {user.Name}, kütüphanemize hoş geldiniz!",
                Type = (int)NotificationType.EmailAndSystem
            };

            var createNotificationResult = await _mediator.Send(createNotificationCommand);

            return new ResponseModel<UserRegisterModel>(registerResult);
        }
    }
}
