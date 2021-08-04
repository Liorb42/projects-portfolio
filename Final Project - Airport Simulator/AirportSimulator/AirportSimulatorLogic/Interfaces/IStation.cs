using AirportSimulatorShared.Interfaces;
using System;

namespace AirportSimulatorLogic.Interfaces
{
    public interface IStation
    {
        int Id { get; set; }
        IFlight Flight { get;  }
        TimeSpan TotalDurationOfStay { get; set; }
        DateTime StayStartTime { get; set; }
        bool IsOpen { get;  }
        bool IsHoldForLandings { get; }
        bool IsHoldForTakeOffs { get; }
        bool IsLandingHoldNeeded { get; }
        bool IsTakeOffHoldNeeded { get; }

        event Action StationOpenEvent;

        event Action StayDurationOverEvent;

        bool GetPermissionToEnter(IFlight flight);
        void StartStayTimer();
        void FlightLeftStation();
        bool HoldStation(IFlightMovement flightMovement);
    }
}