using BicycleRaceSoftware.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BicycleRaceSoftware.Client.Services
{

    public interface IRacerService
    {
        Task StartHub();
        Task LoadRacerList(HttpClient http);

        public List<Racer> Racers { get; set; }
        public List<(CheaterStamp, CheaterStamp)> Cheaters { get; }
        public List<RacerStatus> RacerStatuses { get; }
    }
    public class RacerService : IRacerService
    {
        private HubConnection? _hubConnection;
        private readonly NavigationManager _navManager;

        public List<(CheaterStamp, CheaterStamp)> Cheaters { get; set; }
        public List<RacerStatus?> RacerStatuses { get; set; }

        public List<Racer> Racers { get; set; }

        private int _connectAttemps = 0;
        private int _maxConnectAttemps = 20;

        public RacerService(NavigationManager navManager)
        {

            Cheaters = new List<(CheaterStamp, CheaterStamp)>();
            RacerStatuses = new List<RacerStatus?>(100000);
            for(int i = 0; i < 100000; i++)
            {
                RacerStatuses.Add(null);
            }

            _navManager = navManager;
            _hubConnection = new HubConnectionBuilder()
            .WithUrl(_navManager.ToAbsoluteUri("/racerStatusHub"))
            .Build();

            //Acts as Observer on front end, listens for updates on cheaters and sets the list
            _hubConnection.On<string>("CheaterUpdates", (cheatersList) =>
            {
                Cheaters = JsonConvert.DeserializeObject<List<(CheaterStamp, CheaterStamp)>>(cheatersList);
            });

            //Acts as Observer on front end, listens for updates on Racers and sets the new Racer Status
            _hubConnection.On<string>("RacerUpdates", status =>
            {
                var s = JsonConvert.DeserializeObject<RacerStatus>(status);
                RacerStatuses[s.RacerBibNumber] = s;
            });
        }

        public async Task StartHub()
        {
            await _hubConnection.StartAsync();

            if (_hubConnection.State != HubConnectionState.Connected && _connectAttemps < _maxConnectAttemps)
            {
                _connectAttemps++;
                Console.WriteLine("Failed to Connect, trying again in 100ms");
                Thread.Sleep(100);
                await StartHub();
            }
        }

        public async Task LoadRacerList(HttpClient http)
        {
            var r = await http.GetFromJsonAsync<Racer[]>("RacerList");
            Racers = r.ToList();
        }
    }
}
