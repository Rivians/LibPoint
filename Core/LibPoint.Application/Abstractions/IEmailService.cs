using LibPoint.Domain.Models.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Abstractions
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(SendEmailModel emailModel);
    }
}
