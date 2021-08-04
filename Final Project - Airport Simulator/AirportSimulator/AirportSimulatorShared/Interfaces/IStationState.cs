using System;

namespace AirportSimulatorShared.Interfaces
{
    public interface IStationState
    {
        int Id { get; set; }
        int Number { get; set; }
        int CurrentFlightId { get; set; }
        bool IsFlightLanding { get; set; }
        bool IsFlightTakingOff { get; set; }
        TimeSpan TotalDurationOfStay { get; set; }
        DateTime StayStartTime { get; set; }

    }
}
