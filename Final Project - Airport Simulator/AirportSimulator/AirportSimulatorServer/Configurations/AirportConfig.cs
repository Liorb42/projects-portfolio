using AirportSimulatorLogic.Interfaces;

namespace AirportSimulatorServer.Configurations
{
    public class AirportConfig : IAirportConfig
    {
        public AirportConfig(int numOfStations)
        {            
            StationsConfig = new IStationConfig[numOfStations];
        }
        public IFlightRoute LandingRoute { get; set; }
        public IFlightRoute TakeOffRoute { get; set; }
        public IStationConfig[] StationsConfig { get; set; }
    }
}
