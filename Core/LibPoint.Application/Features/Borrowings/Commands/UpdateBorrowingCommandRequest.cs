﻿using LibPoint.Domain.Entities.Identity;
using LibPoint.Domain.Entities;
using LibPoint.Domain.Models.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Features.Borrowings.Commands
{
    public class UpdateBorrowingCommandRequest:IRequest<ResponseModel<Guid>>
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public Guid BookId { get; set; }

    }
}
