using System;

namespace AirportSimulatorShared.Interfaces
{
    public interface IFlightGenerator
    {
        event Action<IFlight> NewLandingFlightEvent;
        event Action<IFlight> NewTakeOffFlightEvent;

    }
}