using LibPoint.Application.Abstractions;
using LibPoint.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Infrastructure.Services.Background
{
    public class SeatHubService : ISeatHubService
    {
        private readonly IHubContext<SeatHub> _hubContext;
        public SeatHubService(IHubContext<SeatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifySeatListUpdatedAsync()
        {
            await _hubContext.Clients.All.SendAsync("SeatListUpdated");
        }
    }
}
