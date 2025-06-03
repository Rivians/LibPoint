using LibPoint.Application.Features.Reservations.Commands;
using LibPoint.Application.Features.Reservations.Queries;
using LibPoint.Domain.Entities.Enums;
using LibPoint.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibPoint.API.Controllers
{
    //(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)
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
        public async Task<IActionResult> ReserveSeatWithTransaction([FromBody] ReserveSeatWithTransactionCommandRequest request)
        {
            int[] sessions = [0, 1, 2];

            if (!sessions.Contains(request.Session))
                return BadRequest("Just send the value of session 0, 1 or 2");

            if (request.AppUserId == Guid.Empty || request.SeatId == Guid.Empty)
                return BadRequest(new ResponseModel<bool>("One of ID that you request is invalid"));

            var responseModel = await _mediator.Send(request);

            return responseModel.Success ? Ok(responseModel) : BadRequest(responseModel);
        }

        /// <summary>
        /// sadece admin panelinde manuel olarak işlem yapılmak istenildiğinde. Güncel olarak bunun yerine transaction olan versiyonu kullan.
        /// </summary>
        /// <returns></returns>
        [HttpPost("create-reservation")]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommandRequest request, CancellationToken cancellationToken)
        {
            var responseModel = await _mediator.Send(request);

            return responseModel.Success ? Ok(responseModel) : BadRequest(responseModel);
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
            int[] sessions = [0, 1, 2];

            if (!sessions.Contains(session))
                return BadRequest("Just send the value of session 0, 1 or 2");

            var response = await _mediator.Send(new GetActiveReservationsBySessionQueryRequest(session));

            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost("end-reservation-early")]
        public async Task<IActionResult> EndReservationEary([FromBody] EndReservationEarlyCommandRequest request)
        {
            if (request is null)
                return BadRequest("Request is null");

            var result = await _mediator.Send(request);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [Authorize]
        [HttpGet("get-reservations-by-user")]
        public async Task<IActionResult> GetReservationsByUser()
        {
            var userIdString = User.FindFirstValue("Id"); 

            var result = await _mediator.Send(new GetReservationsByUserQueryRequest(Guid.Parse(userIdString)));

            return result.Success ? Ok(result) : BadRequest(result);              
        }

        [Authorize]
        [HttpGet("generate-qr")]
        public async Task<IActionResult> GenerateQr(Guid reservationId)
        {
            var result = await _mediator.Send(new CreateReservationQrCodeQueryRequest(reservationId));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpPost("check-in-reservation")]
        public async Task<IActionResult> CheckInReservation(Guid reservationId)
        {
            var result = await _mediator.Send(new CheckInReservationCommandRequest(reservationId));

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
