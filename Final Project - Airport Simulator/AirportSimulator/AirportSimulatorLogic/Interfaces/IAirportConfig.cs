using System;
using System.Collections.Generic;
using System.Text;

namespace AirportSimulatorLogic.Interfaces
{
    public interface IAirportConfig
    {
        IFlightRoute LandingRoute { get; set; }
        IFlightRoute TakeOffRoute { get; set; }
        IStationConfig[] StationsConfig { get; set; }

    }
}
