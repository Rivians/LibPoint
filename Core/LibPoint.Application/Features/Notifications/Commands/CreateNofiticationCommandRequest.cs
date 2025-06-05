using LibPoint.Domain.Entities.Enums;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Notifications.Commands
{
    public class CreateNofiticationCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid AppUserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int Type { get; set; } // 0 - 1 - 2
    }
}
