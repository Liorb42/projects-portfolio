using AirportSimulatorShared.Interfaces;
using System.Collections.Generic;

namespace AirportSimulatorServer.Services
{
    public interface IAirportRepository
    {
        void UpdateFlight(IFlight flight);
        void UpdateStation(IStationState station);
        IEnumerable<IFlight> GetAllFututreLandings();
        IEnumerable<IFlight> GetAllFututreTakeOffs();
        IEnumerable<IStationState> GetAllStations();
        void UpdateState(IAirportState state);
        
    }
}
