using BicycleRaceSoftware.Shared;
using BicycleRaceSoftware.Client.Shared;

namespace BicycleRaceSoftware.Client.Services
{

    public class ScreenManagerService
    {
        public List<CheaterScreen> CheaterScreens { get; set; } = new List<CheaterScreen>();
        public List<BigScreen> BigScreens { get; set; } = new List<BigScreen>();
        public ScreenManagerService() { }

    }
}
