using System.Collections.Generic;

namespace AirportSimulatorShared.Interfaces
{
    public interface IAirportState
    {
        int Id { get; set; }
        IEnumerable<IFlight> Landings { get; set; }
        IEnumerable<IFlight> TakeOffs { get; set; }
        IEnumerable<IStationState> Stations { get; set; }
    }
}
