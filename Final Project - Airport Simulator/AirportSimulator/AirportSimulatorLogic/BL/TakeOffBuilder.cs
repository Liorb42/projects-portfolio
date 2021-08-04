using AirportSimulatorLogic.Interfaces;
using AirportSimulatorShared.Interfaces;

namespace AirportSimulatorLogic.BL
{
    public class TakeOffBuilder : ITakeOffBuilder
    {
        public IFlightMovement CreateTakeOff(IFlight flight)
        {
            return new FlightMovement(flight);
        }
    }
}
