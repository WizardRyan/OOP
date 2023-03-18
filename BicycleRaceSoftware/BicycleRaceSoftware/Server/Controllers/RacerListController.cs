using BicycleRaceSoftware.Server.Hubs;
using BicycleRaceSoftware.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BicycleRaceSoftware.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RacerListController : ControllerBase
    {
        public static List<Racer> Racers = new List<Racer>();
        public Dictionary<int, int> GroupNumToStartTimeDict = new Dictionary<int, int>();
        public Dictionary<int, string> GroupNumToGroupNameDict = new Dictionary<int, string>();

        private readonly IHubContext<RacerStatusHub> _hubContext;

        public RacerListController(IHubContext<RacerStatusHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet]
        public IEnumerable<Racer> Get()
        {
            return Racers.ToArray();
        }
    }
}
