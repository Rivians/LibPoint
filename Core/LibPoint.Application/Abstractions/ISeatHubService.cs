using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Application.Abstractions
{
    public interface ISeatHubService
    {
        Task NotifySeatListUpdatedAsync();
    }
}
