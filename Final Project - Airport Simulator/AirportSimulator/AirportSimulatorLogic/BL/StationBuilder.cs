using AirportSimulatorLogic.Interfaces;

namespace AirportSimulatorLogic.BL
{
    public class StationBuilder : IStationBuilder
    {
        public IStation CreateStation(IStationConfig config)
        {
            return new Station(config);
            
        }
    }
}
