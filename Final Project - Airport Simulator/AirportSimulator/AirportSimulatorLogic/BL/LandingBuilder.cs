using AirportSimulatorLogic.Interfaces;
using AirportSimulatorShared.Interfaces;

namespace AirportSimulatorLogic.BL
{
    public class LandingBuilder : ILandingBuilder
    {
        public IFlightMovement CreateLanding(IFlight flight)
        {
            return new FlightMovement(flight);
        }
    }
}
