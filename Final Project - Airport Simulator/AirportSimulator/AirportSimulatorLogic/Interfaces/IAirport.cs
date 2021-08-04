using AirportSimulatorShared.Interfaces;
using System;
using System.Collections.Generic;

namespace AirportSimulatorLogic.Interfaces
{
    public interface IAirport
    {
        IEnumerable<IFlight> GetLandings();
        IEnumerable<IFlight> GetTakeOffs();
        IFlightRoute GetLandingRoute();
        IFlightRoute GetTakeOffRoute();
        void SetLandingRoute(IFlightRoute newRoute);
        void SetTakeOffRoute(IFlightRoute newRoute);
        void SetState(IAirportState state);
        IAirportState GetState();
        IEnumerable<IStationState> GetStations();

        event Action<IAirportState> UpdateStateEvent;

    }
}
