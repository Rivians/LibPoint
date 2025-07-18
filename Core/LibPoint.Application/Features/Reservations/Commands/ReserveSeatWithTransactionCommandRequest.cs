﻿using LibPoint.Domain.Entities.Enums;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Reservations.Commands
{
    public class ReserveSeatWithTransactionCommandRequest : IRequest<ResponseModel<bool>>
    {
        public Guid AppUserId { get; set; }
        public Guid SeatId { get; set; }
        //public int Session { get; set; }
        public int Duration { get; set; }
        //  public DateTime burada istek atılıan tarih utc now'a atanıcak.
    }
}
