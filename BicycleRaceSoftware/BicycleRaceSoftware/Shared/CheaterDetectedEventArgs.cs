using BicycleRaceSoftware.Shared;

namespace BicycleRaceSoftware.Shared
{
    public class CheaterDetectedEventArgs : EventArgs
    {
        public List<(CheaterStamp, CheaterStamp)> Cheaters { get; set; }
    }
}
