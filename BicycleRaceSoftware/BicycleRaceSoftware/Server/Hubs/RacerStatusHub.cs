using Microsoft.AspNetCore.SignalR;
using BicycleRaceSoftware.Shared;
using Newtonsoft.Json;

namespace BicycleRaceSoftware.Server.Hubs
{
    public class RacerStatusHub : Hub
    {
        public async Task SendCheaterUpdate(List<(CheaterStamp, CheaterStamp)> cheaters)
        {
            await Clients.All.SendAsync("CheaterUpdates", JsonConvert.SerializeObject(cheaters));
        }

        public async Task SendRacerUpdate(RacerStatus status)
        {
            await Clients.All.SendAsync("RacerUpdates", status);
        }
    }
}
