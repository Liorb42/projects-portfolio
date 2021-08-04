using System.Collections.Generic;

namespace AirportSimulatorLogic.Interfaces
{
    public interface IFlightRouteBuilder
    {
        IFlightRoute CreateFlightRoute(IEnumerable<IStation> stations, int[] route);

    }
}
