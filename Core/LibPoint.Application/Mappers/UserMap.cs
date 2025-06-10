using AutoMapper;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Entities.Identity;
using LibPoint.Domain.Models.Seats;
using LibPoint.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Mappers
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<AppUser, UserModel>()
                .ReverseMap();
        }

    }
}
