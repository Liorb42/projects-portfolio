
using AirportSimulatorShared.Interfaces;
using System;
using System.Collections.Generic;

namespace AirportSimulatorLogic.Interfaces
{
    public interface IControlTower
    {
        IEnumerable<IFlightMovement> Landings { get; }
        IEnumerable<IFlightMovement> TakeOffs { get; }
        IEnumerable<IStation> Stations { get; }
        IFlightRoute LandingRoute { get; set; }
        IFlightRoute TakeOffRoute { get; set; }
        void AddStation(ref IStation station);
        void SetLandingsState(IEnumerable<IFlight> landings);
        void SetTakeOffsState(IEnumerable<IFlight> takeOffs);

        event Action UpdateStateEvent;


    }
}