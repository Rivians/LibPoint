using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Seats.Commands
{
    public class SetSeatFreeCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid Id { get; set; }
        public SetSeatFreeCommandRequest(Guid ıd)
        {
            Id = ıd;
        }
    }
}
