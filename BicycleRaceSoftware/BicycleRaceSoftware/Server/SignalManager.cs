using BicycleRaceSoftware.Server.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using BicycleRaceSoftware.Shared;
using System.Diagnostics;

namespace BicycleRaceSoftware.Server
{
    public class SignalManager
    {

        private readonly IHubContext<RacerStatusHub> _hubContext;

        public SignalManager(IHubContext<RacerStatusHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void OnCheaterDetected(object source, CheaterDetectedEventArgs arg)
        {
            _hubContext.Clients.All.SendAsync("CheaterUpdates", JsonConvert.SerializeObject(arg.Cheaters));
        }

        public void OnDataReceived(object source, RacerStatusReceivedEventArgs arg)
        {
            _hubContext.Clients.All.SendAsync("RacerUpdates", JsonConvert.SerializeObject(arg.Status));
        }
    }
}
