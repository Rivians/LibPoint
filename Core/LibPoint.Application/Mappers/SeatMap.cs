using AutoMapper;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Seats;

namespace LibPoint.Application.Mappers
{
    public class SeatMap : Profile
    {
        public SeatMap()
        {
            CreateMap<Seat, SeatModel>()
                // reservasyonla alakalı ekleme olucak
                .ReverseMap();
        }
    }
}
