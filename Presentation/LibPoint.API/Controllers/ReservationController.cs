using LibPoint.Application.Features.Reservations.Commands;
using LibPoint.Application.Features.Reservations.Queries;
using LibPoint.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-reservation-by-id")]
        public async Task<IActionResult> GetReservationById([FromHeader] Guid reservationId)
        {
            var response = await _mediator.Send(new GetReservationByIdCommandRequest(reservationId));

            if (response.Success is false)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("reserve-seat-with-transaction")]
        public async Task<IActionResult> ReserveSeatWithTransaction([FromHeader] Guid appUserId, [FromHeader] Guid seatId, [FromHeader] Guid reservationId)
        {
            if (appUserId == Guid.Empty || seatId == Guid.Empty || reservationId == Guid.Empty)
                return BadRequest(new ResponseModel<bool>("One of ID that you request is invalid");

            var result = await _mediator.Send();
            // return
        }

        [HttpPost("create-reservation")]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommandRequest request, CancellationToken cancellationToken)
        {
            var responseModel = await _mediator.Send(request);

            if (responseModel.Success)
                return Ok(responseModel);
            else
                return BadRequest(responseModel);
        }

        [HttpGet("get-expired-reservations")]
        public async Task<IActionResult> GetExpiredReservations()
        {
            var response = await _mediator.Send(new GetExpiredReservationsQueryRequest());

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        /// <summary>
        /// parametre olarak 0 (sabah seansı) , 1 (öğle seansı) veya 2 (akşam seansı) yollaman lazım.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        [HttpGet("get-active-reservations-by-sessions")]
        public async Task<IActionResult> GetActiveReservationsBySessions(int session)       // admin paneli için
        {
            int[] sessions = [1, 2, 3];

            if (!sessions.Contains(session))
                return BadRequest("Just send the value of session 1, 2 or 3");

            var response = await _mediator.Send(new GetActiveReservationsBySessionQueryRequest(session));

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
