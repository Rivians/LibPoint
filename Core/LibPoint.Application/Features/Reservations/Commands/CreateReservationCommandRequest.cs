using LibPoint.Domain.Entities.Enums;
using LibPoint.Domain.Models.Responses;
using MediatR;

namespace LibPoint.Application.Features.Reservations.Commands
{
    public class CreateReservationCommandRequest : IRequest<ResponseModel<Guid>>
    {        

        public Guid AppUserId { get; set; }
        public Guid SeatId { get; set; }
        public Session Session { get; set; }
        public int Duration { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
    }
}
