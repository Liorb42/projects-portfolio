using AirportSimulatorLogic.Interfaces;
using System;

namespace AirportSimulatorServer.Configurations
{
    public class StationConfig : IStationConfig
    {
        public int StationId { get; set; }
        public TimeSpan TotalDurationOfStay { get; set; }
        public bool IsHoldForLandings { get; set; }
        public bool IsHoldForTakeOffs { get; set; }
        public bool IsLandingHoldNeeded { get; set; }
        public bool IsTakeOffHoldNeeded { get; set; }

    }
}
