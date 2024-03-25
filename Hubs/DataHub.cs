using adad.Models;
using adad.Services;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Org.BouncyCastle.Bcpg.Sig;

namespace SignalRChat.Hubs
{
    public class DataHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendLocations(string connectionID)
        {
            await Clients.All.SendAsync("LocationData");
        }
    }
}