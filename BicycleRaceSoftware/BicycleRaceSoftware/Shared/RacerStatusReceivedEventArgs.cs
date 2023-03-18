using BicycleRaceSoftware.Shared;

namespace BicycleRaceSoftware.Shared
{
    public class RacerStatusReceivedEventArgs : EventArgs
    {
        public RacerStatus Status { get; set; }
    }
}
