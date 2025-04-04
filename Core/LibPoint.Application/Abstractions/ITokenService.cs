using LibPoint.Domain.Constants;
using LibPoint.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Abstractions
{
    public interface ITokenService 
    {
        Token GenerateToken(AppUser appUser);
    }
}
