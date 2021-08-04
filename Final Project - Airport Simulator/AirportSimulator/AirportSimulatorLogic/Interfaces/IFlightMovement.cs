using AirportSimulatorShared.Interfaces;
using System;

namespace AirportSimulatorLogic.Interfaces
{
    public interface IFlightMovement
    {
        int Id { get; set; }
        IFlight Flight { get; set; }
        IFlightRoute Route { get; set; }
        int CurrentStationId { get; set; }     

        void EnableMoveSequence();

        event Action ConfirmMoveIsDoneEvent;
        event Action<IFlightMovement> InformReadyForNextEvent;
        event Action<IFlightMovement> InformFlightIsDoneEvent;


    }
}