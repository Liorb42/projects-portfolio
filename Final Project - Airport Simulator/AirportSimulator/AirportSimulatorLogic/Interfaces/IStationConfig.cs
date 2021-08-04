using System;

namespace AirportSimulatorLogic.Interfaces
{
    public interface IStationConfig
    {
        int StationId { get; set; }
        TimeSpan TotalDurationOfStay { get; set; }
        bool IsHoldForLandings { get; set; }
        bool IsHoldForTakeOffs { get; set; }
        bool IsLandingHoldNeeded { get; set; }
        bool IsTakeOffHoldNeeded { get; set; }

    }
}