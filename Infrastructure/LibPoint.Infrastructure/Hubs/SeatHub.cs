using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPoint.Infrastructure.Hubs
{
    public class SeatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Bir istemci bağlandı: " + Context.ConnectionId);
            return base.OnConnectedAsync();
        }
    }
}
