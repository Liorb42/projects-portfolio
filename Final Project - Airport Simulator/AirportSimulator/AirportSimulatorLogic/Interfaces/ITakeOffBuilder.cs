using AirportSimulatorShared.Interfaces;

namespace AirportSimulatorLogic.Interfaces
{
    public interface ITakeOffBuilder
    {
        IFlightMovement CreateTakeOff(IFlight flight);

    }
}

