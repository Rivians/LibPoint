using LibPoint.Domain.Models.Responses;
using LibPoint.Domain.Models.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.User.Queries
{
    public class GetUserListQueryRequest : IRequest<ResponseModel<List<UserModel>>>
    {
    }
}
