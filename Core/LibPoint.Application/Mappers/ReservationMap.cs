using AutoMapper;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Reservations;
using LibPoint.Domain.Models.Seats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Mappers
{
    public class ReservationMap : Profile
    {
        public ReservationMap()
        {
            CreateMap<Reservation, ReservationModel>()
                .ForMember(dest => dest.Seat, opt => opt.MapFrom(src => src.Seat))
                .ReverseMap();
        }
    }
}
