using AirportSimulatorShared.Interfaces;

namespace AirportSimulatorLogic.Interfaces
{
    public interface ILandingBuilder
    {
        IFlightMovement CreateLanding(IFlight flight);

    }

}
