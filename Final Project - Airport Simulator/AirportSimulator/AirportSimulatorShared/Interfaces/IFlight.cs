using System;

namespace AirportSimulatorShared.Interfaces
{
    public interface IFlight
    {
        int Id { get; set; }
        int Number { get; set; }
        DateTime TakeOffTime { get; set; }
        DateTime LandingTime { get; set; }
        string Origin { get; set; }
        string Destination { get; set; }
        bool IsLanding { get; set; }
        bool IsTakingOff { get; set; }
        int CurrentStationId { get; set; }
    }
}